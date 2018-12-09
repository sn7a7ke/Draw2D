using Draw2D;
using Plane2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public class Presenter
    {
        IView _view;
        Bitmap bmp;
        Graphics graph;
        Pen pen;

        public Presenter(IView view)
        {
            _view = view;
            Polygon2D p3 = new Polygon2D(new Point2D(10, 10), new Point2D(50, 10), new Point2D(10, 50));
            bmp = new Bitmap(_view.ImageWidth, _view.ImageHeight);
            graph = Graphics.FromImage(bmp);
            pen = new Pen(Color.DarkGreen);
            graph.DrawPolygon(pen,p3.AnglesToPointF);
            _view.Image = bmp;
        }

    }
}
