using System;
using System.Collections;
using System.Collections.Generic;

namespace Plane2D
{
    public class PolygonVertex2D : Point2D
    {
        public PolygonVertex2D Next { get; internal set; } // ПОЧЕМУ нельзя protected???
        public PolygonVertex2D Previous { get; internal set; }

        public PolygonVertex2D() : this(new Point2D(0, 0))
        {
        }

        public PolygonVertex2D(Point2D p) : base(p?.X ??  throw new ArgumentNullException(nameof(p)), p.Y, p.Name)
        {
        }

        public virtual PolygonVertex2D FactoryMethod(Point2D point2D) => new PolygonVertex2D(point2D);

        internal static PolygonVertex2D[] CreateVertices(Point2D[] point2Ds)
        {
            int length = point2Ds.Length;
            Point2D[] newPoint2Ds = Utility.CheckAndSetNames(point2Ds);
            PolygonVertex2D[] pv = new PolygonVertex2D[length];
            for (int i = 0; i < length; i++)
                pv[i] = new PolygonVertex2D(newPoint2Ds[i]);
            return pv;
        }

        public double Angle => Vector2D.AngleBetweenVectors(Previous, this, Next);

        public double AngleDegree => (Vector2D.AngleBetweenVectors(Previous, this, Next) / Math.PI) * 180;

        public Segment2D MiddleLine => new Segment2D(new Point2D((X + Previous.X) / 2, (Y + Previous.Y) / 2), new Point2D((X + Next.X) / 2, (Y + Next.Y) / 2));
    }
}
