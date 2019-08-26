using System;
using System.Collections;
using System.Collections.Generic;

namespace Plane2D
{
    public class PolygonVertex2D : Point2D, IEnumerable<PolygonVertex2D>
    {
        public PolygonVertex2D Next { get; internal set; } // ПОЧЕМУ нельзя protected???
        public PolygonVertex2D Previous { get; internal set; }

        public int Count { get; protected set; } = 0;
        protected bool _isEmpty = true;

        protected PolygonVertex2D(Point2D p) : base(p.X, p.Y)
        {
            Count = 1;
            _isEmpty = false;
        }

        public PolygonVertex2D(Point2D[] vertices) : base(vertices[0].X, vertices[0].Y)
        {            
            if (vertices.Length < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");
            Add(vertices);
        }

        protected virtual PolygonVertex2D CreateVertex(Point2D p)
        {
            PolygonVertex2D pv = new PolygonVertex2D(p)
            {
                _isEmpty = false
            };
            return pv;
        }

        protected void Add(Point2D p)
        {
            if (!_isEmpty || p == null)
                throw new ArgumentException(nameof(p));
            PolygonVertex2D pv = CreateVertex(p);

            if (Next == null)
            {
                Next = pv;
                Previous = pv;
                pv.Next = this;
                pv.Previous = this;
            }
            else
            {
                Previous.Next = pv;
                pv.Previous = Previous;
                pv.Next = this;
                Previous = pv;
            }
            Count++;
        }

        protected void Add(Point2D[] ps)
        {
            if (!_isEmpty || ps == null || ps.Length < 3)
                throw new ArgumentOutOfRangeException("You can add vertices only to the main vertex");

            X = ps[0].X;
            Y = ps[0].Y;
            Count = 1;

            for (int i = 1; i < ps.Length; i++)
                Add(ps[i]);
            _isEmpty = false;
        }

        public double Angle => Vector2D.AngleBetweenVectors(Previous, this, Next);

        public double AngleDegree => (Vector2D.AngleBetweenVectors(Previous, this, Next) / Math.PI) * 180;

        public Segment2D MiddleLine => new Segment2D(new Point2D((X + Previous.X) / 2, (Y + Previous.Y) / 2), new Point2D((X + Next.X) / 2, (Y + Next.Y) / 2));

        
        #region IEnumerable
        public IEnumerator GetEnumerator()
        {
            if (Next == Previous) throw new ArgumentException("Polygon must have at least three vertices");

            PolygonVertex2D currentNode = this;
            do
            {
                yield return currentNode;
                currentNode = currentNode.Next;
            } while (currentNode != this);//} while (currentNode != null && currentNode != this); //кольцевой список
        }

        IEnumerator<PolygonVertex2D> IEnumerable<PolygonVertex2D>.GetEnumerator()
        {
            //return (IEnumerator<PolygonVertex2D>)GetEnumerator();
            if (Next == Previous) throw new ArgumentException("Polygon must have at least three vertices");

            PolygonVertex2D currentNode = this;
            do
            {
                yield return currentNode;
                currentNode = currentNode.Next;
            } while (currentNode != this);//} while (currentNode != null && currentNode != this); //кольцевой список
        }
        #endregion
    }
}
