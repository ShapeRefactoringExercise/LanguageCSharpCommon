using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shape.Lib;

namespace Shape.Tests
{
    [TestClass]
    public class ClassifyRectangleShould
    {
        [TestMethod]
        public void ClassifyFivePointsWhereFirstFourAreDistinctAndLastOneMatchesFirstAndAllAnglesAreRightAsRectangle()
        {
            var points = Builder.Build(
                (0, 0),
                (0, 4),
                (3, 4),
                (3, 0),
                (0, 0)
            );

            var result = Classifier.Classify(points);
            Assert.AreEqual("Rectangle", result.Type);
        }

        [TestMethod]
        public void ClassifyFiveDistinctPointsAsOther()
        {
            var points = Builder.Build(
                (0, 0),
                (0, 4),
                (3, 4),
                (3, 0),
                (0, 1)
            );

            var results = Classifier.Classify(points);
            Assert.AreEqual("Other", results.Type);
        }

        [TestMethod]
        public void ClassifyFivePointsWhereFirstFourHaveRepeatsAsOther()
        {
            var points = Builder.Build(
                (0, 0),
                (0, 3),
                (0, 0),
                (3, 3),
                (0, 0)
            );

            var result = Classifier.Classify(points);
            Assert.AreEqual("Other", result.Type);
        }

        [TestMethod]
        public void ClassifySixPointsWithRepeatInFirstFiveAndLastAndFirstAreSameAsOther()
        {
            var points = Builder.Build(
                (0, 0),
                (0, 5),
                (4, 5),
                (4, 0),
                (4, 5),
                (0, 0)
            );

            var result = Classifier.Classify(points);
            Assert.AreEqual("Other", result.Type);
        }

        [TestMethod]
        public void ClassifyFivePointsThatDoNotHaveOpositeSidesSameLength()
        {
            var points = Builder.Build(
                (0, 0),
                (1, 4),
                (3, 4),
                (3, 1),
                (0, 0)
            );

            var result = Classifier.Classify(points);
            Assert.AreEqual("Other", result.Type);
        }

        [TestMethod]
        public void ClassifyRotatedRectangle()
        {
            var points = Builder.Build(
                (2, 1),
                (1, 2),
                (4, 5),
                (5, 4),
                (2, 1)
            );

            var result = Classifier.Classify(points);
            Assert.AreEqual(result.Type, "Rectangle");
        }

        [TestMethod]
        [Ignore("Requires Angles")]
        public void ClassifyEqualateralFourSideObectWithoutRightAnglesAsOther()
        {
            var points = Builder.Build(
                (0, 0),
                (0, 4),
                (1, 5),
                (1, 1),
                (0, 0)
            );

            var result = Classifier.Classify(points);
            Assert.AreEqual(result.Type, "Other");
        }
    }
}
