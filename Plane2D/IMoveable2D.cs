namespace Plane2D
{
    public interface IMoveable2D
    {
        #region Moveable
        IMoveable2D Shift(double dx, double dy);
        IMoveable2D Rotate(double angle, Point2D center);
        IMoveable2D Rotate(double angle);
        IMoveable2D Symmetry(Point2D center);
        #endregion
    }
}