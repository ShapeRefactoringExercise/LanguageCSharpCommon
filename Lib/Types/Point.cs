namespace Shape.Lib.Types;

public class Point(double x, double y): IShape
{
    private bool Equals(Point other)
    {
        return Classifier.PointsAreEqual(this, other);
    }

    public override bool Equals(object? obj)
    {
        return Classifier.EqualsPoint(this, obj);
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
