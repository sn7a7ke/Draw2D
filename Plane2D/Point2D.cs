using System;
using System.Drawing;

namespace Plane2D
{
    /// <summary>
    /// Point defined by coordinates: (X, Y)
    /// </summary>
    public class Point2D : ICloneable, IMoveable2D, IPoint2D
    {
        public Point2D(double x, double y) : this(x, y, string.Empty)
        {
        }

        public Point2D(double x, double y, string name) { X = x; Y = y; Name = name; }

        public double X { get; protected set; }

        public double Y { get; protected set; }

        public string Name { get; protected set; }

        public double Distance(Point2D point) => Distance(this, point);

        public static double Distance(Point2D A, Point2D B) => Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));

        public static Point2D Middle(Point2D A, Point2D B) => new Point2D((A.X + B.X) / 2, (A.Y + B.Y) / 2);

        #region IMoveable2D
        public virtual IMoveable2D Shift(double dx, double dy) => new Point2D(X + dx, Y + dy);

        public virtual IMoveable2D RotateAroundThePoint(double angle, Point2D center)
        {
            if (Math.Abs(angle).IsZero()) 
                return this;
            double xx = (X - center.X) * Math.Cos(angle) - (Y - center.Y) * Math.Sin(angle) + center.X;
            double yy = (X - center.X) * Math.Sin(angle) + (Y - center.Y) * Math.Cos(angle) + center.Y;
            return new Point2D(xx, yy);
        }

        public virtual IMoveable2D RotateAroundTheCenterOfCoordinates(double angle) => RotateAroundThePoint(angle, new Point2D(0, 0));

        public virtual IMoveable2D SymmetryAboutPoint(Point2D center) => new Point2D(2 * center.X - X, 2 * center.Y - Y);
        #endregion

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

        /// <summary>
        /// Distance to closest point on rectangle
        /// </summary>
        /// <param name="LeftBottom">point of rectangle</param>
        /// <param name="RightTop">point of rectangle</param>
        /// <returns>0: if belongs rectangle, positive: (distance to) if outside, negative: if inside </returns>
        public double DistanceToRectangle(Point2D LeftBottom, Point2D RightTop)
        {
            if (LeftBottom.X >= RightTop.X || LeftBottom.Y >= RightTop.Y)
                throw new ArgumentOutOfRangeException("wrong set LeftBottom and RightTop");
            if (OnRectangle(LeftBottom, RightTop))
                return 0;
            if (IntoRectangle(LeftBottom, RightTop))
                return -1;
            double xx = IntoRectangle(X, LeftBottom.X, RightTop.X);
            double yy = IntoRectangle(Y, LeftBottom.Y, RightTop.Y);
            if (xx == 0)
                return Distance(new Point2D(X, yy));
            if (yy == 0)
                return Distance(new Point2D(xx, Y));
            return Distance(new Point2D(xx, yy));
        }

        private bool OnRectangle(Point2D LeftBottom, Point2D RightTop)
        {
            return ((Math.Abs(X - LeftBottom.X).IsZero() ||
                    Math.Abs(X - RightTop.X).IsZero()) &&
                    IntoRectangle(Y, LeftBottom.Y, RightTop.Y) == 0) ||
                    ((Math.Abs(Y - LeftBottom.Y).IsZero() ||
                    Math.Abs(Y - RightTop.Y).IsZero()) &&
                    IntoRectangle(X, LeftBottom.X, RightTop.X) == 0);
        }

        /// <summary>
        /// closest coordinate
        /// </summary>
        /// <returns>min: if coordinate less than min, 0: if coordinate between min and max, max: if coordinate more than max</returns>
        private double IntoRectangle(double coordinate, double min, double max)
        {
            if (coordinate.Less(min))
                return min;
            if (coordinate.More(max))
                return max;
            else
                return 0;            
        }

        private bool IntoRectangle(Point2D LeftBottom, Point2D RightTop)
        {
            return (LeftBottom.X < X && X < RightTop.X) && (LeftBottom.Y < Y && Y < RightTop.Y);
        }

        public static PointPosition WhatQuarter(Point2D p)
        {
            if (Math.Abs(p.X).IsZero()) 
                return PointPosition.onAxisY;
            if (Math.Abs(p.Y).IsZero()) 
                return PointPosition.onAxisX;
            if (p.X > 0)
            {
                if (p.Y > 0)
                    return PointPosition.firstQuarter;
                else
                    return PointPosition.fourthQuarter;
            }
            else
            {
                if (p.Y > 0)
                    return PointPosition.secondQuarter;
                else
                    return PointPosition.thirdQuarter;
            }
        }

        public PointPosition WhatQuarter() => WhatQuarter(this);

        public PointPosition WhatQuarterRelatively(Point2D p) => (this - p).WhatQuarter();

        public override bool Equals(object obj)
        {            
            if (obj == null || !(obj is Point2D))
                return false;
            return this.Equals(obj as Point2D);
        }

        public bool Equals(Point2D otherPoint)
        {
            if (otherPoint == null)
                return false;
            return X.Equal(otherPoint.X) && Y.Equal(otherPoint.Y);
        }

        public override int GetHashCode() => (int)X ^ (int)Y;

        public static bool operator ==(Point2D obj1, Point2D obj2) => Equals(obj1, obj2);

        public static bool operator !=(Point2D obj1, Point2D obj2) => !Equals(obj1, obj2);

        public static Point2D operator +(Point2D p1, Point2D p2) => new Point2D(p1.X + p2.X, p1.Y + p2.Y);

        public static Point2D operator -(Point2D p1, Point2D p2) => new Point2D(p1.X - p2.X, p1.Y - p2.Y);

        public static Point2D operator +(Point2D p1, double number) => (Point2D)p1.Shift(number, number);

        public static Point2D operator -(Point2D p1, double number) => (Point2D)p1.Shift(-number, -number);

        public override string ToString() => String.Format($"({X}, {Y})");

        public string ToString(string format) => String.Format($"({X.ToString(format)}, {Y.ToString(format)})");


        #region System.Drawing
        public static implicit operator Point(Point2D p) => new Point((int)p.X, (int)p.Y);

        public static implicit operator Point2D(Point p) => new Point2D(p.X, p.Y);
        #endregion

        
        public virtual object Clone() => new Point2D(X, Y);

        public enum PointPosition
        {
            firstQuarter,
            secondQuarter,
            thirdQuarter,
            fourthQuarter,
            onAxisX,
            onAxisY
        }
    }
}
