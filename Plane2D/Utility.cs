using System;

namespace Plane2D
{
    public static class Utility
    {
        public static string[] GetDefaultNames(int count)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException("The number names should be positive");
            string[] vertices = new string[count];
            for (int i = 0; i < count; i++)
                vertices[i] = ((char)('A' + i)).ToString();
            return vertices;
        }

        public static Point2D[] CheckAndSetNames(Point2D[] point2Ds)
        {
            for (int i = 0; i < point2Ds.Length; i++)
                if (string.IsNullOrEmpty(point2Ds[i].Name))
                    return SetNames(point2Ds);
            return point2Ds;
        }

        public static Point2D[] SetNames(Point2D[] point2Ds)
        {
            return SetNames(point2Ds, GetDefaultNames(point2Ds.Length));
        }

        public static Point2D[] SetNames(Point2D[] point2Ds, string[] nameOfVertices)
        {
            if (point2Ds == null)
                throw new ArgumentNullException(nameof(point2Ds));
            if (point2Ds?.Length != nameOfVertices?.Length)
                throw new ArgumentOutOfRangeException("The number of points is not equal to the number of their names");
            int length = point2Ds.Length;
            Point2D[] newPoint2Ds = new Point2D[length];
            for (int i = 0; i < length; i++)
                newPoint2Ds[i] = new Point2D(point2Ds[i].X, point2Ds[i].Y, nameOfVertices[i]);
            return newPoint2Ds;
        }
    }
}
