namespace Shape.Lib.Types;

public class Point(int x, int y): IShape
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public string Type => "Point";
}

public interface IShape
{
    public string Type { get; }
}

public class EmptyShape: IShape
{
    public string Type => "Empty";
}
