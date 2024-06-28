namespace Shape.Lib.Types;

public class Angle
{
    public Angle(AllShape p1, AllShape vertex, AllShape p2)
    {
        P1 = p1;
        Vertex = vertex;
        P2 = p2;

        SideA = new AllShape
        {
            Type = "Line Segment",
            P1 = P1,
            P2 = Vertex,
            Length = Math.Sqrt(Math.Pow(P1.X.GetValueOrDefault() - Vertex.X.GetValueOrDefault(), 2) + Math.Pow(P1.Y.GetValueOrDefault() - Vertex.Y.GetValueOrDefault(), 2)),
            Slope = P1.X.IsEquivalentTo(Vertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (Vertex.Y.GetValueOrDefault() - P1.Y.GetValueOrDefault())) / (1.0 * (Vertex.X.GetValueOrDefault() - P1.X.GetValueOrDefault()))),
            Representation = $"{P1} -> {Vertex}",
            Height = Math.Abs(p1.Y.GetValueOrDefault() - vertex.Y.GetValueOrDefault()),
        };

        SideB = new AllShape
        {
            Type = "Line Segment",
            P1 = P2,
            P2 = Vertex,
            Length = Math.Sqrt(Math.Pow(P2.X.GetValueOrDefault() - Vertex.X.GetValueOrDefault(), 2) + Math.Pow(P2.Y.GetValueOrDefault() - Vertex.Y.GetValueOrDefault(), 2)),
            Slope = P2.X.IsEquivalentTo(Vertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (Vertex.Y.GetValueOrDefault() - P2.Y.GetValueOrDefault())) / (1.0 * (Vertex.X.GetValueOrDefault() - P2.X.GetValueOrDefault()))),
            Representation = $"{P2} -> {Vertex}",
            Height = Math.Abs(P2.Y.GetValueOrDefault() - Vertex.Y.GetValueOrDefault()),
        };

        var sideC = new AllShape
        {
            Type = "Line Segment",
            P1 = P1,
            P2 = P2,
            Length = Math.Sqrt(Math.Pow(P1.X.GetValueOrDefault() - P2.X.GetValueOrDefault(), 2) + Math.Pow(P1.Y.GetValueOrDefault() - P2.Y.GetValueOrDefault(), 2)),
            Slope = P1.X.IsEquivalentTo(P2.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (P2.Y.GetValueOrDefault() - P1.Y.GetValueOrDefault())) / (1.0 * (P2.X.GetValueOrDefault() - P1.X.GetValueOrDefault()))),
            Representation = $"{P1} -> {P2}",
            Height = Math.Abs(P1.Y.GetValueOrDefault() - P2.Y.GetValueOrDefault()),
        };

        var c = sideC.Length.GetValueOrDefault();
        var a = SideA.Length.GetValueOrDefault();
        var b = SideB.Length.GetValueOrDefault();

        Degrees = Math.Acos(Math.Round((Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2)) / (2 * a * b), 6)) * (180 /  Math.PI);
    }

    public AllShape Vertex { get; }
    public AllShape P1 { get; }
    public AllShape P2 { get; }

    public AllShape SideA { get; }
    public AllShape SideB { get; }

    public double Degrees { get; }
}
