using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class TriangleVertex2D : PolygonVertex2D
    {
        //public TriangleVertex2D()
        //{
        //}
        public TriangleVertex2D(params Point2D[] ps) : base(ps)
        {
            if (ps.Length != 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be equally 3");
        }
        protected TriangleVertex2D(Point2D p) : base(p)
        {
        }

        protected override PolygonVertex2D CreateVertex(Point2D p)
        {
            PolygonVertex2D pv = new TriangleVertex2D(p)
            {
                _isEmpty = false
            };
            return pv;
        }

        //protected override void Add(Point2D p)
        //{
        //    if (!_isEmpty || p == null)
        //        throw new ArgumentException(nameof(p));
        //    TriangleVertex2D pv = new TriangleVertex2D(p)
        //    {
        //        _isEmpty = false
        //    };

        //    if (Next == null)
        //    {
        //        Next = pv;
        //        Previous = pv;
        //        pv.Next = this;
        //        pv.Previous = this;
        //    }
        //    else
        //    {
        //        Previous.Next = pv;
        //        pv.Previous = Previous;
        //        pv.Next = this;
        //        Previous = pv;
        //    }
        //    Count++;
        //}


        public Segment2D OppositeSide => new Segment2D(Next, Previous);
        public Segment2D Altitude => new Segment2D(this, new Line2D(Previous, Next).IntersectPerpendicularFromPointWithLine(this));
        public Segment2D Median => new Segment2D(this, new Point2D((Previous.X + Next.X) / 2, (Previous.Y + Next.Y) / 2));
        public Segment2D Bisector
        {
            get
            {
                //Vector2D normal1 = new Line2D(this, Next).GetNormal;
                //Vector2D normal2 = new Line2D(this, Previous).GetNormal;
                //Line2D bis = new Line2D(normal1 + normal2, this);
                //Point2D crossPoint = bis.Intersect(new Line2D(Next, Previous));
                //return new Segment2D(this, crossPoint);
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
    }
}
