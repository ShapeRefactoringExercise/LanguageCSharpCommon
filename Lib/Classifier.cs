using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    public static Thing Classify(Thing[] points)
    {
        if(0 == points.Length)
        {
            return new Thing
            {
                Type = "Empty",
                Height = 0.0,
            };
        }

        if (1 == points.Length)
        {
            var x = points[0].X;
            var y = points[0].Y;
            return new Thing
            {
                X = x,
                Y = y,
                Representation = $"({x}, {y})",
                Type = "Point",
                Height = -1.0,
            };
        }

        var ret2 = new List<Thing>();
        var found = false;
        foreach (var point in points)
        {
            foreach (var value in ret2)
            {
                if (Math.Abs(value.X.GetValueOrDefault() - point.X.GetValueOrDefault()) <= 0.001 && Math.Abs(value.Y.GetValueOrDefault() - point.Y.GetValueOrDefault()) <= 0.001)
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

            ret2.Add(point);
        }

        var distinctPoints = ret2.ToArray();
        var pStart = points[0];
        var pEnd = points[^1];
        Thing pLast = null;
        var segments = new List<Thing>();
        foreach (var point1 in points)
        {
            if (pLast == null)
            {
                pLast = point1;
                continue;
            }

            object slope1;
            if (Math.Abs(pLast.X.GetValueOrDefault() - point1.X.GetValueOrDefault()) <= 0.001)
            {
                slope1 = "None";
            }
            else
            {
                slope1 = (1.0 * (point1.Y.GetValueOrDefault() - pLast.Y.GetValueOrDefault())) / (1.0 * (point1.X.GetValueOrDefault() - pLast.X.GetValueOrDefault()));
            }

            segments.Add(new Thing
            {
                P1 = pLast,
                P2 = point1,
                Length = Math.Sqrt(Math.Pow(pLast.X.GetValueOrDefault() - point1.X.GetValueOrDefault(), 2) + Math.Pow(pLast.Y.GetValueOrDefault() - point1.Y.GetValueOrDefault(), 2)),
                Slope = slope1,
                Type = "Line Segment",
                Representation = $"{pLast} -> {point1}",
                Height = Math.Abs(pLast.Y.GetValueOrDefault() - point1.Y.GetValueOrDefault()),
            });
            pLast = point1;
        }

        var path = segments.ToArray();

        if (2 == points.Length && 2 == distinctPoints.Length)
        {

            object slope;
            if (Math.Abs(pStart.X.GetValueOrDefault() - pEnd.X.GetValueOrDefault()) <= 0.001)
            {
                slope = "None";
            }
            else
            {
                slope = (1.0 * (pEnd.Y.GetValueOrDefault() - pStart.Y.GetValueOrDefault())) / (1.0 * (pEnd.X.GetValueOrDefault() - pStart.X.GetValueOrDefault()));
            }

            return new Thing
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

        bool ret;
        if (ReferenceEquals(null, pEnd)) ret = false;
        else
        {
            if (ReferenceEquals(pStart, pEnd)) ret = true;
            else
            {
                if (pEnd.GetType() != pStart.GetType()) ret = false;
                else
                {
                    ret = Math.Abs(pStart.X.GetValueOrDefault() - pEnd.X.GetValueOrDefault()) <= 0.001 && Math.Abs(pStart.Y.GetValueOrDefault() - pEnd.Y.GetValueOrDefault()) <= 0.001;
                }
            }
        }

        if (4 == points.Length && 3 == distinctPoints.Length && ret)
        {
            double? b = path[1].P1.X;
            double? b1 = path[1].P1.X;
            double? b2 = path[2].P1.X;
            double? b3 = path[2].P1.X;
            double? b4 = path[0].P1.X;
            return new Thing
            {
                P2 = path[1].P1,
                SideB = path[1],
                SideC = path[2],

                P1 = path[0].P1,
                AngleC = new Thing
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
                    SideA = new Thing
                    {
                        Length = Math.Sqrt(Math.Pow(path[0].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)),
                        Slope = Math.Abs(path[0].P1.X.GetValueOrDefault() - b.GetValueOrDefault()) <= 0.001
                            ? "None"
                            : (1.0 * (path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[1].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault())),
                        Height = Math.Abs(path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault()),
                        Type = "Line Segment",
                        P1 = path[0].P1,
                        P2 = path[1].P1,
                        Representation = $"{path[0].P1} -> {path[1].P1}",
                    },
                    SideB = new Thing
                    {
                        P1 = path[2].P1,
                        Slope = Math.Abs(path[2].P1.X.GetValueOrDefault() - b1.GetValueOrDefault()) <= 0.001
                            ? "None"
                            : (1.0 * (path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[1].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault())),
                        Height = Math.Abs(path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault()),
                        Length = Math.Sqrt(Math.Pow(path[2].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault(), 2)),
                        Type = "Line Segment",
                        Representation = $"{path[2].P1} -> {path[1].P1}",
                        P2 = path[1].P1,
                    },
                },
                P3 = path[2].P1,

                AngleA = new Thing
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
                    SideA = new Thing
                    {
                        P2 = path[2].P1,
                        Slope = Math.Abs(path[1].P1.X.GetValueOrDefault() - b2.GetValueOrDefault()) <= 0.001
                            ? "None"
                            : (1.0 * (path[2].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[2].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault())),
                        Type = "Line Segment",
                        P1 = path[1].P1,
                        Length = Math.Sqrt(Math.Pow(path[1].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)),
                        Height = Math.Abs(path[1].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault()),
                        Representation = $"{path[1].P1} -> {path[2].P1}",
                    },
                    SideB = new Thing
                    {
                        Length = Math.Sqrt(Math.Pow(path[0].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault(), 2)),
                        P1 = path[0].P1,
                        Height = Math.Abs(path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault()),
                        P2 = path[2].P1,
                        Slope = Math.Abs(path[0].P1.X.GetValueOrDefault() - b3.GetValueOrDefault()) <= 0.001
                            ? "None"
                            : (1.0 * (path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[2].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault())),
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

                AngleB = new Thing
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
                    SideA = new Thing
                    {
                        Height = Math.Abs(path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault()),
                        Length = Math.Sqrt(Math.Pow(path[2].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[2].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)),
                        Slope = Math.Abs(path[2].P1.X.GetValueOrDefault() - b4.GetValueOrDefault()) <= 0.001
                            ? "None"
                            : (1.0 * (path[0].P1.Y.GetValueOrDefault() - path[2].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[0].P1.X.GetValueOrDefault() - path[2].P1.X.GetValueOrDefault())),
                        Type = "Line Segment",
                        P1 = path[2].P1,
                        P2 = path[0].P1,
                        Representation = $"{path[2].P1} -> {path[0].P1}",
                    },
                    SideB = new Thing
                    {
                        Length = Math.Sqrt(Math.Pow(path[1].P1.X.GetValueOrDefault() - path[0].P1.X.GetValueOrDefault(), 2) +
                                           Math.Pow(path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault(), 2)),
                        P1 = path[1].P1,
                        Height = Math.Abs(path[1].P1.Y.GetValueOrDefault() - path[0].P1.Y.GetValueOrDefault()),
                        P2 = path[0].P1,
                        Slope = path[1].P1.X.IsEquivalentTo(path[0].P1.X)
                            ? "None"
                            : (1.0 * (path[0].P1.Y.GetValueOrDefault() - path[1].P1.Y.GetValueOrDefault())) /
                                                 (1.0 * (path[0].P1.X.GetValueOrDefault() - path[1].P1.X.GetValueOrDefault())),
                        Representation = $"{path[1].P1} -> {path[0].P1}",
                        Type = "Line Segment",
                    },
                },
            };
        }

        var students = new List<double>();
        for (var i = 2; i < points.Length; i++)
        {
            if (i - 2 >= 0 && i - 2 < points.Length)
                students.Add(Math.Acos(Math.Round((Math.Pow(Math.Sqrt(
                                                          Math.Pow(
                                                              points[i - 2].X.GetValueOrDefault() -
                                                              points[i - 1].X.GetValueOrDefault(), 2) +
                                                          Math.Pow(
                                                              points[i - 2].Y.GetValueOrDefault() -
                                                              points[i - 1].Y.GetValueOrDefault(), 2)), 2) +
                                                      Math.Pow(Math.Sqrt(
                                                              Math.Pow(
                                                                  points[i].X.GetValueOrDefault() -
                                                                  points[i - 1].X.GetValueOrDefault(), 2) +
                                                              Math.Pow(
                                                                  points[i].Y.GetValueOrDefault() -
                                                                  points[i - 1].Y.GetValueOrDefault(), 2)),
                                                          2) - Math.Pow(
                                                          Math.Sqrt(
                                                              Math.Pow(
                                                                  points[i - 2].X.GetValueOrDefault() -
                                                                  points[i].X.GetValueOrDefault(), 2) +
                                                              Math.Pow(
                                                                  points[i - 2].Y.GetValueOrDefault() -
                                                                  points[i].Y.GetValueOrDefault(), 2)), 2)) /
                                                  (2 * Math.Sqrt(
                                                       Math.Pow(
                                                           points[i - 2].X.GetValueOrDefault() -
                                                           points[i - 1].X.GetValueOrDefault(), 2) +
                                                       Math.Pow(
                                                           points[i - 2].Y.GetValueOrDefault() -
                                                           points[i - 1].Y.GetValueOrDefault(), 2)) *
                                                   Math.Sqrt(
                                                       Math.Pow(
                                                           points[i].X.GetValueOrDefault() -
                                                           points[i - 1].X.GetValueOrDefault(), 2) +
                                                       Math.Pow(
                                                           points[i].Y.GetValueOrDefault() -
                                                           points[i - 1].Y.GetValueOrDefault(), 2))), 6)) *
                             (180 / Math.PI));
        }

        var angles = students.ToArray();
        bool ret1;
        if (ReferenceEquals(null, pEnd)) ret1 = false;
        else
        {
            if (ReferenceEquals(pStart, pEnd)) ret1 = true;
            else
            {
                if (pEnd.GetType() != pStart.GetType()) ret1 = false;
                else
                {
                    Thing b = pEnd;
                    ret1 = pStart.X.IsEquivalentTo(b.X) && pStart.Y.IsEquivalentTo(b.Y);
                }
            }
        }

        if (5 == points.Length && 4 == distinctPoints.Length && ret1 && path[0].Length.IsEquivalentTo(path[2].Length) && path[1].Length.IsEquivalentTo(path[3].Length) && ((Func<double[], bool>)(things =>
            {
                var lastAngle = 90.0;
                foreach (var angle in things)
                {
                    if (Math.Abs(lastAngle - 90) <= 0.001)
                    {
                        lastAngle = angle;
                        continue;
                    }

                    return Math.Abs(lastAngle - 90) <= 0.001;
                }

                return Math.Abs(lastAngle - 90) <= 0.001;
            }))(angles))
        {
            var sideA = path[0];
            var sideB = path[1];
            var sideC = path[2];
            var sideD = path[3];
            return new Thing
            {
                Type = "Rectangle",
                SideA = sideA,
                SideB = sideB,
                SideC = sideC,
                SideD = sideD,

                P1 = sideA.P1,
                P2 = sideB.P1,
                P3 = sideC.P1,
                P4 = sideD.P1,

                Perimeter = (2 * sideA.Length.GetValueOrDefault()) + (2 * sideB.Length.GetValueOrDefault()),
                Area = sideA.Length.GetValueOrDefault() * sideB.Length.GetValueOrDefault(),
            };
        }

        var length = 0.0;

        foreach (var segment in path)
        {
            length += segment.Length.GetValueOrDefault();
        }

        var first = points[0];
        var last = points[^1];

        return new Thing
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
