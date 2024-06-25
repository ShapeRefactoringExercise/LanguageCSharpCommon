﻿using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    private static Point[] GetDistinct(Point[] points)
    {
        return points.Distinct().ToArray();
    }

    private static LineSegment[] GetPath(Point[] points)
    {
        Point pLast = null;
        var segments = new List<LineSegment>();
        foreach (var point in points)
        {
            if (pLast == null)
            {
                pLast = point;
                continue;
            }

            segments.Add(new LineSegment(pLast, point));
            pLast = point;
        }

        return segments.ToArray();
    }

    private static Angle[] GetAngles(Point[] points)
    {
        var angles = new List<Angle>();
        for (int i = 2; i < points.Length; i++)
        {
            var p1 = points[i - 2];
            var vertex = points[i - 1];
            var p2 = points[i];

            angles.Add(new Angle(p1, vertex, p2));
        }

        return angles.ToArray();
    }

    private static bool AllAreRight(Angle[] angles)
    {
        return angles.All(a => a.Degrees.IsEquivalentTo(90));
    }

    public static IShape Classify(Point[] points)
    {
        if(0 == points.Length)
        {
            return new EmptyShape();
        }

        if (1 == points.Length)
        {
            return points[0];
        }

        var distinctPoints = GetDistinct(points);
        var pStart = points.First();
        var pEnd = points.Last();
        var path = GetPath(points);

        if (2 == points.Length && 2 == distinctPoints.Length)
        {
            return new LineSegment(pStart, pEnd);
        }

        if (4 == points.Length && 3 == distinctPoints.Length && Equals(pStart, pEnd))
        {
            return new Triangle(path[0], path[1], path[2]);
        }

        var angles = GetAngles(points);
        if (5 == points.Length && 4 == distinctPoints.Length && Equals(pStart, pEnd) && path[0].Length.IsEquivalentTo(path[2].Length) && path[1].Length.IsEquivalentTo(path[3].Length) && AllAreRight(angles))
        {
            return new Rectangle(path[0], path[1], path[2], path[3]);
        }

        return new Other(path);
    }
}
