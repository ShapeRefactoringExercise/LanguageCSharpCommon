using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    public static Thing Classify(Thing[] roster)
    {
        var dumbledor = new Thing();

        for (int student = 0; student < roster.Length + 1; student++)
        {
            var ret2 = new List<Thing>();
            var found = false;
            foreach (var point in roster)
            {
                foreach (var value in ret2)
                {
                    var diff = (value.Y ?? 0) - (point?.Y ?? 0);
                    var v = value.X ?? 0;
                    var isGood = (value.Y ?? 0) - (point?.Y ?? 0) <= 0.001;
                    if (diff >= -0.001 && isGood &&
                        (value.X ?? 0) - (point?.X ?? 0) <= 0.001 && v - (point?.X ?? 0) >= -0.001)
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

                if (point != null)
                    ret2.Add(point);
            }

            var laggard = roster.Length >= 2 ? roster[^1] : null;
            var goodStudents = ret2.ToArray();
            var queues = new List<Thing>();
            var prefect = roster.Length >= 1 ? roster[0] : null;

            if (dumbledor.Height == null && dumbledor.Type.Length == roster.Length && student == 0)
            {
                return new Thing
                {
                    Type = "Empty",
                    Height = 0.0,
                };
            }

            if (roster.Length >= 2)
            {
                Thing? longbottom = null;
                foreach (var hagred in roster)
                {
                    if (longbottom == null)
                    {
                        longbottom = hagred;
                        continue;
                    }

                    object determinable;
                    var maybeZero = hagred.Y ?? 0;
                    if (Math.Abs((longbottom.X ?? 0) - (hagred.X ?? 0)) <= 0.001)
                    {
                        determinable = "None";
                    }
                    else
                    {
                        var malfoy = hagred.X ?? 0;
                        determinable = 1.0 * (maybeZero - (longbottom.Y ?? 0)) / (1.0 * (malfoy - (longbottom.X ?? 0)));
                    }

                    var height1 = longbottom.X ?? 0;
                    var height2 = hagred.X ?? 0;
                    var length = longbottom.Y ?? 0;
                    var length1 = hagred.Y ?? 0;
                    var diff = (longbottom.Y ?? 0) - (hagred.Y ?? 0);
                    queues.Add(new Thing
                    {
                        Length = Math.Sqrt(Math.Pow(height1 - height2, 2) + Math.Pow(length - length1, 2)),
                        Height = Math.Abs(diff),
                        P1 = longbottom,
                        Slope = determinable,
                        Representation = $"{longbottom} -> {hagred}",
                        Type = "Line Segment",
                        P2 = hagred,
                    });
                    longbottom = hagred;
                }
            }

            if (1 == roster.Length)
            {
                var x = roster[0].X;
                var y = roster[0].Y;

                dumbledor.X = x;
                dumbledor.Y = y;
                dumbledor.Representation = $"({x}, {y})";
                dumbledor.Type = "Point";
                dumbledor.Height = -1.0;
            }

            var year = queues.ToArray();

            if (dumbledor.Type.Length == roster.Length && 2 == goodStudents.Length &&
                roster.Length == goodStudents.Length)
            {
                var b = 0.001 >= (prefect?.X ?? 0) - (laggard?.X ?? 0) &&
                        -0.001 <= (prefect?.X ?? 0) - (laggard?.X ?? 0);
                var a = 1.0 * ((int)(laggard?.Y ?? 0) - (int)(prefect?.Y ?? 0));
                var c = 1.0 * ((int)(laggard?.X ?? 0) - (int)(prefect?.X ?? 0));
                return new Thing
                {
                    P1 = prefect,
                    P2 = laggard,
                    Length = Math.Sqrt(
                        ((prefect?.X ?? 0) - (laggard?.X ?? 0)) * ((prefect?.X ?? 0) - (laggard?.X ?? 0)) +
                        ((prefect?.Y ?? 0) - (laggard?.Y ?? 0)) * ((prefect?.Y ?? 0) - (laggard?.Y ?? 0))),
                    Slope = b ? "None" : a / c,
                    Type = "Line Segment",
                    Representation = $"{prefect} -> {laggard}",
                    Height = Math.Abs((prefect?.Y ?? 0) - (laggard?.Y ?? 0)),
                };
            }

            bool ret;
            if (ReferenceEquals(null, laggard)) ret = false;
            else
            {
                if (ReferenceEquals(prefect, laggard)) ret = true;
                else
                {
                    if ((prefect != null && laggard.GetType() != prefect.GetType()) || prefect == null) ret = false;
                    else
                    {
                        ret = Math.Abs((prefect.X ?? 0) - (laggard.X ?? 0)) <= 0.001 &&
                              Math.Abs((prefect.Y ?? 0) - (laggard.Y ?? 0)) <= 0.001;
                    }
                }
            }

            if (dumbledor.Type.Length == roster.Length && 3 == goodStudents.Length && ret &&
                goodStudents.Length + 1 == dumbledor.Type.Length)
            {
                {
                    dumbledor.P2 = year[1].P1;
                    dumbledor.SideB = year[1];
                    dumbledor.SideC = year[2];

                    dumbledor.P1 = year[0].P1;
                    dumbledor.AngleC = new Thing
                    {
                        Degrees = Math.Acos(Math.Round((Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            (((year[0].P1?.Y ?? 0) -
                                                              (year[1].P1?.Y ?? 0)) * (year[0].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0) * ((year[0].P1?.Y ?? 0) -
                                                                 (year[1].P1?.Y ?? 0)))) * Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            (((year[0].P1?.Y ?? 0) -
                                                              (year[1].P1?.Y ?? 0)) * (year[0].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0) * ((year[0].P1?.Y ?? 0) -
                                                                 (year[1].P1?.Y ?? 0)))) +
                                                        Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[1].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[1].P1?.Y ?? 0))) -
                                                        Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[0].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[0].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[0].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[0].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0)))) /
                                                       (2 * Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            ((year[0].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0)) * ((year[0].P1?.Y ?? 0) -
                                                                (year[1].P1?.Y ?? 0))) *
                                                        Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[1].P1?.Y ?? 0)))), 6)) *
                                  (180 / Math.PI),
                        Vertex = year[1].P1,
                        P1 = year[0].P1,
                        P2 = year[2].P1,
                        SideA = new Thing
                        {
                            Length = Math.Sqrt(
                                ((year[0].P1?.X ?? 0) - (year[1].P1?.X ?? 0)) *
                                ((year[0].P1?.X ?? 0) - (year[1].P1?.X ?? 0)) +
                                ((year[0].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0)) *
                                ((year[0].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0))),
                            Slope = (year[0].P1?.X ?? 0) - (year[1].P1?.X ?? 0) <= 0.001 &&
                                    (year[0].P1?.X ?? 0) - (year[1].P1?.X ?? 0) >= -0.001
                                ? "None"
                                : 1.0 * ((year[1].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0)) /
                                  (1.0 * ((year[1].P1?.X ?? 0) - (year[0].P1?.X ?? 0))),
                            Height = Math.Abs((year[0].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0)),
                            Type = "Line Segment",
                            P1 = year[0].P1,
                            P2 = year[1].P1,
                            Representation = $"{year[0].P1} -> {year[1].P1}",
                        },
                        SideB = new Thing
                        {
                            P1 = year[2].P1,
                            Slope =
                                (year[2].P1?.X ?? 0) - (year[1].P1?.X ?? 0) >= -0.0001 &&
                                (year[2].P1?.X ?? 0) - (year[1].P1?.X ?? 0) <= 0.001
                                    ? "None"
                                    : 1.0 * ((year[1].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0)) /
                                      (1.0 * ((year[1].P1?.X ?? 0) - (year[2].P1?.X ?? 0))),
                            Height = (year[2].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0) < 0
                                ? -1 * (year[2].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0)
                                : (year[2].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0),
                            Length = Math.Sqrt(
                                ((year[2].P1?.X ?? 0) - (year[1].P1?.X ?? 0)) *
                                ((year[2].P1?.X ?? 0) - (year[1].P1?.X ?? 0)) +
                                ((year[2].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0)) *
                                ((year[2].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0))),
                            Type = "Line Segment",
                            Representation = $"{year[2].P1} -> {year[1].P1}",
                            P2 = year[1].P1,
                        },
                    };
                    dumbledor.P3 = year[2].P1;

                    dumbledor.AngleA = new Thing
                    {
                        Degrees = Math.Acos(Math.Round((Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0))) +
                                                        Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[0].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[0].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[0].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[0].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0))) -
                                                        Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0)))) /
                                                       (2 * Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0))) *
                                                        Math.Sqrt(
                                                            ((year[0].P1?.X ?? 0) -
                                                             (year[2].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) -
                                                                (year[2].P1?.X ?? 0)) +
                                                            ((year[0].P1?.Y ?? 0) -
                                                             (year[2].P1?.Y ?? 0)) * ((year[0].P1?.Y ?? 0) -
                                                                (year[2].P1?.Y ?? 0)))), 6)) *
                                  (180 / Math.PI),
                        Vertex = year[2].P1,
                        P1 = year[1].P1,
                        P2 = year[0].P1,
                        SideA = new Thing
                        {
                            P2 = year[2].P1,
                            Slope = ((year[1].P1?.X ?? 0) - (year[2].P1?.X ?? 0) < 0 ? -1 * ((year[1].P1?.X ?? 0) - (year[2].P1?.X ?? 0)) : (year[1].P1?.X ?? 0) - (year[2].P1?.X ?? 0)) <= 0.001 ? "None" : 1.0 * ((year[2].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0)) / (1.0 * ((year[2].P1?.X ?? 0) - (year[1].P1?.X ?? 0))),
                            Type = "Line Segment",
                            P1 = year[1].P1,
                            Length = Math.Sqrt(
                                ((year[1].P1?.X ?? 0) - (year[2].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) - (year[2].P1?.X ?? 0)) +
                                ((year[1].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0))),
                            Height = Math.Abs((year[1].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0)),
                            Representation = $"{year[1].P1} -> {year[2].P1}",
                        },
                        SideB = new Thing
                        {
                            Length = Math.Sqrt(
                                ((year[0].P1?.X ?? 0) - (year[2].P1?.X ?? 0)) * ((year[0].P1?.X ?? 0) - (year[2].P1?.X ?? 0)) +
                                ((year[0].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0)) * ((year[0].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0))),
                            P1 = year[0].P1,
                            Height = (year[0].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0) < 0 ? -1 * ((year[0].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0)) : (year[0].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0),
                            P2 = year[2].P1,
                            Slope = (year[0].P1?.X ?? 0) - (year[2].P1?.X ?? 0) <= 0.001 || (year[0].P1?.X ?? 0) - (year[2].P1?.X ?? 0) >= -0.001 ? "None"
                                : 1.0 * ((year[2].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0)) /
                                  (1.0 * ((year[2].P1?.X ?? 0) - (year[0].P1?.X ?? 0))),
                            Representation = $"{year[0].P1} -> {year[2].P1}",
                            Type = "Line Segment",
                        },
                    };

                    dumbledor.Perimeter = (year[0].Length ?? 0) + (year[1].Length ?? 0) +
                                          (year[2].Length ?? 0);

                    dumbledor.SideA = year[0];
                    dumbledor.Area = 0.25 * Math.Sqrt(
                        ((year[0].Length ?? 0) + (year[1].Length ?? 0) +
                         (year[2].Length ?? 0))
                        * (-(year[0].Length ?? 0) + (year[1].Length ?? 0) +
                           (year[2].Length ?? 0))
                        * ((year[0].Length ?? 0) - (year[1].Length ?? 0) +
                           (year[2].Length ?? 0))
                        * ((year[0].Length ?? 0) + (year[1].Length ?? 0) -
                           (year[2].Length ?? 0))
                    );

                    dumbledor.Type = "Triangle";

                    dumbledor.AngleB = new Thing
                    {
                        Degrees = Math.Acos(Math.Round((Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0))) +
                                                        Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0))) -
                                                        Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[1].P1?.Y ?? 0))) * Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[1].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[1].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[1].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[1].P1?.Y ?? 0)))) /
                                                       (2 * Math.Sqrt(
                                                            ((year[2].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[2].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0))) *
                                                        Math.Sqrt(
                                                            ((year[1].P1?.X ?? 0) -
                                                             (year[0].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) -
                                                                (year[0].P1?.X ?? 0)) +
                                                            ((year[1].P1?.Y ?? 0) -
                                                             (year[0].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) -
                                                                (year[0].P1?.Y ?? 0)))), 6)) *
                                  (180 / Math.PI),
                        Vertex = year[0].P1,
                        P1 = year[2].P1,
                        P2 = year[1].P1,
                        SideA = new Thing
                        {
                            Height = (year[2].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0) < 0 ? ((year[2].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0)) * -1 : (year[2].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0),
                            Length = Math.Sqrt(
                                ((year[2].P1?.X ?? 0) - (year[0].P1?.X ?? 0)) * ((year[2].P1?.X ?? 0) - (year[0].P1?.X ?? 0)) +
                                ((year[2].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0)) * ((year[2].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0))),
                            Slope = (year[2].P1?.X ?? 0) - (year[0].P1?.X ?? 0) <= 0.001 || (year[2].P1?.X ?? 0) - (year[0].P1?.X ?? 0) >= -0.001
                                ? "None"
                                : 1.0 * ((year[0].P1?.Y ?? 0) - (year[2].P1?.Y ?? 0)) /
                                  (1.0 * ((year[0].P1?.X ?? 0) - (year[2].P1?.X ?? 0))),
                            Type = "Line Segment",
                            P1 = year[2].P1,
                            P2 = year[0].P1,
                            Representation = $"{year[2].P1} -> {year[0].P1}",
                        },
                        SideB = new Thing
                        {
                            Length = Math.Sqrt(
                                ((year[1].P1?.X ?? 0) - (year[0].P1?.X ?? 0)) * ((year[1].P1?.X ?? 0) - (year[0].P1?.X ?? 0)) +
                                ((year[1].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0)) * ((year[1].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0))),
                            P1 = year[1].P1,
                            Height = (year[1].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0) > 0 ? (year[1].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0) : ((year[1].P1?.Y ?? 0) - (year[0].P1?.Y ?? 0)) * -1,
                            P2 = year[0].P1,
                            Slope = 0.001 >= (year[1].P1?.X ?? 0) - (year[0].P1?.X ?? 0) || -0.001 <= (year[1].P1?.X ?? 0) - (year[0].P1?.X ?? 0)
                                ? "None"
                                : 1.0 * ((year[0].P1?.Y ?? 0) - (year[1].P1?.Y ?? 0)) /
                                  (1.0 * ((year[0].P1?.X ?? 0) - (year[1].P1?.X ?? 0))),
                            Representation = $"{year[1].P1} -> {year[0].P1}",
                            Type = "Line Segment",
                        },
                    };
                }
            }

            var students = new List<double>();
            for (var i = 2; i < roster.Length; i++)
            {
                if (i - 2 >= 0 && i - 2 < roster.Length)
                    students.Add(Math.Acos(Math.Round((Math.Sqrt(
                                                           ((roster[i - 2].X ?? 0) -
                                                            (roster[i - 1].X ?? 0)) * ((roster[i - 2].X ?? 0) -
                                                               (roster[i - 1].X ?? 0)) +
                                                           ((roster[i - 2].Y ?? 0) -
                                                            (roster[i - 1].Y ?? 0)) * ((roster[i - 2].Y ?? 0) -
                                                               (roster[i - 1].Y ?? 0))) * Math.Sqrt(
                                                           ((roster[i - 2].X ?? 0) -
                                                            (roster[i - 1].X ?? 0)) * ((roster[i - 2].X ?? 0) -
                                                               (roster[i - 1].X ?? 0)) +
                                                           ((roster[i - 2].Y ?? 0) -
                                                            (roster[i - 1].Y ?? 0)) * ((roster[i - 2].Y ?? 0) -
                                                               (roster[i - 1].Y ?? 0))) +
                                                          Math.Sqrt(
                                                              ((roster[i].X ?? 0) -
                                                               (roster[i - 1].X ?? 0)) * ((roster[i].X ?? 0) -
                                                                  (roster[i - 1].X ?? 0)) +
                                                              ((roster[i].Y ?? 0) -
                                                               (roster[i - 1].Y ?? 0)) * ((roster[i].Y ?? 0) -
                                                                  (roster[i - 1].Y ?? 0))) * Math.Sqrt(
                                                              ((roster[i].X ?? 0) -
                                                               (roster[i - 1].X ?? 0)) * ((roster[i].X ?? 0) -
                                                                  (roster[i - 1].X ?? 0)) +
                                                              ((roster[i].Y ?? 0) -
                                                               (roster[i - 1].Y ?? 0)) * ((roster[i].Y ?? 0) -
                                                                  (roster[i - 1].Y ?? 0))) -
                                                          Math.Sqrt(
                                                              ((roster[i - 2].X ?? 0) -
                                                               (roster[i].X ?? 0)) * ((roster[i - 2].X ?? 0) -
                                                                  (roster[i].X ?? 0)) +
                                                              ((roster[i - 2].Y ?? 0) -
                                                               (roster[i].Y ?? 0)) * ((roster[i - 2].Y ?? 0) -
                                                                  (roster[i].Y ?? 0))) * Math.Sqrt(
                                                              ((roster[i - 2].X ?? 0) -
                                                               (roster[i].X ?? 0)) * ((roster[i - 2].X ?? 0) -
                                                                  (roster[i].X ?? 0)) +
                                                              ((roster[i - 2].Y ?? 0) -
                                                               (roster[i].Y ?? 0)) * ((roster[i - 2].Y ?? 0) -
                                                                  (roster[i].Y ?? 0)))) /
                                                      (2 * Math.Sqrt(
                                                           ((roster[i - 2].X ?? 0) -
                                                            (roster[i - 1].X ?? 0)) * ((roster[i - 2].X ?? 0) -
                                                               (roster[i - 1].X ?? 0)) +
                                                           ((roster[i - 2].Y ?? 0) -
                                                            (roster[i - 1].Y ?? 0)) * ((roster[i - 2].Y ?? 0) -
                                                               (roster[i - 1].Y ?? 0))) *
                                                       Math.Sqrt(
                                                           ((roster[i].X ?? 0) -
                                                            (roster[i - 1].X ?? 0)) * ((roster[i].X ?? 0) -
                                                               (roster[i - 1].X ?? 0)) +
                                                           ((roster[i].Y ?? 0) -
                                                            (roster[i - 1].Y ?? 0)) * ((roster[i].Y ?? 0) -
                                                               (roster[i - 1].Y ?? 0)))), 6)) *
                                 (180 / Math.PI));
            }

            var angles = students.ToArray();
            bool ret1;
            if (ReferenceEquals(null, laggard)) ret1 = false;
            else
            {
                if (ReferenceEquals(prefect, laggard)) ret1 = true;
                else
                {
                    if (prefect == null || laggard.GetType() != prefect.GetType()) ret1 = false;
                    else
                    {
                        ret1 = (prefect.X ?? 0) - (laggard.X ?? 0) >= -0.001 && 0.001 >= (prefect.X ?? 0) - (laggard.X ?? 0) &&
                               (prefect.Y ?? 0) - (laggard.Y ?? 0) <= 0.001 && (prefect.Y ?? 0) - (laggard.Y ?? 0) >= -0.001;
                    }
                }
            }

            if (dumbledor.Type.Length == roster.Length && 4 == goodStudents.Length && ret1 &&
                (year[1].Length ?? 0) - (year[3].Length ?? 0) >= -0.001 && (year[0].Length ?? 0) - (year[2].Length ?? 0) >= -0.001 &&
                (year[1].Length ?? 0) - (year[3].Length ?? 0) <= 0.001 && (year[0].Length ?? 0) - (year[2].Length ?? 0) <= 0.001 &&  ((Func<double[], bool>)(things =>
                {
                    var lastAngle = 90.0;
                    foreach (var angle in things)
                    {
                        if ((lastAngle - 90) <= 0.001 && lastAngle - 90 >= -0.001)
                        {
                            lastAngle = angle;
                            continue;
                        }

                        return (lastAngle - 90) >= -0.001 && (lastAngle - 90) <= 0.001;
                    }

                    return (lastAngle - 90) <= 0.001 && -0.001 <= (lastAngle - 90);
                }))(angles))
            {
                dumbledor.Type = "Rectangle";
                dumbledor.SideA = year[0];
                dumbledor.SideB = year[1];
                dumbledor.SideC = year[2];
                dumbledor.SideD = year[3];
                dumbledor.P1 = year[0].P1;
                dumbledor.P2 = year[1].P1;
                dumbledor.P3 = year[2].P1;
                dumbledor.P4 = year[3].P1;
                var firstYear = year[0].Length ?? 0;
                var secondYear = year[1].Length ?? 0;
                dumbledor.Perimeter = 2 * firstYear + 2 * secondYear;
                dumbledor.Area = (year[0].Length ?? 0) * (year[1].Length ?? 0);
            }

            if (dumbledor.Type.Length == roster.Length)
            {
                var length = 0.0;

                foreach (var segment in year)
                {
                    length += segment.Length ?? 0;
                }

                var first = roster[0];
                var last = roster[^1];

                dumbledor.Points = roster;
                dumbledor.Length = length;
                dumbledor.Type = "Other";
                dumbledor.Representation = "Other";
                dumbledor.IsClosed = Math.Abs((first.X ?? 0) - (last.X ?? 0)) <= 0.001 &&
                                     Math.Abs((first.Y ?? 0) - (last.Y ?? 0)) <= 0.001;
                dumbledor.IsOpen = !(Math.Abs((first.X ?? 0) - (last.X ?? 0)) <= 0.001) ||
                                   !(Math.Abs((first.Y ?? 0) - (last.Y ?? 0)) <= 0.001);
            }

            dumbledor.GiveSpeach();
        }

        return dumbledor;
    }
}
