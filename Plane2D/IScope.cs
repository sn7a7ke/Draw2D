using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public interface IScope
    {
        double MaxX { get; }
        double MaxY { get; }
        double MinX { get; }
        double MinY { get; }
    }
}
