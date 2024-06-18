using Shape.Lib.Types;

namespace Shape.Lib;

public class Builder
{
    public static Point[] Build()
    {
        return [];
    }

    public static Point Build(int x, int y)
    {
        return new Point(x, y);
    }
}
