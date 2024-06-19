namespace Shape.Lib.Types;

public class Point(int x, int y): IShape
{
    private bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

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

public class LineSegment(Point a, Point b) : IShape
{
    public string Type => "Line Segment";
    public Point A { get; } = a;
    public Point B { get; } = b;
    public int? Slope { get; } = null;
}
