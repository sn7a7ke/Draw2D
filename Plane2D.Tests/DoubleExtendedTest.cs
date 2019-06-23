using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace Plane2D.Tests
{
    [TestFixture]
    class DoubleExtendedTest
    {
        double d1;
        double d2;
        [Test]
        public void Epsilon()
        {
            double newEpsilon = 0.00001;
            DoubleExtended.Epsilon = newEpsilon;
            Assert.IsTrue(DoubleExtended.Epsilon == newEpsilon);
        }

        [Test]
        public void Equal()
        {
            d1 = 2.54;
            d2 = 2.54000000001;
            Assert.IsTrue(d1.Equal(d2));
        }

        [Test]
        public void IsZero()
        {
            d1 = 0.00000000001;
            Assert.IsTrue(d1.IsZero());
        }

        [Test]
        public void More()
        {
            d1 = 2.64;
            d2 = 2.57;
            Assert.IsTrue(d1.More(d2));
        }

        [Test]
        public void MoreOrEqual()
        {
            d1 = 2.57;
            d2 = 2.57000000000001;
            Assert.IsTrue(d1.MoreOrEqual(d2));
        }

        [Test]
        public void Less()
        {
            d1 = 2.64;
            d2 = 2.57;
            Assert.IsTrue(d2.Less(d1));
        }

        [Test]
        public void LessOrEqual()
        {
            d1 = 2.57000000000001;
            d2 = 2.57;
            Assert.IsTrue(d2.LessOrEqual(d1));
        }
    }
}
