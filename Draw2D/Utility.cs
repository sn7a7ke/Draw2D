using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Plane2D;

namespace Draw2D
{
    class Utility
    {
        //private Graphics graph;
        private Pen pen;
        //private Pen originPen;
        private readonly int deltaBmp;

        public Point Origin { get ; private set; }

        public Utility(Pen pen, Point origin) //Graphics graph, 
        {
            //this.graph = graph ?? throw new ArgumentNullException(nameof(graph));
            this.pen = pen ?? throw new ArgumentNullException(nameof(pen));
            this.Origin = origin;
            deltaBmp = 100;
        }

        public void DrawCoordinateAxes(Graphics graph, int width, int height)
        {            
            Pen _pen = new Pen(pen.Color) { CustomEndCap = new AdjustableArrowCap(5, 5, false) };         
            DrawLine(graph,_pen, new Point(0, Origin.Y - height), new Point(0, Origin.Y));
            DrawLine(graph, _pen, new Point(0, 0), new Point(width - Origin.X - deltaBmp, 0));            
        }

        public void DrawLine(Graphics graph, Pen pen, Point p1, Point p2)
        {
            graph.DrawLine(pen, new Point(Origin.X + p1.X, Origin.Y - p1.Y),
                                new Point(Origin.X + p2.X, Origin.Y - p2.Y));
        }

        public void DrawPolygons(Graphics graph, List<Polygon2D> polygons)
        {
            for (int i = 0; i < polygons.Count; i++)
                graph.DrawPolygon(pen, GetPolygonInCoordinateSystem(polygons[i]));
        }

        public void DrawPoints(Graphics graph, List<Point> points)
        {
            if (points?.Count > 1)
                for (int i = 0; i < points.Count - 1; i++)
                    graph.DrawLine(pen, points[i], points[i + 1]);
        }

        public Point ToPointInCoordinateSystem(Point2D point) =>
            new Point(Origin.X + (int)point.X, Origin.Y - (int)point.Y);

        public Point2D ToPoint2DFromCoordinateSystem(Point p) =>
            new Point2D(p.X - Origin.X, Origin.Y - p.Y);

        public Point[] GetPolygonInCoordinateSystem(Polygon2D polygon2D)
        {
            Point2D[] vers = polygon2D.GetVertices;
            Point[] points = new Point[vers.Length];
            for (int i = 0; i < vers.Length; i++)
                points[i] = ToPointInCoordinateSystem(vers[i]);
            return points;
        }

        public Polygon2D GetPolygonFromCoordinateSystem(List<Point> ps)
        {
            Point2D[] vers = new Point2D[ps.Count];
            for (int i = 0; i < ps.Count; i++)
                vers[i] = ToPoint2DFromCoordinateSystem(ps[i]);
            return new Polygon2D(vers);
        }
    }
}
