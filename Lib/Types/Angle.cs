namespace Shape.Lib.Types;

public class Angle
{
    public Angle(Point p1, Point vertex, Point p2)
    {
        P1 = p1;
        Vertex = vertex;
        P2 = p2;

        SideA = new LineSegment(P1, Vertex);
        SideB = new LineSegment(P2, Vertex);

        var sideC = new LineSegment(P1, P2);

        var c = sideC.Length; // 3, 4, 5,
        var a = SideA.Length; // 4, 5, 3,
        var b = SideB.Length; // 5, 3, 4

        var applesauce = (Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2));
        var pearsauce = 2 * a * b;
        var bananaCream = Math.Round(applesauce / pearsauce, 6);

        Degrees = Math.Acos(bananaCream) * (180 /  Math.PI); // 36.87, 53.13, 90
    }

    public Point Vertex { get; }
    public Point P1 { get; }
    public Point P2 { get; }

    public LineSegment SideA { get; }
    public LineSegment SideB { get; }

    public double Degrees { get; }
}
