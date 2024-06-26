using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    public static bool PointsAreEqual(Point a, Point b)
    {
        return a.X.IsEquivalentTo(b.X) && a.Y.IsEquivalentTo(b.Y);
    }

    public static bool EqualsPoint(Point a, object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(a, obj)) return true;
        if (obj.GetType() != a.GetType()) return false;
        return PointsAreEqual(a, (Point)obj);
    }

    private static Point[] GetDistinct(Point[] points)
    {
        var ret = new List<Point>();
        var found = false;
        foreach (var point in points)
        {
            foreach (var value in ret)
            {
                if (PointsAreEqual(value, point))
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                found = false;
                continue;
            }

            ret.Add(point);
        }

        return ret.ToArray();
    }

    private static LineSegment[] GetPath(Point[] points)
    {
        Point pLast = null;
        var segments = new List<LineSegment>();
        foreach (var point in points)
        {
            if (pLast == null)
            {
                pLast = point;
                continue;
            }

            segments.Add(new LineSegment(pLast, point));
            pLast = point;
        }

        return segments.ToArray();
    }

    private static Angle[] GetAngles(Point[] points)
    {
        var angles = new List<Angle>();
        for (int i = 2; i < points.Length; i++)
        {
            var p1 = points[i - 2];
            var vertex = points[i - 1];
            var p2 = points[i];

            angles.Add(new Angle(p1, vertex, p2));
        }

        return angles.ToArray();
    }

    private static bool AllAreRight(Angle[] angles)
    {
        var lastAngle = 90.0;
        foreach (var angle in angles)
        {
            if (lastAngle.IsEquivalentTo(90))
            {
                lastAngle = angle.Degrees;
                continue;
            }

            return lastAngle.IsEquivalentTo(90);
        }

        return lastAngle.IsEquivalentTo(90);
    }

    public static IShape Classify(Point[] points)
    {
        if(0 == points.Length)
        {
            return new AllShape { Type = "Empty" };
        }

        if (1 == points.Length)
        {
            return points[0];
        }

        var distinctPoints = GetDistinct(points);
        var pStart = points[0];
        var pEnd = points[^1];
        var path = GetPath(points);

        if (2 == points.Length && 2 == distinctPoints.Length)
        {
            return new LineSegment(pStart, pEnd);
        }

        if (4 == points.Length && 3 == distinctPoints.Length && EqualsPoint(pStart, pEnd))
        {
            return new Triangle(path[0], path[1], path[2]);
        }

        var angles = GetAngles(points);
        if (5 == points.Length && 4 == distinctPoints.Length && EqualsPoint(pStart, pEnd) && path[0].Length.IsEquivalentTo(path[2].Length) && path[1].Length.IsEquivalentTo(path[3].Length) && AllAreRight(angles))
        {
            return new Rectangle(path[0], path[1], path[2], path[3]);
        }

        return new Other(path);
    }
}
