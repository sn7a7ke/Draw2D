using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plane2D;

namespace Draw2D
{
    class Utility
    {
        //private Graphics graph;
        private Pen pen;
        //private Pen originPen;
        private Point origin;
        int deltaBmp;

        public Utility(Pen pen, Point origin) //Graphics graph, 
        {
            //this.graph = graph ?? throw new ArgumentNullException(nameof(graph));
            this.pen = pen ?? throw new ArgumentNullException(nameof(pen));
            this.origin = origin;
            deltaBmp = 100;
        }

        public void DrawCoordinateAxes(Graphics graph, int width, int height)
        {            
            Pen _pen = new Pen(pen.Color) { CustomEndCap = new AdjustableArrowCap(5, 5, false) };         
            DrawLineInCoordinateSystem(graph,_pen, new Point(0, origin.Y - height), new Point(0, origin.Y));
            DrawLineInCoordinateSystem(graph, _pen, new Point(0, 0), new Point(width - origin.X - deltaBmp, 0));            
        }
        public void DrawLineInCoordinateSystem(Graphics graph, Pen pen, Point p1, Point p2)
        {
            graph.DrawLine(pen, new Point(origin.X + p1.X, origin.Y - p1.Y),
                                new Point(origin.X + p2.X, origin.Y - p2.Y));
        }
        public void DrawShapes(Graphics graph, List<Polygon2D> polygons)
        {
            for (int i = 0; i < polygons.Count; i++)
                polygons[i].GetPolygonInCoordinateSystem(origin).Draw(graph, pen);
        }
        public void DrawPoints(Graphics graph, List<Point> points)
        {
            if (points.Count > 1)
                for (int i = 0; i < points.Count - 1; i++)
                    graph.DrawLine(pen, points[i], points[i + 1]);
        }

    }
}
