using System;

namespace Plane2D
{
    public class Segment2D
    {
        public Point2D A { get; private set; }

        public Point2D B { get; private set; }

        public Segment2D(Point2D pointA, Point2D pointB)
        {
            A = pointA ?? throw new ArgumentNullException(nameof(pointA));
            B = pointB ?? throw new ArgumentNullException(nameof(pointB));
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

        public override string ToString() => ToString(string.Empty);

        public string ToString(string format) => String.Format($"[{A.ToString(format)}-{B.ToString(format)}], Length-{Length.ToString(format)}");

        public override bool Equals(object obj)
        {            
            if (obj == null || !(obj is Segment2D))
                return false;
            return this.Equals(obj as Segment2D);
        }

        public bool Equals(Segment2D otherSegment)
        {
            if (otherSegment == null)
                return false;
            bool A1EqualsA2AndB1EqualsB2 = A.X.Equal(otherSegment.A.X) && A.Y.Equal(otherSegment.A.Y) && B.X.Equal(otherSegment.B.X) && B.Y.Equal(otherSegment.B.Y);
            bool A1EqualsB2AndB1EqualsA2 = A.X.Equal(otherSegment.B.X) && A.Y.Equal(otherSegment.B.Y) && B.X.Equal(otherSegment.A.X) && B.Y.Equal(otherSegment.A.Y);
            return A1EqualsA2AndB1EqualsB2 || A1EqualsB2AndB1EqualsA2;
        }

        public override int GetHashCode() => (int)A.X ^ (int)A.Y ^ (int)B.X ^ (int)B.Y;

        public static bool operator ==(Segment2D obj1, Segment2D obj2) => Equals(obj1, obj2);

        public static bool operator !=(Segment2D obj1, Segment2D obj2) => !Equals(obj1, obj2);

        public static implicit operator Vector2D(Segment2D s) => new Vector2D(s.A, s.B);
    }
}
