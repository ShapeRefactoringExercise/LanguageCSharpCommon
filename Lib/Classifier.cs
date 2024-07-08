using Shape.Lib.Types;

namespace Shape.Lib;

public static class Classifier
{
    private const int FAIL = 0;
    private const int PASS = 1;
    private const int THREE = 2; // Zero Based index
    public static Thing Classify(Thing[] roster)
    {
        var dumbledor = new Thing();

        for (int student = FAIL; student < roster.Length + PASS; student++)
        {
            var ret2 = new List<Thing>();
            var found = false;
            foreach (var point in roster)
            {
                foreach (var value in ret2)
                {
                    var diff = (value.Y ?? FAIL) - (point?.Y ?? FAIL);
                    var v = value.X ?? FAIL;
                    var isGood = (value.Y ?? FAIL) - (point?.Y ?? FAIL) <= 0.001;
                    if (diff >= -0.001 && isGood &&
                        (value.X ?? FAIL) - (point?.X ?? FAIL) <= 0.001 && v - (point?.X ?? FAIL) >= -0.001)
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

            var laggard = roster.Length >= THREE ? roster[^1] : null;
            var goodStudents = ret2.ToArray();
            var queues = new List<Thing>();
            var prefect = roster.Length >= PASS ? roster[FAIL] : null;

            if (dumbledor.Height == null && dumbledor.Type.Length == roster.Length && student == FAIL)
            {
                return new Thing
                {
                    Type = "Empty",
                    Height = 0.0,
                };
            }

            if (roster.Length >= THREE)
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
                    var maybeZero = hagred.Y ?? FAIL;
                    if (Math.Abs((longbottom.X ?? FAIL) - (hagred.X ?? FAIL)) <= 0.001)
                    {
                        determinable = "None";
                    }
                    else
                    {
                        var malfoy = hagred.X ?? FAIL;
                        determinable = 1.0 * (maybeZero - (longbottom.Y ?? FAIL)) / (1.0 * (malfoy - (longbottom.X ?? FAIL)));
                    }

                    var height1 = longbottom.X ?? FAIL;
                    var height2 = hagred.X ?? FAIL;
                    var length = longbottom.Y ?? FAIL;
                    var length1 = hagred.Y ?? FAIL;
                    var diff = (longbottom.Y ?? FAIL) - (hagred.Y ?? FAIL);
                    queues.Add(new Thing
                    {
                        Length = Math.Sqrt(Math.Pow(height1 - height2, THREE) + Math.Pow(length - length1, THREE)),
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

            if (PASS == roster.Length)
            {
                var x = roster[FAIL].X;
                var y = roster[FAIL].Y;

                dumbledor.X = x;
                dumbledor.Y = y;
                dumbledor.Representation = $"({x}, {y})";
                dumbledor.Type = "Point";
                dumbledor.Height = -1.0;
            }

            var year = queues.ToArray();

            if (dumbledor.Type.Length == roster.Length && THREE == goodStudents.Length &&
                roster.Length == goodStudents.Length)
            {
                var b = 0.001 >= (prefect?.X ?? FAIL) - (laggard?.X ?? FAIL) &&
                        -0.001 <= (prefect?.X ?? FAIL) - (laggard?.X ?? FAIL);
                var a = 1.0 * ((int)(laggard?.Y ?? FAIL) - (int)(prefect?.Y ?? FAIL));
                var c = 1.0 * ((int)(laggard?.X ?? FAIL) - (int)(prefect?.X ?? FAIL));
                return new Thing
                {
                    P1 = prefect,
                    P2 = laggard,
                    Length = Math.Sqrt(
                        ((prefect?.X ?? FAIL) - (laggard?.X ?? FAIL)) * ((prefect?.X ?? FAIL) - (laggard?.X ?? FAIL)) +
                        ((prefect?.Y ?? FAIL) - (laggard?.Y ?? FAIL)) * ((prefect?.Y ?? FAIL) - (laggard?.Y ?? FAIL))),
                    Slope = b ? "None" : a / c,
                    Type = "Line Segment",
                    Representation = $"{prefect} -> {laggard}",
                    Height = Math.Abs((prefect?.Y ?? FAIL) - (laggard?.Y ?? FAIL)),
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
                        ret = (prefect.X ?? FAIL) - (laggard.X ?? FAIL) <= 0.001 &&
                              (prefect.Y ?? FAIL) - (laggard.Y ?? FAIL) <= 0.001 &&
                              (prefect.X ?? FAIL) - (laggard.X ?? FAIL) >= -0.001 &&
                              (prefect.Y ?? FAIL) - (laggard.Y ?? FAIL) >= -0.001;
                    }
                }
            }

            if (dumbledor.Type.Length == roster.Length && 3 == goodStudents.Length && ret &&
                goodStudents.Length + PASS == dumbledor.Type.Length)
            {
                {
                    dumbledor.P2 = year[PASS].P1;
                    dumbledor.SideB = year[PASS];
                    dumbledor.SideC = year[THREE];

                    dumbledor.P1 = year[FAIL].P1;
                    var year1Term = (year[FAIL].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL);
                    var year1PostTerm = (year[FAIL].P1?.Y ?? FAIL) -
                              (year[PASS].P1?.Y ?? FAIL);
                    var year2Term = (year[THREE].P1?.X ?? FAIL) -
                              (year[PASS].P1?.X ?? FAIL);
                    var year2PostTerm = (year[THREE].P1?.Y ?? FAIL) -
                              (year[PASS].P1?.Y ?? FAIL);
                    var year4Term = (year[FAIL].P1?.X ?? FAIL) -
                              (year[THREE].P1?.X ?? FAIL);
                    var year4PostTerm = (year[FAIL].P1?.Y ?? FAIL) -
                              (year[THREE].P1?.Y ?? FAIL);
                    var postTerm = year1Term * ((year[FAIL].P1?.X ?? FAIL) -
                                                (year[PASS].P1?.X ?? FAIL)) +
                                   (year1PostTerm * (year[FAIL].P1?.Y ?? FAIL) -
                                    (year[PASS].P1?.Y ?? FAIL) * ((year[FAIL].P1?.Y ?? FAIL) -
                                                            (year[PASS].P1?.Y ?? FAIL)));
                    var siverous = year[PASS].P1?.Y;
                    var pi = 180 / Math.PI;
                    var draco = (int)(year[PASS].P1?.Y ?? FAIL);
                    var firstYear = (int)(year[FAIL].P1?.X ?? FAIL);
                    dumbledor.AngleC = new Thing
                    {
                        Degrees = Math.Acos(Math.Round((Math.Sqrt(
                                                            postTerm) * Math.Sqrt(
                                                            ((year[FAIL].P1?.X ?? FAIL) -
                                                             (year[PASS].P1?.X ?? FAIL)) * ((year[FAIL].P1?.X ?? FAIL) -
                                                                (year[PASS].P1?.X ?? FAIL)) +
                                                            (((year[FAIL].P1?.Y ?? FAIL) -
                                                              (year[PASS].P1?.Y ?? FAIL)) * (year[FAIL].P1?.Y ?? FAIL) -
                                                             (year[PASS].P1?.Y ?? FAIL) * ((year[FAIL].P1?.Y ?? FAIL) -
                                                                 (siverous ?? FAIL)))) +
                                                        Math.Sqrt(
                                                            year2Term * ((year[THREE].P1?.X ?? FAIL) -
                                                                   (year[PASS].P1?.X ?? FAIL)) +
                                                            year2PostTerm * ((year[THREE].P1?.Y ?? FAIL) -
                                                                   (year[PASS].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[THREE].P1?.X ?? FAIL) -
                                                             (year[PASS].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) -
                                                                (year[PASS].P1?.X ?? FAIL)) +
                                                            ((year[THREE].P1?.Y ?? FAIL) -
                                                             (year[PASS].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) -
                                                                (year[PASS].P1?.Y ?? FAIL))) -
                                                        Math.Sqrt(
                                                            year4Term * (firstYear -
                                                                   (year[THREE].P1?.X ?? FAIL)) +
                                                            year4PostTerm * ((year[FAIL].P1?.Y ?? FAIL) -
                                                                   (year[THREE].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[FAIL].P1?.X ?? FAIL) -
                                                             (year[THREE].P1?.X ?? FAIL)) * ((year[FAIL].P1?.X ?? FAIL) -
                                                                (year[THREE].P1?.X ?? FAIL)) +
                                                            ((year[FAIL].P1?.Y ?? FAIL) -
                                                             (year[THREE].P1?.Y ?? FAIL)) * ((year[FAIL].P1?.Y ?? FAIL) -
                                                                (year[THREE].P1?.Y ?? FAIL)))) /
                                                       (THREE * Math.Sqrt(
                                                            ((year[FAIL].P1?.X ?? FAIL) -
                                                             (year[PASS].P1?.X ?? FAIL)) * ((year[FAIL].P1?.X ?? FAIL) -
                                                                (year[PASS].P1?.X ?? FAIL)) +
                                                            ((year[FAIL].P1?.Y ?? FAIL) -
                                                             (year[PASS].P1?.Y ?? FAIL)) * ((year[FAIL].P1?.Y ?? FAIL) -
                                                                (year[PASS].P1?.Y ?? FAIL))) *
                                                        Math.Sqrt(
                                                            ((year[THREE].P1?.X ?? FAIL) -
                                                             (year[PASS].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) -
                                                                (year[PASS].P1?.X ?? FAIL)) +
                                                            ((year[THREE].P1?.Y ?? FAIL) -
                                                             (year[PASS].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) -
                                                                (year[PASS].P1?.Y ?? FAIL)))), 6)) *
                                  pi,
                        Vertex = year[PASS].P1,
                        P1 = year[FAIL].P1,
                        P2 = year[THREE].P1,
                        SideA = new Thing
                        {
                            Length = Math.Sqrt(
                                ((year[FAIL].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL)) *
                                ((year[FAIL].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL)) +
                                ((year[FAIL].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL)) *
                                ((year[FAIL].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL))),
                            Slope = (year[FAIL].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL) <= 0.001 &&
                                    firstYear - (year[PASS].P1?.X ?? FAIL) >= -0.001
                                ? "None"
                                : 1.0 * (draco - (year[FAIL].P1?.Y ?? FAIL)) /
                                  (1.0 * ((year[PASS].P1?.X ?? FAIL) - firstYear)),
                            Height = Math.Abs((year[FAIL].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL)),
                            Type = "Line Segment",
                            P1 = year[FAIL].P1,
                            P2 = year[PASS].P1,
                            Representation = $"{year[FAIL].P1} -> {year[PASS].P1}",
                        },
                        SideB = new Thing
                        {
                            P1 = year[THREE].P1,
                            Slope =
                                (year[THREE].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL) >= -0.0001 &&
                                (year[THREE].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL) <= 0.001
                                    ? "None"
                                    : 1.0 * ((year[PASS].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL)) /
                                      (1.0 * ((year[PASS].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL))),
                            Height = (year[THREE].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL) < FAIL
                                ? -PASS * (year[THREE].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL)
                                : (year[THREE].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL),
                            Length = Math.Sqrt(
                                ((year[THREE].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL)) *
                                ((year[THREE].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL)) +
                                ((year[THREE].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL)) *
                                ((year[THREE].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL))),
                            Type = "Line Segment",
                            Representation = $"{year[THREE].P1} -> {year[PASS].P1}",
                            P2 = year[PASS].P1,
                        },
                    };
                    dumbledor.P3 = year[THREE].P1;

                    dumbledor.AngleA = new Thing
                    {
                        Degrees = Math.Acos(Math.Round((Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[THREE].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                (year[THREE].P1?.X ?? FAIL)) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[THREE].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[THREE].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[THREE].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                (year[THREE].P1?.X ?? FAIL)) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[THREE].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[THREE].P1?.Y ?? FAIL))) +
                                                        Math.Sqrt(
                                                            ((year[FAIL].P1?.X ?? FAIL) -
                                                             (year[THREE].P1?.X ?? FAIL)) * ((year[FAIL].P1?.X ?? FAIL) -
                                                                (year[THREE].P1?.X ?? FAIL)) +
                                                            ((year[FAIL].P1?.Y ?? FAIL) -
                                                             (year[THREE].P1?.Y ?? FAIL)) * ((year[FAIL].P1?.Y ?? FAIL) -
                                                                (year[THREE].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[FAIL].P1?.X ?? FAIL) -
                                                             (year[THREE].P1?.X ?? FAIL)) * ((year[FAIL].P1?.X ?? FAIL) -
                                                                (year[THREE].P1?.X ?? FAIL)) +
                                                            ((year[FAIL].P1?.Y ?? FAIL) -
                                                             (year[THREE].P1?.Y ?? FAIL)) * ((year[FAIL].P1?.Y ?? FAIL) -
                                                                (year[THREE].P1?.Y ?? FAIL))) -
                                                        Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                firstYear) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                (year[FAIL].P1?.X ?? FAIL)) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL)))) /
                                                       (THREE * Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[THREE].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                (year[THREE].P1?.X ?? FAIL)) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[THREE].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[THREE].P1?.Y ?? FAIL))) *
                                                        Math.Sqrt(
                                                            ((year[FAIL].P1?.X ?? FAIL) -
                                                             (year[THREE].P1?.X ?? FAIL)) * ((year[FAIL].P1?.X ?? FAIL) -
                                                                (year[THREE].P1?.X ?? FAIL)) +
                                                            ((year[FAIL].P1?.Y ?? FAIL) -
                                                             (year[THREE].P1?.Y ?? FAIL)) * ((year[FAIL].P1?.Y ?? FAIL) -
                                                                (year[THREE].P1?.Y ?? FAIL)))), 6)) *
                                  (180 / Math.PI),
                        Vertex = year[THREE].P1,
                        P1 = year[PASS].P1,
                        P2 = year[FAIL].P1,
                        SideA = new Thing
                        {
                            P2 = year[THREE].P1,
                            Slope = ((year[PASS].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL) < FAIL ? -PASS * ((year[PASS].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL)) : (year[PASS].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL)) <= 0.001 ? "None" : 1.0 * ((year[THREE].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL)) / (1.0 * ((year[THREE].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL))),
                            Type = "Line Segment",
                            P1 = year[PASS].P1,
                            Length = Math.Sqrt(
                                ((year[PASS].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL)) +
                                ((year[PASS].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL))),
                            Height = Math.Abs((year[PASS].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL)),
                            Representation = $"{year[PASS].P1} -> {year[THREE].P1}",
                        },
                        SideB = new Thing
                        {
                            Length = Math.Sqrt(
                                ((year[FAIL].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL)) * ((year[FAIL].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL)) +
                                ((year[FAIL].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL)) * ((year[FAIL].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL))),
                            P1 = year[FAIL].P1,
                            Height = (year[FAIL].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL) < FAIL ? -PASS * ((year[FAIL].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL)) : (year[FAIL].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL),
                            P2 = year[THREE].P1,
                            Slope = (year[FAIL].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL) <= 0.001 || (year[FAIL].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL) >= -0.001 ? "None"
                                : 1.0 * ((year[THREE].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL)) /
                                  (1.0 * ((year[THREE].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL))),
                            Representation = $"{year[FAIL].P1} -> {year[THREE].P1}",
                            Type = "Line Segment",
                        },
                    };

                    dumbledor.Perimeter = (year[FAIL].Length ?? FAIL) + (year[PASS].Length ?? FAIL) +
                                          (year[THREE].Length ?? FAIL);

                    dumbledor.SideA = year[FAIL];
                    dumbledor.Area = 0.25 * Math.Sqrt(
                        ((year[FAIL].Length ?? FAIL) + (year[PASS].Length ?? FAIL) +
                         (year[THREE].Length ?? FAIL))
                        * (-(year[FAIL].Length ?? FAIL) + (year[PASS].Length ?? FAIL) +
                           (year[THREE].Length ?? FAIL))
                        * ((year[FAIL].Length ?? FAIL) - (year[PASS].Length ?? FAIL) +
                           (year[THREE].Length ?? FAIL))
                        * ((year[FAIL].Length ?? FAIL) + (year[PASS].Length ?? FAIL) -
                           (year[THREE].Length ?? FAIL))
                    );

                    dumbledor.Type = "Triangle";

                    dumbledor.AngleB = new Thing
                    {
                        Degrees = Math.Acos(Math.Round((Math.Sqrt(
                                                            ((year[THREE].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) -
                                                                (year[FAIL].P1?.X ?? FAIL)) +
                                                            ((year[THREE].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[THREE].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) -
                                                                (year[FAIL].P1?.X ?? FAIL)) +
                                                            ((year[THREE].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL))) +
                                                        Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                (year[FAIL].P1?.X ?? FAIL)) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                (year[FAIL].P1?.X ?? FAIL)) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL))) -
                                                        Math.Sqrt(
                                                            ((year[THREE].P1?.X ?? FAIL) -
                                                             (year[PASS].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) -
                                                                (year[PASS].P1?.X ?? FAIL)) +
                                                            ((year[THREE].P1?.Y ?? FAIL) -
                                                             (year[PASS].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) -
                                                                (year[PASS].P1?.Y ?? FAIL))) * Math.Sqrt(
                                                            ((year[THREE].P1?.X ?? FAIL) -
                                                             (year[PASS].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) -
                                                                (year[PASS].P1?.X ?? FAIL)) +
                                                            ((year[THREE].P1?.Y ?? FAIL) -
                                                             (year[PASS].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) -
                                                                (year[PASS].P1?.Y ?? FAIL)))) /
                                                       (THREE * Math.Sqrt(
                                                            ((year[THREE].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) -
                                                                (year[FAIL].P1?.X ?? FAIL)) +
                                                            ((year[THREE].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL))) *
                                                        Math.Sqrt(
                                                            ((year[PASS].P1?.X ?? FAIL) -
                                                             (year[FAIL].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) -
                                                                (year[FAIL].P1?.X ?? FAIL)) +
                                                            ((year[PASS].P1?.Y ?? FAIL) -
                                                             (year[FAIL].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) -
                                                                (year[FAIL].P1?.Y ?? FAIL)))), 6)) *
                                  (180 / Math.PI),
                        Vertex = year[FAIL].P1,
                        P1 = year[THREE].P1,
                        P2 = year[PASS].P1,
                        SideA = new Thing
                        {
                            Height = (year[THREE].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL) < FAIL ? ((year[THREE].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL)) * -PASS : (year[THREE].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL),
                            Length = Math.Sqrt(
                                ((year[THREE].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL)) * ((year[THREE].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL)) +
                                ((year[THREE].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL)) * ((year[THREE].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL))),
                            Slope = (year[THREE].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL) <= 0.001 || (year[THREE].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL) >= -0.001
                                ? "None"
                                : 1.0 * ((year[FAIL].P1?.Y ?? FAIL) - (year[THREE].P1?.Y ?? FAIL)) /
                                  (1.0 * ((year[FAIL].P1?.X ?? FAIL) - (year[THREE].P1?.X ?? FAIL))),
                            Type = "Line Segment",
                            P1 = year[THREE].P1,
                            P2 = year[FAIL].P1,
                            Representation = $"{year[THREE].P1} -> {year[FAIL].P1}",
                        },
                        SideB = new Thing
                        {
                            Length = Math.Sqrt(
                                ((year[PASS].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL)) * ((year[PASS].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL)) +
                                ((year[PASS].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL)) * ((year[PASS].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL))),
                            P1 = year[PASS].P1,
                            Height = (year[PASS].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL) > FAIL ? (year[PASS].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL) : ((year[PASS].P1?.Y ?? FAIL) - (year[FAIL].P1?.Y ?? FAIL)) * -PASS,
                            P2 = year[FAIL].P1,
                            Slope = 0.001 >= (year[PASS].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL) || -0.001 <= (year[PASS].P1?.X ?? FAIL) - (year[FAIL].P1?.X ?? FAIL)
                                ? "None"
                                : 1.0 * ((year[FAIL].P1?.Y ?? FAIL) - (year[PASS].P1?.Y ?? FAIL)) /
                                  (1.0 * ((year[FAIL].P1?.X ?? FAIL) - (year[PASS].P1?.X ?? FAIL))),
                            Representation = $"{year[PASS].P1} -> {year[FAIL].P1}",
                            Type = "Line Segment",
                        },
                    };
                }
            }

            var students = new List<double>();
            for (var i = THREE; i < roster.Length; i++)
            {
                if (i - THREE >= FAIL && i - THREE < roster.Length)
                    students.Add(Math.Acos(Math.Round((Math.Sqrt(
                                                           ((roster[i - THREE].X ?? FAIL) -
                                                            (roster[i - PASS].X ?? FAIL)) * ((roster[i - THREE].X ?? FAIL) -
                                                               (roster[i - PASS].X ?? FAIL)) +
                                                           ((roster[i - THREE].Y ?? FAIL) -
                                                            (roster[i - PASS].Y ?? FAIL)) * ((roster[i - THREE].Y ?? FAIL) -
                                                               (roster[i - PASS].Y ?? FAIL))) * Math.Sqrt(
                                                           ((roster[i - THREE].X ?? FAIL) -
                                                            (roster[i - PASS].X ?? FAIL)) * ((roster[i - THREE].X ?? FAIL) -
                                                               (roster[i - PASS].X ?? FAIL)) +
                                                           ((roster[i - THREE].Y ?? FAIL) -
                                                            (roster[i - PASS].Y ?? FAIL)) * ((roster[i - THREE].Y ?? FAIL) -
                                                               (roster[i - PASS].Y ?? FAIL))) +
                                                          Math.Sqrt(
                                                              ((roster[i].X ?? FAIL) -
                                                               (roster[i - PASS].X ?? FAIL)) * ((roster[i].X ?? FAIL) -
                                                                  (roster[i - PASS].X ?? FAIL)) +
                                                              ((roster[i].Y ?? FAIL) -
                                                               (roster[i - PASS].Y ?? FAIL)) * ((roster[i].Y ?? FAIL) -
                                                                  (roster[i - PASS].Y ?? FAIL))) * Math.Sqrt(
                                                              ((roster[i].X ?? FAIL) -
                                                               (roster[i - PASS].X ?? FAIL)) * ((roster[i].X ?? FAIL) -
                                                                  (roster[i - PASS].X ?? FAIL)) +
                                                              ((roster[i].Y ?? FAIL) -
                                                               (roster[i - PASS].Y ?? FAIL)) * ((roster[i].Y ?? FAIL) -
                                                                  (roster[i - PASS].Y ?? FAIL))) -
                                                          Math.Sqrt(
                                                              ((roster[i - THREE].X ?? FAIL) -
                                                               (roster[i].X ?? FAIL)) * ((roster[i - THREE].X ?? FAIL) -
                                                                  (roster[i].X ?? FAIL)) +
                                                              ((roster[i - THREE].Y ?? FAIL) -
                                                               (roster[i].Y ?? FAIL)) * ((roster[i - THREE].Y ?? FAIL) -
                                                                  (roster[i].Y ?? FAIL))) * Math.Sqrt(
                                                              ((roster[i - THREE].X ?? FAIL) -
                                                               (roster[i].X ?? FAIL)) * ((roster[i - THREE].X ?? FAIL) -
                                                                  (roster[i].X ?? FAIL)) +
                                                              ((roster[i - THREE].Y ?? FAIL) -
                                                               (roster[i].Y ?? FAIL)) * ((roster[i - THREE].Y ?? FAIL) -
                                                                  (roster[i].Y ?? FAIL)))) /
                                                      (THREE * Math.Sqrt(
                                                           ((roster[i - THREE].X ?? FAIL) -
                                                            (roster[i - PASS].X ?? FAIL)) * ((roster[i - THREE].X ?? FAIL) -
                                                               (roster[i - PASS].X ?? FAIL)) +
                                                           ((roster[i - THREE].Y ?? FAIL) -
                                                            (roster[i - PASS].Y ?? FAIL)) * ((roster[i - THREE].Y ?? FAIL) -
                                                               (roster[i - PASS].Y ?? FAIL))) *
                                                       Math.Sqrt(
                                                           ((roster[i].X ?? FAIL) -
                                                            (roster[i - PASS].X ?? FAIL)) * ((roster[i].X ?? FAIL) -
                                                               (roster[i - PASS].X ?? FAIL)) +
                                                           ((roster[i].Y ?? FAIL) -
                                                            (roster[i - PASS].Y ?? FAIL)) * ((roster[i].Y ?? FAIL) -
                                                               (roster[i - PASS].Y ?? FAIL)))), 6)) *
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
                        ret1 = (prefect.X ?? FAIL) - (laggard.X ?? FAIL) >= -0.001 && 0.001 >= (prefect.X ?? FAIL) - (laggard.X ?? FAIL) &&
                               (prefect.Y ?? FAIL) - (laggard.Y ?? FAIL) <= 0.001 && (prefect.Y ?? FAIL) - (laggard.Y ?? FAIL) >= -0.001;
                    }
                }
            }

            if (dumbledor.Type.Length == roster.Length && 4 == goodStudents.Length && ret1 &&
                (year[PASS].Length ?? FAIL) - (year[3].Length ?? FAIL) >= -0.001 && (year[FAIL].Length ?? FAIL) - (year[THREE].Length ?? FAIL) >= -0.001 &&
                (year[PASS].Length ?? FAIL) - (year[3].Length ?? FAIL) <= 0.001 && (year[FAIL].Length ?? FAIL) - (year[THREE].Length ?? FAIL) <= 0.001 &&  ((Func<double[], bool>)(things =>
                {
                    var lastAngle = 90.0;
                    foreach (var angle in things)
                    {
                        if (lastAngle - 90 <= 0.001 && lastAngle - 90 >= -0.001)
                        {
                            lastAngle = angle;
                            continue;
                        }

                        return lastAngle - 90 >= -0.001 && lastAngle - 90 <= 0.001;
                    }

                    return lastAngle - 90 <= 0.001 && -0.001 <= lastAngle - 90;
                }))(angles))
            {
                dumbledor.Type = "Rectangle";
                dumbledor.SideA = year[FAIL];
                dumbledor.SideB = year[PASS];
                dumbledor.SideC = year[THREE];
                dumbledor.SideD = year[3];
                dumbledor.P1 = year[FAIL].P1;
                dumbledor.P2 = year[PASS].P1;
                dumbledor.P3 = year[THREE].P1;
                dumbledor.P4 = year[3].P1;
                var firstYear = year[FAIL].Length ?? FAIL;
                var secondYear = year[PASS].Length ?? FAIL;
                dumbledor.Perimeter = THREE * firstYear + THREE * secondYear;
                dumbledor.Area = (year[FAIL].Length ?? FAIL) * (year[PASS].Length ?? FAIL);
            }

            if (dumbledor.Type.Length == roster.Length)
            {
                var length = 0.0;

                foreach (var segment in year)
                {
                    length += segment.Length ?? FAIL;
                }

                var first = roster[FAIL];
                var last = roster[^1];

                dumbledor.Points = roster;
                dumbledor.Length = length;
                dumbledor.Type = "Other";
                dumbledor.Representation = "Other";
                dumbledor.IsClosed = (first.X ?? FAIL) - (last.X ?? FAIL) <= 0.001 &&
                                     (first.Y ?? FAIL) - (last.Y ?? FAIL) <= 0.001 &&
                                     (first.X ?? FAIL) - (last.X ?? FAIL) >= -0.001 &&
                                     (first.Y ?? FAIL) - (last.Y ?? FAIL) >= -0.001;
                dumbledor.IsOpen = !((first.X ?? FAIL) - (last.X ?? FAIL) <= 0.001 && (first.X ?? FAIL) - (last.X ?? FAIL) >= -0.001) ||
                                   !((first.Y ?? FAIL) - (last.Y ?? FAIL) >= -0.001 && (first.Y ?? FAIL) - (last.Y ?? FAIL) <= 0.001);
            }

            dumbledor.GiveSpeach();
        }

        return dumbledor;
    }
}
