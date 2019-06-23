using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plane2D.Tests
{
    [TestFixture]
    class DoubleComparerTest
    {
        [Test]
        public void TestGetHashCode()
        {
            var dc = new DoubleComparer();
            double d = 2.45;
            Assert.IsTrue(dc.GetHashCode(d) == ((int)Math.Truncate(d) ^ (int)Math.Truncate(d / DoubleExtended.Epsilon)));
        }

    }
}
