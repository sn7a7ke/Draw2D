using NUnit.Framework;
using System;


namespace Plane2D.Tests
{
    [TestFixture]
    class Segment2DTest
    {
        [Test]
        public void IsIntersectSegmentGood()
        {
            Segment2D AB = new Segment2D(new Point2D(-1,-1), new Point2D(3,5));
            Segment2D CD = new Segment2D(new Point2D(4, 1), new Point2D(0, 2));
            Assert.IsTrue(Segment2D.IsIntersectSegment(AB, CD));
        }
        [Test]
        public void IsIntersectSegmentBad()
        {
            Segment2D AB = new Segment2D(new Point2D(10, 2), new Point2D(3, 5));
            Segment2D CD = new Segment2D(new Point2D(4, 1), new Point2D(0, 2));
            Assert.IsFalse(Segment2D.IsIntersectSegment(AB, CD));
        }
    }
}
