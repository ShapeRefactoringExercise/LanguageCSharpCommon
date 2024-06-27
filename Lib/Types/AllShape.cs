namespace Shape.Lib.Types;

public class AllShape: IShape
{
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Type);
    }

    public string Type { get; set; }
    public double X { get; set; }

    public double Y { get; set; }

    public string Representation { get; set; }

    public override string ToString()
    {
        return Representation;
    }
}
