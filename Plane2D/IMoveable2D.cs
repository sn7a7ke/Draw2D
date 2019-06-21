namespace Plane2D
{
    public interface IMoveable2D
    {
        #region Moveable
        IMoveable2D Shift(double dx, double dy);
        IMoveable2D RotateAroundThePoint(double angle, Point2D center);
        IMoveable2D RotateAroundTheCenterOfCoordinates(double angle);
        IMoveable2D SymmetryAboutPoint(Point2D center);
        #endregion
    }
}