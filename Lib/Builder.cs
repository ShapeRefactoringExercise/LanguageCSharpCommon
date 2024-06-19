using Shape.Lib.Types;

namespace Shape.Lib;

public class Builder
{
    public static Point Build(int x, int y)
    {
        return new Point(x, y);
    }

    public static Point[] Build(params (int, int)[] points)
    {
        return points
            .Select(coord => Build(coord.Item1, coord.Item2))
            .ToArray();
    }
}
