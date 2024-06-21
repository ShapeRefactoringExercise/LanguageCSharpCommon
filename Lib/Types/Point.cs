﻿namespace Shape.Lib.Types;

public class Point(double x, double y): IShape
{
    private bool Equals(Point other)
    {
        return X.IsEquivalentTo(other.X) && Y.IsEquivalentTo(other.Y);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public double X { get; } = x;
    public double Y { get; } = y;
    public string Type => "Point";

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
