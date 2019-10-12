namespace Plane2D
{
    public interface IShape2D : IMoveable2D, IScope
    {
        bool IsConvex { get; }

        Point2D Center { get; }

        double Perimeter { get; }

        double Square { get; }

        IShape2D RotateAroundTheCenterOfShape(double angle);
    }
}
