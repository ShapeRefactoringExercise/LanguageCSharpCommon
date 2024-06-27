namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(AllShape sideA, AllShape sideB, AllShape sideC)
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

        Perimeter = sideA.Length.GetValueOrDefault() + sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault();

        Area = 0.25 * Math.Sqrt(
            (sideA.Length.GetValueOrDefault() + sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault())
            * (-(sideA.Length.GetValueOrDefault()) + sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault())
            * (sideA.Length.GetValueOrDefault() - sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault())
            * (sideA.Length.GetValueOrDefault() + sideB.Length.GetValueOrDefault() - sideC.Length.GetValueOrDefault())
        );
    }

    public AllShape P1 { get; }
    public AllShape P2 { get; }
    public AllShape P3 { get; }

    public AllShape SideA { get; }
    public AllShape SideB { get; }
    public AllShape SideC { get; }

    public Angle AngleA { get; }
    public Angle AngleB { get; }
    public Angle AngleC { get; }

    public double Perimeter { get; }

    public double Area { get; }

    public string Type => "Triangle";
}
