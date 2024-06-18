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

        return points[0];
    }
}
