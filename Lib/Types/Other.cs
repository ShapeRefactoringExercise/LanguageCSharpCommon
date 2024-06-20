namespace Shape.Lib.Types;

public class Other(Point[] points): IShape
{
    public string Type => "Other";

    public Point[] Points => points.Select(p => p).ToArray();

    public bool IsClosed => Equals(points.First(), points.Last());

    public bool IsOpen => !IsClosed;
}
