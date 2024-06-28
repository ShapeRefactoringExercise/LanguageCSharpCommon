using Shape.Lib.Types;

namespace Shape.Lib;

public class Builder
{
    public static Things Build(double x, double y)
    {
        return new Things
        {
            Type = "Point",
            X = x,
            Y = y,
            Representation = $"({x}, {y})",
            Height = 0.0,
        };
    }

    public static Things[] Build(params (double, double)[] points)
    {
        return points
            .Select(coord => Build(coord.Item1, coord.Item2))
            .ToArray();
    }
}
