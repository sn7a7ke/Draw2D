using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    class Triangle2D : Polygon2D
    {
        public Triangle2D(Point2D vertexA, Point2D vertexB, Point2D vertexC) : base(vertexA, vertexB, vertexC)
        {

        }

        public Point2D VertexA => _head;
        public Point2D VertexB => _head.Next;
        public Point2D VertexC => _head.Previous;

        public double EdgeAB => Point2D.Distance(VertexA, VertexB);
        public double EdgeBC => Point2D.Distance(VertexB, VertexC);
        public double EdgeAC => Point2D.Distance(VertexA, VertexC);
        public double AngleA => Angle(EdgeBC, EdgeAB, EdgeAC);
        public double AngleB => Angle(EdgeAC, EdgeAB, EdgeBC);
        public double AngleC => Angle(EdgeAB, EdgeBC, EdgeAC);
        // сомнительная необходимость
        public double AngleADegree => AngleDegree(AngleA);
        public double AngleBDegree => AngleDegree(AngleB);
        public double AngleCDegree => AngleDegree(AngleC);

        public double AltitudeALength;
        public double AltitudeBLength;
        public double AltitudeCLength;
        public double MedianALength;
        public double MedianBLength;
        public double MedianCLength;
        public double BisectorALength;
        public double BisectorBLength;
        public double BisectorCLength;

        public Point2D AltitudeA;
        public Point2D AltitudeB;
        public Point2D AltitudeC;
        public Point2D MedianA;
        public Point2D MedianB;
        public Point2D MedianC;
        public Point2D BisectorA;
        public Point2D BisectorB;
        public Point2D BisectorC;

        public double Circumradius;
        public double Inradius;
        public Point2D Incenter
        {
            get
            {
                // ПРОВЕРИТЬ!!!
                double xx = (EdgeBC * VertexA.X + EdgeAC * VertexB.X + EdgeAB * VertexC.X) / Perimeter;
                double yy = (EdgeBC * VertexA.Y + EdgeAC * VertexB.Y + EdgeAB * VertexC.Y) / Perimeter;
                return new Point2D(xx, yy);
            }
        }


        public Point2D IntersectionAltitudes;
        public Point2D IntersectionMedians;
        public Point2D IntersectionBisectors;



        private double Angle(double opposite, double adjacent1, double adjacent2) =>
            Math.Acos((adjacent1 * adjacent1 + adjacent2 * adjacent2 - opposite * opposite)
                / (2 * adjacent1 * adjacent2));
        private double AngleDegree(double angle) => (angle / Math.PI) * 180;




        public override bool IsConvex => true;

        //public override double Square
        //{
        //    get
        //    {
        //        //Vector2D v1 = new Vector2D()


        //    }
        //}

        public class TriangleVertex : PolygonVertex
        {
            public TriangleVertex(Point2D p) : base(p.X, p.Y)
            {
            }

            public double OppositeSide { get; }
            public double AltitudeLength { get; }
            public double MedianLength { get; }
            public double BisectorLength { get; }

            public Point2D Altitude { get; }
            public Point2D Median { get; }
            public Point2D Bisector { get; }


        }
    }
}
