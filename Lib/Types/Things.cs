namespace Shape.Lib.Types;

public class Things
{
    public string Type { get; set; } = "Unknown";

    public double? X { get; set; }

    public double? Y { get; set; }

    public Things[]? Points { get; set; }

    public bool? IsClosed { get; set; }

    public bool? IsOpen { get; set; }

    public double? Length { get; set; }

    public Things? P1 { get; set; }

    public Things? P2 { get; set; }

    public Things? P3 { get; set; }

    public Things? P4 { get; set; }

    public object Slope { get; set; }

    public string Representation { get; set; } = "None";

    public Things? Vertex { get; set; }

    public Things? SideA { get; set; }
    public Things? SideB { get; set; }

    public Things? SideC { get; set; }

    public Things? SideD { get; set; }

    public double? Degrees { get; set; }

    public double? Height { get; set; }

    public Things? AngleA { get; set; }

    public Things? AngleB { get; set; }

    public Things? AngleC { get; set; }

    public double? Perimeter { get; set; }

    public double? Area { get; set; }

    public override string ToString()
    {
        return Representation;
    }
}
