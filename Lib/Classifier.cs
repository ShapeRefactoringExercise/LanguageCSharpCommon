using Shape.Lib.Types;

namespace Shape.Lib;

public class Classifier
{
    public static IShape Classify(Point[] points)
    {
        if(0 == points.Length)
        {
            return new EmptyShape();
        }

        if (2 == points.Length)
        {
            return new LineSegment(points[0], points[1]);
        }

        return points[0];
    }
}
