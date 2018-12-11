using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class Point2D //: ITransformation
    {
        public const double epsilon = 0.0000001;

        public Point2D(double x, double y) { X = x; Y = y; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Distance(Point2D point) => Distance(this, point);
        public static double Distance(Point2D A, Point2D B) => Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));

        public Point2D Shift(double dx, double dy) => new Point2D(X + dx, Y + dy);
        public Point2D Rotate(double angle, Point2D center)
        {
            double xx = (X - center.X) * Math.Cos(angle) - (Y - center.Y) * Math.Sin(angle) + center.X;
            double yy = (X - center.X) * Math.Sin(angle) + (Y - center.Y) * Math.Cos(angle) + center.Y;
            return new Point2D(xx, yy);
        }
        //public Point2D Rotate(double angle) => Rotate(angle, new Point2D(0, 0));
        public Point2D Symmetry(Point2D center) => new Point2D(2 * center.X - X, 2 * center.Y - Y);
        //public Point2D Symmetry(Point2D A, Point2D B)
        //{
        //    throw new NotImplementedException();
        //}

        public void Draw(Graphics graph, Pen pen) => graph.DrawLine(pen, this, this);

        #region Vector
        public static Point2D GetNormVector(Point2D start, Point2D finish) => new Point2D(finish.X - start.X, finish.Y - start.Y);
        public static double VectorProduct(Point2D A, Point2D B) => A.X * B.Y - A.Y * B.X;
        public static double ScalarProduct(Point2D A, Point2D B) => A.X * B.X + A.Y * B.Y;
        public static double LengthVector(Point2D A) => Math.Sqrt(A.X * A.X + A.Y * A.Y);
        public static double AngleBetweenNormVector(Point2D A, Point2D B)
        {
            double scalar = ScalarProduct(A, B);
            double lenTwo = (LengthVector(A) * LengthVector(B));
            if (Math.Abs(Math.Abs(scalar) - lenTwo) < epsilon)
                lenTwo = scalar;
            double angle = Math.Acos(scalar / lenTwo);
            return angle;
        }



        #endregion



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
        public Point GetPointInCoordinateSystem(Point origin)
        {
            return new Point(origin.X + (int)X, origin.Y - (int)Y);
        }
        //TODO выкинуть из: класса и наследников?
        public static Point2D GetPointFromCoordinateSystem(Point origin, Point p)
        {
            return new Point2D(p.X - origin.X, origin.Y - p.Y);
        }


        #endregion
    }
}
