using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shape.Lib;
using Shape.Lib.Types;

namespace Shape.Tests
{
    [TestClass]
    public class LineSegmentShould
    {
        [TestMethod]
        public void ContainThePointsThatCreatedIt()
        {
            var p1 = Builder.Build(0, 0);
            var p2 = Builder.Build(50, 0);

            var result = Classifier.Classify(new[] { p1, p2 });

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;
            Assert.AreEqual(p1, segment.P1);
            Assert.AreEqual(p2, segment.P2);
        }

        [TestMethod]
        public void ContainDifferentPointsThatCreatedIt()
        {
            var p1 = Builder.Build(25, -3);
            var p2 = Builder.Build(-20, 11);

            var result = Classifier.Classify(new[] { p1, p2 });

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;
            Assert.AreEqual(p1, segment.P1);
            Assert.AreEqual(p2, segment.P2);
        }

        [TestMethod]
        public void ContainLengthOfFiveFor0_0And0_5()
        {
            var points = Builder.Build(
                (0, 0),
                (0, 5)
            );

            var result = Classifier.Classify(points);

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;

            Assert.AreEqual(5, segment.Length, 0.001);
        }

        [TestMethod]
        public void ContainLengthOf50For0_0And50_0()
        {
            var points = Builder.Build(
                (0, 0),
                (50, 0)
            );

            var result = Classifier.Classify(points);

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;

            Assert.AreEqual(50, segment.Length, 0.001);
        }

        [TestMethod]
        public void ContainLengthOf5For1_4And5_7()
        {
            var p1 = Builder.Build(1, 4);
            var p2 = Builder.Build(5, 7);

            var result = Classifier.Classify(new[] { p1, p2 });

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;
            Assert.AreEqual(5, segment.Length, 0.001);
        }

        [TestMethod]
        public void HaveASlopeOfZeroWhenBothPointsHaveSameY()
        {
            const int y = 4;
            var points = Builder.Build(
                (1, y),
                (5, y)
            );

            var result = Classifier.Classify(points);

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;
            Assert.IsTrue(segment.Slope.IsSome);
            Assert.AreEqual(0, segment.Slope.Value, 0.001);
        }

        [TestMethod]
        public void HaveSlopeOfOneWhenXAndYChangeBySameAmount()
        {
            const int change = 4;
            var p1 = Builder.Build(1, 4);
            var p2 = Builder.Build(p1.X.GetValueOrDefault() + change, p1.Y.GetValueOrDefault() + change);

            var result = Classifier.Classify(new[] { p1, p2 });

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;
            Assert.IsTrue(segment.Slope.IsSome);
            Assert.AreEqual(1, segment.Slope.Value, 0.001);
        }

        [TestMethod]
        public void HaveSlopeNegFour()
        {
            var points = Builder.Build(
                (0, 0),
                (-1, 4)
            );

            var result = Classifier.Classify(points);

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;
            Assert.IsTrue(segment.Slope.IsSome);
            Assert.AreEqual(-4, segment.Slope.Value, 0.001);
        }

        [TestMethod]
        public void NotHaveSlopeIfBothPointsHaveSameX()
        {
            const int x = 3;
            var points = Builder.Build(
                (x, 0),
                (x, 4)
            );

            var result = Classifier.Classify(points);

            Assert.AreEqual(result.Type, "Line Segment");

            var segment = (LineSegment)result;
            Assert.IsFalse(segment.Slope.IsSome);
            Assert.IsTrue(segment.Slope.IsNone);
        }

        [TestMethod]
        public void HaveToString()
        {
            var points = Builder.Build((45, -52), (-20, -20));
            var segment = Classifier.Classify(points);

            Assert.AreEqual(segment.ToString(), "(45, -52) -> (-20, -20)");
        }
    }
}
