using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Plane2D;

namespace Draw2D
{
    class Canvas
    {
        private readonly int _deltaBmp;
        private Pen _penForAll;
        private Pen _PenForHighlighting;

        private Bitmap _mainBmp;
        private Graphics _graph;

        public delegate void Source(Image image);

        public Source Input { get; set; }

        public Point Origin { get; private set; }

        public Polygon2D SelectedPolygon2D { get; private set; }

        public List<Polygon2D> Polygons2D { get; private set; }  // vertex in abolute coordinates Origin == (0, 0)

        public List<Point> Points { get; private set; } // points in screen coordinates (Left, Top) == (0, 0)

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Pen PenForAll
        {
            get => _penForAll;

            set
            {
                _penForAll = value ?? _penForAll;
            }
        }

        public Pen PenForHighlighting
        {
            get => _PenForHighlighting;

            set
            {
                _PenForHighlighting = value ?? _PenForHighlighting;
            }
        }

        public Canvas(Point origin, int width, int height) //Graphics graph, 
        {
            Origin = origin;
            Width = width;
            Height = height;
            _deltaBmp = 100;

            PenForAll = new Pen(Color.DarkRed);
            PenForHighlighting = (Pen)PenForAll.Clone();
            PenForHighlighting.Width *= 2;

            ClearAll();
        }


        #region Polygon2D
        public void AddPolygon2D(Polygon2D polygon2D)
        {
            Polygons2D.Add(polygon2D);
            DrawPolygon(polygon2D);
        }

        public void RemovePolygon2D(Polygon2D polygon2D)
        {
            Polygons2D.Remove(polygon2D);
            if (SelectedPolygon2D == polygon2D)
                SelectedPolygon2D = null;
            RefreshPictureBox();
        }

        public void ChangePolygon2D(Polygon2D currentPolygon2D, Polygon2D newPolygon2D)
        {
            var polygon2D = FindPolygon2D(currentPolygon2D);
            polygon2D = newPolygon2D;
            if (SelectedPolygon2D == currentPolygon2D)
                SelectedPolygon2D = newPolygon2D;
            RefreshPictureBox();
        }

        private Polygon2D FindPolygon2D(Polygon2D polygon2D)
        {
            return Polygons2D.Find(p => p.Equals(polygon2D));
        }

        public void SelectPolygon2D(int number)
        {
            int count = Polygons2D.Count;
            if (count > 0 && number >= 0 && number < count)
            {
                Polygon2D newSelectedPolygon2D = Polygons2D[number];
                if (SelectedPolygon2D != newSelectedPolygon2D)
                {
                    SelectedPolygon2D = newSelectedPolygon2D;
                    RefreshPictureBox();
                }                
            }
        }
        #endregion


        #region Points
        public void AddPoint(Point point)
        {
            Points.Add(point);
            int count = Points.Count;
            if (count > 1)
                DrawLine(Points[count - 2], Points[count - 1]);
        }

        public void RemovePoint(Point point)
        {
            int count = Points.Count;
            if (count > 1)
                Points.RemoveAt(count - 1);
            RefreshPictureBox();
        }
        #endregion


        private void RefreshPictureBox() => RefreshPictureBox(Width, Height);

        private void RefreshPictureBox(int width, int height)
        {
            ClearPictureBox();
            DrawPolygons();
            if (SelectedPolygon2D != null)
            {
                _graph.DrawPolygon(PenForHighlighting, GetPolygonInCoordinateSystem(SelectedPolygon2D));
            }
            //_view.OutputText = _mainBmp.Width.ToString() + " " + _mainBmp.Height.ToString();            
            DrawPoints();
            Input?.Invoke(_mainBmp);
        }

        private void ClearPictureBox()
        {
            _mainBmp = new Bitmap(Width, Height);
            _graph = Graphics.FromImage(_mainBmp);
            DrawCoordinateAxes();
        }

        public void ClearAll()
        {
            ClearPictureBox();
            Polygons2D = new List<Polygon2D>();
            SelectedPolygon2D = null;
            Points = new List<Point>();
        }

        private void DrawCoordinateAxes()
        {
            Pen penForCoordAxes = new Pen(PenForAll.Color) { CustomEndCap = new AdjustableArrowCap(5, 5, false) };
            _graph.DrawLine(penForCoordAxes, new Point(0, Origin.Y - Height), new Point(0, Origin.Y));
            _graph.DrawLine(penForCoordAxes, new Point(0, 0), new Point(Width - Origin.X - _deltaBmp, 0));
        }

        private void DrawLine(Point p1, Point p2)
        {
            _graph.DrawLine(PenForAll, new Point(Origin.X + p1.X, Origin.Y - p1.Y),
                                new Point(Origin.X + p2.X, Origin.Y - p2.Y));
        }

        private void DrawPolygon(Polygon2D polygon2D)
        {
            _graph.DrawPolygon(PenForAll, GetPolygonInCoordinateSystem(polygon2D));
        }

        private void DrawPolygons()
        {
            for (int i = 0; i < Polygons2D.Count; i++)
                DrawPolygon(Polygons2D[i]);
        }

        private void DrawPoints()
        {
            if (Points?.Count > 1)
                for (int i = 0; i < Points.Count - 1; i++)
                    DrawLine(Points[i], Points[i + 1]);
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
    }
}
