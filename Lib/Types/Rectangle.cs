namespace Shape.Lib.Types;

public class Rectangle : IShape
{
    public Rectangle(LineSegment segment1, LineSegment segment2, LineSegment segment3, LineSegment segment4)
    {
        P1 = segment1.P1;
        P2 = segment2.P1;
        P3 = segment3.P1;
        P4 = segment4.P1;
    }

    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }
    public Point P4 { get; }

    public string Type => "Rectangle";
}
