using NUnit.Framework;
using System;

namespace Plane2D.Tests
{
    [TestFixture]
    class Point2DTest
    {
        Point2D p1;
        Point2D p2;

        [SetUp]
        public void Init()
        {
            p1 = new Point2D(2, 3);
            p2 = new Point2D(4, 7);
        }

        [Test]
        public void Distance() => Assert.IsTrue(Math.Abs(p1.Distance(p2) - Math.Sqrt(2 * 2 + 4 * 4)) < Point2D.epsilon);
        [Test]
        public void Middle() => Assert.IsTrue(Point2D.Middle(p1, p2).Equals(new Point2D(3, 5)));
        [Test]
        public void Shift() => Assert.IsTrue(p1.Shift(2, 4).Equals(p2));
        [Test]
        public void Rotate() => Assert.IsTrue(p1.Rotate(Math.PI / 2, p2).Equals(new Point2D(8, 5)));
        [Test]
        public void Symmetry() => Assert.IsTrue(p1.Symmetry(p2).Equals(new Point2D(6, 11)));
        [Test]
        public void Min()
        {
            Point2D p3 = new Point2D(1, 15);
            Assert.IsTrue(Point2D.Min(p1, p2, p3).Equals(new Point2D(1, 3)));
        }
        [Test]
        public void Max()
        {
            Point2D p3 = new Point2D(1, 15);
            Assert.IsTrue(Point2D.Max(p1, p2, p3).Equals(new Point2D(4, 15)));
        }
        [Test]
        public void CloneGood() => Assert.IsTrue(p1.Clone().Equals(p1));
        [Test]
        public void CloneBad() => Assert.AreNotSame(p1.Clone(), p1);

        [Test]
        public void EqualsGood()
        {
            Point2D p2 = new Point2D(2, 3);
            Assert.IsTrue(p1.Equals(p2));
        }
        [Test]
        public void EqualsBad() => Assert.IsFalse(p1.Equals(p2));
        [Test]
        public void OperatorEquals()
        {
            Point2D p2 = new Point2D(2, 3);
            Assert.IsTrue(p1==p2);
        }
        [Test]
        public void OperatorNotEquals() => Assert.IsTrue(p1 != p2);
    }
}
