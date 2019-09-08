using System.Collections.Generic;

namespace Plane2D
{
    public interface IVertices<out T> where T : PolygonVertex2D, new()
    {
        int Count { get; }
        Point2D[] GetVertices { get; }
        T Head { get; }

        IEnumerator<T> GetEnumerator();
        //Vertices<U> Transform<U>() where U : T, new();
    }
}