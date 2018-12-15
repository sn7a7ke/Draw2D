using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plane2D;

namespace Plane2D //2
{
    public class Polygon2D<TVertex> : Shape2D
        where TVertex : PolygonVertex2D, new()
    {
        protected TVertex _head;
        public int QuantityVertices { get; private set; }
        public TVertex this[int index]
        {
            get
            {
                if (index < 0 || index >= QuantityVertices)
                    throw new ArgumentOutOfRangeException(nameof(index));
                TVertex current = _head;
                for (int i = 0; i < index; i++, current = (TVertex)current.Next)
                    ;
                return current;
            }
        }
        public Polygon2D(params Point2D[] vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (vertices.Length < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");


            _head = new TVertex();// vertices[0]);
            //for (int i = 1; i < vertices.Length; i++)
                _head.Add(vertices);
            QuantityVertices = _head.Count;
        }

        public Point2D[] GetVertices()
        {
            // Array!?
            List<Point2D> verticies = new List<Point2D>();
            foreach (TVertex item in _head)
                verticies.Add(item);
            return verticies.ToArray();
        }

        public Point2D Center
        {
            get
            {
                double xx = 0;
                double yy = 0;
                foreach (TVertex item in _head)
                {
                    xx += item.X;
                    yy += item.Y;
                }
                return new Point2D(xx / QuantityVertices, yy / QuantityVertices);
            }
        }
        public override double Perimeter
        {
            get
            {
                double perim = 0;
                foreach (TVertex item in _head)
                    perim += item.Distance(item.Next);
                return perim;
            }
        }
        public override double Square
        {
            get
            {
                //==== КАК!?? ===
                throw new NotImplementedException();
            }
        }

        public Polygon2D<TVertex> Shift(double dx, double dy)
        {
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].Shift(dx, dy);
            return new Polygon2D<TVertex>(vers);
        }

        public Polygon2D<TVertex> Rotate(double angle, Point2D center)
        {
            if (angle == 0)
                return this;
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].Rotate(angle, center);
            return new Polygon2D<TVertex>(vers);
        }
        public Polygon2D<TVertex> Rotate(double angle) => Rotate(angle, Center);
        public Polygon2D<TVertex> Symmetry(Point2D center)
        {
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].Symmetry(center);
            return new Polygon2D<TVertex>(vers);
        }

        //TODO переделать логику
        private double AngleSum()
        {
            double sum = 0;
            foreach (TVertex item in _head)
                sum += new Vector2D(item, item.Next).AngleBetweenVector(new Vector2D(item.Next, item.Next.Next));

            sum = Math.PI * QuantityVertices - sum;
            return sum;
        }
        public static double AngleSumForConvex(int n) => n < 3 ? 0 : (n - 2) * Math.PI;
        public override bool IsConvex
        {
            get
            {
                if (QuantityVertices < 4)
                    return true;
                if (Math.Abs(AngleSum() - AngleSumForConvex(QuantityVertices)) < Point2D.epsilon)
                    return true;
                else
                    return false;
            }
        }

        public override string Name => base.Name + " v" + QuantityVertices + " c" + Center;
        public override string ToString() => base.Name + " v" + QuantityVertices; //GetType().Name + " v" + QuantityVertices;
        public string VerticesToString(string separator = " ") => string.Join(separator, _head);

        public static Polygon2D<TVertex> GetPolygonFromCoordinateSystem(List<Point> ps, Point origin)
        {
            Point2D[] vers = new Point2D[ps.Count];
            for (int i = 0; i < ps.Count; i++)
                vers[i] = Point2D.ToPoint2DFromCoordinateSystem(origin, ps[i]);
            return new Polygon2D<TVertex>(vers);
        }
        public Polygon2D<TVertex> GetPolygonInCoordinateSystem(Point origin)
        {
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].ToPointInCoordinateSystem(origin);
            return new Polygon2D<TVertex>(vers);
        }
        public Point[] VerticesToPoint
        {
            get
            {
                Point[] p = new Point[QuantityVertices];
                TVertex currentNode = _head;
                for (int i = 0; i < QuantityVertices; i++, currentNode = (TVertex)currentNode.Next)
                    p[i] = currentNode;
                return p;
            }
        }

        public override void Draw(Graphics graph, Pen pen) => graph.DrawPolygon(pen, VerticesToPoint);
    }
}
