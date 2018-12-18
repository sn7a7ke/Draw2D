using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    class Triangle2D : Polygon2D<PolygonVertex2D>
    {
        public Triangle2D(Point2D A, Point2D B, Point2D C) : base(A, B, C)
        {
        }
    }
}
