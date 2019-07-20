using System.Collections.Generic;
using Plane2D;

namespace Draw2D.Canvas
{
    public class Polygons2DOnCanvas
    {
        public List<Polygon2D> List { get; private set; }

        public int Count => List.Count;

        public Polygon2D this[int index]
        {
            get
            {
                if (Count == 0 || index < 0 || index >= Count)
                    return null;
                return List[index];
            }
        }

        public Polygon2D Selected { get; private set; }

        public Polygons2DOnCanvas()
        {
            Clear();
        }

        public bool Add(Polygon2D polygon2D)
        {
            if (polygon2D == null)
                return false;
            List.Add(polygon2D);
            Selected = polygon2D;
            return true;
        }

        public bool Remove(Polygon2D polygon2D)
        {
            if (!List.Remove(polygon2D))
                return false;
            if (Selected == polygon2D)
                Selected = null;
            return true;
        }

        public bool ChangeSelected(Polygon2D newPolygon2D)
        {
            if (Selected == null || newPolygon2D == null || Selected == newPolygon2D)
                return false;
            var id = FindId(Selected);
            List.RemoveAt(id);
            List.Insert(id, newPolygon2D);
            Select(newPolygon2D);
            return true;
        }

        public bool Select(int number)
        {
            Polygon2D poly = this[number];
            if (poly == null || Selected == poly)
                return false;
            Selected = poly;
            return true;
        }

        public bool Select(Polygon2D newSelectedPolygon2D)
        {
            if (newSelectedPolygon2D == null)
                return false;
            var poly = Find(newSelectedPolygon2D);
            if (poly == null || Selected == poly)
                return false;
            Selected = poly;
            return true;
        }

        public bool SelectNext()
        {
            if (Count <= 1)
                return false;
            int id = FindId(Selected);
            if (id != Count - 1)
                Select(++id);
            else
                Select(0);
            return true;
        }

        public bool SelectPrevious()
        {
            if (Count <= 1)
                return false;
            int id = FindId(Selected);
            if (id != 0)
                Select(--id);
            else
                Select(Count - 1);
            return true;
        }

        private Polygon2D Find(Polygon2D polygon2D)
        {
            return List.Find(p => p.Equals(polygon2D));
        }

        private int FindId(Polygon2D polygon2D)
        {
            //поиск id Seleted полигона. Может лучше хранить его id?
            return List.FindIndex(p => p.Equals(polygon2D));
        }

        public void Clear()
        {
            List = new List<Polygon2D>();
            Selected = null;
        }
    }
}
