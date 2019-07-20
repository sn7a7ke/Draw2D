using System.Collections.Generic;
using System.Drawing;

namespace Draw2D.Canvas
{
    public class PointsOnCanvas
    {
        Point _temporary;
        public Point Temporary
        {
            get => _temporary;
            set
            {
                if (_temporary == value)
                    return;
                _temporary = value;
            }
        }

        public Point First => List.Count > 0 ? List[0] : new Point(0, 0);

        public List<Point> List { get; private set; }

        public int Count => List.Count;

        public PointsOnCanvas()
        {
            Clear();
        }

        public void Add(Point point)
        {
            List.Add(point);
            Temporary = point;
        }

        public void RemoveLast()
        {
            if (Count > 0)
                List.RemoveAt(Count - 1);
        }

        public void Clear()
        {
            List = new List<Point>();
        }
    }
}
