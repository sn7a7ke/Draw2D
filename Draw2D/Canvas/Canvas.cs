using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Plane2D;

namespace Draw2D.Canvas
{
    class Canvas
    {
        public const int minimumQuantityOfVertices = 3;

        private Graphics _graph;
        private readonly int _fontSizeInPixels = 14;
        private readonly string _fontName = "Courier New";
        private readonly Font _font;
        private readonly Brush _brush;

        public Bitmap MainBmp { get; private set; }

        Point _origin;
        public Point Origin
        {
            get => _origin;
            private set => _origin = (value.X < 0 || value.Y < 0)
                ? throw new ArgumentOutOfRangeException(nameof(Origin), "Cordinates isn't negative.")
                : value;
        }

        private int _width;
        public int Width
        {
            get => _width;
            private set => _width = (value >= 0)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(Width), "Sizes of canvas must be positive");
        }

        private int _height;
        public int Height
        {
            get => _height;
            private set => _height = (value >= 0)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(Height), "Sizes of canvas must be positive");
        }

        private Pen _penForAll;
        public Pen PenForAll
        {
            get => _penForAll;
            set => _penForAll = value ?? _penForAll;
        }

        private Pen _PenForHighlighting;
        public Pen PenForHighlighting
        {
            get => _PenForHighlighting;
            set => _PenForHighlighting = value ?? _PenForHighlighting;
        }

        private Pen _penForCoordAxes;
        public Pen PenForCoordAxes
        {
            get => _penForCoordAxes;
            set => _penForCoordAxes = value ?? _penForCoordAxes;
        }

        public Polygons2DOnCanvas Polygons2D { get; private set; } // vertex in abolute coordinates Origin == (0, 0)

        public PointsOnCanvas Points { get; private set; } // points in screen coordinates (Left, Top) == (0, 0)

        public Canvas(Point origin, int width, int height)
        {
            Origin = origin;
            Width = width;
            Height = height;
            PenForAll = new Pen(Color.DarkRed);
            PenForHighlighting = (Pen)PenForAll.Clone();
            PenForHighlighting.Width *= 2;
            PenForCoordAxes = new Pen(PenForAll.Color) { CustomEndCap = new AdjustableArrowCap(5, 5, false) };
            _font = new Font(_fontName, _fontSizeInPixels, GraphicsUnit.Pixel);
            _brush = Brushes.Green;
            Clear();
        }

        public void Resize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Refresh()
        {
            Draw();
            RefreshPolygons();
            RefreshPoints();
        }

        private void RefreshPolygons()
        {
            DrawPolygons();
            DrawSelectedPolygon();
        }

        private void DrawVerticesNames(Polygon2D poly)
        {
            for (int i = 0; i < poly.QuantityVertices; i++)
                _graph.DrawString(poly[i].Name, _font, _brush, ToPointInCoordinateSystem(TextPlaceNew(poly[i], poly)));
        }

        public Point2D[] PointArrayToPoint2DArray(Point[] points)
        {
            var points2D = new Point2D[points.Length];
            for (int i = 0; i < points.Length; i++)
                points2D[i] = points[i];
            return points2D;
        }

        private void RefreshPoints()
        {
            if (Points.List.Count == 0)
                return;
            for (int i = 0; i < Points.List.Count - 1; i++)
                DrawLine(Points.List[i], Points.List[i + 1]);
            DrawLine(Points.List[Points.List.Count - 1], Points.Temporary);
        }

        private void Draw()
        {
            MainBmp = new Bitmap(Width, Height);
            _graph = Graphics.FromImage(MainBmp);
            DrawCoordinateAxes();
        }

        public void Clear()
        {
            Draw();
            Polygons2D = new Polygons2DOnCanvas();
            Points = new PointsOnCanvas();
        }

        private void DrawCoordinateAxes()
        {
            _graph.DrawLine(PenForCoordAxes, new Point(Origin.X, Origin.Y), new Point(Width, Origin.Y));
            _graph.DrawLine(PenForCoordAxes, new Point(Origin.X, Origin.Y), new Point(0, 0));
        }

        private void DrawLine(Point p1, Point p2)
        {
            _graph.DrawLine(PenForAll, new Point(p1.X, p1.Y),
                    new Point(p2.X, p2.Y));
        }

        private void DrawPolygon(Pen penForAll, Polygon2D polygon2D)
        {
            _graph.DrawPolygon(penForAll, GetPolygonInCoordinateSystem(polygon2D));
        }

        private void DrawPolygons()
        {
            for (int i = 0; i < Polygons2D.List.Count; i++)
                DrawPolygon(PenForAll, Polygons2D.List[i]);
        }

        private void DrawSelectedPolygon()
        {
            if (Polygons2D.Selected == null)
                return;
            DrawPolygon(PenForHighlighting, Polygons2D.Selected);
            DrawVerticesNames(Polygons2D.Selected);
        }

        private Point ToPointInCoordinateSystem(Point2D point2D) =>
            new Point(Origin.X + (int)point2D.X, Origin.Y - (int)point2D.Y);

        private Point2D ToPoint2DFromCoordinateSystem(Point point) =>
            new Point2D(point.X - Origin.X, Origin.Y - point.Y);

        private Point[] GetPolygonInCoordinateSystem(Polygon2D polygon2D)
        {
            Point2D[] vers = polygon2D.GetVertices;
            Point[] points = new Point[vers.Length];
            for (int i = 0; i < vers.Length; i++)
                points[i] = ToPointInCoordinateSystem(vers[i]);
            return points;
        }

        private Polygon2D GetPolygonFromCoordinateSystem(List<Point> points)
        {
            Point2D[] vers = new Point2D[points.Count];
            for (int i = 0; i < points.Count; i++)
                vers[i] = ToPoint2DFromCoordinateSystem(points[i]);
            return new Polygon2D(vers);
        }

        public void AddPolygon()
        {
            Polygon2D poly = GetPolygonFromCoordinateSystem(Points.List);
            Points.Clear();
            Polygons2D.Add(poly);
        }

        private Point2D TextPlaceNew(Point2D point2D, Polygon2D polygon2D)
        {
            var previosPoint = Point2D.Middle(polygon2D[point2D].Previous, point2D);
            var nextPoint = Point2D.Middle(polygon2D[point2D].Next, point2D);
            var ort = new Vector2D(new Line2D(previosPoint, nextPoint).IntersectPerpendicularFromPointWithLine(point2D), point2D).Ort;
            var half = _fontSizeInPixels / 2;
            return new Point2D(point2D.X - half + half * ort.X, point2D.Y + half + half * ort.Y);
        }
    }
}
