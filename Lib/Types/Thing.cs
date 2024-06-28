namespace Shape.Lib.Types;

public class Thing
{
    public string Type { get; set; } = "Unknown";

    public double? X { get; set; }

    public double? Y { get; set; }

    public Thing[]? Points { get; set; }

    public bool? IsClosed { get; set; }

    public bool? IsOpen { get; set; }

    public double? Length { get; set; }

    public Thing? P1 { get; set; }

    public Thing? P2 { get; set; }

    public Thing? P3 { get; set; }

    public Thing? P4 { get; set; }

    public object Slope { get; set; }

    public string Representation { get; set; } = "None";

    public Thing? Vertex { get; set; }

    public Thing? SideA { get; set; }
    public Thing? SideB { get; set; }

    public Thing? SideC { get; set; }

    public Thing? SideD { get; set; }

    public double? Degrees { get; set; }

    public double? Height { get; set; }

    public Thing? AngleA { get; set; }

    public Thing? AngleB { get; set; }

    public Thing? AngleC { get; set; }

    public double? Perimeter { get; set; }

    public double? Area { get; set; }

    public string GiveSpeach()
    {
        if (Type.Replace("+", "").Length == 0)
        {
            var v = Type;
            Type += "+";
            return v;
        }

        return Type;
    }

    public override string ToString()
    {
        return Representation;
    }
}
