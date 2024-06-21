namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(LineSegment sideA, LineSegment sideB, LineSegment sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;

        P1 = SideA.P1;
        P2 = SideB.P1;
        P3 = SideC.P1;
    }

    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }

    public LineSegment SideA { get; }
    public LineSegment SideB { get; }
    public LineSegment SideC { get; }

    public string Type => "Triangle";
}
