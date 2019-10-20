using NUnit.Framework;
using System;

namespace Plane2D.Tests
{
    [TestFixture]
    class TriangleVertex2DTest
    {
        Point2D p1;
        Point2D p2;
        Point2D p3;
        Point2D p4;
        Triangle2D triangle2D;
        TriangleVertex2D tv1;

        [SetUp]
        public void Init()
        {
            p1 = new Point2D(1, 5);
            p2 = new Point2D(4, 5);
            p3 = new Point2D(4, 1);
            p4 = new Point2D(1, 3);
            triangle2D = new Triangle2D(new Point2D[] { p1, p2, p3 });
            tv1 = triangle2D.A;
        }

        [Test]
        public void Angle() => Assert.IsTrue(Math.Abs(tv1.Next.Angle - Math.PI / 2) < DoubleExtended.Epsilon);

        [Test]
        public void OppositeSideLength()
        {
            Assert.IsTrue(Math.Abs(tv1.OppositeSide.Length - 4) < DoubleExtended.Epsilon);
        }

        [Test]
        public void AltitudeLength()
        {
            Assert.IsTrue(Math.Abs(tv1.Altitude.Length - 3) < DoubleExtended.Epsilon);
        }

        [Test]
        public void AltitudeB()
        {
            Assert.IsTrue(tv1.Altitude.B == p2);
        }

        [Test]
        public void MedianLength()
        {
            Assert.IsTrue(Math.Abs(tv1.Median.Length - Math.Sqrt(13)) < DoubleExtended.Epsilon);
        }

        [Test]
        public void MedianB()
        {
            Assert.IsTrue(tv1.Median.B == new Point2D(4, 3));
        }

        [Test]
        public void BisectorLength()
        {
            triangle2D = new Triangle2D(new Point2D[] { p4, p2, p3 });
            tv1 = triangle2D.A;
            Assert.IsTrue(Math.Abs(tv1.Bisector.Length - 3) < DoubleExtended.Epsilon);
        }

        [Test]
        public void BisectorB()
        {
            triangle2D = new Triangle2D(new Point2D[] { p4, p2, p3 });
            tv1 = triangle2D.A;
            Assert.IsTrue(tv1.Bisector.B == new Point2D(4, 3));
        }

        [Test]
        public void MiddleLineLength()
        {
            Assert.IsTrue(Math.Abs(tv1.MiddleLine.Length - 2) < DoubleExtended.Epsilon);
        }

        [Test]
        public void MiddleLineB()
        {
            Assert.IsTrue(tv1.MiddleLine.B == new Point2D(2.5, 5));
        }

        [Test]
        public void MiddleLineA()
        {
            Assert.IsTrue(tv1.MiddleLine.A == new Point2D(2.5, 3));
        }
    }
}
