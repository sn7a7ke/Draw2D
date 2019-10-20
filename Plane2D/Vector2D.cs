using System;

namespace Plane2D
{
    /// <summary>
    /// Vector with center (0,0)
    /// </summary>
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

        public double Length => Distance(new Point2D(0, 0), this);

        public Vector2D Ort => new Vector2D(X / Length, Y / Length);

        public double AngleBetweenVectors(Vector2D B) => AngleBetweenVectors(this, B);

        public static double VectorProduct(Vector2D A, Vector2D B) => A.X * B.Y - A.Y * B.X;

        public static double ScalarProduct(Vector2D A, Vector2D B) => A.X * B.X + A.Y * B.Y;

        // Если угол больше 180 градусов ??????????????????????????????????????
        public static double AngleBetweenVectors(Vector2D A, Vector2D B)
        {
            double scalar = ScalarProduct(A, B);
            double lenTwo = (A.Length * B.Length);
            if (Math.Abs(scalar).Equal(lenTwo)) 
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

        public override IMoveable2D Shift(double dx, double dy) => (Vector2D)Clone();

        public override IMoveable2D RotateAroundThePoint(double angle, Point2D center)
        {
            Point2D zeroPoint = (Point2D) new Point2D(0, 0).RotateAroundThePoint(angle, center);
            Point2D newVectorPoint = (Point2D) base.RotateAroundThePoint(angle, center);
            return new Vector2D(zeroPoint, newVectorPoint);
        }

        public override IMoveable2D RotateAroundTheCenterOfCoordinates(double angle) => new Vector2D((Point2D)new Point2D(X, Y).RotateAroundTheCenterOfCoordinates(angle));

        public override IMoveable2D SymmetryAboutPoint(Point2D center) => new Vector2D((Point2D)new Point2D(X, Y).SymmetryAboutPoint(new Point2D(0, 0)));
                 
        public static Vector2D operator +(Vector2D v1, Vector2D v2) => v1.Add(v2);

        public static Vector2D operator -(Vector2D v1, Vector2D v2) => v1.Sub(v2);

        public static Vector2D operator *(Vector2D v1, double num) => v1.MulByNumber(num);

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector2D))
                return false;
            return this.Equals(obj as Vector2D);
        }

        public bool Equals(Vector2D otherVector)
        {
            if (otherVector == null)
                return false;
            return X.Equal(otherVector.X) && Y.Equal(otherVector.Y);
        }

        public override int GetHashCode() => (int)X ^ (int)Y * 11;

        public static bool operator ==(Vector2D obj1, Vector2D obj2) => Equals(obj1, obj2);

        public static bool operator !=(Vector2D obj1, Vector2D obj2) => !Equals(obj1, obj2);

        public override string ToString() => ToString(string.Empty);

        public override string ToString(string format) => String.Format($"[{base.ToString(format)}], Length-{Length.ToString(format)}");

        public override object Clone() => new Vector2D(X, Y);
    }
}
