namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(LineSegment sideA, LineSegment sideB, LineSegment sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;

        P1 = SideA.P1; // PB
        P2 = SideB.P1; // PC
        P3 = SideC.P1; // PA

        AngleA = new Angle(P2, P3, P1);
        AngleB = new Angle(P3, P1, P2);
        AngleC = new Angle(P1, P2, P3);

        Perimeter = sideA.Length + sideB.Length + sideC.Length;

        Area = 0.25 * Math.Sqrt(
            (sideA.Length + sideB.Length + sideC.Length)
            * (-(sideA.Length) + sideB.Length + sideC.Length)
            * (sideA.Length - sideB.Length + sideC.Length)
            * (sideA.Length + sideB.Length - sideC.Length)
        );
    }

    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }

    public LineSegment SideA { get; }
    public LineSegment SideB { get; }
    public LineSegment SideC { get; }

    public Angle AngleA { get; }
    public Angle AngleB { get; }
    public Angle AngleC { get; }

    public double Perimeter { get; }

    public double Area { get; }

    public string Type => "Triangle";
}
