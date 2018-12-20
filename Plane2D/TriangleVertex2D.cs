using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class TriangleVertex2D : PolygonVertex2D
    {
        public TriangleVertex2D()
        {
        }
        public TriangleVertex2D(params Point2D[] ps) : base(ps)
        {
        }
        protected TriangleVertex2D(Point2D p) : base(p)
        {
        }

        public Segment2D OppositeSide => new Segment2D(Next, Previous);
        public Segment2D Altitude => new Segment2D(this, new Line2D(Previous, Next).PerpendicularFromPointToPointOnLine(this));        
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

        public Segment2D MiddleLine => new Segment2D(new Point2D((X + Previous.X) / 2, (Y + Previous.Y) / 2), new Point2D((X + Next.X) / 2, (Y + Next.Y) / 2));


        //public double OppositeSide => Distance(Next, Previous);
        //public double AltitudeLength => new Line2D(Previous, Next).DistanceFromPointToLine(this);
        //public double MedianLength => Distance(Median);
        //public double BisectorLength => Distance(Bisector);

        //public Point2D Altitude => new Line2D(Previous, Next).PerpendicularFromPointToPointOnLine(this);
        //public Point2D Median => new Point2D((Previous.X + Next.X) / 2, (Previous.Y + Next.Y) / 2);
        //public Point2D Bisector
        //{
        //    get
        //    {
        //        double lambda = ((TriangleVertex2D)Next).OppositeSide / ((TriangleVertex2D)Previous).OppositeSide;
        //        double xx = GetKoordinate(lambda, Previous.X, Next.X);
        //        double yy = GetKoordinate(lambda, Previous.Y, Next.Y);
        //        return new Point2D(xx, yy);
        //    }
        //}
        //private double GetKoordinate(double lambda, double oneK, double twoK)
        //{
        //    if (oneK <= twoK)
        //        return (oneK + lambda * twoK) / (1 + lambda);
        //    else
        //    {
        //        double u = 1 / lambda;
        //        return (oneK * u + twoK) / (1 + u);
        //    }
        //}

        // поднять в Triangle2D?!
        //public Point2D IntersectionAltitudes { get; }
        //public Point2D IntersectionMedians { get; }
        //public Point2D IntersectionBisectors { get; }
        //public double Circumradius { get; }
        //public double Inradius { get; }

        //public Point2D Incenter
        //{
        //    get
        //    {
        //        // ПРОВЕРИТЬ!!!
        //        double xx = (EdgeBC * VertexA.X + EdgeAC * VertexB.X + EdgeAB * VertexC.X) / Perimeter;
        //        double yy = (EdgeBC * VertexA.Y + EdgeAC * VertexB.Y + EdgeAB * VertexC.Y) / Perimeter;
        //        return new Point2D(xx, yy);
        //    }
        //}




    }
}
