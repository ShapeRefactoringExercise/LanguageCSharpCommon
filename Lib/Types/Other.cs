namespace Shape.Lib.Types;

public class Other: IShape
{
    public Other(LineSegment[] segments)
    {
        var pointCollector = new List<AllShape>();

        if (0 < segments.Length)
        {
            pointCollector.Add(segments[0].P1);
        }

        segments.ToList().ForEach(s => { pointCollector.Add(s.P2); });

        Points = pointCollector.ToArray();

        Length = segments.Sum(s => s.Length);
    }

    public string Type => "Other";

    public AllShape[] Points { get; }

    public bool IsClosed => Classifier.EqualsPoint(Points.First(), Points.Last());

    public bool IsOpen => !IsClosed;

    public double Length { get; }
}
