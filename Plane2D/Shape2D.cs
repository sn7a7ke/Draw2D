using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public abstract class Shape2D
    {
        public virtual string Name => this.GetType().Name;
        public abstract bool IsConvex { get; }
        public abstract double Square();
        public abstract double Perimeter();
        public abstract void Draw(Graphics graph, Pen pen);
    }
}
