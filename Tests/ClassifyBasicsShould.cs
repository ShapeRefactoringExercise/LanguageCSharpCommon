using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Shape.Lib;
using Shape.Lib.Types;

// using Shape.Lib.Types;

namespace Shape.Tests
{
    [TestClass]
    public class ClassifyBasicsShould
    {
        [TestMethod]
        public void ClassifyAnEmptyArrayAsEmpty()
        {
            var points = Builder.Build();
            var result = Classifier.Classify(points);

            Assert.AreEqual("Empty", result.Type);
        }

        [TestMethod]
        public void ClassifyASinglePointAsPoint()
        {
            var point = Builder.Build(0, 0);
            var points = new[] { point };
            var result = Classifier.Classify(points);

            Assert.AreEqual("Point", result.Type);
            var rPoint = (AllShape)result;
            Assert.AreEqual(point.X, rPoint.X);
            Assert.AreEqual(point.Y, rPoint.Y);
        }

        [TestMethod]
        public void PointShouldHaveToString()
        {
            var point = Builder.Build(45.5, 30);
            Assert.AreEqual(point.ToString(), "(45.5, 30)");
        }
    }
}
