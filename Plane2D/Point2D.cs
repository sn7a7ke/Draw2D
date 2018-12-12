using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class Point2D : ICloneable //: ITransformation
    {
        public const double epsilon = 0.0000001;

        public Point2D(double x, double y) { X = x; Y = y; }
        private Point2D()        {        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Distance(Point2D point) => Distance(this, point);
        public static double Distance(Point2D A, Point2D B) => Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));

        public virtual Point2D Shift(double dx, double dy) => new Point2D(X + dx, Y + dy);
        public virtual Point2D Rotate(double angle, Point2D center)
        {
            double xx = (X - center.X) * Math.Cos(angle) - (Y - center.Y) * Math.Sin(angle) + center.X;
            double yy = (X - center.X) * Math.Sin(angle) + (Y - center.Y) * Math.Cos(angle) + center.Y;
            return new Point2D(xx, yy);
        }
        public virtual Point2D Symmetry(Point2D center) => new Point2D(2 * center.X - X, 2 * center.Y - Y);

        /// <summary>
        /// lower left corner of the circum rectangle
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2D Min(params Point2D[] points)
        {
            Point2D p = (Point2D)points[0].Clone();
            for (int i = 1; i < points.Length; i++)
            {
                if (p.X > points[i].X)
                    p.X = points[i].X;
                if (p.Y > points[i].Y)
                    p.Y = points[i].Y;
            }
            return p;
        }
        /// <summary>
        /// upper right corner of the circum rectangle
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2D Max(params Point2D[] points)
        {
            Point2D p = (Point2D)points[0].Clone();
            for (int i = 1; i < points.Length; i++)
            {
                if (p.X < points[i].X)
                    p.X = points[i].X;
                if (p.Y < points[i].Y)
                    p.Y = points[i].Y;
            }
            return p;
        }


        public virtual void Draw(Graphics graph, Pen pen) => graph.DrawLine(pen, this, this);

        public override bool Equals(object obj)
        {
            if (!(obj is Point2D p))
                return false;
            return (X == p.X && Y == p.Y);
        }
        public override int GetHashCode() => (int)X ^ (int)Y;
        public override string ToString() => String.Format("({0},{1})", X, Y);

        #region transformation to System.Drawing
        public static implicit operator PointF(Point2D p) => new PointF((float)p.X, (float)p.Y);
        public static implicit operator Point2D(PointF p) => new Point2D(p.X, p.Y);
        public static implicit operator Point(Point2D p) => new Point((int)p.X, (int)p.Y);
        public static implicit operator Point2D(Point p) => new Point2D(p.X, p.Y);

        //TODO выкинуть из: класса и наследников?
        public Point ToPointInCoordinateSystem(Point origin) => new Point(origin.X + (int)X, origin.Y - (int)Y);
        //TODO выкинуть из: класса и наследников?
        public static Point2D ToPoint2DFromCoordinateSystem(Point origin, Point p) => new Point2D(p.X - origin.X, origin.Y - p.Y);
        #endregion

        public object Clone() => new Point2D(X, Y);
    }
}
