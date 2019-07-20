using System.Collections.Generic;
using Plane2D;

namespace Draw2D.Canvas
{
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

        public bool Add(Polygon2D polygon2D)
        {
            if (polygon2D == null)
                return false;
            List.Add(polygon2D);
            Refresh?.Invoke();
            return true;
        }

        public bool Remove(Polygon2D polygon2D)
        {
            if (!List.Remove(polygon2D))
                return false;
            if (Selected == polygon2D)
                Selected = null;
            Refresh?.Invoke();
            return true;
        }

        public bool Change(Polygon2D currentPolygon2D, Polygon2D newPolygon2D)
        {
            if (currentPolygon2D == newPolygon2D)
                return false;
            var polygon2D = Find(currentPolygon2D);
            if (polygon2D == null)
                return false;
            polygon2D = newPolygon2D;
            Select(polygon2D);
            Refresh?.Invoke();
            return true;
        }

        public bool Select(int number)
        {
            int count = List.Count;
            if (count == 0 || number < 0 || number >= count || Selected == List[number])
                return false;
            Selected = List[number];
            Refresh?.Invoke();
            return true;
        }

        public bool Select(Polygon2D newSelectedPolygon2D)
        {
            if (newSelectedPolygon2D == null)
                return false;
            var polygon2D = Find(newSelectedPolygon2D);
            if (polygon2D == null || Selected == polygon2D)
                return false;
            Selected = polygon2D;
            Refresh?.Invoke();
            return true;
        }

        public bool SelectNext()
        {
            if (List.Count <= 1)
                return false;
            int idSelected = FindId(Selected);
            Select(++idSelected);
            Refresh?.Invoke();
            return true;
        }

        public bool SelectPrevious()
        {
            if (List.Count <= 1)
                return false;
            int idSelected = FindId(Selected);
            Select(--idSelected);
            Refresh?.Invoke();
            return true;
        }

        private Polygon2D Find(Polygon2D polygon2D)
        {
            return List.Find(p => p.Equals(polygon2D));
        }

        private int FindId(Polygon2D polygon2D)
        {
            return List.FindIndex(p => p.Equals(polygon2D));
        }
    }
}
