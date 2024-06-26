using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shape.Lib;
using Shape.Lib.Types;

namespace Shape.Tests
{
    [TestClass]
    public class RectangleShould
    {
        private static (Point[] points, Rectangle result) GetRectangle(double x, double y, double height, double length)
        {
            var points = Builder.Build(
                (x, y),
                (x + height, y),
                (x + height, y + length),
                (x, y + length),
                (x, y)
            );

            var shape = Classifier.Classify(points);
            Assert.AreEqual(shape.Type, "Rectangle");

            var result = (Rectangle)shape;
            return (points, result);
        }

        private static void CheckRectangle(Random random, Action<Rectangle, double, double, double, double, IReadOnlyList<Point>> check)
        {
            var length = random.NextDouble() * 100;
            var height = random.NextDouble() * 100;
            var x = random.NextDouble() * 10;
            var y = random.NextDouble() * 10;

            var (points, result) = GetRectangle(x, y, height, length);

            check(result, x, y, height, length, points);
        }

        [TestMethod]
        public void ContainTheDistinctPointsThatCreatedIt()
        {
            var (points, result) = GetRectangle(0, 0, 3, 4);

            Assert.AreEqual(points[0], result.P1);
            Assert.AreEqual(points[1], result.P2);
            Assert.AreEqual(points[2], result.P3);
            Assert.AreEqual(points[3], result.P4);
        }

        [TestMethod]
        public void ContainLineSegments()
        {
            var random = new Random();

            void Check(Rectangle result, double x, double y, double height, double length, IReadOnlyList<Point> points)
            {
                Assert.AreEqual("Line Segment", result.SideA.Type, $"A x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[0].X, result.SideA.P1.X, 0.001, $"A.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[0].Y, result.SideA.P1.Y, 0.001, $"A.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[1].X, result.SideA.P2.X, 0.001, $"A.P2 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[1].Y, result.SideA.P2.Y, 0.001, $"A.P2 x: {x}, y:{y}, height:{height}, length: {length}");

                Assert.AreEqual("Line Segment", result.SideB.Type, $"B x: {x}, y:{y}, height:{height}, length: {length}");

                Assert.AreEqual(points[1].X, result.SideB.P1.X, 0.001, $"B.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[1].Y, result.SideB.P1.Y, 0.001, $"B.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[2].X, result.SideB.P2.X, 0.001, $"B.P2 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[2].Y, result.SideB.P2.Y, 0.001, $"B.P2 x: {x}, y:{y}, height:{height}, length: {length}");

                Assert.AreEqual("Line Segment", result.SideC.Type, $"C x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[2].X, result.SideC.P1.X, 0.001, $"C.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[2].Y, result.SideC.P1.Y, 0.001, $"C.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[3].X, result.SideC.P2.X, 0.001, $"C.P2 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[3].Y, result.SideC.P2.Y, 0.001, $"C.P2 x: {x}, y:{y}, height:{height}, length: {length}");

                Assert.AreEqual("Line Segment", result.SideD.Type, $"D x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[3].X, result.SideD.P1.X, 0.01, $"D.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[3].Y, result.SideD.P1.Y, 0.01, $"D.P1 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[0].X, result.SideD.P2.X, 0.001, $"D.P2 x: {x}, y:{y}, height:{height}, length: {length}");
                Assert.AreEqual(points[0].Y, result.SideD.P2.Y, 0.001, $"D.P2 x: {x}, y:{y}, height:{height}, length: {length}");
            }

            for (var i = 0; i < 100; i++)
            {
                CheckRectangle(random, Check);
            }
        }

        [TestMethod]
        public void CalculatesArea()
        {
            var random = new Random();

            void Check(Rectangle result, double x, double y, double height, double length, IReadOnlyList<Point> points)
            {
                Assert.AreEqual(height * length, result.Area, 0.001, $"l: {length}, h: {height}, x:{x}, y: {y}");
            }

            for (var i = 0; i < 100; i++)
            {
                CheckRectangle(random, Check);
            }
        }

        [TestMethod]
        public void CalculatePerimeter()
        {
            var random = new Random();

            void Check(Rectangle result, double x, double y, double height, double length, IReadOnlyList<Point> points)
            {
                Assert.AreEqual(2 * (height + length), result.Perimeter, 0.001, $"l: {length}, h: {height}, x:{x}, y: {y}");
            }

            for (var i = 0; i < 100; i++)
            {
                CheckRectangle(random, Check);
            }
        }
    }
}
