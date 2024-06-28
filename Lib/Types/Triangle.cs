namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(AllShape sideA, AllShape sideB, AllShape sideC)
    {
        AngleA = new AllShape
        {
            Degrees = Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                Math.Pow(sideB.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideB.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault(), 2)), 2) + Math.Pow(Math.Sqrt(
                Math.Pow(sideA.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideA.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault(), 2)), 2) - Math.Pow(Math.Sqrt(
                Math.Pow(sideB.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideB.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault(), 2)), 2)) / (2 * Math.Sqrt(
                Math.Pow(sideB.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideB.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault(), 2)) * Math.Sqrt(
                Math.Pow(sideA.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideA.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault(), 2))), 6)) * (180 / Math.PI),
            Vertex = sideC.P1,
            P1 = sideB.P1,
            P2 = sideA.P1,
            SideA = new AllShape
            {
                P2 = sideC.P1,
                Slope = sideB.P1.X.IsEquivalentTo(sideC.P1.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (sideC.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault())) /
                                         (1.0 * (sideC.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault()))),
                Type = "Line Segment",
                P1 = sideB.P1,
                Length = Math.Sqrt(Math.Pow(sideB.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault(), 2) +
                                   Math.Pow(sideB.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault(), 2)),
                Height = Math.Abs(sideB.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault()),
                Representation = $"{sideB.P1} -> {sideC.P1}",
            },
            SideB = new AllShape
            {
                Length = Math.Sqrt(Math.Pow(sideA.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault(), 2) +
                                   Math.Pow(sideA.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault(), 2)),
                P1 = sideA.P1,
                Height = Math.Abs(sideA.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault()),
                P2 = sideC.P1,
                Slope = sideA.P1.X.IsEquivalentTo(sideC.P1.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (sideC.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault())) /
                                         (1.0 * (sideC.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault()))),
                Representation = $"{sideA.P1} -> {sideC.P1}",
                Type = "Line Segment",
            },
        };

        AngleB = new AllShape
        {
            Degrees = Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                Math.Pow(sideC.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideC.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault(), 2)), 2) + Math.Pow(Math.Sqrt(
                Math.Pow(sideB.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideB.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault(), 2)), 2) - Math.Pow(Math.Sqrt(
                Math.Pow(sideC.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideC.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault(), 2)), 2)) / (2 * Math.Sqrt(
                Math.Pow(sideC.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideC.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault(), 2)) * Math.Sqrt(
                Math.Pow(sideB.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideB.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault(), 2))), 6)) * (180 / Math.PI),
            Vertex = sideA.P1,
            P1 = sideC.P1,
            P2 = sideB.P1,
            SideA = new AllShape
            {
                Height = Math.Abs(sideC.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault()),
                Length = Math.Sqrt(Math.Pow(sideC.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault(), 2) +
                                   Math.Pow(sideC.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault(), 2)),
                Slope = sideC.P1.X.IsEquivalentTo(sideA.P1.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (sideA.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault())) /
                                         (1.0 * (sideA.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault()))),
                Type = "Line Segment",
                P1 = sideC.P1,
                P2 = sideA.P1,
                Representation = $"{sideC.P1} -> {sideA.P1}",
            },
            SideB = new AllShape
            {
                Length = Math.Sqrt(Math.Pow(sideB.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault(), 2) +
                                   Math.Pow(sideB.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault(), 2)),
                P1 = sideB.P1,
                Height = Math.Abs(sideB.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault()),
                P2 = sideA.P1,
                Slope = sideB.P1.X.IsEquivalentTo(sideA.P1.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (sideA.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault())) /
                                         (1.0 * (sideA.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault()))),
                Representation = $"{sideB.P1} -> {sideA.P1}",
                Type = "Line Segment",
            },
        };

        AngleC = new AllShape
        {
            Degrees = Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                Math.Pow(sideA.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideA.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault(), 2)), 2) + Math.Pow(Math.Sqrt(
                Math.Pow(sideC.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideC.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault(), 2)), 2) - Math.Pow(Math.Sqrt(
                Math.Pow(sideA.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideA.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault(), 2)), 2)) / (2 * Math.Sqrt(
                Math.Pow(sideA.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideA.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault(), 2)) * Math.Sqrt(
                Math.Pow(sideC.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault(), 2) +
                Math.Pow(sideC.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault(), 2))), 6)) * (180 / Math.PI),
            Vertex = sideB.P1,
            P1 = sideA.P1,
            P2 = sideC.P1,
            SideA = new AllShape
            {
                Length = Math.Sqrt(Math.Pow(sideA.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault(), 2) +
                                   Math.Pow(sideA.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault(), 2)),
                Slope = sideA.P1.X.IsEquivalentTo(sideB.P1.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (sideB.P1.Y.GetValueOrDefault() - sideA.P1.Y.GetValueOrDefault())) /
                                         (1.0 * (sideB.P1.X.GetValueOrDefault() - sideA.P1.X.GetValueOrDefault()))),
                Height = Math.Abs(sideA.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault()),
                Type = "Line Segment",
                P1 = sideA.P1,
                P2 = sideB.P1,
                Representation = $"{sideA.P1} -> {sideB.P1}",
            },
            SideB = new AllShape
            {
                P1 = sideC.P1,
                Slope = sideC.P1.X.IsEquivalentTo(sideB.P1.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (sideB.P1.Y.GetValueOrDefault() - sideC.P1.Y.GetValueOrDefault())) /
                                         (1.0 * (sideB.P1.X.GetValueOrDefault() - sideC.P1.X.GetValueOrDefault()))),
                Height = Math.Abs(sideC.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault()),
                Length = Math.Sqrt(Math.Pow(sideC.P1.X.GetValueOrDefault() - sideB.P1.X.GetValueOrDefault(), 2) +
                                   Math.Pow(sideC.P1.Y.GetValueOrDefault() - sideB.P1.Y.GetValueOrDefault(), 2)),
                Type = "Line Segment",
                Representation = $"{sideC.P1} -> {sideB.P1}",
                P2 = sideB.P1,
            },
        };

        SideA = sideA;
        SideB = sideB;
        SideC = sideC;

        P1 = sideA.P1;
        P2 = sideB.P1;
        P3 = sideC.P1;

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

    public AllShape AngleA { get; }
    public AllShape AngleB { get; }
    public AllShape AngleC { get; }

    public double Perimeter { get; }

    public double Area { get; }

    public string Type => "Triangle";
}
