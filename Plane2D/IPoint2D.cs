﻿namespace Plane2D
{
    public interface IPoint2D : IMoveable2D
    {
        double X { get; }

        double Y { get; }

        object Clone();

        double Distance(Point2D point);

        double DistanceToRectangle(Point2D LeftBottom, Point2D RightTop);

        bool Equals(object obj);

        int GetHashCode();

        string ToString();

        string ToString(string format);

        Point2D.PointPosition WhatQuarter();

        Point2D.PointPosition WhatQuarterRelatively(Point2D p);
    }
}