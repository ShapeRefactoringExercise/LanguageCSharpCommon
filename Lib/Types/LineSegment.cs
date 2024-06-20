namespace Shape.Lib.Types;

public class LineSegment(Point a, Point b) : IShape
{
    public string Type => "Line Segment";
    public Point P1 { get; } = a;
    public Point P2 { get; } = b;
    public Maybe<double> Slope { get; } = Maybe<double>.None;
    public double Length { get; } = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
}
