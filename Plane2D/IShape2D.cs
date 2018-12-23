using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    public interface IShape2D
    {
        string Name { get; }
        bool IsConvex { get; }
        double Square { get; }
        double Perimeter { get; }
        Point2D Center { get; }
        Point2D LeftBottomRectangleVertex { get; }
        Point2D RightTopRectangleVertex { get; }


        //УБРАТЬ нарушение soliD
        //void Draw(Graphics graph, Pen pen);
    }



    // СДЕЛАТЬ ИНТЕРФЕЙСОМ
    //public abstract class Shape2D
    //{
    //    public virtual string Name { get; }
    //    public abstract bool IsConvex { get; }
    //    public abstract double Square { get; }
    //    public abstract double Perimeter { get; }

    //    //УБРАТЬ нарушение soliD
    //    public abstract void Draw(Graphics graph, Pen pen);
    //}
}
