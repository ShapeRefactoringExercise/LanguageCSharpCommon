using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    private static Point[] GetDistinct(Point[] points)
    {
        return points.Distinct().ToArray();
    }

    private static LineSegment[] GetPath(Point[] points)
    {
        Point start = points.First();
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

    public static IShape Classify(Point[] points)
    {
        if(0 == points.Length)
        {
            return new EmptyShape();
        }

        if (1 == points.Length)
        {
            return points[0];
        }

        var distinctPoints = GetDistinct(points);
        var pStart = points.First();
        var pEnd = points.Last();
        var path = GetPath(points);

        if (2 == points.Length && 2 == distinctPoints.Length)
        {
            return new LineSegment(pStart, pEnd);
        }

        if (4 == points.Length && 3 == distinctPoints.Length && Equals(pStart, pEnd))
        {
            return new Triangle(path[0], path[1], path[2]);
        }

        if (5 == points.Length && 4 == distinctPoints.Length && Equals(pStart, pEnd) && path[0].Length.IsEquivalentTo(path[2].Length) && path[1].Length.IsEquivalentTo(path[3].Length))
        {
            return new Rectangle(path[0], path[1], path[2], path[3]);
        }

        return new Other(path);
    }
}
