using System;

namespace Plane2D
{
    public interface IShape2D : IMoveable2D
    {
        string Summary { get; }
        bool IsConvex { get; }
        Point2D Center { get; }
        double Perimeter { get; }
        double Square { get; }
        Point2D LeftBottomRectangleVertex { get; }
        Point2D RightTopRectangleVertex { get; }
    }
}
