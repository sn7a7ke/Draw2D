using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Plane2D;

namespace Draw2D
{
    class Canvas
    {
        public const int minimumQuantityOfVertices = 3;

        private readonly int _deltaBmp;
        private Graphics _graph;

        public Bitmap MainBmp { get; private set; }

        Point _origin;
        public Point Origin
        {
            get => _origin;
            private set
            {
                if (value.X < 0 || value.Y < 0)
                    throw new ArgumentOutOfRangeException(nameof(Origin), "Cordinates isn't negative.");
                _origin = value;
            }
        }

        private int _width;
        public int Width
        {
            get => _width;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Width), "Sizes of canvas must be positive");
                _width = value;
                RefreshPictureBox();
            }
        }

        private int _height;
        public int Height
        {
            get => _height;
            private set
            {
                if (value >= 0)
                    throw new ArgumentOutOfRangeException(nameof(Height), "Sizes of canvas must be positive");
                _height = value;
                RefreshPictureBox();
            }
        }

        private Pen _penForAll;
        public Pen PenForAll
        {
            get => _penForAll;

            set
            {
                _penForAll = value ?? _penForAll;
            }
        }

        private Pen _PenForHighlighting;
        public Pen PenForHighlighting
        {
            get => _PenForHighlighting;

            set
            {
                _PenForHighlighting = value ?? _PenForHighlighting;
            }
        }


        public Polygons2DOnCanvas Polygons2D { get; set; } // vertex in abolute coordinates Origin == (0, 0)

        public PointsOnCanvas Points { get; private set; } // points in screen coordinates (Left, Top) == (0, 0)

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

        private void RefreshPictureBox()
        {
            ClearPictureBox();
            RefreshPolygons();
            //_view.OutputText = _mainBmp.Width.ToString() + " " + _mainBmp.Height.ToString();            
            RefreshPoints();
        }

        private void RefreshPolygons()
        {
            DrawPolygons();
            if (Polygons2D.Selected != null)
            {
                _graph.DrawPolygon(PenForHighlighting, GetPolygonInCoordinateSystem(Polygons2D.Selected));
            }
        }

        private void RefreshPoints()
        {
            if (Points.List.Count > 1)
            {
                for (int i = 0; i < Points.List.Count - 1; i++)
                    DrawLine(Points.List[i], Points.List[i + 1]);
                DrawLine(Points.List[Points.List.Count], Points.Temporary);
            }
        }

        private void ClearPictureBox()
        {
            MainBmp = new Bitmap(Width, Height);
            _graph = Graphics.FromImage(MainBmp);
            DrawCoordinateAxes();
        }

        public void ClearAll()
        {
            ClearPictureBox();
            Polygons2D = new Polygons2DOnCanvas
            {
                Refresh = RefreshPictureBox
            };
            Points = new PointsOnCanvas()
            {
                Refresh = RefreshPictureBox
            };
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
            for (int i = 0; i < Polygons2D.List.Count; i++)
                DrawPolygon(Polygons2D.List[i]);
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


        public class Polygons2DOnCanvas
        {
            public List<Polygon2D> List { get; private set; }

            public Polygon2D Selected { get; private set; }

            public delegate void RefreshPolygons();

            public RefreshPolygons Refresh { get; set; } // for correct work must be filling

            public Polygons2DOnCanvas()
            {
                List = new List<Polygon2D>();
                Selected = null;
            }

            public void Add(Polygon2D polygon2D)
            {
                List.Add(polygon2D);
                Refresh?.Invoke();
            }

            public void Remove(Polygon2D polygon2D)
            {
                List.Remove(polygon2D);
                if (Selected == polygon2D)
                    Selected = null;
                Refresh?.Invoke();
            }

            public void Change(Polygon2D currentPolygon2D, Polygon2D newPolygon2D)
            {
                var polygon2D = Find(currentPolygon2D);
                polygon2D = newPolygon2D;
                if (Selected == currentPolygon2D)
                    Selected = newPolygon2D;
                Refresh?.Invoke();
            }

            private Polygon2D Find(Polygon2D polygon2D)
            {
                return List.Find(p => p.Equals(polygon2D));
            }

            public void Select(int number)
            {
                int count = List.Count;
                if (count > 0 && number >= 0 && number < count)
                {
                    if (Selected != List[number])
                    {
                        Selected = List[number];
                        Refresh?.Invoke();
                    }
                }
            }

            public void Select(Polygon2D newSelectedPolygon2D)
            {
                Selected = Find(newSelectedPolygon2D);
                Refresh?.Invoke();
            }
        }


        public class PointsOnCanvas
        {
            public Point Temporary { get; set; }

            public Point First => List.Count > 0 ? List[0] : new Point(0, 0);

            public List<Point> List { get; private set; }

            public delegate void RefreshPoints();

            public RefreshPoints Refresh { get; set; } // for correct work must be filling

            public PointsOnCanvas()
            {
                List = new List<Point>();
            }

            public void AddPoint(Point point)
            {
                List.Add(point);
                Temporary = point;
                if (List.Count > 1)
                    Refresh?.Invoke();
            }

            public void RemovePoint(Point point)
            {
                if (List.Count > 0)
                    List.RemoveAt(List.Count - 1);
                Refresh?.Invoke();
            }
        }
    }
}
