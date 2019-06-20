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
    public class Line2D : IFunction2D
    {
        public Line2D(double A, double B, double C)
        {
            //if (A == 0 && B == 0)
            if (A.IsZero() && B.IsZero())
                throw new ArgumentOutOfRangeException("A and B should not be zero at the same time");
            this.A = A;
            this.B = B;
            this.C = C;
        }
        public Line2D(double k, double b) : this(1, -k, -b)
        {
            //A = 1;
            //B = -k;
            //C = -b;
        }
        public Line2D(Point2D p1, Point2D p2) : this(p2.Y - p1.Y, p1.X - p2.X, p1.Y * p2.X - p1.X * p2.Y)
        {
            //A = p2.Y - p1.Y;
            //B = p1.X - p2.X;
            //C = p1.Y * p2.X - p1.X * p2.Y;
        }
        public Line2D(Vector2D vNormal, Point2D pVia) : this(vNormal.X, vNormal.Y, -vNormal.X * pVia.X - vNormal.Y * pVia.Y)
        {
            //A = vNormal.X;
            //B = vNormal.Y;
            //C = -A * pVia.X - B * pVia.Y;
        }

        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public bool IsLineInKB => !B.IsZero();//B != 0;
        /// <summary>
        /// y = k * x + b; where k - Kk, b - Kb
        /// </summary>
        public double? Kk 
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

        public bool IsParallel(Line2D l) => (A * l.B - B * l.A).IsZero();//A * l.B - B * l.A == 0;
        public bool IsParallelAxisX() => A.IsZero();//A == 0;
        public bool IsParallelAxisY() => B.IsZero();//B == 0;
        public bool IsPerpendicular(Line2D l) => (A * l.A + B * l.B).IsZero();//A * l.A + B * l.B == 0;
        public bool IsOnLine(Point2D p) => (A * p.X + B * p.Y + C).IsZero();//A * p.X + B * p.Y + C == 0;

        public double DistanceFromPointToLine(Point2D p) => Math.Abs(A * p.X + B * p.Y + C) / Math.Sqrt(A * A + B * B);
        public Vector2D GetNormal => new Vector2D(A, B);


        #region IFunction2D
        public double MaxX
        {
            get
            {
                if (B.IsZero()) //(B == 0)
                    return -C / A;
                else
                    return double.PositiveInfinity;
            }
        }

        public double MaxY
        {
            get
            {
                if (A.IsZero()) //(A == 0)
                    return (double)Kb;
                else
                    return double.PositiveInfinity;
            }
        }

        public double MinX
        {
            get
            {
                if (B.IsZero()) //(B == 0)
                    return -C / A;
                else
                    return double.NegativeInfinity;
            }
        }

        public double MinY
        {
            get
            {
                if (A.IsZero()) //(A == 0)
                    return (double)Kb;
                else
                    return double.NegativeInfinity;
            }
        }

        /// <summary>
        /// function value: y = f(x)
        /// </summary>
        /// <param name="x"></param>
        /// <returns>y</returns>
        public List<double> FuncYFromX(double x)
        {
            if (B.IsZero()) //(B == 0)
                return null;
            else
                return new List<double>() { (-A * x - C) / B };
        }

        public List<double> InverseFuncXFromY(double y)
        {
            if (A.IsZero()) //(A == 0)
                return null;
            else
                return new List<double>() { (-B * y - C) / A };

        }
        public Line2D GetTangent(Point2D p)
        {
            if (FuncYFromX(p.X).Contains(p.Y, new DoubleComparer()))
                return this;
            return null;
        }

        #endregion


        public Line2D PerpendicularFromPoint(Point2D p) => new Line2D(-B, A, B * p.X - A * p.Y);
        public Point2D IntersectPerpendicularFromPointWithLine(Point2D p)
        {
            double zz = A * A + B * B;
            double xx = (B * (B * p.X - A * p.Y) - A * C) / zz;
            double yy = (A * (-B * p.X + A * p.Y) - B * C) / zz;
            return new Point2D(xx, yy);
        }
        public Point2D Intersect(Line2D line)
        {
            if (IsParallel(line))
                return null;
            double xx;
            double yy;
            double denominator = A * line.B - B * line.A;
            yy = (C * line.A - A * line.C) / denominator;
            xx = (B * line.C - C * line.B) / denominator;
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
            if (det.IsZero())
                return true;
            return false;
        }
        public override int GetHashCode() => (A.IsZero()) ? (int)(A / B * 101 + C / B) : (int)(B / A * 101 + C / A); //(A != 0) ? (int)(B / A * 101 + C / A) : (int)(A / B * 101 + C / B);

        public static bool operator ==(Line2D obj1, Line2D obj2) => obj1.Equals(obj2);
        public static bool operator !=(Line2D obj1, Line2D obj2) => !obj1.Equals(obj2);
    }
}
