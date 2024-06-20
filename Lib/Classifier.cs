using Shape.Lib.Types;

namespace Shape.Lib;

public class Classifier
{
    private static Point[] GetDistinct(Point[] points)
    {
        return points.Distinct().ToArray();
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

        return new Other(points);
    }
}
