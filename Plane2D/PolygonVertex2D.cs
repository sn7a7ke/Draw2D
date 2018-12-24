using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Plane2D;

namespace Plane2D //2
{
    public class PolygonVertex2D : Point2D, IEnumerable<PolygonVertex2D>
    {

        public PolygonVertex2D Next { get; internal set; } // ПОЧЕМУ нельзя protected???
        public PolygonVertex2D Previous { get; internal set; }

        public int Count { get; protected set; } = 0;
        protected bool _isEmpty = true;

        //public PolygonVertex2D() : base(0, 0)
        //{
        //}
        protected PolygonVertex2D(Point2D p) : base(p.X, p.Y)
        {
            //if (p == null) throw new ArgumentNullException(nameof(p));
            Count = 1;
            _isEmpty = false;
        }
        public PolygonVertex2D(Point2D[] ps) : base(ps[0].X, ps[0].Y)
        {            
            if (ps.Length < 3)
                throw new ArgumentOutOfRangeException("The quantity vertices must be no less 3");
            Add(ps);
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
            //PolygonVertex2D pv = new PolygonVertex2D(p)
            //{
            //    _isEmpty = false
            //};


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
        public void Add(Point2D[] ps)
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

        public double Angle
        {
            get
            {
                //if (Next == Previous) throw new ArgumentOutOfRangeException("Polygon must have at least three vertices");
                return Vector2D.AngleBetweenVectors(Previous, this, Next);
            }
        }
        public double AngleDegree
        {
            get
            {
                return (Vector2D.AngleBetweenVectors(Previous, this, Next) / Math.PI) * 180;
            }
        }


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
            if (Next == Previous) throw new ArgumentException("Polygon must have at least three vertices");

            PolygonVertex2D currentNode = this;
            do
            {
                yield return currentNode;
                currentNode = currentNode.Next;
            } while (currentNode != this);//} while (currentNode != null && currentNode != this); //кольцевой список
        }
    }
}
