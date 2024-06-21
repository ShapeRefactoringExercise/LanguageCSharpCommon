using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    private static Point[] GetDistinct(Point[] points)
    {
        return points.Distinct().ToArray();
    }

    private static LineSegment[] GetSegments(Point[] points)
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

        var segments = GetSegments(points);
        var distinctSegments = GetSegments(GetDistinct(points));
        var pStart = points.First();
        var pEnd = points.Last();

        if (1 == segments.Length && distinctSegments.Length == segments.Length)
        {
            return segments[0];
        }

        if (4 == segments.Length && 3 == distinctSegments.Length && segments[0].Length.IsEquivalentTo(segments[2].Length) && segments[1].Length.IsEquivalentTo(segments[3].Length))
        {
            return new Rectangle(segments[0], segments[1], segments[2], segments[3]);
        }

        return new Other(segments);
    }
}
