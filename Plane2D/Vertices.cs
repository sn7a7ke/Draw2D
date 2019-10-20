using System;
using System.Collections;
using System.Collections.Generic;

namespace Plane2D
{
    public class Vertices<T> : IEnumerable<T>, IVertices<T> where T : PolygonVertex2D, new()
    {
        public T Head { get; protected set; }

        public int Count { get; protected set; } = 0;

        public Vertices(Point2D[] point2Ds) : this(CreateVertices(point2Ds))
        {
        }

        public Vertices(T[] vertices)
        {
            if (vertices?.Length < 3)
                throw new ArgumentOutOfRangeException(nameof(vertices), "The quantity vertices must be no less 3");
            Count = vertices.Length;
            Head = vertices[0];
            for (int i = 1; i < Count; i++)
            {
                if (Head.Next == null)
                {
                    Head.Next = vertices[i];
                    Head.Previous = vertices[i];
                    vertices[i].Next = Head;
                    vertices[i].Previous = Head;
                }
                else
                {
                    Head.Previous.Next = vertices[i];
                    vertices[i].Previous = Head.Previous;
                    vertices[i].Next = Head;
                    Head.Previous = vertices[i];
                }
            }
        }

        public static T[] CreateVertices(Point2D[] point2Ds)
        {
            int length = point2Ds.Length;
            var creator = new T();
            T[] pv = new T[length];
            for (int i = 0; i < length; i++)
                pv[i] = (T)creator.FactoryMethod(point2Ds[i]);
            return pv;
        }

        public Point2D[] GetVertices
        {
            get
            {
                var vs = new Point2D[Count];
                var current = Head;
                for (int i = 0; i < Count; i++)
                {
                    vs[i] = current;
                    current = (T)current.Next;
                }
                return vs;
            }
        }


        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            T currentNode = Head;
            do
            {
                yield return currentNode;
                currentNode = (T)currentNode.Next;
            } while (currentNode != Head);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}
