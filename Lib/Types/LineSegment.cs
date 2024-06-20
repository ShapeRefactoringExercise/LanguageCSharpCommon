namespace Shape.Lib.Types;

public class LineSegment : IShape
{
    public LineSegment(Point a, Point b)
    {
        P1 = a;
        P2 = b;
        Length = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

        if (a.X == b.X)
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
    public Point P1 { get; }

    public Point P2 { get; }

    public Maybe<double> Slope { get; }
    public double Length { get; }
}
