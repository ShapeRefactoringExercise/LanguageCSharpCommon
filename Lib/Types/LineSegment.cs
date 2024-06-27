namespace Shape.Lib.Types;

public class LineSegment : IShape
{
    public LineSegment(AllShape a, AllShape b)
    {
        P1 = a;
        P2 = b;
        Length = Math.Sqrt(Math.Pow(P1.X.GetValueOrDefault() - P2.X.GetValueOrDefault(), 2) + Math.Pow(P1.Y.GetValueOrDefault() - P2.Y.GetValueOrDefault(), 2));

        if (a.X.IsEquivalentTo(b.X))
        {
            Slope = Maybe<double>.None;
        }
        else
        {
            var result =
                (1.0 * (b.Y.GetValueOrDefault() - a.Y.GetValueOrDefault())) / (1.0 * (b.X.GetValueOrDefault() - a.X.GetValueOrDefault()));
            Slope = Maybe<double>.Some(result);
        }
    }

    public string Type => "Line Segment";
    public AllShape P1 { get; }

    public AllShape P2 { get; }

    public Maybe<double> Slope { get; }
    public double Length { get; }

    public override string ToString()
    {
        return $"{P1} -> {P2}";
    }
}
