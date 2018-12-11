using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class Polygon2D : Shape2D
    {
        public Point2D Center
        {
            get
            {
                double xx = 0;
                double yy = 0;
                for (int i = 0; i < QuantityVertices; i++)
                {
                    xx += Vertices[i].X;
                    yy += Vertices[i].Y;
                }
                return new Point2D(xx / QuantityVertices, yy / QuantityVertices);
            }
        }

        public int QuantityVertices => Vertices.Length;
        public bool IsConvex { get; protected set; }
        // LinkedList<Point2D> Может ТАК!?
        public Point2D[] Vertices { get; private set; }
        //public PointF[] VerticesToPointF
        //{
        //    get
        //    {
        //        PointF[] pf = new PointF[QuantityVertices];
        //        for (int i = 0; i < QuantityVertices; i++)
        //            pf[i] = Vertices[i];
        //        return pf;
        //    }
        //}
        public Point[] VerticesToPoint
        {
            get
            {
                Point[] p = new Point[QuantityVertices];
                for (int i = 0; i < QuantityVertices; i++)
                    p[i] = Vertices[i];
                return p;
            }
        }

        public override string Name => base.Name + " v" + QuantityVertices + " c" + Center;

        public Polygon2D(params Point2D[] vertices) 
        {
            this.Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            if (vertices.Length < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");
        }



        public Polygon2D Shift(double dx, double dy)
        {
            Point2D[] newC = new Point2D[QuantityVertices];
            for (int i = 0; i < QuantityVertices; i++)
                newC[i] = Vertices[i].Shift(dx, dy);
            return new Polygon2D(newC);
        }
        public Polygon2D Rotate(double angle, Point2D center)
        {
            Point2D[] newC = new Point2D[QuantityVertices];
            for (int i = 0; i < QuantityVertices; i++)
                newC[i] = Vertices[i].Rotate(angle, center);
            return new Polygon2D(newC);
        }
        public Polygon2D Rotate(double angle) => Rotate(angle, new Point2D(100,70));
        public Polygon2D Symmetry(Point2D center)
        {
            Point2D[] newC = new Point2D[QuantityVertices];
            for (int i = 0; i < QuantityVertices; i++)
                newC[i] = Vertices[i].Symmetry(center);
            return new Polygon2D(newC);
        }

        public override double Perimeter()
        {
            double perim = 0;
            for (int i = 0; i < QuantityVertices-1; i++)            
                perim += Vertices[i].Distance(Vertices[i + 1]);
            perim += Vertices[0].Distance(Vertices[QuantityVertices-1]);
            return perim;
        }

        public override double Square()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => base.Name + " v" + QuantityVertices; //GetType().Name + " v" + QuantityVertices;

        public string VerticesToString(string separator = " ") => string.Join(separator, Vertices as object[]);

        public static Polygon2D GetPolygonFromCoordinateSystem(List<Point> ps, Point origin)
        {
            Point2D[] p2D = new Point2D[ps.Count];
            for (int i = 0; i < ps.Count; i++)
            {
                //p2D[i] = ps[i];
                p2D[i] = Point2D.GetPointFromCoordinateSystem(origin, ps[i]);
            }
            return new Polygon2D(p2D);
        }
        public Polygon2D GetPolygonInCoordinateSystem(Point origin)
        {
            Point2D[] newC = new Point2D[QuantityVertices];
            for (int i = 0; i < QuantityVertices; i++)
                newC[i] = Vertices[i].GetPointInCoordinateSystem(origin);
            return new Polygon2D(newC);

        }

        public override void Draw(Graphics graph, Pen pen) => graph.DrawPolygon(pen, VerticesToPoint);
    }
}
