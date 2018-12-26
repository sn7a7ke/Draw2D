using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Plane2D.Tests
{
    [TestFixture]
    class Circle2DTest
    {
        Point2D p1;
        double radius;
        Circle2D circle;

        [SetUp]
        public void Init()
        {
            p1 = new Point2D(2, 3);
            radius = 4;
            circle = new Circle2D(p1, radius);
        }

        [Test]
        public void Ctor_Exception()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle2D(p1, -2));
        }
        [Test]
        public void Ctor_Exception2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle2D(p1, 0.0000000001));
        }


        [Test]
        public void GetCircle_Null()
        {
            Point2D p1 = new Point2D(1, 1);
            Point2D p2 = new Point2D(2, 3);
            Point2D p3 = new Point2D(3, 5);
            Assert.IsNull(Circle2D.GetCircle(p1, p2, p3));
        }
        [Test]
        public void GetCircle_Vertical12()
        {
            Point2D p1 = new Point2D(1, 1);
            Point2D p2 = new Point2D(1, 5);
            Point2D p3 = new Point2D(4, 1);
            Point2D center = new Point2D(2.5, 3);
            Assert.IsTrue(Circle2D.GetCircle(p1, p2, p3) == new Circle2D(center, 2.5));
        }
        [Test]
        public void GetCircle_Vertical23()
        {
            Point2D p1 = new Point2D(1, 1);
            Point2D p2 = new Point2D(1, 5);
            Point2D p3 = new Point2D(4, 1);
            Point2D center = new Point2D(2.5, 3);
            Assert.IsTrue(Circle2D.GetCircle(p3, p1, p2) == new Circle2D(center, 2.5));
        }
        [Test]
        public void GetCircle_Vertical31()
        {
            Point2D p1 = new Point2D(1, 1);
            Point2D p2 = new Point2D(1, 5);
            Point2D p3 = new Point2D(4, 1);
            Point2D center = new Point2D(2.5, 3);
            Assert.IsTrue(Circle2D.GetCircle(p2, p3, p1) == new Circle2D(center, 2.5));
        }



        [Test]
        public void FuncYFromX()
        {
            List<double> ps = circle.FuncYFromX(2);
            Assert.IsTrue(ps.Count == 2 && (ps[0] * ps[1]).Equal(-7));
        }
        [Test]
        public void FuncYFromX_null()
        {
            List<double> ps = circle.FuncYFromX(7);
            Assert.IsNull(ps);
        }
        [Test]
        public void InverseFuncXFromY()
        {
            List<double> ps = circle.InverseFuncXFromY(3);
            Assert.IsTrue(ps.Count == 2 && (ps[0] * ps[1]).Equal(-12));
        }
        [Test]
        public void InverseFuncXFromY_null()
        {
            List<double> ps = circle.InverseFuncXFromY(-3);
            Assert.IsNull(ps);
        }
        [Test]
        public void Square()
        {
            Assert.IsTrue(circle.Square.Equal(Math.PI * radius * radius));
        }
        [Test]
        public void Perimeter()
        {
            Assert.IsTrue(circle.Perimeter.Equal(2 * Math.PI * radius));
        }
        [Test]
        public void IsOnTheCircle_On()
        {
            Assert.IsTrue(circle.IsOnTheCircle(new Point2D(6, 3)) == 0);
        }
        [Test]
        public void IsOnTheCircle_Inside()
        {
            Assert.IsTrue(circle.IsOnTheCircle(new Point2D(4, 4)) == -1);
        }
        [Test]
        public void IsOnTheCircle_Distance()
        {
            Assert.IsTrue(circle.IsOnTheCircle(new Point2D(10, -3)) == 6);
        }
        [Test]
        public void GetPoint1()
        {
            Assert.IsTrue(circle.GetPoint(0) == new Point2D(6, 3));
        }
        [Test]
        public void GetPoint2()
        {
            Assert.IsTrue(circle.GetPoint(Math.PI / 2) == new Point2D(2, 7));
        }
        [Test]
        public void GetTangent()
        {
            Assert.IsTrue(circle.GetTangent(circle.GetPoint(Math.PI / 4)) == new Line2D(circle.GetPoint(Math.PI / 4), circle).PerpendicularFromPoint(circle.GetPoint(Math.PI / 4)));
        }
        [Test]
        public void GetTangent_Out()
        {
            Point2D p2 = new Point2D(4, 7);
            Assert.IsNull(circle.GetTangent(p2));
        }
        [Test]
        public void GetTangent_OnAxisX1()
        {
            Assert.IsTrue(circle.GetTangent(new Point2D(6, 3)) == new Line2D(1, 0, -6));
        }
        [Test]
        public void GetTangent_OnAxisX2()
        {
            Assert.IsTrue(circle.GetTangent(new Point2D(-2, 3)) == new Line2D(1, 0, 2));
        }
        [Test]
        public void GetTangent_OnAxisY1()
        {
            Assert.IsTrue(circle.GetTangent(new Point2D(2, 7)) == new Line2D(0, 1, -7));
        }
        [Test]
        public void GetTangent_OnAxisY2()
        {
            Assert.IsTrue(circle.GetTangent(new Point2D(2, -1)) == new Line2D(0, 1, 1));
        }
    }
}
