using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shape.Lib;
using Shape.Lib.Types;

namespace Shape.Tests
{
    [TestClass]
    public class ClassifyLineSegmentShould
    {
        [TestMethod]
        public void ClassifyTwoDifferentPointsAsLineSegment()
        {
            var points = Builder.Build(
                (0, 0),
                (50, 0)
            );
        
            var result = Classifier.Classify(points);
            Assert.AreEqual("Line Segment", result.Type);
            var lineSegment = (LineSegment)result;
            Assert.AreEqual(lineSegment.A, Builder.Build(0, 0));
            Assert.AreEqual(lineSegment.B, Builder.Build(50, 0));
            Assert.IsNull(lineSegment.Slope);
        }
        
        // [TestMethod]
        // public void ClassifyTwoPointsOfTheSameLocationAsOther()
        // {
        //     var points = Builder.Build(
        //         (0, 0),
        //         (0, 0)
        //     );
        //
        //     var result = Classifier.Classify(points);
        //     Assert.AreEqual("Other", result.Type);
        // }
    }
}
