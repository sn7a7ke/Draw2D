using NUnit.Framework;
using System;

namespace Plane2D.Tests
{
    [TestFixture]
    class Vector2DTest
    {
        Vector2D v1;
        Vector2D v2;

        [SetUp]
        public void Init()
        {
            v1 = new Vector2D(2, 3);
            v2 = new Vector2D(4, 7);
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
        public void VectorProduct()
        {
            Assert.IsTrue(Math.Abs(v1.VectorProduct(v2) - 2) < DoubleExtended.Epsilon);
        }
        [Test]
        public void ScalarProduct()
        {
            Assert.IsTrue(Math.Abs(v1.ScalarProduct(v2) - 29) < DoubleExtended.Epsilon);
        }
        [Test]
        public void Length()
        {
            Assert.IsTrue(Math.Abs(v1.Length - Math.Sqrt(4 + 9)) < DoubleExtended.Epsilon);
        }
        [Test]
        public void AngleBetweenVectors()
        {
            Assert.IsTrue(Math.Abs(v1.AngleBetweenVectors(new Vector2D(3,-2)) - Math.PI/2) < DoubleExtended.Epsilon);
        }

    }
}
