using Shape.Lib.Types;

namespace Shape.Lib;

public class Classifier
{
    public static IShape Classify(Point[] points)
    {
        return new EmptyShape();
    }
}
