﻿using NUnit.Framework;
using System;

namespace Plane2D.Tests
{
    [TestFixture]
    class PolygonVertex2DTest
    {
        Point2D p1;
        Point2D p2;
        Point2D p3;
        Point2D p4;
        Polygon2D poly;
        PolygonVertex2D pv1;

        [SetUp]        
        public void Init()
        {
            p1 = new Point2D(1, 1);
            p2 = new Point2D(4, 5);
            p3 = new Point2D(8, 2);
            p4 = new Point2D(5, -2);
            poly = new Polygon2D(new Point2D[] { p1, p2, p3, p4 });
            pv1 = poly[0];
        }

        [Test]
        public void Angle() => Assert.IsTrue(Math.Abs(pv1.Angle - Math.PI/2) < DoubleExtended.Epsilon);

        [Test]
        public void AngleDegree() => Assert.IsTrue(Math.Abs(pv1.Next.AngleDegree - 90) < DoubleExtended.Epsilon);

        [Test]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))] //не работает
        public void Ctor()
        {
            Assert.Throws<ArgumentNullException>(() =>new PolygonVertex2D(null));
        }
    }
}
