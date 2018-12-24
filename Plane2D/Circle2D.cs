﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    // создать интерфейс функция
    public class Circle2D : Point2D, IFunction2D, IShape2D
    {
        public Circle2D(double xCenter, double yCenter, double radius) : base(xCenter, yCenter)
        {
            if (radius.LessOrEqual(0.0))
                throw new ArgumentOutOfRangeException("Radius must be more than 0");
            Radius = radius;
        }
        public Circle2D(Point2D Center, double radius) : this(Center.X, Center.Y, radius)
        {
        }
        public double Radius { get; private set; }


        #region IFunction

        public double MaxX => X + Radius;

        public double MaxY => Y + Radius;

        public double MinX => X - Radius;

        public double MinY => Y - Radius;

        public List<double> FuncYFromX(double x)
        {
            if (x.Less(MinX) || x.More(MaxX))
                return null;
            List<double> answer = new List<double>();
            answer.Add(Y + Root(x - X));
            answer.Add(Y - Root(x - X));
            return answer;
        }
        public List<double> InverseFuncXFromY(double y)
        {
            if (y.Less(MinY) || y.More(MaxY))
                return null;
            List<double> answer = new List<double>();
            answer.Add(X + Root(y - Y));
            answer.Add(X - Root(y - Y));
            return answer;
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

        public Point2D LeftBottomRectangleVertex => new Point2D(MinX, MinY);
        public Point2D RightTopRectangleVertex => new Point2D(MaxX, MaxY);

        public string Name => ToString() + " S-" + Square;

        public bool IsConvex => true;

        public double Square => Math.PI * Radius * Radius;

        public double Perimeter => 2 * Math.PI * Radius;

        public Point2D Center => this;

        #endregion

        /// <summary>
        /// Distance to closest point on circle
        /// </summary>
        /// <param name="p"></param>
        /// <returns>0: if belongs circle, positive: (distance to) if outside, negative: if inside </returns>
        public double IsOnTheCircle(Point2D p)
        {
            double temp = Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2) - Radius * Radius;

            //if (Math.Abs(temp) < epsilon)
            if (temp.IsZero())
                return 0;
            if (temp < 0)
                return -1;
            return Distance(p) - Radius;
        }

        public Point2D GetPoint(double angleInRadians)
        {
            double xx = X + Radius * Math.Cos(angleInRadians);
            double yy = Y + Radius * Math.Sin(angleInRadians);
            return new Point2D(xx, yy);
        }

        public Line2D GetTangent(Point2D p)
        {
            if (IsOnTheCircle(p) != 0)
                return null; // or exception?
            int signX = Math.Sign(p.X - X);
            int signY = Math.Sign(p.Y - Y);

            if (p.WhatQuarterRelatively(this) == PointPosition.onAxisX)
                return new Line2D(1, 0, -(X + Radius * signX));
            if (p.WhatQuarterRelatively(this) == PointPosition.onAxisY)
                return new Line2D(0, 1, -(Y + Radius * signY));
            double k = (X - p.X) * signX / Root(X - p.X);
            double b = p.Y - k * p.X;
            return new Line2D(k, b);
        }

        #region override Base

        public override Point2D Shift(double dx, double dy) => new Circle2D(base.Shift(dx, dy), Radius);

        public override Point2D Rotate(double angle, Point2D center) => new Circle2D(base.Rotate(angle, center), Radius);

        public override Point2D Symmetry(Point2D center) => new Circle2D(base.Symmetry(center), Radius);

        public override string ToString() => GetType().Name + " r" + Radius + " c" + Center;

        #endregion

    }
}