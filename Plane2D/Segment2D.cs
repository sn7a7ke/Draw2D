using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public class Segment2D
    {
        public Point2D A { get; private set; }
        public Point2D B { get; private set; }

        public Segment2D(Point2D a, Point2D b)
        {
            A = a ?? throw new ArgumentNullException(nameof(a));
            B = b ?? throw new ArgumentNullException(nameof(b));
        }

        public Point2D Middle => Point2D.Middle(A, B);
        public double Length => A.Distance(B);

        public static bool IsIntersectSegmentABAndCD(Segment2D AB, Segment2D CD)
        {
            Vector2D vAB = AB;
            bool IntersectLineABWithSegmentCD = Math.Sign(vAB.VectorProduct(new Vector2D(AB.A, CD.A)) * vAB.VectorProduct(new Vector2D(AB.A, CD.B))) < 0;
            Vector2D vCD = CD;
            bool IntersectLineCDWithSegmentAB = Math.Sign(vCD.VectorProduct(new Vector2D(CD.A, AB.A)) * vCD.VectorProduct(new Vector2D(CD.A, AB.B))) < 0;
            return IntersectLineABWithSegmentCD && IntersectLineCDWithSegmentAB;
        }

        public static bool IsIntersectSegmentABAndCD(Point2D A, Point2D B, Point2D C, Point2D D)
        {
            return IsIntersectSegmentABAndCD(new Segment2D(A, B), new Segment2D(C, D));
        }

        public override string ToString() => String.Format("[{0}-{1}], Length-{2}", A, B, Length);

        public override bool Equals(object obj)
        {
            if (!(obj is Segment2D s))
                return false;
            bool AEgualsA = A.X.Equal(s.A.X) && A.Y.Equal(s.A.Y) && B.X.Equal(s.B.X) && B.Y.Equal(s.B.Y);
            bool AEgualsB = A.X.Equal(s.B.X) && A.Y.Equal(s.B.Y) && B.X.Equal(s.A.X) && B.Y.Equal(s.A.Y);
            return AEgualsA || AEgualsB;
        }

        public override int GetHashCode() => (int)A.X ^ (int)A.Y ^ (int)B.X ^ (int)B.Y;
        public static bool operator ==(Segment2D obj1, Segment2D obj2) => Equals(obj1, obj2);
        public static bool operator !=(Segment2D obj1, Segment2D obj2) => !Equals(obj1, obj2);


        public static implicit operator Vector2D(Segment2D s) => new Vector2D(s.A, s.B);
    }
}
