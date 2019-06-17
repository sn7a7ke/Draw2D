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
        Triangle2D t1;

        [SetUp]
        public void Init()
        {
            p1 = new Point2D(1, 5);
            p2 = new Point2D(4, 5);
            p3 = new Point2D(4, 1);

            t1 = new Triangle2D(p1, p2, p3);
        }
        [Test]
        public void Perimeter()
        {
            Assert.IsTrue(t1.Perimeter.Equal(12));
        }
        [Test]
        public void Square()
        {
            Assert.IsTrue(t1.Square.Equal(6));
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
            Assert.IsTrue(t1.IntersectionBisectors == new Point2D(3, 4));
        }
        [Test]
        public void Circumradius()
        {
            Assert.IsTrue(Math.Abs(t1.Circumradius - 2.5) < DoubleExtended.Epsilon);
        }
        [Test]
        public void Inradius()
        {
            Assert.IsTrue(t1.Inradius.Equal(1));
        }
        [Test]
        public void Incenter()
        {
            Assert.IsTrue(t1.Incenter == new Point2D(3, 4));
        }
        [Test]
        public void Incenter_IntersectionBisectors()
        {
            Assert.IsTrue(t1.Incenter == t1.IntersectionBisectors);
        }
        [Test]
        public void CircumCircle()
        {
            Assert.IsTrue(t1.CircumCircle == new Circle2D(new Point2D(2.5, 3), 2.5));
        }
        [Test]
        public void InscribedCircle()
        {
            Assert.IsTrue(t1.InscribedCircle == new Circle2D(new Point2D(3, 4), 1));
        }

        [Test]
        public void OppositeInradius_Null()
        {
            Point2D p4 = new Point2D(1, 3);
            Assert.IsNull(t1.OppositeInradius(p4));
        }
        [Test]
        public void OppositeInradius()
        {
            Segment2D seg = t1.OppositeInradius(p1);
            Assert.IsTrue(seg == new Segment2D(new Point2D(3, 4), new Point2D(4, 4)));
        }

        //[Test] 
        //public void OppositeInradius_Null() // Вызывает ОШИБКУ VS!!!
        //{
        //    Point2D p4 = new Point2D(1, 3);
        //    Assert.IsNull(t1.OppositeInradius(new TriangleVertex2D(p1, p2, p4)));
        //}




        [Test]
        public void IsSimilar_False()
        {
            Point2D p4 = new Point2D(1, 3);
            Assert.IsFalse(t1.IsSimilar(new Triangle2D(p4, p2, p3)));
        }
        [Test]
        public void IsSimilar_TrueA()
        {
            Assert.IsTrue(t1.IsSimilar(new Triangle2D(p1, p3, p2)));
        }
        [Test]
        public void IsSimilar_TrueB()
        {
            Assert.IsTrue(t1.IsSimilar(new Triangle2D(p3, p1, p2)));
        }
        [Test]
        public void IsSimilar_TrueC()
        {
            Assert.IsTrue(t1.IsSimilar(new Triangle2D(p2, p3, p1)));
        }
        [Test]
        public void IsSimilar_False2()
        {
            Point2D p2 = new Point2D(2, 5);
            Assert.IsFalse(t1.IsSimilar(new Triangle2D(p1, p2, p3)));
        }
    }
}
