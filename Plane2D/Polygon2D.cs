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
        protected Node<Point2D> _head;
        public int QuantityVertices { get; private set; }
        //public Point2D VertexA { get; private set; }
        private void Add(Node<Point2D> node)
        {
            if (_head == null)
            {
                _head = node;
                _head.Next = _head;
                _head.Previous = _head;
            }
            else
            {
                _head.Previous.Next = node;
                node.Previous = _head.Previous;
                node.Next = _head;
                _head.Previous = node;
            }
            QuantityVertices++;
        }
        private void Add(Point2D p) => Add(new Node<Point2D>(p));
        public Polygon2D(params Point2D[] vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (vertices.Length < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");
            for (int i = 0; i < vertices.Length; i++)
                Add(vertices[i]);
        }
        private Polygon2D() { }

        public Point2D Center
        {
            get
            {
                double xx = 0;
                double yy = 0;
                foreach (Node<Point2D> item in _head)
                {
                    xx += item.Value.X;
                    yy += item.Value.Y;
                }
                return new Point2D(xx / QuantityVertices, yy / QuantityVertices);
            }
        }
        public override double Perimeter
        {
            get
            {
                double perim = 0;
                foreach (Node<Point2D> item in _head)
                    perim += item.Value.Distance(item.Next.Value);
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

        public Polygon2D Shift(double dx, double dy)
        {
            Polygon2D newP = new Polygon2D();
            foreach (Node<Point2D> item in _head)
                newP.Add(item.Value.Shift(dx, dy));
            return newP;
        }

        public Polygon2D Rotate(double angle, Point2D center)
        {
            Polygon2D newP = new Polygon2D();
            foreach (Node<Point2D> item in _head)
                newP.Add(item.Value.Rotate(angle, center));
            return newP;
        }
        public Polygon2D Rotate(double angle) => Rotate(angle, Center);
        public Polygon2D Symmetry(Point2D center)
        {
            Polygon2D newP = new Polygon2D();
            foreach (Node<Point2D> item in _head)
                newP.Add(item.Value.Symmetry(center));
            return newP;
        }

        //TODO переделать логику
        private double AngleSum()
        {
            double sum = 0;
            foreach (Node<Point2D> item in _head)
                sum += new Vector2D(item.Value, item.Next.Value).AngleBetweenVector(new Vector2D(item.Next.Value, item.Next.Next.Value));

            //sum += Vector2D.AngleBetweenVector(new Vector2D(item.Value, item.Next.Value),
            //                            new Vector2D(item.Next.Value, item.Next.Next.Value));

            //sum += Point2D.AngleBetweenVector(Point2D.GetNormVector(item.Value, item.Next.Value),
            //                                            Point2D.GetNormVector(item.Next.Value, item.Next.Next.Value));
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
                    return false;
                else
                    return true;
            }
        }

        public override string Name => base.Name + " v" + QuantityVertices + " c" + Center;
        public override string ToString() => base.Name + " v" + QuantityVertices; //GetType().Name + " v" + QuantityVertices;
        public string VerticesToString(string separator = " ") => string.Join(separator, _head);//string.Join(separator, Vertices as object[]);

        public static Polygon2D GetPolygonFromCoordinateSystem(List<Point> ps, Point origin)
        {
            Polygon2D newP = new Polygon2D();
            for (int i = 0; i < ps.Count; i++)
                newP.Add(Point2D.ToPoint2DFromCoordinateSystem(origin, ps[i]));
            return newP;
        }
        public Polygon2D GetPolygonInCoordinateSystem(Point origin)
        {
            Polygon2D newP = new Polygon2D();
            foreach (Node<Point2D> item in _head)
                newP.Add(item.Value.ToPointInCoordinateSystem(origin));
            return newP;
        }
        public Point[] VerticesToPoint
        {
            get
            {
                Point[] p = new Point[QuantityVertices];
                Node<Point2D> currentNode = _head;
                for (int i = 0; i < QuantityVertices; i++, currentNode = currentNode.Next)
                    p[i] = currentNode.Value;
                return p;
            }
        }

        public override void Draw(Graphics graph, Pen pen) => graph.DrawPolygon(pen, VerticesToPoint);
    }
}
