namespace Shape.Lib.Types;

public class LineSegment : IShape
{
    public LineSegment(AllShape a, AllShape b)
    {
        P1 = a;
        P2 = b;
        Length = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

        if (a.X.IsEquivalentTo(b.X))
        {
            Slope = Maybe<double>.None;
        }
        else
        {
            var result =
                (1.0 * (b.Y - a.Y)) / (1.0 * (b.X - a.X));
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
