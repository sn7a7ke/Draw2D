using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    // не надо наследовать от точки?
    public class Circle2D : IFunction2D, IShape2D
    {
        public Circle2D(double xCenter, double yCenter, double radius)
        {
            if (radius.LessOrEqual(0.0))
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be more than 0");
            Radius = radius;
            Center = new Point2D(xCenter, yCenter);
        }

        public Circle2D(Point2D Center, double radius) : this(Center.X, Center.Y, radius)
        {
        }

        public static Circle2D GetCircle(Point2D p1, Point2D p2, Point2D p3)
        {
            Line2D l1 = new Line2D(p1, p2);
            Point2D mp1 = Point2D.Middle(p1, p2);
            Line2D l2;
            Point2D mp2;
            if (l1.IsOnLine(p3))
                return null;
            if (p1.X == p2.X)
            {
                l1 = new Line2D(p1, p3);
                mp1 = Point2D.Middle(p1, p3);
                l2 = new Line2D(p2, p3);
                mp2 = Point2D.Middle(p2, p3);
            }
            else if (p2.X == p3.X)
            {
                l2 = new Line2D(p1, p3);
                mp2 = Point2D.Middle(p1, p3);
            }
            else
            {
                l2 = new Line2D(p2, p3);
                mp2 = Point2D.Middle(p2, p3);
            }
            Point2D center = l1.PerpendicularFromPoint(mp1).Intersect(l2.PerpendicularFromPoint(mp2));
            return new Circle2D(center, center.Distance(p1));
        }

        public double Radius { get; private set; }


        #region IFunction
        public double MaxX => Center.X + Radius;

        public double MaxY => Center.Y + Radius;

        public double MinX => Center.X - Radius;

        public double MinY => Center.Y - Radius;

        public List<double> FuncYFromX(double x)
        {
            if (x.Less(MinX) || x.More(MaxX))
                return null;
            List<double> answer = new List<double>();
            answer.Add(Center.Y + Root(x - Center.X));
            answer.Add(Center.Y - Root(x - Center.X));
            return answer;
        }

        public List<double> InverseFuncXFromY(double y)
        {
            if (y.Less(MinY) || y.More(MaxY))
                return null;
            List<double> answer = new List<double>();
            answer.Add(Center.X + Root(y - Center.Y));
            answer.Add(Center.X - Root(y - Center.Y));
            return answer;
        }

        public Line2D GetTangent(Point2D p)
        {
            if (IsOnTheCircle(p) != 0)
                return null; // or exception?
            int signX = Math.Sign(p.X - Center.X);
            int signY = Math.Sign(p.Y - Center.Y);

            if (p.WhatQuarterRelatively(Center) == Point2D.PointPosition.onAxisX)
                return new Line2D(1, 0, -(Center.X + Radius * signX));
            if (p.WhatQuarterRelatively(Center) == Point2D.PointPosition.onAxisY)
                return new Line2D(0, 1, -(Center.Y + Radius * signY));
            double k = (Center.X - p.X) * signX / Root(Center.X - p.X);
            double b = p.Y - k * p.X;
            return new Line2D(k, b);
        }

        private double Root(double coordinate)
        {
            return Math.Sqrt(UnderRoot(coordinate));
        }

        private double UnderRoot(double coordinate)
        {
            return Radius * Radius - coordinate * coordinate;
        }
        #endregion


        #region IShape
        public bool IsConvex => true;

        public double Square => Math.PI * Radius * Radius;

        public double Perimeter => 2 * Math.PI * Radius;

        public Point2D Center { get; private set; }
        #endregion


        /// <summary>
        /// Distance to closest point on circle
        /// </summary>
        /// <param name="p"></param>
        /// <returns>0: if belongs circle, positive: (distance to) if outside, negative: if inside </returns>
        public double IsOnTheCircle(Point2D p)
        {
            double temp = Math.Pow(p.X - Center.X, 2) + Math.Pow(p.Y - Center.Y, 2) - Radius * Radius;

            if (temp.IsZero())
                return 0;
            if (temp < 0)
                return -1;
            return Center.Distance(p) - Radius;
        }

        public Point2D GetPoint(double angleInRadians)
        {
            double xx = Center.X + Radius * Math.Cos(angleInRadians);
            double yy = Center.Y + Radius * Math.Sin(angleInRadians);
            return new Point2D(xx, yy);
        }


        #region override Base
        public IMoveable2D Shift(double dx, double dy) => new Circle2D((Point2D)Center.Shift(dx, dy), Radius);

        public IMoveable2D RotateAroundThePoint(double angle, Point2D center) => new Circle2D((Point2D)Center.RotateAroundThePoint(angle, center), Radius);

        public IShape2D RotateAroundTheCenterOfShape(double angle) => this;

        public IMoveable2D SymmetryAboutPoint(Point2D center) => new Circle2D((Point2D)Center.SymmetryAboutPoint(center), Radius);

        public IMoveable2D RotateAroundTheCenterOfCoordinates(double angle) => RotateAroundThePoint(angle, new Point2D(0, 0));

        public override string ToString() => ToString(string.Empty); //GetType().Name + " C-" + Center.ToString() + " r-" + Radius;

        public string ToString(string format) => GetType().Name + " C-" + Center.ToString(format) + " r-" + Radius.ToString(format);

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Circle2D))
                return false;
            return this.Equals(obj as Circle2D);
        }

        public bool Equals(Circle2D otherCircle)
        {
            if (otherCircle == null)
                return false;
            return this.GetHashCode() == otherCircle.GetHashCode();
        }

        public static bool operator ==(Circle2D obj1, Circle2D obj2) => Equals(obj1, obj2);

        public static bool operator !=(Circle2D obj1, Circle2D obj2) => !Equals(obj1, obj2);

        public override int GetHashCode() => (int)(100 * Center.GetHashCode() * Radius);
        #endregion
    }
}
