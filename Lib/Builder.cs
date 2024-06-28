using Shape.Lib.Types;

namespace Shape.Lib;

public class Builder
{
    public static AllShape Build(double x, double y)
    {
        return new AllShape
        {
            Type = "Point",
            X = x,
            Y = y,
            Representation = $"({x}, {y})",
            Height = 0.0,
        };
    }

    public static AllShape[] Build(params (double, double)[] points)
    {
        return points
            .Select(coord => Build(coord.Item1, coord.Item2))
            .ToArray();
    }
}
