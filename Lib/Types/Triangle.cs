namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(LineSegment side1, LineSegment side2, LineSegment side3)
    {
    }

    public string Type => "Triangle";
}
