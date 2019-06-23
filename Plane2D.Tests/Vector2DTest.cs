using NUnit.Framework;
using System;

namespace Plane2D.Tests
{
    [TestFixture]
    class Vector2DTest
    {
        Point2D p1;
        Vector2D v1;
        Vector2D v2;

        [SetUp]
        public void Init()
        {
            p1 = new Point2D(2, 3);
            v1 = new Vector2D(p1);
            v2 = new Vector2D(4, 7);
        }

        [Test]
        public void Ctor_4double()
        {
            Vector2D changedVector = new Vector2D(2, 2, 4, 5);
            Vector2D resultVector = v1;
            Assert.IsTrue(changedVector == resultVector);
        }


        [Test]
        public void Add()
        {
            Assert.IsTrue(v1.Add(v2) == new Vector2D(6, 10));
        }

        [Test]
        public void Sub()
        {
            Assert.IsTrue(v1.Sub(v2) == new Vector2D(-2, -4));
        }

        [Test]
        public void MulByNumber()
        {
            Assert.IsTrue(v1.MulByNumber(1.5) == new Vector2D(3, 4.5));
        }

        [Test]
        public void OperatorAdd()
        {
            Assert.IsTrue(v1 + v2 == new Vector2D(6, 10));
        }

        [Test]
        public void OperatorSub()
        {
            Assert.IsTrue(v1 - v2 == new Vector2D(-2, -4));
        }

        [Test]
        public void OperatorMulByNumber()
        {
            Assert.IsTrue(v1 * 1.5 == new Vector2D(3, 4.5));
        }

        [Test]
        public void OperatorNotEquals()
        {
            Assert.IsTrue(v1 != v2);
        }

        [Test]
        public void IsEqualsOneParam_VectorNull_False()
        {
            Vector2D vec = null;
            Assert.IsFalse(v1.Equals(vec));
        }

        [Test]
        public void IsEqualsOneParam_Object_False()
        {
            object obj = null;
            Assert.IsFalse(v1.Equals(obj));
        }

        [Test]
        public void CheckGetHashCode()
        {
            int result = (int)v1.X ^ (int)v1.Y * 11;
            Assert.IsTrue(v1.GetHashCode() == result);
        }

        [Test]
        public void VectorProduct()
        {
            Assert.IsTrue(v1.VectorProduct(v2).Equal(2));
        }

        [Test]
        public void ScalarProduct()
        {
            Assert.IsTrue(v1.ScalarProduct(v2).Equal(29));
        }

        [Test]
        public void Length()
        {
            Assert.IsTrue(v1.Length.Equal(Math.Sqrt(13)));
        }

        [Test]
        public void AngleBetweenVectors()
        {
            Assert.IsTrue(v1.AngleBetweenVectors(new Vector2D(3, -2)).Equal(Math.PI / 2));
        }

        [Test]
        public void AngleBetweenVectors_LenTwoEqualsScalar()
        {
            Vector2D vec1 = new Vector2D(2, 2);
            Vector2D vec2 = new Vector2D(3, 3);
            Assert.IsTrue(vec1.AngleBetweenVectors(vec2).IsZero());
        }

        [Test]
        public void Shift()
        {
            Assert.IsTrue((Vector2D)v1.Shift(1, 1) == v1);
        }

        [Test]
        public void RotateAroundThePoint()
        {
            Vector2D changedVector = (Vector2D)v1.RotateAroundThePoint(Math.PI / 2, new Point2D(1, 1));
            Vector2D resultVector = new Vector2D(-3, 2);
            Assert.IsTrue(changedVector == resultVector);
        }

        [Test]
        public void RotateAroundTheCenterOfCoordinates()
        {
            Vector2D changedVector = (Vector2D)v1.RotateAroundTheCenterOfCoordinates(Math.PI / 2);
            Vector2D resultVector = new Vector2D(-3, 2);
            Assert.IsTrue(changedVector == resultVector);
        }

        [Test]
        public void SymmetryAboutPoint()
        {
            Vector2D changedVector = (Vector2D)v1.SymmetryAboutPoint(new Point2D(5, 3));
            Vector2D resultVector = new Vector2D(-2, -3);
            Assert.IsTrue(changedVector == resultVector);
        }
    }
}
