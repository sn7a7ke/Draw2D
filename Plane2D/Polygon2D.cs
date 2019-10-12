using System;
using System.Collections.Generic;
using System.Text;

namespace Plane2D
{
    /// <summary>
    /// Polygon (n-gon; n>=3) defined by vertices (points): (X1, Y1) ... (Xn, Yn)
    /// </summary>
    public class Polygon2D : IShape2D
    {
        protected IVertices<PolygonVertex2D> vertices;

        public Polygon2D(params Point2D[] vertices) : this(new Vertices<PolygonVertex2D>(PolygonVertex2D.CreateVertices(vertices ?? throw new ArgumentNullException(nameof(vertices)))))
        {
        }

        public Polygon2D(IVertices<PolygonVertex2D> vertices)
        {
            this.vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));

            QuantityVertices = vertices.Count;
        }

        public int QuantityVertices { get; private set; }

        public PolygonVertex2D this[int index]
        {
            get
            {
                if (index < 0 || index >= QuantityVertices)
                    throw new ArgumentOutOfRangeException(nameof(index));
                PolygonVertex2D current = vertices.Head;
                for (int i = 0; i < index; i++, current = current.Next)
                    ;
                return current;
            }
        }

        public PolygonVertex2D this[Point2D point2D]
        {
            get
            {
                if (point2D == null)
                    return null;
                PolygonVertex2D current = vertices.Head;
                for (int i = 0; i < QuantityVertices; i++, current = current.Next)
                    if (point2D == current)
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
                foreach (PolygonVertex2D item in vertices)
                    verticies.Add(item);
                return verticies.ToArray();
            }
        }


        #region IShape
        public virtual bool IsConvex
        {
            get
            {
                if (QuantityVertices < 4)
                    return true;
                if (AngleSum().Equal(AngleSumForConvex(QuantityVertices)))
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
                foreach (PolygonVertex2D item in vertices)
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
                foreach (PolygonVertex2D item in vertices)
                    perim += item.Distance(item.Next);
                return perim;
            }
        }

        public virtual double Square
        {
            get
            {
                if (!IsWithSelfIntersect)
                {
                    double sum = 0;
                    foreach (PolygonVertex2D v in vertices)
                        sum += (v.X + v.Next.X) * (v.Y - v.Next.Y);
                    return Math.Abs(sum) / 2;
                }
                else
                    return -1;//==== КАК!?? ===                
            }
        }

        public double MaxX => Point2D.Max(GetVertices).X;

        public double MaxY => Point2D.Max(GetVertices).Y;

        public double MinX => Point2D.Min(GetVertices).X;

        public double MinY => Point2D.Min(GetVertices).Y;

        public virtual IMoveable2D Shift(double dx, double dy)
        {
            Point2D[] vers = GetVertices;
            for (int i = 0; i < vers.Length; i++)
                vers[i] = (Point2D)vers[i].Shift(dx, dy);
            return new Polygon2D(vers);
        }

        public virtual IMoveable2D RotateAroundThePoint(double angle, Point2D center)
        {
            if (angle.IsZero())
                return this;
            Point2D[] vers = GetVertices;
            for (int i = 0; i < vers.Length; i++)
                vers[i] = (Point2D)vers[i].RotateAroundThePoint(angle, center);
            return new Polygon2D(vers);
        }

        public virtual IShape2D RotateAroundTheCenterOfShape(double angle) => (Polygon2D)RotateAroundThePoint(angle, Center);

        public virtual IMoveable2D RotateAroundTheCenterOfCoordinates(double angle) => (Polygon2D)RotateAroundThePoint(angle, new Point2D(0, 0));

        public virtual IMoveable2D SymmetryAboutPoint(Point2D center)
        {
            Point2D[] vers = GetVertices;
            for (int i = 0; i < vers.Length; i++)
                vers[i] = (Point2D)vers[i].SymmetryAboutPoint(center);
            return new Polygon2D(vers);
        }
        #endregion


        //TODO переделать логику
        protected double AngleSum()
        {
            double sum = 0;
            foreach (PolygonVertex2D item in vertices)
                sum += new Vector2D(item, item.Next).AngleBetweenVectors(new Vector2D(item.Next, item.Next.Next));

            sum = Math.PI * QuantityVertices - sum;
            return sum;
        }

        public static double AngleSumForConvex(int n) => n < 3 ? 0 : (n - 2) * Math.PI;

        public virtual bool IsWithSelfIntersect
        {
            get
            {
                if (QuantityVertices < 4)
                    return false;
                Segment2D chekedSegment;
                Segment2D anotherSegment;
                for (int i = 0; i < QuantityVertices - 3; i++)
                {
                    chekedSegment = new Segment2D(this[i], this[i].Next);
                    for (int j = i + 2; j < QuantityVertices; j++)
                    {
                        anotherSegment = new Segment2D(this[j], this[j].Next);
                        if (Segment2D.IsIntersectSegmentABAndCD(chekedSegment, anotherSegment))
                            return true;
                    }
                }
                return false;
            }
        }

        public PointPositionInRelationToPolygon WhereIsPointInRelationToPolygon(Point2D point2D)
        {
            if (point2D.X >= MaxX && point2D.Y >= MaxY)
                return PointPositionInRelationToPolygon.rightAbove;
            if (point2D.X >= MaxX && point2D.Y <= MinY)
                return PointPositionInRelationToPolygon.rightBelow;
            if (point2D.X <= MinX && point2D.Y <= MinY)
                return PointPositionInRelationToPolygon.leftBelow;
            if (point2D.X <= MinX && point2D.Y >= MaxY)
                return PointPositionInRelationToPolygon.leftAbove;
            if (point2D.X >= MaxX)
                return PointPositionInRelationToPolygon.right;
            if (point2D.X <= MinX)
                return PointPositionInRelationToPolygon.left;
            if (point2D.Y >= MaxY)
                return PointPositionInRelationToPolygon.above;
            if (point2D.Y <= MinY)
                return PointPositionInRelationToPolygon.below;
            return PointPositionInRelationToPolygon.inside;
        }

        public override string ToString() => ToString(string.Empty);

        public virtual string ToString(string format) => GetType().Name + " v" + QuantityVertices + " c" + Center.ToString(format);

        public string VerticesToString(string separator = " ") => string.Join(separator, vertices);

        public enum PointPositionInRelationToPolygon
        {
            left,
            right,
            above,
            below,
            leftAbove,
            leftBelow,
            rightAbove,
            rightBelow,
            inside
        }
    }
}
