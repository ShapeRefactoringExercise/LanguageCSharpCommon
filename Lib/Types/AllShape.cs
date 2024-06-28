namespace Shape.Lib.Types;

public class AllShape: IShape
{
    public string Type { get; set; } = "Unknown";

    public double? X { get; set; }

    public double? Y { get; set; }

    public AllShape[]? Points { get; set; }

    public bool? IsClosed { get; set; }

    public bool? IsOpen { get; set; }

    public double? Length { get; set; }

    public AllShape? P1 { get; set; }

    public AllShape? P2 { get; set; }

    public Maybe<double>? Slope { get; set; }

    public string Representation { get; set; } = "None";

    public AllShape? Vertex { get; set; }

    public AllShape? SideA { get; set; }
    public AllShape? SideB { get; set; }

    public double? Degrees { get; set; }

    public override string ToString()
    {
        return Representation;
    }
}
