using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plane2D;

namespace Plane2D //2
{
    /// <summary>
    /// Polygon (n-gon; n>=3) defined by vertices (points): (X1, Y1) ... (Xn, Yn)
    /// </summary>
    public class Polygon2D : IShape2D
    {
        protected PolygonVertex2D _head;
        public Polygon2D(params Point2D[] vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (vertices.Length < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");
            
            _head = new PolygonVertex2D(vertices);
            QuantityVertices = _head.Count;
        }
        protected Polygon2D(PolygonVertex2D head)
        {
            if (head == null) throw new ArgumentNullException(nameof(head));
            if (head.Count < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");

            _head = head;
            QuantityVertices = _head.Count;
        }

        public int QuantityVertices { get; private set; }
        public PolygonVertex2D this[int index]
        {
            get
            {
                if (index < 0 || index >= QuantityVertices)
                    throw new ArgumentOutOfRangeException(nameof(index));
                PolygonVertex2D current = _head;
                for (int i = 0; i < index; i++, current = current.Next)
                    ;
                return current;
            }
        }
        public PolygonVertex2D this[Point2D point]
        {
            get
            {
                if (point == null)
                    return null;
                PolygonVertex2D current = _head;
                for (int i = 0; i < QuantityVertices; i++, current = current.Next)
                    if (point==current)
                        return current;
                return null;
            }
        }
        public Segment2D this[int vertex1, int vertex2] => new Segment2D(this[vertex1], this[vertex2]);

        public Point2D[] GetVertices
        {
            get
            {
                // Array!?
                List<Point2D> verticies = new List<Point2D>();
                foreach (PolygonVertex2D item in _head)
                    verticies.Add(item);
                return verticies.ToArray();
            }
        }


        #region IShape
        public virtual string Name => ToString() + " S-" + Square;
        public virtual bool IsConvex
        {
            get
            {
                if (QuantityVertices < 4)
                    return true;
                if (AngleSum().Equal(AngleSumForConvex(QuantityVertices)))//(Math.Abs(AngleSum() - AngleSumForConvex(QuantityVertices)) < Point2D.epsilon)
                    return true;
                else
                    return false;
            }
        }
        public virtual Point2D Center
        {
            get
            {
                double xx = 0;
                double yy = 0;
                foreach (PolygonVertex2D item in _head)
                {
                    xx += item.X;
                    yy += item.Y;
                }
                return new Point2D(xx / QuantityVertices, yy / QuantityVertices);
            }
        }
        public virtual double Perimeter
        {
            get
            {
                double perim = 0;
                foreach (PolygonVertex2D item in _head)
                    perim += item.Distance(item.Next);
                return perim;
            }
        }
        public virtual double Square
        {
            get
            {
                if (IsWithoutIntersect)
                {
                    double sum = 0;
                    foreach (PolygonVertex2D v in _head)
                        sum += (v.X + v.Next.X) * (v.Y - v.Next.Y);
                    return Math.Abs(sum) / 2;
                }
                else
                    return -1;//==== КАК!?? ===                
            }
        }
        /// <summary>
        /// Left Bottom Rectangle Vertex containing this Polygon
        /// </summary>
        public virtual Point2D LeftBottomRectangleVertex => Point2D.Min(GetVertices);
        public virtual Point2D RightTopRectangleVertex => Point2D.Max(GetVertices);

        public virtual IMoveable2D Shift(double dx, double dy)
        {
            Point2D[] vers = GetVertices;
            for (int i = 0; i < vers.Length; i++)
                vers[i] = (Point2D)vers[i].Shift(dx, dy);
            return new Polygon2D(vers);
        }
        public virtual IMoveable2D Rotate(double angle, Point2D center)
        {
            if (angle.IsZero())//(angle == 0)
                return this;
            Point2D[] vers = GetVertices;
            for (int i = 0; i < vers.Length; i++)
                vers[i] = (Point2D)vers[i].Rotate(angle, center);
            return new Polygon2D(vers);
        }
        public virtual IMoveable2D Rotate(double angle) => Rotate(angle, Center);
        public virtual IMoveable2D Symmetry(Point2D center)
        {
            Point2D[] vers = GetVertices;
            for (int i = 0; i < vers.Length; i++)
                vers[i] = (Point2D)vers[i].Symmetry(center);
            return new Polygon2D(vers);
        }
        #endregion


        //TODO переделать логику
        protected double AngleSum()
        {
            double sum = 0;
            foreach (PolygonVertex2D item in _head)
                sum += new Vector2D(item, item.Next).AngleBetweenVectors(new Vector2D(item.Next, item.Next.Next));

            sum = Math.PI * QuantityVertices - sum;
            return sum;
        }
        public static double AngleSumForConvex(int n) => n < 3 ? 0 : (n - 2) * Math.PI;

        
        // === НЕ РЕАЛИЗОВАНО!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ===
        public virtual bool IsWithoutIntersect => true;
        // =========================================================


        public override string ToString() => GetType().Name + " v" + QuantityVertices + " c" + Center; //GetType().Name + " v" + QuantityVertices;
        public string VerticesToString(string separator = " ") => string.Join(separator, _head);


        #region System.Drawing
        public static Polygon2D GetPolygonFromCoordinateSystem(List<Point> ps, Point origin)
        {
            Point2D[] vers = new Point2D[ps.Count];
            for (int i = 0; i < ps.Count; i++)
                vers[i] = Point2D.ToPoint2DFromCoordinateSystem(origin, ps[i]);
            return new Polygon2D(vers);
        }
        public virtual Polygon2D GetPolygonInCoordinateSystem(Point origin)
        {
            Point2D[] vers = GetVertices;
            for (int i = 0; i < vers.Length; i++)
                vers[i] = vers[i].ToPointInCoordinateSystem(origin);
            return new Polygon2D(vers);
        }
        public Point[] VerticesToPoint
        {
            get
            {
                Point[] p = new Point[QuantityVertices];
                PolygonVertex2D currentNode = _head;
                for (int i = 0; i < QuantityVertices; i++, currentNode = currentNode.Next)
                    p[i] = currentNode;
                return p;
            }
        }
        #endregion
    }
}
