﻿namespace Shape.Lib.Types;

public class Rectangle : IShape
{
    public Rectangle(LineSegment sideA, LineSegment sideB, LineSegment sideC, LineSegment sideD)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
        SideD = sideD;

        P1 = SideA.P1;
        P2 = SideB.P1;
        P3 = SideC.P1;
        P4 = SideD.P1;
    }

    public LineSegment SideA { get; }
    public LineSegment SideB { get; }
    public LineSegment SideC { get; }
    public LineSegment SideD { get; }

    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }
    public Point P4 { get; }

    public string Type => "Rectangle";
}
