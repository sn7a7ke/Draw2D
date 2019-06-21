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
        public void Distance() => Assert.IsTrue(Math.Abs(p1.Distance(p2) - Math.Sqrt(2 * 2 + 4 * 4)) < DoubleExtended.Epsilon);


        [Test]
        public void DistanceToRectangle_Exception()
        {
            Point2D p0 = new Point2D(2, 4.5);
            Assert.Throws<ArgumentOutOfRangeException>(() => p0.DistanceToRectangle(p2, p1));
        }
        [Test]
        public void DistanceToRectangle_On()
        {
            Point2D p0 = new Point2D(2, 4.5);
            Assert.IsTrue(p0.DistanceToRectangle(p1, p2) == 0);
        }
        [Test]
        public void DistanceToRectangle_Inside()
        {
            Point2D p0 = new Point2D(3.5, 4.5);
            Assert.IsTrue(p0.DistanceToRectangle(p1, p2) == -1);
        }
        [Test]
        public void DistanceToRectangle_Outside1()
        {
            Point2D p0 = new Point2D(2.5, 8);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 1) < DoubleExtended.Epsilon);
        }
        [Test]
        public void DistanceToRectangle_Outside2()
        {
            Point2D p0 = new Point2D(8, 10);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 5) < DoubleExtended.Epsilon);
        }
        [Test]
        public void DistanceToRectangle_Outside3()
        {
            Point2D p0 = new Point2D(6, 5);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 2) < DoubleExtended.Epsilon);
        }
        [Test]
        public void DistanceToRectangle_Outside4()
        {
            Point2D p0 = new Point2D(8, 0);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 5) < DoubleExtended.Epsilon);
        }
        [Test]
        public void DistanceToRectangle_Outside5()
        {
            Point2D p0 = new Point2D(3, 2);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 1) < DoubleExtended.Epsilon);
        }
        [Test]
        public void DistanceToRectangle_Outside6()
        {
            Point2D p0 = new Point2D(-2, 0);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 5) < DoubleExtended.Epsilon);
        }
        [Test]
        public void DistanceToRectangle_Outside7()
        {
            Point2D p0 = new Point2D(-3, 4);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 5) < DoubleExtended.Epsilon);
        }
        [Test]
        public void DistanceToRectangle_Outside8()
        {
            Point2D p0 = new Point2D(-1, 11);
            Assert.IsTrue(Math.Abs(p0.DistanceToRectangle(p1, p2) - 5) < DoubleExtended.Epsilon);
        }

        [Test]
        public void WhatQuarter_OnX() => Assert.IsTrue(Point2D.WhatQuarter(new Point2D(7, 0)).Equals(Point2D.PointPosition.onAxisX));
        [Test]
        public void WhatQuarter_OnY() => Assert.IsTrue(Point2D.WhatQuarter(new Point2D(0, 7)).Equals(Point2D.PointPosition.onAxisY));
        [Test]
        public void WhatQuarter_1() => Assert.IsTrue(Point2D.WhatQuarter(new Point2D(3, 7)).Equals(Point2D.PointPosition.firstQuarter));
        [Test]
        public void WhatQuarter_2() => Assert.IsTrue(Point2D.WhatQuarter(new Point2D(-3, 7)).Equals(Point2D.PointPosition.secondQuarter));
        [Test]
        public void WhatQuarter_3() => Assert.IsTrue(Point2D.WhatQuarter(new Point2D(-3, -7)).Equals(Point2D.PointPosition.thirdQuarter));
        [Test]
        public void WhatQuarter_4() => Assert.IsTrue(Point2D.WhatQuarter(new Point2D(3, -7)).Equals(Point2D.PointPosition.fourthQuarter));


        [Test]
        public void WhatQuarterRelatively_OnY() => Assert.IsTrue(p1.WhatQuarterRelatively(new Point2D(2, 7)).Equals(Point2D.PointPosition.onAxisY));
        [Test]
        public void WhatQuarterRelatively_1() => Assert.IsTrue(p1.WhatQuarterRelatively(new Point2D(1, 1.7)).Equals(Point2D.PointPosition.firstQuarter));




        [Test]
        public void Middle() => Assert.IsTrue(Point2D.Middle(p1, p2).Equals(new Point2D(3, 5)));
        [Test]
        public void Shift() => Assert.IsTrue(p1.Shift(2, 4).Equals(p2));
        [Test]
        public void Rotate() => Assert.IsTrue(p1.RotateAroundThePoint(Math.PI / 2, p2).Equals(new Point2D(8, 5)));
        [Test]
        public void Symmetry() => Assert.IsTrue(p1.SymmetryAboutPoint(p2).Equals(new Point2D(6, 11)));
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
        public void OperatorPlus() => Assert.IsTrue((p1 + p2).Equals(new Point2D(6, 10)));
        [Test]
        public void OperatorMinus() => Assert.IsTrue((p1 - p2).Equals(new Point2D(-2, -4)));
        [Test]
        public void OperatorPlusNumber() => Assert.IsTrue((p1 + 3).Equals(new Point2D(5, 6)));
        [Test]
        public void OperatorMinusNumber() => Assert.IsTrue((p1 - 3).Equals(new Point2D(-1, 0)));






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
            Assert.IsTrue(p1 == p2);
        }
        [Test]
        public void OperatorNotEquals() => Assert.IsTrue(p1 != p2);
    }
}
