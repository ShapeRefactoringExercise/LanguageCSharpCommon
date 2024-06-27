namespace Shape.Lib.Types;

public class Rectangle : IShape
{
    public Rectangle(LineSegment sideA, LineSegment sideB, LineSegment sideC, LineSegment sideD)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
        SideD = sideD;

        P1 = SideA.P1;
        P2 = SideB.P1;
        P3 = SideC.P1;
        P4 = SideD.P1;

        Perimeter = (2 * SideA.Length) + (2 * SideB.Length);
        Area = SideA.Length * SideB.Length;
    }

    public AllShape P1 { get; }
    public AllShape P2 { get; }
    public AllShape P3 { get; }
    public AllShape P4 { get; }

    public LineSegment SideA { get; }
    public LineSegment SideB { get; }
    public LineSegment SideC { get; }
    public LineSegment SideD { get; }

    public double Perimeter { get; }
    public double Area { get; }

    public string Type => "Rectangle";
}
