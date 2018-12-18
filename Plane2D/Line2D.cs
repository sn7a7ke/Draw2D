using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    /// <summary>
    /// Line defined by the equation: A * x + B * y + C = 0;
    /// </summary>
    public class Line2D
    {
        public Line2D(double A, double B, double C)
        {
            if (A == 0 && B == 0)
                throw new ArgumentOutOfRangeException("A and B should not be zero at the same time");
            this.A = A;
            this.B = B;
            this.C = C;
        }
        public Line2D(double k, double b)
        {
            A = 1;
            B = -k;
            C = -b;
        }
        public Line2D(Point2D p1, Point2D p2)
        {
            A = p2.Y - p1.Y;
            B = p1.X - p2.X;
            C = p1.Y * p2.X - p1.X * p2.Y;
        }

        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public bool IsLineInKB => B != 0;
        /// <summary>
        /// y = k * x + b; where k - Kk, b - Kb
        /// </summary>
        public double? Kk //=> IsLineInKB ? (-A / B) : null;//ОШИБКА!
        {
            get
            {
                if (IsLineInKB)
                    return -A / B;
                else
                    return null;
            }
        }
        /// <summary>
        /// y = k * x + b; where k - Kk, b - Kb
        /// </summary>
        public double? Kb
        {
            get
            {
                if (IsLineInKB)
                    return -C / B;
                else
                    return null;
            }
        }
        /// <summary>
        /// function value: y = f(x)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double FFromX(double x) => (-A * x - C) / B;
        public bool IsParallel(Line2D l) => A * l.B - B * l.A == 0;
        public bool IsParallelAxisX() => A == 0;
        public bool IsParallelAxisY() => B == 0;
        public bool IsPerpendicular(Line2D l) => A * l.A + B * l.B == 0;
        public bool IsOnLine(Point2D p) => A * p.X + B * p.Y + C == 0;

        public double DistanceFromPointToLine(Point2D p) => Math.Abs(A * p.X + B * p.Y + C) / Math.Sqrt(A * A + B * B);
        public Vector2D GetNormal => new Vector2D(A, B);
        public Line2D PerpendicularFromPoint(Point2D p)
        {
            if (A != 0)
                return new Line2D(-A, B, A * p.X - B * p.Y);
            else
                return new Line2D(A, -B, B * p.Y - A * p.X);
        }
        public double PerpendicularFromPointLength(Point2D p) => Math.Abs(A * p.X + B * p.Y + C) / Math.Sqrt(A * A + B * B);
        public Point2D PerpendicularFromPointToPointOnLine(Point2D p)
        {
            double zz = A * A + B * B;
            double xx = (B * (B * p.X - A * p.Y) - A * C) / zz;
            double yy = (A * (-B * p.X + A * p.Y) - B * C) / zz;
            return new Point2D(xx, yy);
        }
        public Point2D Intersect(Line2D l)
        {
            if (IsParallel(l))
                return null;
            double xx;
            double yy;
            double denominator = A * l.B - B * l.A;
            yy = (C * l.A - A * l.C) / denominator;
            xx = (B * l.C - C * l.B) / denominator;            
            return new Point2D(xx, yy);
        }
        public double AngleBetweenLines(Line2D l) => Vector2D.AngleBetweenVectors(new Vector2D(A, B), new Vector2D(l.A, l.B));
        

        public override string ToString() => string.Format($"{A} * x + {B} * y + {C} = 0");
        public override bool Equals(object obj)
        {
            if (!(obj is Line2D l))
                return false;
            // Детерминант матрицы 3х3 == 0 => строки взаимно зависимы
            double det = A * l.B - A * l.C - B * l.A + B * l.C + C * l.A - C * l.B;
            if (det == 0)
                return true;
            return false;
        }
        public override int GetHashCode() => (A != 0) ? (int)(B / A * 101 + C / A) : (int)(A / B * 101 + C / B);

        public static bool operator ==(Line2D obj1, Line2D obj2) => obj1.Equals(obj2);
        public static bool operator !=(Line2D obj1, Line2D obj2) => !obj1.Equals(obj2);
    }
}
