using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class Polygon2D : Shape2D
    {
        protected PolygonVertex _head;
        public int QuantityVertices { get; private set; }
        public PolygonVertex this[int index]
        {
            get
            {
                if (index < 0 || index >= QuantityVertices)
                    throw new ArgumentOutOfRangeException(nameof(index));
                PolygonVertex current = _head;
                for (int i = 0; i < index; i++, current = current.Next)
                    ;
                return current;
            }
        }
        public Polygon2D(params Point2D[] vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (vertices.Length < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");
            _head = new PolygonVertex(vertices[0]);
            for (int i = 1; i < vertices.Length; i++)
                _head.Add(vertices[i]);
            QuantityVertices = _head.Count;
        }

        public Point2D[] GetVertices()
        {
            // Array!?
            List<Point2D> verticies = new List<Point2D>();
            foreach (PolygonVertex item in _head)
                verticies.Add(item);
            return verticies.ToArray();
        }

        public Point2D Center
        {
            get
            {
                double xx = 0;
                double yy = 0;
                foreach (PolygonVertex item in _head)
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
                foreach (PolygonVertex item in _head)
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

        public Polygon2D Shift(double dx, double dy)
        {
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].Shift(dx, dy);
            return new Polygon2D(vers);
        }

        public Polygon2D Rotate(double angle, Point2D center)
        {
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].Rotate(angle, center);
            return new Polygon2D(vers);
        }
        public Polygon2D Rotate(double angle) => Rotate(angle, Center);
        public Polygon2D Symmetry(Point2D center)
        {
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].Symmetry(center);
            return new Polygon2D(vers);
        }

        //TODO переделать логику
        private double AngleSum()
        {
            double sum = 0;
            foreach (PolygonVertex item in _head)
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
                    return false;
                else
                    return true;
            }
        }

        public override string Name => base.Name + " v" + QuantityVertices + " c" + Center;
        public override string ToString() => base.Name + " v" + QuantityVertices; //GetType().Name + " v" + QuantityVertices;
        public string VerticesToString(string separator = " ") => string.Join(separator, _head);

        public static Polygon2D GetPolygonFromCoordinateSystem(List<Point> ps, Point origin)
        {
            Point2D[] vers = new Point2D[ps.Count];
            for (int i = 0; i < ps.Count; i++)
                vers[i] = Point2D.ToPoint2DFromCoordinateSystem(origin, ps[i]);
            return new Polygon2D(vers);
        }
        public Polygon2D GetPolygonInCoordinateSystem(Point origin)
        {
            Point2D[] vers = GetVertices();
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].ToPointInCoordinateSystem(origin);
            return new Polygon2D(vers);
        }
        public Point[] VerticesToPoint
        {
            get
            {
                Point[] p = new Point[QuantityVertices];
                PolygonVertex currentNode = _head;
                for (int i = 0; i < QuantityVertices; i++, currentNode = currentNode.Next)
                    p[i] = currentNode;
                return p;
            }
        }

        public override void Draw(Graphics graph, Pen pen) => graph.DrawPolygon(pen, VerticesToPoint);


        public class PolygonVertex : Point2D, IEnumerable<PolygonVertex>// : ICloneable
        {
            //PolygonProperty property;
            public int Count { get; private set; } = 0;
            private bool _isHead = true;
            public PolygonVertex(double x, double y) : this(new Point2D(x, y))
            {
            }
            public PolygonVertex(Point2D p) : base(p.X, p.Y)
            {
                Count = 1;
            }

            //public PolygonVertex(Point2D[] ps) : base(p.X, p.Y)
            //{





            //    Count = 1;
            //}
            public void Add(Point2D p)
            {
                if (!_isHead)
                    throw new ArgumentException("You can add vertices only to the main vertex");
                PolygonVertex pv = new PolygonVertex(p)
                {
                    _isHead = false
                };

                if (Next == null)
                {
                    Next = pv;
                    Previous = pv;
                    pv.Next = this;
                    pv.Previous = this;
                }
                else
                {
                    Previous.Next = pv;
                    pv.Previous = Previous;
                    pv.Next = this;
                    Previous = pv;
                }
                Count++;
            }

            public double Angle
            {
                get
                {
                    if (Next == Previous) throw new ArgumentException("Polygon must have at least three vertices");

                    return Vector2D.AngleBetweenVector(Previous, this, Next);
                }
            }

            public PolygonVertex Next { get; private set; }
            public PolygonVertex Previous { get; private set; }

            public IEnumerator GetEnumerator()
            {
                if (Next == Previous) throw new ArgumentException("Polygon must have at least three vertices");

                PolygonVertex currentNode = this;
                do
                {
                    yield return currentNode;
                    currentNode = currentNode.Next;
                } while (currentNode != this);//} while (currentNode != null && currentNode != this); //кольцевой список
            }

            IEnumerator<PolygonVertex> IEnumerable<PolygonVertex>.GetEnumerator()
            {
                if (Next == Previous) throw new ArgumentException("Polygon must have at least three vertices");

                PolygonVertex currentNode = this;
                do
                {
                    yield return currentNode;
                    currentNode = currentNode.Next;
                } while (currentNode != this);//} while (currentNode != null && currentNode != this); //кольцевой список
            }
        }
    }
}
