using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class Vector2D : Point2D
    {
        public Vector2D(double x, double y) : base(x, y) { }
        public Vector2D(double startX, double startY, double finishX, double finishY) : base(finishX - startX, finishY - startY) { }
        public Vector2D(Point2D point2D) : base(point2D.X, point2D.Y) { }
        public Vector2D(Point2D start, Point2D finish) : base(finish.X - start.X, finish.Y - start.Y) { }

        public Vector2D Add(Vector2D v) => new Vector2D(X + v.X, Y + v.Y);
        public Vector2D Sub(Vector2D v) => new Vector2D(X - v.X, Y - v.Y);
        public Vector2D MulByNumber(double num) => new Vector2D(X * num, Y * num);
        public double VectorProduct(Vector2D B) => VectorProduct(this, B);
        public double ScalarProduct(Vector2D B) => ScalarProduct(this, B);
        public double Length { get => Math.Sqrt(X * X + Y * Y); }
        public double AngleBetweenVectors(Vector2D B) => AngleBetweenVectors(this, B);

        public static double VectorProduct(Vector2D A, Vector2D B) => A.X * B.Y - A.Y * B.X;
        public static double ScalarProduct(Vector2D A, Vector2D B) => A.X * B.X + A.Y * B.Y;
        //public static double LengthVector(Vector2D A) => Math.Sqrt(A.X * A.X + A.Y * A.Y);
        
        // Если угол больше 180 градусов ??????????????????????????????????????
        public static double AngleBetweenVectors(Vector2D A, Vector2D B)
        {
            double scalar = ScalarProduct(A, B);
            double lenTwo = (A.Length * B.Length);
            //double lenTwo = (LengthVector(A) * LengthVector(B));
            if (Math.Abs(Math.Abs(scalar) - lenTwo) < epsilon)
                lenTwo = scalar;
            double angle = Math.Acos(scalar / lenTwo);
            return angle;
        }

        public static double AngleBetweenVectors(Point2D beforePoint, Point2D anglePoint, Point2D afterPoint)
        {
            Vector2D A = new Vector2D(beforePoint, anglePoint);
            Vector2D B = new Vector2D(afterPoint, anglePoint);
            return AngleBetweenVectors(A, B);
        }

        public override Point2D Shift(double dx, double dy) => new Vector2D(new Point2D(dx, dy));
        public override Point2D Rotate(double angle, Point2D center) => new Vector2D(base.Rotate(angle, center));
        public override Point2D Symmetry(Point2D center) => new Vector2D(base.Symmetry(center));

        public override void Draw(Graphics graph, Pen pen)
        {
            //pen.CustomEndCap = new AdjustableArrowCap(5, 5, false);
            // pen СТРЕЛОЧКА!!!
            graph.DrawLine(pen, new Point(0, 0), this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2D p))
                return false;
            return (X == p.X && Y == p.Y);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
