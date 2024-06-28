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
                Height = Math.Abs(pLast.Y.GetValueOrDefault() - point.Y.GetValueOrDefault()),
            });
            pLast = point;
        }

        return segments.ToArray();
    }

    private static double[] SolvioHexia(AllShape[] roster)
    {
        var students = new List<double>();
        for (var i = 2; i < roster.Length; i++)
        {
            if (i - 2 >= 0 && i - 2 < roster.Length)
                students.Add(Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                                                        Math.Pow(
                                                            roster[i - 2].X.GetValueOrDefault() -
                                                            roster[i - 1].X.GetValueOrDefault(), 2) +
                                                        Math.Pow(
                                                            roster[i - 2].Y.GetValueOrDefault() -
                                                            roster[i - 1].Y.GetValueOrDefault(), 2)), 2) +
                                                    Math.Pow(Math.Sqrt(
                                                            Math.Pow(
                                                                roster[i].X.GetValueOrDefault() -
                                                                roster[i - 1].X.GetValueOrDefault(), 2) +
                                                            Math.Pow(
                                                                roster[i].Y.GetValueOrDefault() -
                                                                roster[i - 1].Y.GetValueOrDefault(), 2)),
                                                        2) - Math.Pow(
                                                        Math.Sqrt(
                                                            Math.Pow(
                                                                roster[i - 2].X.GetValueOrDefault() -
                                                                roster[i].X.GetValueOrDefault(), 2) +
                                                            Math.Pow(
                                                                roster[i - 2].Y.GetValueOrDefault() -
                                                                roster[i].Y.GetValueOrDefault(), 2)), 2)) /
                                                (2 * Math.Sqrt(
                                                     Math.Pow(
                                                         roster[i - 2].X.GetValueOrDefault() -
                                                         roster[i - 1].X.GetValueOrDefault(), 2) +
                                                     Math.Pow(
                                                         roster[i - 2].Y.GetValueOrDefault() -
                                                         roster[i - 1].Y.GetValueOrDefault(), 2)) *
                                                 Math.Sqrt(
                                                     Math.Pow(
                                                         roster[i].X.GetValueOrDefault() -
                                                         roster[i - 1].X.GetValueOrDefault(), 2) +
                                                     Math.Pow(
                                                         roster[i].Y.GetValueOrDefault() -
                                                         roster[i - 1].Y.GetValueOrDefault(), 2))), 6)) *
                           (180 / Math.PI));
        }

        return students.ToArray();
    }

    private static bool AllAreRight(double[] angles)
    {
        var lastAngle = 90.0;
        foreach (var angle in angles)
        {
            if (lastAngle.IsEquivalentTo(90))
            {
                lastAngle = angle;
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
            return new AllShape
            {
                Type = "Empty",
                Height = 0.0,
            };
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
                Type = "Point",
                Height = -1.0,
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
                Height = Math.Abs(pStart.Y.GetValueOrDefault() - pEnd.Y.GetValueOrDefault()),
            };
        }

        if (4 == points.Length && 3 == distinctPoints.Length && EqualsPoint(pStart, pEnd))
        {
            return new AllShape
            {
                P2 = path[1].P1,
                SideB = path[1],
                SideC = path[2],

                P1 = path[0].P1,
                AngleC = new AllShape
                {
                    Degrees = Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                        Math.Pow(path[0].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)), 2) + Math.Pow(Math.Sqrt(
                        Math.Pow(path[2].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)), 2) - Math.Pow(Math.Sqrt(
                        Math.Pow(path[0].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)), 2)) / (2 * Math.Sqrt(
                        Math.Pow(path[0].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)) * Math.Sqrt(
                        Math.Pow(path[2].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2))), 6)) * (180 / Math.PI),
                    Vertex = path[1].P1,
                    P1 = path[0].P1,
                    P2 = path[2].P1,
                    SideA = new AllShape
                    {
                        Length = Math.Sqrt(Math.Pow(path[0].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)),
                        Slope = path[0].P1.X.IsEquivalentTo(path[1].P1.X)
                            ? Maybe<double>.None
                            : Maybe<double>.Some((1.0 * (path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[1].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault()))),
                        Height = Math.Abs(path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault()),
                        Type = "Line Segment",
                        P1 = path[0].P1,
                        P2 = path[1].P1,
                        Representation = $"{path[0].P1} -> {path[1].P1}",
                    },
                    SideB = new AllShape
                    {
                        P1 = path[2].P1,
                        Slope = path[2].P1.X.IsEquivalentTo(path[1].P1.X)
                            ? Maybe<double>.None
                            : Maybe<double>.Some((1.0 * (path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[1].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault()))),
                        Height = Math.Abs(path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault()),
                        Length = Math.Sqrt(Math.Pow(path[2].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)),
                        Type = "Line Segment",
                        Representation = $"{path[2].P1} -> {path[1].P1}",
                        P2 = path[1].P1,
                    },
                },
                P3 = path[2].P1,

                AngleA = new AllShape
                {
                    Degrees = Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                        Math.Pow(path[1].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)), 2) + Math.Pow(Math.Sqrt(
                        Math.Pow(path[0].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)), 2) - Math.Pow(Math.Sqrt(
                        Math.Pow(path[1].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)), 2)) / (2 * Math.Sqrt(
                        Math.Pow(path[1].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)) * Math.Sqrt(
                        Math.Pow(path[0].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2))), 6)) * (180 / Math.PI),
                    Vertex = path[2].P1,
                    P1 = path[1].P1,
                    P2 = path[0].P1,
                    SideA = new AllShape
                    {
                        P2 = path[2].P1,
                        Slope = path[1].P1.X.IsEquivalentTo(path[2].P1.X)
                            ? Maybe<double>.None
                            : Maybe<double>.Some((1.0 * (path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[2].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault()))),
                        Type = "Line Segment",
                        P1 = path[1].P1,
                        Length = Math.Sqrt(Math.Pow(path[1].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)),
                        Height = Math.Abs(path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault()),
                        Representation = $"{path[1].P1} -> {path[2].P1}",
                    },
                    SideB = new AllShape
                    {
                        Length = Math.Sqrt(Math.Pow(path[0].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)),
                        P1 = path[0].P1,
                        Height = Math.Abs(path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault()),
                        P2 = path[2].P1,
                        Slope = path[0].P1.X.IsEquivalentTo(path[2].P1.X)
                            ? Maybe<double>.None
                            : Maybe<double>.Some((1.0 * (path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[2].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault()))),
                        Representation = $"{path[0].P1} -> {path[2].P1}",
                        Type = "Line Segment",
                    },
                },

                Perimeter = path[0].Length.GetValueOrDefault() + path[1].Length.GetValueOrDefault() +
                            path[2].Length.GetValueOrDefault(),

                SideA = path[0],
                Area = 0.25 * Math.Sqrt(
                    (path[0].Length.GetValueOrDefault() + path[1].Length.GetValueOrDefault() +
                     path[2].Length.GetValueOrDefault())
                    * (-(path[0].Length.GetValueOrDefault()) + path[1].Length.GetValueOrDefault() +
                       path[2].Length.GetValueOrDefault())
                    * (path[0].Length.GetValueOrDefault() - path[1].Length.GetValueOrDefault() +
                       path[2].Length.GetValueOrDefault())
                    * (path[0].Length.GetValueOrDefault() + path[1].Length.GetValueOrDefault() -
                       path[2].Length.GetValueOrDefault())
                ),

                Type = "Triangle",

                AngleB = new AllShape
                {
                    Degrees = Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                        Math.Pow(path[2].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)), 2) + Math.Pow(Math.Sqrt(
                        Math.Pow(path[1].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)), 2) - Math.Pow(Math.Sqrt(
                        Math.Pow(path[2].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)), 2)) / (2 * Math.Sqrt(
                        Math.Pow(path[2].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)) * Math.Sqrt(
                        Math.Pow(path[1].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                        Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2))), 6)) * (180 / Math.PI),
                    Vertex = path[0].P1,
                    P1 = path[2].P1,
                    P2 = path[1].P1,
                    SideA = new AllShape
                    {
                        Height = Math.Abs(path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault()),
                        Length = Math.Sqrt(Math.Pow(path[2].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)),
                        Slope = path[2].P1.X.IsEquivalentTo(path[0].P1.X)
                            ? Maybe<double>.None
                            : Maybe<double>.Some((1.0 * (path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[0].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault()))),
                        Type = "Line Segment",
                        P1 = path[2].P1,
                        P2 = path[0].P1,
                        Representation = $"{path[2].P1} -> {path[0].P1}",
                    },
                    SideB = new AllShape
                    {
                        Length = Math.Sqrt(Math.Pow(path[1].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)),
                        P1 = path[1].P1,
                        Height = Math.Abs(path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault()),
                        P2 = path[0].P1,
                        Slope = path[1].P1.X.IsEquivalentTo(path[0].P1.X)
                            ? Maybe<double>.None
                            : Maybe<double>.Some((1.0 * (path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[0].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault()))),
                        Representation = $"{path[1].P1} -> {path[0].P1}",
                        Type = "Line Segment",
                    },
                },
            };
        }

        var angles = SolvioHexia(points);
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
