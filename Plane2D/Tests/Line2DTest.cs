using NUnit.Framework;
using System;


namespace Plane2D.Tests
{
    [TestFixture]
    class Line2DTest
    {
        Line2D l1;
        Line2D l2;

        [SetUp]
        public void Init()
        {
            l1 = new Line2D(2, 1, -4);
            l2 = new Line2D(-1, 2, 2);
        }
        [Test]
        public void IsPerpendicular()
        {
            Assert.IsTrue(l1.IsPerpendicular(l2));
        }
        [Test]
        public void DistanceFromPointToLine()
        {
            Assert.IsTrue(Math.Abs(l1.DistanceFromPointToLine(new Point2D(6, 2)) - Math.Sqrt(20)) < Point2D.epsilon);
        }

        [Test]
        public void GetTangent()
        {
            Assert.IsTrue(l1.GetTangent(new Point2D(2, 0)).Equals(l1));
        }
        [Test]
        public void GetTangent_Null()
        {
            Assert.IsNull(l1.GetTangent(new Point2D(4, 0)));
        }

        [Test]
        public void PerpendicularFromPoint()
        {
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(6, 2)).Equals(l2));
        }
        [Test]
        public void PerpendicularFromPoint2()
        {
            Line2D l1 = new Line2D(1, 0, -2);
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(4, 1)).Equals(new Line2D(0, 1, -1)));
        }
        [Test]
        public void PerpendicularFromPoint3()
        {
            Line2D l1 = new Line2D(0, 1, -2);
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(1, 4)).Equals(new Line2D(1, 0, -1)));
        }
        [Test]
        public void PerpendicularFromPoint_PointOnLine()
        {
            Line2D l1 = new Line2D(0, 1, -2);
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(0, 2)).Equals(l2));
        }

        [Test]
        public void PerpendicularFromPointToPointOnLine()
        {
            Assert.IsTrue(l1.IntersectPerpendicularFromPointWithLine(new Point2D(6, 2)).Equals(new Point2D(2, 0)));
        }
        [Test]
        public void Intersect()
        {
            Assert.IsTrue(l1.Intersect(l2).Equals(new Point2D(2, 0)));
        }
        [Test]
        public void Intersect2()
        {
            Assert.IsNull(l1.Intersect(l1));
        }

        [Test]
        public void EqualsGood()
        {
            Assert.IsTrue(l1.Equals(new Line2D(4, 2, -8)));
        }
        [Test]
        public void EqualsBad()
        {
            Assert.IsFalse(l1.Equals(new Line2D(4, 2, -6)));
        }

        [Test]
        public void Ctor()
        {
            Assert.IsTrue(l1.Equals(new Line2D(new Point2D(2, 0), new Point2D(0, 4))));
        }
        [Test]
        public void Ctor2()
        {
            Assert.IsTrue(l1.Equals(new Line2D(new Vector2D(2, 1), new Point2D(1, 2))));
        }

    }
}
