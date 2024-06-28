﻿namespace Shape.Lib.Types;

public class AllShape
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

    public AllShape? P3 { get; set; }

    public AllShape? P4 { get; set; }

    public object Slope { get; set; }

    public string Representation { get; set; } = "None";

    public AllShape? Vertex { get; set; }

    public AllShape? SideA { get; set; }
    public AllShape? SideB { get; set; }

    public AllShape? SideC { get; set; }

    public AllShape? SideD { get; set; }

    public double? Degrees { get; set; }

    public double? Height { get; set; }

    public AllShape? AngleA { get; set; }

    public AllShape? AngleB { get; set; }

    public AllShape? AngleC { get; set; }

    public double? Perimeter { get; set; }

    public double? Area { get; set; }

    public override string ToString()
    {
        return Representation;
    }
}
