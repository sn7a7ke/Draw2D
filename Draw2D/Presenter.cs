using Plane2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw2D
{
    public class Presenter
    {
        IView _view;
        Bitmap bmp;
        Bitmap beforeFigureBmp;
        Bitmap lastAngleFigureBmp;
        Bitmap beforeSelectedFigureBmp;

        Graphics graph;
        Color color;
        Pen pen;

        List<Point> points;
        int deltaDraw = 5;
        int deltaBmp = 100;
        List<Polygon2D> polygons;
        Polygon2D polygon2D;
        Polygon2D selectedPolygon2D;
        Point origin;


        public Presenter(IView view)
        {

            _view = view;

            _view.OutputText = _view.GetImageWidth.ToString() + " " + _view.GetImageHeight.ToString();
            bmp = new Bitmap(_view.GetImageWidth, _view.GetImageHeight);
            graph = Graphics.FromImage(bmp);
            color = Color.DarkRed;

            origin = new Point(0, 400);

            DrawCoordinateAxes();

            polygons = new List<Polygon2D>();
            polygon2D = new Polygon2D(new Point2D(100, 10), new Point2D(50, 100), new Point2D(150, 100));
            polygons.Add(polygon2D);
            polygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);

            _view.Image = bmp;
            beforeFigureBmp = new Bitmap(bmp);

            points = new List<Point>();

            _view.DoDraw_Click += _view_DoDraw_Click;
            _view.DoSelect_Click += _view_DoSelect_Click;
            _view.DoShift_Click += _view_DoShift_Click;
            _view.DoRotate_Click += _view_DoRotate_Click;
            _view.DoSymmetry_Click += _view_DoSymmetry_Click;
            _view.DoPictureBox_Resize += _view_DoPictureBox_Resize;

            _view.DoPictureBox_MouseMove += _view_DoPictureBox_MouseMove;
            _view.DoPictureBox_MouseClick += _view_DoPictureBox_MouseClick;
        }

        private void _view_DoPictureBox_Resize(object sender, EventArgs e)
        {
            if (_view.GetImageHeight > bmp.Height || _view.GetImageWidth >= bmp.Width)
            {
                Bitmap newBmp = new Bitmap(bmp.Width + deltaBmp, bmp.Height + deltaBmp);
                graph = Graphics.FromImage(newBmp);

                DrawShapes(graph, pen);
                //graph.DrawImage(bmp,0,0);

                bmp = newBmp;
                _view.OutputText = bmp.Width.ToString() + " " + bmp.Height.ToString();


                newBmp = new Bitmap(bmp.Width + deltaBmp, bmp.Height + deltaBmp);
                graph = Graphics.FromImage(newBmp);
                graph.DrawImage(lastAngleFigureBmp, 0, 0);
                lastAngleFigureBmp = newBmp;

                _view.Image = lastAngleFigureBmp;
            }
        }

        private void _view_DoPictureBox_MouseMove(object sender, EventArgs e)
        {
            if (!(e is MouseEventArgs))
                throw new ArgumentException(this + " isn't MouseEventArgs");
            MouseEventArgs em = (MouseEventArgs)e;

            _view.SetLabelMouseLocation = String.Format("{0}:{1}", origin.X + em.X, origin.Y - em.Y);
        }

        private void _view_DoShift_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null)
            {
                bmp = new Bitmap(beforeFigureBmp);
                graph = Graphics.FromImage(bmp);

                for (int i = 0; i < points.Count - 1; i++)
                    graph.DrawLine(pen, points[i], points[i + 1]);

                _view.Image = bmp;
                lastAngleFigureBmp = new Bitmap(bmp);
            }
        }

        private void _view_DoRotate_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _view_DoSymmetry_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _view_DoSelect_Click(object sender, EventArgs e)
        {
            if (polygon2D != null)
            {
                selectedPolygon2D = polygon2D;
                beforeSelectedFigureBmp = new Bitmap(beforeFigureBmp);
                _view.OutputText = selectedPolygon2D.VerticesToString(Environment.NewLine) + Environment.NewLine
                    + "QuantityVertices: " + selectedPolygon2D.QuantityVertices + Environment.NewLine
                    + "Center: " + selectedPolygon2D.Center + Environment.NewLine
                    + "Perimeter: " + selectedPolygon2D.Perimeter();
            }
        }

        private void _view_DoPictureBox_MouseMove_Draw(object sender, EventArgs e)
        {
            // === ДУБЛЬ!!!! ===
            if (!(e is MouseEventArgs))
                throw new ArgumentException(this + " isn't MouseEventArgs");
            MouseEventArgs em = (MouseEventArgs)e;

            if (points.Count != 0)
            {
                _view.Image = lastAngleFigureBmp;
                bmp = new Bitmap(lastAngleFigureBmp);
                graph = Graphics.FromImage(bmp);
                graph.DrawLine(pen, points[points.Count - 1], em.Location);
                _view.Image = bmp;
                if (points.Count > 1 && Point2D.Distance(em.Location, points[0]) <= deltaDraw)
                    _view.SetCursorImage = Cursors.WaitCursor;
                else
                    _view.SetCursorImage = Cursors.Cross;
            }
        }

        private void _view_DoPictureBox_MouseClick(object sender, EventArgs e)
        {
            if (!(e is MouseEventArgs))
                throw new ArgumentException(this + " isn't MouseEventArgs");
            MouseEventArgs em = (MouseEventArgs)e;

            MouseButtons mouseButton = em.Button;
            if (mouseButton == MouseButtons.Left)
            {
                Point mouseLocation = em.Location;
                if (points.Count == 0)
                {
                    beforeFigureBmp = new Bitmap(bmp);

                    points.Add(mouseLocation);
                    //bmp.SetPixel(points[points.Count - 1].X, points[points.Count - 1].Y, color);

                    _view.Image = bmp;
                    lastAngleFigureBmp = new Bitmap(bmp);

                    _view.DoPictureBox_MouseMove += _view_DoPictureBox_MouseMove_Draw;
                }
                else if (Point2D.Distance(mouseLocation, points[0]) <= deltaDraw)
                {
                    if (points.Count < 3)
                    {
                        points.Clear();
                        //ToDo очистить фигуру
                        throw new NotImplementedException();


                    }
                    polygon2D = Polygon2D.GetPolygonFromCoordinateSystem(points, origin);
                    polygons.Add(polygon2D);

                    _view.Image = lastAngleFigureBmp;
                    bmp = new Bitmap(lastAngleFigureBmp);
                    graph = Graphics.FromImage(bmp);

                    graph.DrawLine(pen, points[points.Count - 1], points[0]);
                    points.Clear();

                    _view.Image = bmp;
                    lastAngleFigureBmp = new Bitmap(bmp);

                    _view.SetCursorImage = Cursors.Cross;
                    _view.DoPictureBox_MouseMove -= _view_DoPictureBox_MouseMove_Draw;

                    GC.Collect(0, GCCollectionMode.Forced);
                }
                else
                {
                    points.Add(mouseLocation);

                    graph.DrawLine(pen, points[points.Count - 2], points[points.Count - 1]);

                    _view.Image = bmp;
                    lastAngleFigureBmp = new Bitmap(bmp);
                }
            }
            if (mouseButton == MouseButtons.Right)
            {
                if (points.Count == 0)
                    return;

                points.RemoveAt(points.Count - 1);
                bmp = new Bitmap(beforeFigureBmp);
                graph = Graphics.FromImage(bmp);


                DrawPoints(graph, pen);

                // Дубль, переделать !!!
                //if (points.Count > 1)
                //    for (int i = 0; i < points.Count - 1; i++)
                //        graph.DrawLine(pen, points[i], points[i + 1]);



                //switch (points.Count)
                //{
                //    case 0:
                //        break;
                //    case 1:
                //        //graph.DrawLine(pen, points[0], points[0]);
                //        //bmp.SetPixel(points[0].X, points[0].Y, color);
                //        break;
                //    default:                        
                //        for (int i = 0; i < points.Count - 1; i++)
                //            graph.DrawLine(pen, points[i], points[i + 1]);
                //        break;
                //}
                _view.Image = bmp;
                lastAngleFigureBmp = new Bitmap(bmp);


                //if (points.Count != 0)
                //{
                //    bmp = new Bitmap(beforeFigureBmp);
                //    graph = Graphics.FromImage(bmp);

                //    points.RemoveAt(points.Count - 1);
                //    if (points.Count == 1)
                //        bmp.SetPixel(points[0].X, points[0].Y, color);
                //    else
                //        for (int i = 0; i < points.Count - 1; i++)
                //            graph.DrawLine(pen, points[i], points[i + 1]);

                //    _view.Image = bmp;
                //    lastAngleFigureBmp = new Bitmap(bmp);
                //}



                //points.Clear();
                //bmp = beforeFigureBmp;
                //graph = Graphics.FromImage(bmp);
                //_view.Image = bmp;                
            }
        }

        private void _view_DoDraw_Click(object sender, EventArgs e)
        {
            polygon2D = new Polygon2D(new Point2D(100, 100), new Point2D(200, 100), new Point2D(200, 200), new Point2D(100, 200));

            polygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);
            //graph.DrawPolygon(pen, polygon2D.VerticesToPoint);

            //float fontSize = 12;
            //PointF textLocation = polygon2D.Center;
            //string str = polygon2D.ToString();//p4.GetType().Name + " (" + p4.QuantityAngles + ")";//"square";
            //Font font = new Font("Times New Roman", fontSize);
            //textLocation.X -= graph.MeasureString(str, font).Width / 2;
            //textLocation.Y -= graph.MeasureString(str, font).Height / 2;
            //graph.DrawString(str, font, pen.Brush, textLocation);

            _view.Image = bmp;
            _view.OutputText = polygon2D.VerticesToString(Environment.NewLine);
        }

        private void DrawCoordinateAxes()
        {
            pen = new Pen(color) { CustomEndCap = new AdjustableArrowCap(4, 4) };
            DrawLineInCoordinateSystem(graph, pen, new Point(0, 0), new Point(0, origin.Y));
            DrawLineInCoordinateSystem(graph, pen, new Point(0, 0), new Point(bmp.Width, 0));
            pen = new Pen(color);
        }
        private void DrawLineInCoordinateSystem(Graphics graph, Pen pen, Point p1, Point p2)
        {
            graph.DrawLine(pen, new Point(origin.X + p1.X, origin.Y - p1.Y),
                                new Point(origin.X + p2.X, origin.Y - p2.Y));
        }
        private void DrawShapes(Graphics graph, Pen pen)
        {
            DrawCoordinateAxes();
            for (int i = 0; i < polygons.Count; i++)
                polygons[i].GetPolygonInCoordinateSystem(origin).Draw(graph, pen);

            // Дубль, переделать !!!
            //switch (points.Count)
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        //bmp.SetPixel(points[0].X, points[0].Y, color);
            //        break;
            //    default:
            //        for (int i = 0; i < points.Count - 1; i++)
            //            graph.DrawLine(pen, points[i], points[i + 1]);
            //        break;
            //}
        }
        private void DrawPoints(Graphics graph, Pen pen)
        {
            if (points.Count > 1)
                for (int i = 0; i < points.Count - 1; i++)
                    graph.DrawLine(pen, points[i], points[i + 1]);
        }
    }
}
