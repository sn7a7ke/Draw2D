using System;
using System.Collections.Generic;

namespace Plane2D
{
    public class DoubleComparer : IEqualityComparer<double>
    {
        public bool Equals(double x, double y) => x.GetHashCode() == y.GetHashCode();

        public int GetHashCode(double obj)
        {
            return (int)Math.Truncate(obj) ^ (int)Math.Truncate(obj / DoubleExtended.Epsilon);
        }
    }
}
