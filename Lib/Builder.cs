using Shape.Lib.Types;

namespace Shape.Lib;

public class Builder
{
    public static Thing Build(double x, double y)
    {
        return new Thing
        {
            Type = "Point",
            X = x,
            Y = y,
            Representation = $"({x}, {y})",
            Height = 0.0,
        };
    }

    public static Thing[] Build(params (double, double)[] points)
    {
        return points
            .Select(coord => Build(coord.Item1, coord.Item2))
            .ToArray();
    }
}
