﻿using System;

namespace Plane2D
{
    // Добавить
    // всевозможные построения: public Triangle2D GetTriangle(...);
    // тип: остро, прямо, тупо; равнобедр, равностор
    // три точки на одной прямой или совпадают => exception in Line2D

    public class Triangle2D : Polygon2D
    {
        public TriangleVertex2D A { get; private set; }

        public TriangleVertex2D B { get; private set; }

        public TriangleVertex2D C { get; private set; }

        public Triangle2D(params Point2D[] point2Ds) : base(new Vertices<TriangleVertex2D>(point2Ds))
        {
            if (point2Ds?.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(point2Ds), "The quantity vertices must be equally 3");
            A = (TriangleVertex2D)vertices.Head;
            B = (TriangleVertex2D)A.Next;
            C = (TriangleVertex2D)A.Previous;
        }

        public Point2D IntersectionAltitudes => new Line2D(A.Altitude.A, A.Altitude.B).Intersect(new Line2D(B.Altitude.A, B.Altitude.B));

        public Point2D IntersectionMedians => new Line2D(A.Median.A, A.Median.B).Intersect(new Line2D(B.Median.A, B.Median.B));

        public Point2D IntersectionBisectors
        {
            get
            {
                Line2D l1 = new Line2D(A.Bisector.A, A.Bisector.B);
                Line2D l2 = new Line2D(B.Bisector.A, B.Bisector.B);
                Point2D interPoint2D = l1.Intersect(l2);
                return interPoint2D;
            }
        }

        public Circle2D CircumCircle => Circle2D.GetCircle(A, B, C);

        public Circle2D InscribedCircle => new Circle2D(Incenter, Inradius);

        public double Circumradius => A.OppositeSide.Length * B.OppositeSide.Length * C.OppositeSide.Length / (4 * Square);

        public double Inradius => 2 * Square / Perimeter;

        public Point2D Incenter
        {
            get
            {
                double xx = (A.OppositeSide.Length * A.X + B.OppositeSide.Length * B.X + C.OppositeSide.Length * C.X) / Perimeter;
                double yy = (A.OppositeSide.Length * A.Y + B.OppositeSide.Length * B.Y + C.OppositeSide.Length * C.Y) / Perimeter;
                return new Point2D(xx, yy);
            }
        }


        #region IShape2D
        public override IMoveable2D Shift(double dx, double dy)
        {
            Point2D[] ps = ((Polygon2D)base.Shift(dx, dy)).GetVertices;
            return new Triangle2D(ps[0], ps[1], ps[2]);
        }

        public override IMoveable2D RotateAroundThePoint(double angle, Point2D center)
        {
            Point2D[] ps = ((Polygon2D)base.RotateAroundThePoint(angle, center)).GetVertices;
            return new Triangle2D(ps[0], ps[1], ps[2]);
        }

        public override IShape2D RotateAroundTheCenterOfShape(double angle) => (Triangle2D)RotateAroundThePoint(angle, Center);

        public override IMoveable2D RotateAroundTheCenterOfCoordinates(double angle) => (Triangle2D)RotateAroundThePoint(angle, new Point2D(0, 0));

        public override IMoveable2D SymmetryAboutPoint(Point2D center)
        {
            Point2D[] ps = ((Polygon2D)base.SymmetryAboutPoint(center)).GetVertices;
            return new Triangle2D(ps[0], ps[1], ps[2]);
        }
        #endregion


        public Segment2D OppositeInradius(Point2D vertex)
        {
            TriangleVertex2D triVertex = (TriangleVertex2D)this[vertex];
            if (triVertex == null)
                return null;
            Point2D pointOfTangency = new Line2D(triVertex.OppositeSide.A, triVertex.OppositeSide.B).IntersectPerpendicularFromPointWithLine(InscribedCircle.Center);
            return new Segment2D(InscribedCircle.Center, pointOfTangency);
        }

        public bool IsSimilar(Triangle2D tri)
        {
            if (A.Angle != tri.A.Angle && A.Angle != tri.B.Angle && A.Angle != tri.C.Angle)
                return false;
            if (A.Angle == tri.A.Angle && (B.Angle == tri.B.Angle || B.Angle == tri.C.Angle))
                return true;
            if (A.Angle == tri.B.Angle && (B.Angle == tri.C.Angle || B.Angle == tri.A.Angle))
                return true;
            if (A.Angle == tri.C.Angle && (B.Angle == tri.B.Angle || B.Angle == tri.A.Angle))
                return true;
            return false;
        }
    }
    //isosceles - равнобедренный
}
