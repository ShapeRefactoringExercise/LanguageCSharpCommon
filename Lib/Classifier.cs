using Shape.Lib.Types;

namespace Shape.Lib;

public class Classifier
{
    private static Point[] GetDistinct(Point[] points)
    {
        return points.Distinct().ToArray();
    }

    private static LineSegment[] GetSegments(Point[] points)
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

    public static IShape Classify(Point[] points)
    {
        var distinct = GetDistinct(points);

        if(0 == points.Length)
        {
            return new EmptyShape();
        }

        if (1 == points.Length)
        {
            return points[0];
        }

        if (2 == points.Length && distinct.Length == points.Length)
        {
            return new LineSegment(points[0], points[1]);
        }

        var segments = GetSegments(points);
        return new Other(segments);
    }
}
