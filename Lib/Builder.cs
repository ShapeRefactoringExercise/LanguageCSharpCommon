using Shape.Lib.Types;

namespace Shape.Lib;

public class Builder
{
    public static Point Build(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point[] Build(params (double, double)[] points)
    {
        return points
            .Select(coord => Build(coord.Item1, coord.Item2))
            .ToArray();
    }
}
