using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    // Добавить
    // тип: остро, прямо, тупо; равнобедр, равностор
    // подобие

    public class Triangle2D : Polygon2D
    {
        public TriangleVertex2D A { get; private set; }
        public TriangleVertex2D B { get; private set; }
        public TriangleVertex2D C { get; private set; }

        public Triangle2D(Point2D A, Point2D B, Point2D C) : base(new TriangleVertex2D(A, B, C))
        {
            
            this.A = (TriangleVertex2D)_head;
            this.B = (TriangleVertex2D)_head.Next;
            this.C = (TriangleVertex2D)_head.Next.Next;
            //if (!(_head is TriangleVertex2D head))
            //    throw new ArgumentOutOfRangeException(nameof(_head));
            //A = head;
            //B = (TriangleVertex2D)head.Next;
            //C = (TriangleVertex2D)head.Next.Next;
        }


        public Point2D IntersectionAltitudes => new Line2D(A.Altitude.A, A.Altitude.B).Intersect(new Line2D(B.Altitude.A, B.Altitude.B));
        public Point2D IntersectionMedians => new Line2D(A.Median.A, A.Median.B).Intersect(new Line2D(B.Median.A, B.Median.B));
        public Point2D IntersectionBisectors => new Line2D(A.Bisector.A, A.Bisector.B).Intersect(new Line2D(B.Bisector.A, B.Bisector.B));
        public double Circumradius => A.OppositeSide.Length * B.OppositeSide.Length * C.OppositeSide.Length / (4 * Square);
        public double Inradius => 2 * Square / Perimeter;

        public Point2D Incenter
        {
            get
            {
                // ПРОВЕРИТЬ!!! находится НА БИСЕКТРИСАХ!
                double xx = (A.OppositeSide.Length * A.X + B.OppositeSide.Length * B.X + C.OppositeSide.Length * C.X) / Perimeter;
                double yy = (A.OppositeSide.Length * A.Y + B.OppositeSide.Length * B.Y + C.OppositeSide.Length * C.Y) / Perimeter;
                return new Point2D(xx, yy);
            }
        }

        public override Polygon2D Shift(double dx, double dy)
        {
            Point2D[] ps = base.Shift(dx, dy).GetVertices;
            return new Triangle2D(ps[0], ps[1], ps[2]);
        }
        public override Polygon2D Rotate(double angle, Point2D center)
        {
            Point2D[] ps = base.Rotate(angle, center).GetVertices;
            return new Triangle2D(ps[0], ps[1], ps[2]);
        }
        public override Polygon2D Rotate(double angle) => this.Rotate(angle, Center);
        public override Polygon2D Symmetry(Point2D center)
        {
            Point2D[] ps = base.Symmetry(center).GetVertices;
            return new Triangle2D(ps[0], ps[1], ps[2]);
        }
        //public override Polygon2D GetPolygonInCoordinateSystem(Point origin)
        //{
        //    Point2D[] vers = GetVertices;
        //    for (int i = 0; i < vers.Length; i++)
        //        vers[i] = vers[i].ToPointInCoordinateSystem(origin);
        //    return new Polygon2D(vers);
        //}
    }
    //isosceles - равнобедренный
}
