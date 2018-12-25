using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    class DoubleComparer : IEqualityComparer<double>
    {

        public bool Equals(double x, double y) => x.GetHashCode() == y.GetHashCode();
        public int GetHashCode(double obj)
        {
            return (int)Math.Truncate(obj) ^ (int)Math.Truncate(obj / DoubleExtended.Epsilon);
        }
    }
}
