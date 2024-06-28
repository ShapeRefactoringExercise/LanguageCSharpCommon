namespace Shape.Lib.Types;

public class Triangle: IShape
{
    public Triangle(AllShape sideA, AllShape sideB, AllShape sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;

        P1 = SideA.P1;
        P2 = SideB.P1;
        P3 = SideC.P1;

        var ap1 = P2;
        var avertex = P3;
        var ap2 = P1;

        var aSideA = new AllShape
        {
            Type = "Line Segment",
            P1 = ap1,
            P2 = avertex,
            Length = Math.Sqrt(Math.Pow(ap1.X.GetValueOrDefault() - avertex.X.GetValueOrDefault(), 2) + Math.Pow(ap1.Y.GetValueOrDefault() - avertex.Y.GetValueOrDefault(), 2)),
            Slope = ap1.X.IsEquivalentTo(avertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (avertex.Y.GetValueOrDefault() - ap1.Y.GetValueOrDefault())) / (1.0 * (avertex.X.GetValueOrDefault() - ap1.X.GetValueOrDefault()))),
            Representation = $"{ap1} -> {avertex}",
            Height = Math.Abs(ap1.Y.GetValueOrDefault() - avertex.Y.GetValueOrDefault()),
        };

        var aSideB = new AllShape
        {
            Type = "Line Segment",
            P1 = ap2,
            P2 = avertex,
            Length = Math.Sqrt(Math.Pow(ap2.X.GetValueOrDefault() - avertex.X.GetValueOrDefault(), 2) + Math.Pow(ap2.Y.GetValueOrDefault() - avertex.Y.GetValueOrDefault(), 2)),
            Slope = ap2.X.IsEquivalentTo(avertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (avertex.Y.GetValueOrDefault() - ap2.Y.GetValueOrDefault())) / (1.0 * (avertex.X.GetValueOrDefault() - ap2.X.GetValueOrDefault()))),
            Representation = $"{ap2} -> {avertex}",
            Height = Math.Abs(ap2.Y.GetValueOrDefault() - avertex.Y.GetValueOrDefault()),
        };

        var aSideC = new AllShape
        {
            Type = "Line Segment",
            P1 = ap1,
            P2 = ap2,
            Length = Math.Sqrt(Math.Pow(ap1.X.GetValueOrDefault() - ap2.X.GetValueOrDefault(), 2) + Math.Pow(ap1.Y.GetValueOrDefault() - ap2.Y.GetValueOrDefault(), 2)),
            Slope = ap1.X.IsEquivalentTo(ap2.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (ap2.Y.GetValueOrDefault() - ap1.Y.GetValueOrDefault())) / (1.0 * (ap2.X.GetValueOrDefault() - ap1.X.GetValueOrDefault()))),
            Representation = $"{ap1} -> {ap2}",
            Height = Math.Abs(ap1.Y.GetValueOrDefault() - ap2.Y.GetValueOrDefault()),
        };

        var ac = aSideC.Length.GetValueOrDefault();
        var aa = aSideA.Length.GetValueOrDefault();
        var ab = aSideB.Length.GetValueOrDefault();

        AngleA = new AllShape
        {
            Degrees = Math.Acos(Math.Round((Math.Pow(aa, 2) + Math.Pow(ab, 2) - Math.Pow(ac, 2)) / (2 * aa * ab), 6)) * (180 /  Math.PI),
            Vertex = avertex,
            P1 = ap1,
            P2 = ap2,
            SideA = aSideA,
            SideB = aSideB,
        };

        var bp1 = P3;
        var bvertex = P1;
        var bp2 = P2;

        var bSideA = new AllShape
        {
            Type = "Line Segment",
            P1 = bp1,
            P2 = bvertex,
            Length = Math.Sqrt(Math.Pow(bp1.X.GetValueOrDefault() - bvertex.X.GetValueOrDefault(), 2) + Math.Pow(bp1.Y.GetValueOrDefault() - bvertex.Y.GetValueOrDefault(), 2)),
            Slope = bp1.X.IsEquivalentTo(bvertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (bvertex.Y.GetValueOrDefault() - bp1.Y.GetValueOrDefault())) / (1.0 * (bvertex.X.GetValueOrDefault() - bp1.X.GetValueOrDefault()))),
            Representation = $"{bp1} -> {bvertex}",
            Height = Math.Abs(bp1.Y.GetValueOrDefault() - bvertex.Y.GetValueOrDefault()),
        };

        var bSideB = new AllShape
        {
            Type = "Line Segment",
            P1 = bp2,
            P2 = bvertex,
            Length = Math.Sqrt(Math.Pow(bp2.X.GetValueOrDefault() - bvertex.X.GetValueOrDefault(), 2) + Math.Pow(bp2.Y.GetValueOrDefault() - bvertex.Y.GetValueOrDefault(), 2)),
            Slope = bp2.X.IsEquivalentTo(bvertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (bvertex.Y.GetValueOrDefault() - bp2.Y.GetValueOrDefault())) / (1.0 * (bvertex.X.GetValueOrDefault() - bp2.X.GetValueOrDefault()))),
            Representation = $"{bp2} -> {bvertex}",
            Height = Math.Abs(bp2.Y.GetValueOrDefault() - bvertex.Y.GetValueOrDefault()),
        };

        var bSideC = new AllShape
        {
            Type = "Line Segment",
            P1 = bp1,
            P2 = bp2,
            Length = Math.Sqrt(Math.Pow(bp1.X.GetValueOrDefault() - bp2.X.GetValueOrDefault(), 2) + Math.Pow(bp1.Y.GetValueOrDefault() - bp2.Y.GetValueOrDefault(), 2)),
            Slope = bp1.X.IsEquivalentTo(bp2.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (bp2.Y.GetValueOrDefault() - bp1.Y.GetValueOrDefault())) / (1.0 * (bp2.X.GetValueOrDefault() - bp1.X.GetValueOrDefault()))),
            Representation = $"{bp1} -> {bp2}",
            Height = Math.Abs(bp1.Y.GetValueOrDefault() - bp2.Y.GetValueOrDefault()),
        };

        var bc = bSideC.Length.GetValueOrDefault();
        var ba = bSideA.Length.GetValueOrDefault();
        var bb = bSideB.Length.GetValueOrDefault();

        AngleB = new AllShape
        {
            Degrees = Math.Acos(Math.Round((Math.Pow(ba, 2) + Math.Pow(bb, 2) - Math.Pow(bc, 2)) / (2 * ba * bb), 6)) * (180 /  Math.PI),
            Vertex = bvertex,
            P1 = bp1,
            P2 = bp2,
            SideA = bSideA,
            SideB = bSideB,
        };

        var cp1 = P1;
        var cvertex = P2;
        var cp2 = P3;

        var cSideA = new AllShape
        {
            Type = "Line Segment",
            P1 = cp1,
            P2 = cvertex,
            Length = Math.Sqrt(Math.Pow(cp1.X.GetValueOrDefault() - cvertex.X.GetValueOrDefault(), 2) + Math.Pow(cp1.Y.GetValueOrDefault() - cvertex.Y.GetValueOrDefault(), 2)),
            Slope = cp1.X.IsEquivalentTo(cvertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (cvertex.Y.GetValueOrDefault() - cp1.Y.GetValueOrDefault())) / (1.0 * (cvertex.X.GetValueOrDefault() - cp1.X.GetValueOrDefault()))),
            Representation = $"{cp1} -> {cvertex}",
            Height = Math.Abs(cp1.Y.GetValueOrDefault() - cvertex.Y.GetValueOrDefault()),
        };

        var cSideB = new AllShape
        {
            Type = "Line Segment",
            P1 = cp2,
            P2 = cvertex,
            Length = Math.Sqrt(Math.Pow(cp2.X.GetValueOrDefault() - cvertex.X.GetValueOrDefault(), 2) + Math.Pow(cp2.Y.GetValueOrDefault() - cvertex.Y.GetValueOrDefault(), 2)),
            Slope = cp2.X.IsEquivalentTo(cvertex.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (cvertex.Y.GetValueOrDefault() - cp2.Y.GetValueOrDefault())) / (1.0 * (cvertex.X.GetValueOrDefault() - cp2.X.GetValueOrDefault()))),
            Representation = $"{cp2} -> {cvertex}",
            Height = Math.Abs(cp2.Y.GetValueOrDefault() - cvertex.Y.GetValueOrDefault()),
        };

        var csideC = new AllShape
        {
            Type = "Line Segment",
            P1 = cp1,
            P2 = cp2,
            Length = Math.Sqrt(Math.Pow(cp1.X.GetValueOrDefault() - cp2.X.GetValueOrDefault(), 2) + Math.Pow(cp1.Y.GetValueOrDefault() - cp2.Y.GetValueOrDefault(), 2)),
            Slope = cp1.X.IsEquivalentTo(cp2.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (cp2.Y.GetValueOrDefault() - cp1.Y.GetValueOrDefault())) / (1.0 * (cp2.X.GetValueOrDefault() - cp1.X.GetValueOrDefault()))),
            Representation = $"{cp1} -> {cp2}",
            Height = Math.Abs(cp1.Y.GetValueOrDefault() - cp2.Y.GetValueOrDefault()),
        };

        var cc = csideC.Length.GetValueOrDefault();
        var ca = cSideA.Length.GetValueOrDefault();
        var cb = cSideB.Length.GetValueOrDefault();

        AngleC = new AllShape
        {
            Degrees = Math.Acos(Math.Round((Math.Pow(ca, 2) + Math.Pow(cb, 2) - Math.Pow(cc, 2)) / (2 * ca * cb), 6)) * (180 /  Math.PI),
            Vertex = cvertex,
            P1 = cp1,
            P2 = cp2,
            SideA = cSideA,
            SideB = cSideB,
        };

        Perimeter = sideA.Length.GetValueOrDefault() + sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault();

        Area = 0.25 * Math.Sqrt(
            (sideA.Length.GetValueOrDefault() + sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault())
            * (-(sideA.Length.GetValueOrDefault()) + sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault())
            * (sideA.Length.GetValueOrDefault() - sideB.Length.GetValueOrDefault() + sideC.Length.GetValueOrDefault())
            * (sideA.Length.GetValueOrDefault() + sideB.Length.GetValueOrDefault() - sideC.Length.GetValueOrDefault())
        );
    }

    public AllShape P1 { get; }
    public AllShape P2 { get; }
    public AllShape P3 { get; }

    public AllShape SideA { get; }
    public AllShape SideB { get; }
    public AllShape SideC { get; }

    public AllShape AngleA { get; }
    public AllShape AngleB { get; }
    public AllShape AngleC { get; }

    public double Perimeter { get; }

    public double Area { get; }

    public string Type => "Triangle";
}
