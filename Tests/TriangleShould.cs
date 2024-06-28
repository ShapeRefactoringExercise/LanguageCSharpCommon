using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shape.Lib;
using Shape.Lib.Types;

namespace Shape.Tests
{
    [TestClass]
    public class TriangleShould
    {
        private static Triangle GetTriangle ((double, double) c1, (double, double) c2, (double, double) c3)
        {
            var points = Builder.Build(new[] { c1, c2, c3, c1 });
            var shape = Classifier.Classify(points);

            Assert.AreEqual(shape.Type, "Triangle");
            return (Triangle)shape;
        }

        private static Triangle GetTriangle(AllShape p1, AllShape p2, AllShape p3)
        {
            var shape = Classifier.Classify(new[] { p1, p2, p3, p1 });

            Assert.AreEqual(shape.Type, "Triangle");
            return (Triangle)shape;
        }

        [TestMethod]
        public void ContainTheDistinctPointsThatConstructedIt()
        {
            var p1 = Builder.Build(0, 0);
            var p2 = Builder.Build(0, 5);
            var p3 = Builder.Build(3, 5);

            var result = GetTriangle(p1, p2, p3);

            Assert.AreEqual(p1, result.P1);
            Assert.AreEqual(p2, result.P2);
            Assert.AreEqual(p3, result.P3);
        }

        [TestMethod]
        public void ContainDifferentPointsItsConstructedWith()
        {
            var p1 = Builder.Build(60, 90);
            var p2 = Builder.Build(-1, 5);
            var p3 = Builder.Build(-50, -5);

            var result = GetTriangle(p1, p2, p3);

            Assert.AreEqual(p1, result.P1);
            Assert.AreEqual(p2, result.P2);
            Assert.AreEqual(p3, result.P3);
        }

        [TestMethod]
        public void HaveLegLengthsOfThreeFourFive()
        {
            var result = GetTriangle(
                (0, 0),
                (0, 3),
                (4, 3)
            );

            Assert.AreEqual("Line Segment", result.SideA.Type);
            Assert.AreEqual("Line Segment", result.SideB.Type);
            Assert.AreEqual("Line Segment", result.SideC.Type);

            Assert.AreEqual(result.P1.X.GetValueOrDefault(), result.SideA.P1.X.GetValueOrDefault(), 0.001, "SideA.P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P1.Y.GetValueOrDefault(), result.SideA.P1.Y.GetValueOrDefault(), 0.001, "SideA.P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P2.X.GetValueOrDefault(), result.SideA.P2.X.GetValueOrDefault(), 0.001, "SideA.P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P2.Y.GetValueOrDefault(), result.SideA.P2.Y.GetValueOrDefault(), 0.001, "SideA.P2.Y.GetValueOrDefault()");

            Assert.AreEqual(result.P2.X.GetValueOrDefault(), result.SideB.P1.X.GetValueOrDefault(), 0.001, "SideB.P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P2.Y.GetValueOrDefault(), result.SideB.P1.Y.GetValueOrDefault(), 0.001, "SideB.P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P3.X.GetValueOrDefault(), result.SideB.P2.X.GetValueOrDefault(), 0.001, "SideB.P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P3.Y.GetValueOrDefault(), result.SideB.P2.Y.GetValueOrDefault(), 0.001, "SideB.P2.Y.GetValueOrDefault()");

            Assert.AreEqual(result.P3.X.GetValueOrDefault(), result.SideC.P1.X.GetValueOrDefault(), 0.001, "SideC.P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P3.Y.GetValueOrDefault(), result.SideC.P1.Y.GetValueOrDefault(), 0.001, "SideC.P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P1.X.GetValueOrDefault(), result.SideC.P2.X.GetValueOrDefault(), 0.001, "SideC.P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P1.Y.GetValueOrDefault(), result.SideC.P2.Y.GetValueOrDefault(), 0.001, "SideC.P2.Y.GetValueOrDefault()");

            Assert.AreEqual(3, result.SideA.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(4, result.SideB.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(5, result.SideC.Length.GetValueOrDefault(), 0.001);

            Assert.IsFalse(result.SideA.Slope.IsSome, "SideA.Slope.IsSome");

            Assert.IsTrue(result.SideB.Slope.IsSome, "SideB.Slope.IsSome");
            Assert.AreEqual(0, result.SideB.Slope.Value, 0.001);

            Assert.IsTrue(result.SideC.Slope.IsSome, "SideC.Slope.IsSome");
            Assert.AreEqual(0.75, result.SideC.Slope.Value, 0.001);
        }

        [TestMethod]
        public void HaveLegLengthsOfTwelveSixteenTwenty()
        {
            var result = GetTriangle(
                (0, 0),
                (0, 12),
                (16, 12)
            );

            Assert.AreEqual("Line Segment", result.SideA.Type);
            Assert.AreEqual("Line Segment", result.SideB.Type);
            Assert.AreEqual("Line Segment", result.SideC.Type);

            Assert.AreEqual(result.P1.X.GetValueOrDefault(), result.SideA.P1.X.GetValueOrDefault(), 0.001, "SideA.P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P1.Y.GetValueOrDefault(), result.SideA.P1.Y.GetValueOrDefault(), 0.001, "SideA.P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P2.X.GetValueOrDefault(), result.SideA.P2.X.GetValueOrDefault(), 0.001, "SideA.P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P2.Y.GetValueOrDefault(), result.SideA.P2.Y.GetValueOrDefault(), 0.001, "SideA.P2.Y.GetValueOrDefault()");

            Assert.AreEqual(result.P2.X.GetValueOrDefault(), result.SideB.P1.X.GetValueOrDefault(), 0.001, "SideB.P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P2.Y.GetValueOrDefault(), result.SideB.P1.Y.GetValueOrDefault(), 0.001, "SideB.P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P3.X.GetValueOrDefault(), result.SideB.P2.X.GetValueOrDefault(), 0.001, "SideB.P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P3.Y.GetValueOrDefault(), result.SideB.P2.Y.GetValueOrDefault(), 0.001, "SideB.P2.X.GetValueOrDefault()");

            Assert.AreEqual(result.P3.X.GetValueOrDefault(), result.SideC.P1.X.GetValueOrDefault(), 0.001, "SideC.P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P3.Y.GetValueOrDefault(), result.SideC.P1.Y.GetValueOrDefault(), 0.001, "SideC.P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P1.X.GetValueOrDefault(), result.SideC.P2.X.GetValueOrDefault(), 0.001, "SideC.P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P1.Y.GetValueOrDefault(), result.SideC.P2.Y.GetValueOrDefault(), 0.001, "SideC.P2.X.GetValueOrDefault()");

            Assert.AreEqual(12, result.SideA.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(16, result.SideB.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(20, result.SideC.Length.GetValueOrDefault(), 0.001);

            Assert.IsFalse(result.SideA.Slope.IsSome, "SideA.Slope.IsSome");

            Assert.IsTrue(result.SideB.Slope.IsSome, "SideA.Slope.IsSome");
            Assert.AreEqual(0, result.SideB.Slope.Value, 0.001);

            Assert.IsTrue(result.SideC.Slope.IsSome, "SideC.Slope.IsSome");
            Assert.AreEqual(0.75, result.SideC.Slope.Value, 0.001);
        }

        [TestMethod]
        public void HaveAnglesRightTriangleOfThreeFourFive()
        {
            var result = GetTriangle(
                (0, 0),
                (0, 3),
                (4, 3)
            );

            Assert.AreEqual(result.P2.X.GetValueOrDefault(), result.AngleA.P1.X.GetValueOrDefault(), 0.001, "AngleA P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P2.Y.GetValueOrDefault(), result.AngleA.P1.Y.GetValueOrDefault(), 0.001, "AngleA P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P3.X.GetValueOrDefault(), result.AngleA.Vertex.X.GetValueOrDefault(), 0.001, "AngleA Vertex.X.GetValueOrDefault()");
            Assert.AreEqual(result.P3.Y.GetValueOrDefault(), result.AngleA.Vertex.Y.GetValueOrDefault(), 0.001, "AngleA Vertex.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P1.X.GetValueOrDefault(), result.AngleA.P2.X.GetValueOrDefault(), 0.001, "AngleA P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P1.Y.GetValueOrDefault(), result.AngleA.P2.Y.GetValueOrDefault(), 0.001, "AngleA P2.Y.GetValueOrDefault()");
            Assert.AreEqual(36.87, result.AngleA.Degrees.GetValueOrDefault(), 0.001);

            Assert.AreEqual(result.SideB.P1.X.GetValueOrDefault(), result.AngleA.SideA.P1.X.GetValueOrDefault(), 0.001, "AngleA SideA P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.P1.Y.GetValueOrDefault(), result.AngleA.SideA.P1.Y.GetValueOrDefault(), 0.001, "AngleA SideA P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.P2.X.GetValueOrDefault(), result.AngleA.SideA.P2.X.GetValueOrDefault(), 0.001, "AngleA SideA P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.P2.Y.GetValueOrDefault(), result.AngleA.SideA.P2.Y.GetValueOrDefault(), 0.001, "AngleA SideA P2.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.Length, result.AngleA.SideA.Length, "AngleA SideA Length");

            Assert.AreEqual(result.SideC.P2.X.GetValueOrDefault(), result.AngleA.SideB.P1.X.GetValueOrDefault(), 0.001, "AngleA SideB P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.P2.Y.GetValueOrDefault(), result.AngleA.SideB.P1.Y.GetValueOrDefault(), 0.001, "AngleA SideB P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.P1.X.GetValueOrDefault(), result.AngleA.SideB.P2.X.GetValueOrDefault(), 0.001, "AngleA SideB P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.P1.Y.GetValueOrDefault(), result.AngleA.SideB.P2.Y.GetValueOrDefault(), 0.001, "AngleA SideB P2.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.Length, result.AngleA.SideB.Length, "AngleA SideB Length");

            Assert.AreEqual(result.P3.X.GetValueOrDefault(), result.AngleB.P1.X.GetValueOrDefault(), 0.001, "AngleB P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P3.Y.GetValueOrDefault(), result.AngleB.P1.Y.GetValueOrDefault(), 0.001, "AngleB P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P1.X.GetValueOrDefault(), result.AngleB.Vertex.X.GetValueOrDefault(), 0.001, "AngeleB Vertex.X.GetValueOrDefault()");
            Assert.AreEqual(result.P1.Y.GetValueOrDefault(), result.AngleB.Vertex.Y.GetValueOrDefault(), 0.001, "AngeleB Vertex.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P2.X.GetValueOrDefault(), result.AngleB.P2.X.GetValueOrDefault(), 0.001, "AngleB P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P2.Y.GetValueOrDefault(), result.AngleB.P2.Y.GetValueOrDefault(), 0.001, "AngleB P2.Y.GetValueOrDefault()");
            Assert.AreEqual(53.13, result.AngleB.Degrees.GetValueOrDefault(), 0.001);

            Assert.AreEqual(result.SideC.P1.X.GetValueOrDefault(), result.AngleB.SideA.P1.X.GetValueOrDefault(), 0.001, "AngleB SideA P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.P1.Y.GetValueOrDefault(), result.AngleB.SideA.P1.Y.GetValueOrDefault(), 0.001, "AngleB SideA P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.P2.X.GetValueOrDefault(), result.AngleB.SideA.P2.X.GetValueOrDefault(), 0.001, "AngleB SideA P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.P2.Y.GetValueOrDefault(), result.AngleB.SideA.P2.Y.GetValueOrDefault(), 0.001, "AngleB SideA P2.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideC.Length, result.AngleB.SideA.Length, "AngleA SideA Length");

            Assert.AreEqual(result.SideA.P2.X.GetValueOrDefault(), result.AngleB.SideB.P1.X.GetValueOrDefault(), 0.001, "AngleB SideA P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.P2.Y.GetValueOrDefault(), result.AngleB.SideB.P1.Y.GetValueOrDefault(), 0.001, "AngleB SideA P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.P1.X.GetValueOrDefault(), result.AngleB.SideB.P2.X.GetValueOrDefault(), 0.001, "AngleB SideA P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.P1.Y.GetValueOrDefault(), result.AngleB.SideB.P2.Y.GetValueOrDefault(), 0.001, "AngleB SideA P2.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.Length, result.AngleB.SideB.Length, "AngleA SideA Length");

            Assert.AreEqual(result.P1.X.GetValueOrDefault(), result.AngleC.P1.X.GetValueOrDefault(), 0.001, "AngleC P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.P1.Y.GetValueOrDefault(), result.AngleC.P1.Y.GetValueOrDefault(), 0.001, "AngleC P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P2.Y.GetValueOrDefault(), result.AngleC.Vertex.Y.GetValueOrDefault(), 0.001, "AngleC Vertex.Y.GetValueOrDefault()");
            Assert.AreEqual(result.P3.X.GetValueOrDefault(), result.AngleC.P2.X.GetValueOrDefault(), 0.001, "AngleC P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.P3.Y.GetValueOrDefault(), result.AngleC.P2.Y.GetValueOrDefault(), 0.001, "AngleC P2.Y.GetValueOrDefault()");
            Assert.AreEqual(90, result.AngleC.Degrees.GetValueOrDefault(), 0.001);

            Assert.AreEqual(result.SideA.P1.X.GetValueOrDefault(), result.AngleC.SideA.P1.X.GetValueOrDefault(), 0.001, "AngleC SideA P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.P1.Y.GetValueOrDefault(), result.AngleC.SideA.P1.Y.GetValueOrDefault(), 0.001, "AngleC SideA P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.P2.X.GetValueOrDefault(), result.AngleC.SideA.P2.X.GetValueOrDefault(), 0.001, "AngleC SideA P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.P2.Y.GetValueOrDefault(), result.AngleC.SideA.P2.Y.GetValueOrDefault(), 0.001, "AngleC SideA P2.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideA.Length, result.AngleC.SideA.Length, "AngleC SideA Length");

            Assert.AreEqual(result.SideB.P2.X.GetValueOrDefault(), result.AngleC.SideB.P1.X.GetValueOrDefault(), 0.001, "AngleC SideB P1.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.P2.Y.GetValueOrDefault(), result.AngleC.SideB.P1.Y.GetValueOrDefault(), 0.001, "AngleC SideB P1.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.P1.X.GetValueOrDefault(), result.AngleC.SideB.P2.X.GetValueOrDefault(), 0.001, "AngleC SideB P2.X.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.P1.Y.GetValueOrDefault(), result.AngleC.SideB.P2.Y.GetValueOrDefault(), 0.001, "AngleC SideB P2.Y.GetValueOrDefault()");
            Assert.AreEqual(result.SideB.Length, result.AngleC.SideB.Length, "AngleA SideA Length");
        }

        [TestMethod]
        public void CorrectlyHandleThirtySixtyNinetyTriangle()
        {
            const double a = 3.0;
            var b = a * Math.Sqrt(3);


            var result = GetTriangle(
                (a, 0),
                (0, 0),
                (0, b)
            );

            Assert.AreEqual(a, result.SideA.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(b, result.SideB.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(2 * a, result.SideC.Length.GetValueOrDefault(), 0.001);

            Assert.IsTrue(result.SideA.Slope.IsSome, "SideA.Slope.IsSome");
            Assert.AreEqual(0, result.SideA.Slope.Value, 0.001);

            Assert.IsFalse(result.SideB.Slope.IsSome, "SideB.Slope.IsSome");

            Assert.IsTrue(result.SideC.Slope.IsSome, "SideC.Slope.IsSome");
            Assert.AreEqual(-1.732, result.SideC.Slope.Value, 0.001);

            Assert.AreEqual(30, result.AngleA.Degrees.GetValueOrDefault(), 0.001);
            Assert.AreEqual(60, result.AngleB.Degrees.GetValueOrDefault(), 0.001);
            Assert.AreEqual(90, result.AngleC.Degrees.GetValueOrDefault(), 0.001);

            Assert.AreEqual(7.794, result.Area, 0.001);
        }

        [TestMethod]
        public void CorrectlyHandleFirstAndLastPointOnSameX()
        {
            var result = GetTriangle(
                (1, 0),
                (0, 0),
                (1, 2)
            );

            Assert.AreEqual(1, result.SideA.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(2.236, result.SideB.Length.GetValueOrDefault(), 0.001);
            Assert.AreEqual(2, result.SideC.Length.GetValueOrDefault(), 0.001);

            Assert.IsTrue(result.SideA.Slope.IsSome, "SideA.Slope.IsSome");
            Assert.AreEqual(0, result.SideA.Slope.Value, 0.001);

            Assert.IsTrue(result.SideB.Slope.IsSome, "SideB.Slope.IsSome");
            Assert.AreEqual(2, result.SideB.Slope.Value, 0.001);

            Assert.IsFalse(result.SideC.Slope.IsSome, "SideC.Slope.IsSome");

            Assert.AreEqual(26.565, result.AngleA.Degrees.GetValueOrDefault(), 0.001);
            Assert.AreEqual(90, result.AngleB.Degrees.GetValueOrDefault(), 0.001);
            Assert.AreEqual(63.434, result.AngleC.Degrees.GetValueOrDefault(), 0.001);

            Assert.AreEqual(1, result.Area, 0.001);
        }

        [TestMethod]
        public void CorrectlyHandleFortyFiveFortyFiveNinetyTriangle()
        {
            const double change = 3;
            const double x = 0;
            const double y = 0;
            var result = GetTriangle(
                (x, y),
                (x + change, y),
                (x + change, change + y)
            );

            Assert.AreEqual(45, result.AngleA.Degrees.GetValueOrDefault(), 0.001);
            Assert.AreEqual(45, result.AngleB.Degrees.GetValueOrDefault(), 0.001);
            Assert.AreEqual(90, result.AngleC.Degrees.GetValueOrDefault(), 0.001);
            Assert.AreEqual(4.5, result.Area, 0.001);
        }

        [TestMethod]
        public void CalculateTheAreaOfEquilateralTriangle()
        {
            const double a = 16;
            var p1 = Builder.Build(0, 0);
            var p2 = Builder.Build(a, 0);
            var p3 = Builder.Build(a / 2.0, (Math.Sqrt(3) * a) / 2.0);

            var result = GetTriangle(p1, p2, p3);

            Assert.AreEqual(110.851, result.Area, 0.001);
        }

        [TestMethod]
        public void CalculateAreaOfThreeFourFiveTriangle()
        {
            var result = GetTriangle(
                (0, 0),
                (0, 3),
                (4, 3)
            );

            Assert.AreEqual(6, result.Area, 0.001);
        }

        [TestMethod]
        public void CalculateThePerimeterOfAnEquilateralTriangle()
        {
            const double a = 16;
            var result = GetTriangle(
                (0, 0),
                (a, 0),
                (a / 2.0, (Math.Sqrt(3) * a) / 2.0)
            );

            Assert.AreEqual(a * 3, result.Perimeter, 0.001);
        }

        [TestMethod]
        public void CalculateThePerimeterOfThreeFourFiveTriangle()
        {
            var result = GetTriangle(
                (0, 0),
                (3, 0),
                (3, 4)
            );

            Assert.AreEqual(12, result.Perimeter, 0.001);
        }
    }
}
