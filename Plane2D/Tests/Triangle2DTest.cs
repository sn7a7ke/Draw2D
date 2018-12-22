using NUnit.Framework;
using System;

namespace Plane2D.Tests
{
    [TestFixture]
    class Triangle2DTest
    {
        Point2D p1;
        Point2D p2;
        Point2D p3;
        Point2D p4;
        Triangle2D t1;

        [SetUp]
        public void Init()
        {
            p1 = new Point2D(1, 5);
            p2 = new Point2D(4, 5);
            p3 = new Point2D(4, 1);
            p4 = new Point2D(1, 3);

            t1 = new Triangle2D(p1, p2, p3);
        }
        [Test]
        public void IntersectionAltitudes()
        {
            Assert.IsTrue(t1.IntersectionAltitudes == p2);
        }
        [Test]
        public void IntersectionMedians()
        {
            Assert.IsTrue(t1.IntersectionMedians == t1.Center);
        }
        [Test]
        public void IntersectionBisectors()
        {
            p1 = new Point2D(1, 1);
            p2 = new Point2D(7, 1);
            p3 = new Point2D(4, Math.Sqrt(27));
            t1 = new Triangle2D(p1, p2, p3);

            Assert.IsTrue(t1.IntersectionBisectors == new Point2D(4, (double)2/3 + Math.Sqrt(3)));
        }
        [Test]
        public void Circumradius()
        {
            Assert.IsTrue(Math.Abs(t1.Circumradius - 2.5) < Point2D.epsilon);
        }




    }
}
