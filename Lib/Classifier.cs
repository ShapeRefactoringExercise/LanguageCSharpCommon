using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    public static bool PointsAreEqual(AllShape a, AllShape b)
    {
        return a.X.IsEquivalentTo(b.X) && a.Y.IsEquivalentTo(b.Y);
    }

    public static bool EqualsPoint(AllShape a, object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(a, obj)) return true;
        if (obj.GetType() != a.GetType()) return false;
        return PointsAreEqual(a, (AllShape)obj);
    }

    private static AllShape[] GetDistinct(AllShape[] points)
    {
        var ret = new List<AllShape>();
        var found = false;
        foreach (var point in points)
        {
            foreach (var value in ret)
            {
                if (PointsAreEqual(value, point))
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                found = false;
                continue;
            }

            ret.Add(point);
        }

        return ret.ToArray();
    }

    private static AllShape[] GetPath(AllShape[] points)
    {
        AllShape pLast = null;
        var segments = new List<AllShape>();
        foreach (var point in points)
        {
            if (pLast == null)
            {
                pLast = point;
                continue;
            }

            Maybe<double> slope;
            if (pLast.X.IsEquivalentTo(point.X))
            {
                slope = Maybe<double>.None;
            }
            else
            {
                var result =
                    (1.0 * (point.Y.GetValueOrDefault() - pLast.Y.GetValueOrDefault())) / (1.0 * (point.X.GetValueOrDefault() - pLast.X.GetValueOrDefault()));
                slope = Maybe<double>.Some(result);
            }

            segments.Add(new AllShape
            {
                P1 = pLast,
                P2 = point,
                Length = Math.Sqrt(Math.Pow(pLast.X.GetValueOrDefault() - point.X.GetValueOrDefault(), 2) + Math.Pow(pLast.Y.GetValueOrDefault() - point.Y.GetValueOrDefault(), 2)),
                Slope = slope,
                Type = "Line Segment",
                Representation = $"{pLast} -> {point}",
            });
            pLast = point;
        }

        return segments.ToArray();
    }

    private static AllShape[] GetAngles(AllShape[] points)
    {
        var angles = new List<AllShape>();
        for (int i = 2; i < points.Length; i++)
        {
            var p1 = points[i - 2];
            var vertex = points[i - 1];
            var p2 = points[i];

            var SideA = new AllShape
            {
                Type = "Line Segment",
                P1 = p1,
                P2 = vertex,
                Length = Math.Sqrt(Math.Pow(p1.X.GetValueOrDefault() - vertex.X.GetValueOrDefault(), 2) +
                                   Math.Pow(p1.Y.GetValueOrDefault() - vertex.Y.GetValueOrDefault(), 2)),
                Slope = p1.X.IsEquivalentTo(vertex.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (vertex.Y.GetValueOrDefault() - p1.Y.GetValueOrDefault())) /
                                         (1.0 * (vertex.X.GetValueOrDefault() - p1.X.GetValueOrDefault()))),
                Representation = $"{p1} -> {vertex}"
            };

            var SideB = new AllShape
            {
                Type = "Line Segment",
                P1 = p2,
                P2 = vertex,
                Length = Math.Sqrt(Math.Pow(p2.X.GetValueOrDefault() - vertex.X.GetValueOrDefault(), 2) +
                                   Math.Pow(p2.Y.GetValueOrDefault() - vertex.Y.GetValueOrDefault(), 2)),
                Slope = p2.X.IsEquivalentTo(vertex.X)
                    ? Maybe<double>.None
                    : Maybe<double>.Some((1.0 * (vertex.Y.GetValueOrDefault() - p2.Y.GetValueOrDefault())) /
                                         (1.0 * (vertex.X.GetValueOrDefault() - p2.X.GetValueOrDefault()))),
                Representation = $"{p2} -> {vertex}"
            };

            var sideC = new AllShape
            {
                Type = "Line Segment",
                P1 = p1,
                P2 = p2,
                Length = Math.Sqrt(Math.Pow(p1.X.GetValueOrDefault() - p2.X.GetValueOrDefault(), 2) + Math.Pow(p1.Y.GetValueOrDefault() - p2.Y.GetValueOrDefault(), 2)),
                Slope = p1.X.IsEquivalentTo(p2.X) ? Maybe<double>.None : Maybe<double>.Some((1.0 * (p2.Y.GetValueOrDefault() - p1.Y.GetValueOrDefault())) / (1.0 * (p2.X.GetValueOrDefault() - p1.X.GetValueOrDefault()))),
                Representation = $"{p1} -> {p2}"
            };

            var c = sideC.Length.GetValueOrDefault();
            var a = SideA.Length.GetValueOrDefault();
            var b = SideB.Length.GetValueOrDefault();


            angles.Add(new AllShape
            {
                P1 = p1,
                Vertex = vertex,
                P2 = p2,
                Degrees = Math.Acos(Math.Round((Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2)) / (2 * a * b), 6)) * (180 /  Math.PI),
                SideA = SideA,
                SideB = SideB,
            }); //(p1, vertex, p2)
        }

        return angles.ToArray();
    }

    private static bool AllAreRight(AllShape[] angles)
    {
        var lastAngle = 90.0;
        foreach (var angle in angles)
        {
            if (lastAngle.IsEquivalentTo(90))
            {
                lastAngle = angle.Degrees.GetValueOrDefault();
                continue;
            }

            return lastAngle.IsEquivalentTo(90);
        }

        return lastAngle.IsEquivalentTo(90);
    }

    public static IShape Classify(AllShape[] points)
    {
        if(0 == points.Length)
        {
            return new AllShape { Type = "Empty" };
        }

        if (1 == points.Length)
        {
            var x = points[0].X;
            var y = points[0].Y;
            return new AllShape
            {
                X = x,
                Y = y,
                Representation = $"({x}, {y})",
                Type = "Point"
            };
        }

        var distinctPoints = GetDistinct(points);
        var pStart = points[0];
        var pEnd = points[^1];
        var path = GetPath(points);

        if (2 == points.Length && 2 == distinctPoints.Length)
        {

            Maybe<double> slope;
            if (pStart.X.IsEquivalentTo(pEnd.X))
            {
                slope = Maybe<double>.None;
            }
            else
            {
                var result =
                    (1.0 * (pEnd.Y.GetValueOrDefault() - pStart.Y.GetValueOrDefault())) / (1.0 * (pEnd.X.GetValueOrDefault() - pStart.X.GetValueOrDefault()));
                slope = Maybe<double>.Some(result);
            }

            return new AllShape
            {
                P1 = pStart,
                P2 = pEnd,
                Length = Math.Sqrt(Math.Pow(pStart.X.GetValueOrDefault() - pEnd.X.GetValueOrDefault(), 2) + Math.Pow(pStart.Y.GetValueOrDefault() - pEnd.Y.GetValueOrDefault(), 2)),
                Slope = slope,
                Type = "Line Segment",
                Representation = $"{pStart} -> {pEnd}",
            };
        }

        if (4 == points.Length && 3 == distinctPoints.Length && EqualsPoint(pStart, pEnd))
        {
            return new Triangle(path[0], path[1], path[2]);
        }

        var angles = GetAngles(points);
        if (5 == points.Length && 4 == distinctPoints.Length && EqualsPoint(pStart, pEnd) && path[0].Length.IsEquivalentTo(path[2].Length) && path[1].Length.IsEquivalentTo(path[3].Length) && AllAreRight(angles))
        {
            return new Rectangle(path[0], path[1], path[2], path[3]);
        }

        var length = 0.0;

        foreach (var segment in path)
        {
            length += segment.Length.GetValueOrDefault();
        }

        var first = points[0];
        var last = points[^1];

        return new AllShape
        {
            Points = points,
            Length = length,
            Type = "Other",
            Representation = "Other",
            IsClosed = first.X.IsEquivalentTo(last.X) && first.Y.IsEquivalentTo(last.Y),
            IsOpen = !first.X.IsEquivalentTo(last.X) || !first.Y.IsEquivalentTo(last.Y),
        };
    }
}
