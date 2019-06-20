using System.Drawing;

namespace Plane2D
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
        Point ToPointInCoordinateSystem(Point origin);
        string ToString();
        Point2D.PointPosition WhatQuarter();
        Point2D.PointPosition WhatQuarterRelatively(Point2D p);
    }
}