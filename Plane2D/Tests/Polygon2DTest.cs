using NUnit.Framework;
using System;

namespace Plane2D.Tests
{
    [TestFixture]

    class Polygon2DTest
    {
        Point2D p1;
        Point2D p2;
        Point2D p3;
        Point2D p4;
        Polygon2D pl1;

        [SetUp]
        public void Init()
        {
            p1 = new Point2D(1, 1);
            p2 = new Point2D(4, 5);
            p3 = new Point2D(8, 2);
            p4 = new Point2D(5, -2);
            pl1 = new Polygon2D(p1, p2, p3, p4);
        }
        [Test]
        public void This() => Assert.IsTrue(pl1[1] == p2);
        [Test]
        public void GetVertices() => Assert.IsTrue(pl1.GetVertices[3] == p4);
        [Test]
        public void Center() => Assert.IsTrue(pl1.Center == new Point2D(4.5, 1.5));
        [Test]
        public void Perimeter() => Assert.IsTrue(Math.Abs(pl1.Perimeter - 20) < Point2D.epsilon);
        [Test]
        public void Square() => Assert.IsTrue(Math.Abs(pl1.Square - 25) < Point2D.epsilon);
        [Test]
        public void AngleSumForConvex() => Assert.IsTrue(Math.Abs(Polygon2D.AngleSumForConvex(7) - 5 * Math.PI) < Point2D.epsilon);
        [Test]
        public void IsConvexGood() => Assert.IsTrue(pl1.IsConvex);
        [Test]
        public void IsConvexBad()
        {
            pl1 = new Polygon2D(p1, p2, p4, p3);
            Assert.IsFalse(pl1.IsConvex);
        }
        [Test]
        public void Ctor()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Polygon2D(new Point2D[] { p1, p2 }));
        }
        [Test]
        public void Ctor2()
        {
            Assert.Throws<ArgumentNullException>(() => new Polygon2D(null));
        }

    }
}
