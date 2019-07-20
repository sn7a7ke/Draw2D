using System.Collections.Generic;
using System.Drawing;

namespace Draw2D.Canvas
{
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
