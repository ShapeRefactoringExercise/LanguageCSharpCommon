namespace Shape.Lib.Types;

public class AllShape: IShape
{
    public override int GetHashCode()
    {
        return HashCode.Combine(
            X,
            Y,
            Type,
            Points,
            IsClosed,
            IsOpen,
            Length,
            HashCode.Combine(
                P1,
                P2,
                Slope
            )
        );
    }

    public string Type { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public AllShape[]? Points { get; set; }

    public bool? IsClosed { get; set; }

    public bool? IsOpen { get; set; }

    public double? Length { get; set; }

    public AllShape? P1 { get; set; }

    public AllShape? P2 { get; set; }

    public Maybe<double>? Slope { get; set; }

    public string Representation { get; set; }

    public override string ToString()
    {
        return Representation;
    }
}
