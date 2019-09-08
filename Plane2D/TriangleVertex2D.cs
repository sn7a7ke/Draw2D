using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class TriangleVertex2D : PolygonVertex2D
    {
        public TriangleVertex2D() : this(new Point2D(0, 0))
        {
        }

        public TriangleVertex2D(Point2D p) : base(p)
        {
        }

        public override PolygonVertex2D FactoryMethod(Point2D point2D) => new TriangleVertex2D(point2D);

        internal new static TriangleVertex2D[] CreateVertices(Point2D[] point2Ds)
        {
            int length = point2Ds.Length;
            TriangleVertex2D[] pv = new TriangleVertex2D[length];
            for (int i = 0; i < length; i++)
                pv[i] = new TriangleVertex2D(point2Ds[i]);
            return pv;
        }

        public Segment2D OppositeSide => new Segment2D(Next, Previous);

        public Segment2D Altitude => new Segment2D(this, new Line2D(Previous, Next).IntersectPerpendicularFromPointWithLine(this));

        public Segment2D Median => new Segment2D(this, new Point2D((Previous.X + Next.X) / 2, (Previous.Y + Next.Y) / 2));

        public Segment2D Bisector
        {
            get
            {
                double lambda = ((TriangleVertex2D)Next).OppositeSide.Length / ((TriangleVertex2D)Previous).OppositeSide.Length;
                double xx = GetKoordinate(lambda, Previous.X, Next.X);
                double yy = GetKoordinate(lambda, Previous.Y, Next.Y);
                return new Segment2D(this, new Point2D(xx, yy));
            }
        }

        private double GetKoordinate(double lambda, double oneK, double twoK)
        {
            if (oneK <= twoK)
                return (oneK + lambda * twoK) / (1 + lambda);
            else
            {
                double u = 1 / lambda;
                return (oneK * u + twoK) / (1 + u);
            }
        }
    }
}
