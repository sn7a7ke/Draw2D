using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    class Vertex2D : Point2D
    {
        public Vertex2D(double x, double y) : base(x,y)
        {            
        }
        public Vertex2D Next { get; set; }
        public Vertex2D Previous { get; set; }

    }
}
