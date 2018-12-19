using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class Segment2D
    {
        Point2D A;
        Point2D B;

        public Segment2D(Point2D a, Point2D b)
        {
            A = a ?? throw new ArgumentNullException(nameof(a));
            B = b ?? throw new ArgumentNullException(nameof(b));
        }

        public Point2D Middle => Point2D.Middle(A, B);
        public double Length => A.Distance(B);

        public static bool IsIntersectSegment(Segment2D AB, Segment2D CD)
        {
            Vector2D vAB = AB;
            bool IntersectLineABWithSegmentCD = Math.Sign(vAB.VectorProduct(new Vector2D(AB.A, CD.A)) * vAB.VectorProduct(new Vector2D(AB.A, CD.B))) < 0;
            Vector2D vCD = CD;
            bool IntersectLineCDWithSegmentAB = Math.Sign(vCD.VectorProduct(new Vector2D(CD.A, AB.A)) * vCD.VectorProduct(new Vector2D(CD.A, AB.B))) < 0;
            return IntersectLineABWithSegmentCD && IntersectLineCDWithSegmentAB;
        }

        public static bool IsIntersectSegment2(Point2D A, Point2D B, Point2D C, Point2D D)
        {
            return IsIntersectSegment(new Segment2D(A, B), new Segment2D(C, D));
        }

        public static implicit operator Vector2D(Segment2D s) => new Vector2D(s.A, s.B);
    }
}
