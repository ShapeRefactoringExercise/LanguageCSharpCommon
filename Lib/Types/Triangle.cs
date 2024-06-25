namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(LineSegment sideA, LineSegment sideB, LineSegment sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;

        PB = SideA.P1;
        PC = SideB.P1;
        PA = SideC.P1;

        AngleA = new Angle(PC, PA, PB);
        AngleB = new Angle(PA, PB, PC);
        AngleC = new Angle(PB, PC, PA);
    }

    public Point PB { get; }
    public Point PC { get; }
    public Point PA { get; }

    public LineSegment SideA { get; }
    public LineSegment SideB { get; }
    public LineSegment SideC { get; }

    public Angle AngleA { get; }
    public Angle AngleB { get; }
    public Angle AngleC { get; }
    public string Type => "Triangle";
}
