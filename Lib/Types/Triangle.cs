namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(LineSegment side1, LineSegment side2, LineSegment side3)
    {
        P1 = side1.P1;
        P2 = side2.P1;
        P3 = side3.P1;
    }

    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }

    public string Type => "Triangle";
}
