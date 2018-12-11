using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    class Triangle2D : Polygon2D
    {
        public Triangle2D(Point2D vertexA, Point2D vertexB, Point2D vertexC) : base(vertexA, vertexB, vertexC)
        {
            IsConvex = true;
        }
    }
}
