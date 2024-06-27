namespace Shape.Lib.Types;

public class Rectangle : IShape
{
    public Rectangle(AllShape sideA, AllShape sideB, AllShape sideC, AllShape sideD)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
        SideD = sideD;

        P1 = SideA.P1;
        P2 = SideB.P1;
        P3 = SideC.P1;
        P4 = SideD.P1;

        Perimeter = (2 * SideA.Length.GetValueOrDefault()) + (2 * SideB.Length.GetValueOrDefault());
        Area = SideA.Length.GetValueOrDefault() * SideB.Length.GetValueOrDefault();
    }

    public AllShape P1 { get; }
    public AllShape P2 { get; }
    public AllShape P3 { get; }
    public AllShape P4 { get; }

    public AllShape SideA { get; }
    public AllShape SideB { get; }
    public AllShape SideC { get; }
    public AllShape SideD { get; }

    public double Perimeter { get; }
    public double Area { get; }

    public string Type => "Rectangle";
}
