using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    /// <summary>
    /// Point defined by coordinates: (X, Y)
    /// </summary>
    public class Point2D : ICloneable //: ITransformation
    {
        public const double epsilon = 0.0000001;

        public Point2D(double x, double y) { X = x; Y = y; }
        private Point2D() { }

        public double X { get; protected set; }
        public double Y { get; protected set; }
        public double Distance(Point2D point) => Distance(this, point);
        public static double Distance(Point2D A, Point2D B) => Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        public static Point2D Middle(Point2D A, Point2D B) => new Point2D((A.X + B.X) / 2, (A.Y + B.Y) / 2);

        public virtual Point2D Shift(double dx, double dy) => new Point2D(X + dx, Y + dy);
        public virtual Point2D Rotate(double angle, Point2D center)
        {
            if (Math.Abs(angle) < epsilon)
                return this;
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


        //public virtual void Draw(Graphics graph, Pen pen) => graph.DrawLine(pen, this, this);

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

        /// <summary>
        /// closest coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>min: if coordinate less than min, 0: if coordinate between min and max, max: if coordinate more than max</returns>
        private double IntoRectangle(double coordinate, double min, double max)
        {
            if (min - coordinate > epsilon)
                return min;
            if (coordinate - max > epsilon)
                return max;
            else
                return 0;
        }
        private bool OnRectangle(Point2D LeftBottom, Point2D RightTop)
        {
            return ((Math.Abs(X - LeftBottom.X) < epsilon ||
                    Math.Abs(X - RightTop.X) < epsilon) &&
                    IntoRectangle(Y, LeftBottom.Y, RightTop.Y) == 0) ||
                    ((Math.Abs(Y - LeftBottom.Y) < epsilon ||
                    Math.Abs(Y - RightTop.Y) < epsilon) &&
                    IntoRectangle(X, LeftBottom.X, RightTop.X) == 0);
        }
        private bool IntoRectangle(Point2D LeftBottom, Point2D RightTop)
        {
            return (LeftBottom.X < X && X < RightTop.X) && (LeftBottom.Y < Y && Y < RightTop.Y);
        }
        public PointPosition WhatQuarter
        {
            get
            {
                if (Math.Abs(X) < epsilon)
                    return PointPosition.onAxisY;
                if (Math.Abs(Y) < epsilon)
                    return PointPosition.onAxisX;
                if (X > 0)
                {
                    if (Y > 0)
                        return PointPosition.firstQuarter;
                    else
                        return PointPosition.fourthQuarter;
                }
                else
                {
                    if (Y > 0)
                        return PointPosition.secondQuarter;
                    else
                        return PointPosition.thirdQuarter;
                }
            }
        }



        public override bool Equals(object obj)
        {
            if (!(obj is Point2D p))
                return false;
            return X.Equal(p.X) && Y.Equal(p.Y);
            //return (Math.Abs(X - p.X) < epsilon && Math.Abs(Y - p.Y) < epsilon);
        }
        public override int GetHashCode() => (int)X ^ (int)Y;

        public static bool operator ==(Point2D obj1, Point2D obj2)
        {
            return Equals(obj1, obj2);
        }
        public static bool operator !=(Point2D obj1, Point2D obj2)
        {
            return !Equals(obj1, obj2);
        }

        public override string ToString() => String.Format("({0},{1})", X, Y);

        #region transformation to System.Drawing
        public static implicit operator PointF(Point2D p) => new PointF((float)p.X, (float)p.Y);
        public static implicit operator Point2D(PointF p) => new Point2D(p.X, p.Y);
        public static implicit operator Point(Point2D p) => new Point((int)p.X, (int)p.Y);
        public static implicit operator Point2D(Point p) => new Point2D(p.X, p.Y);

        //TODO выкинуть из: класса и наследников?
        public Point ToPointInCoordinateSystem(Point origin) => new Point(origin.X + (int)X, origin.Y - (int)Y);
        //TODO выкинуть из: класса и наследников?
        public static Point2D ToPoint2DFromCoordinateSystem(Point origin, Point p) =>
            new Point2D(p.X - origin.X, origin.Y - p.Y);
        #endregion

        public object Clone() => new Point2D(X, Y);

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
