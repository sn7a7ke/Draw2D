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
    public class Presenter : IDisposable
    {
        IView _view;
        Bitmap bmp;
        Bitmap beforeFigureBmp;
        Bitmap lastAngleFigureBmp;
        //Bitmap beforeSelectedFigureBmp;
        Graphics graph;
        Color color;
        Pen pen;
        //int scale=1;

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
            //_view.OutputText = _view.GetImageWidth.ToString() + " " + _view.GetImageHeight.ToString();
            bmp = new Bitmap(_view.GetImageWidth, _view.GetImageHeight);
            //_view.Image = new Bitmap(_view.GetImageWidth, _view.GetImageHeight);
            graph = Graphics.FromImage(bmp); // _view.Graph;
            color = Color.DarkRed;
            pen = new Pen(color);

            origin = new Point(0, 400);
            DrawCoordinateAxes(graph);

            polygons = new List<Polygon2D>();
            polygon2D = new Polygon2D(new Point2D(100, 10), new Point2D(50, 100), new Point2D(150, 100));
            polygons.Add(polygon2D);
            polygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);

            points = new List<Point>();

            _view.Image = bmp;

            beforeFigureBmp = new Bitmap(bmp);
            //lastAngleFigureBmp = new Bitmap(bmp);

            _view.DoDraw_Click += _view_DoDraw_Click;
            _view.DoSelect_Click += _view_DoSelect_Click;
            _view.DoShift_Click += _view_DoShift_Click;
            _view.DoRotate_Click += _view_DoRotate_Click;
            _view.DoSymmetry_Click += _view_DoSymmetry_Click;

            _view.DoPictureBox_Resize += _view_DoPictureBox_Resize;
            _view.DoPictureBox_MouseMove += _view_DoPictureBox_MouseMove;
            _view.DoPictureBox_MouseClick += _view_DoPictureBox_MouseClick;

            _view.DoClearToolStripMenuItem_Click += _view_DoClearToolStripMenuItem_Click;
            _view.DoToolsToolStripMenuItem_Click += _view_DoToolsToolStripMenuItem_Click;

            //MessageBox.Show(polygon2D.Name);
            //MessageBox.Show(new Point2D(10, 10).Rotate(Math.PI / 2, new Point2D(20, 20)).ToString());
        }

        private void _view_DoToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)((ToolStripMenuItem)_view.MenuS.Items["Tools"]).DropDownItems["Choose"]).DropDownItems.Clear();
            if (polygons.Count != 0)
            {
                ToolStripMenuItem[] mi = new ToolStripMenuItem[polygons.Count];
                for (int i = 0; i < polygons.Count; i++)
                    mi[i] = new ToolStripMenuItem(polygons[i].Name, null, ChooseShape, i.ToString());
                ((ToolStripMenuItem)((ToolStripMenuItem)_view.MenuS.Items["Tools"]).DropDownItems["Choose"]).DropDownItems.AddRange(mi);
            }

        }

        private void ChooseShape(object sender, EventArgs e)
        {
            if (int.TryParse(((ToolStripMenuItem)sender).Name, out int nn))
            {
                selectedPolygon2D = polygons[nn];
                _view.OutputText = selectedPolygon2D.VerticesToString(Environment.NewLine) + Environment.NewLine
                    + "QuantityVertices: " + selectedPolygon2D.QuantityVertices + Environment.NewLine
                    + "Center: " + selectedPolygon2D.Center + Environment.NewLine
                    + "Perimeter: " + selectedPolygon2D.Perimeter;
            }

            //string chPoly = ((ToolStripMenuItem)sender).Text;
        }

        private void _view_DoClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearPictureBox(bmp.Width, bmp.Height);
            _view.Image = lastAngleFigureBmp;
            polygons.Clear();
        }

        private void ClearPictureBox(int width, int height)
        {
            Bitmap newBmp = new Bitmap(width, height);
            Graphics newGraph;
            newGraph = Graphics.FromImage(newBmp);
            DrawCoordinateAxes(newGraph);
            bmp = newBmp;
            beforeFigureBmp = new Bitmap(newBmp);
            lastAngleFigureBmp = new Bitmap(newBmp);
        }

        private void _view_DoPictureBox_Resize(object sender, EventArgs e)
        {
            if (_view.GetImageHeight > bmp.Height || _view.GetImageWidth >= bmp.Width)
                RefreshPictureBox(bmp.Width + deltaBmp, bmp.Height + deltaBmp);
        }

        private void RefreshPictureBox(int width, int height)
        {
            bmp = new Bitmap(width, height);
            Graphics newGraph;
            newGraph = Graphics.FromImage(bmp);
            DrawCoordinateAxes(newGraph);            
            //beforeFigureBmp = new Bitmap(bmp);
            //lastAngleFigureBmp = new Bitmap(bmp);

            DrawShapes(newGraph, pen);            

            _view.OutputText = bmp.Width.ToString() + " " + bmp.Height.ToString();

            beforeFigureBmp = new Bitmap(bmp);
            lastAngleFigureBmp = new Bitmap(bmp);

            newGraph = Graphics.FromImage(lastAngleFigureBmp);
            DrawPoints(newGraph, pen);

            _view.Image = lastAngleFigureBmp;
        }

        private void _view_DoPictureBox_MouseMove(object sender, EventArgs e)
        {
            //if (!(e is MouseEventArgs))
            //    throw new ArgumentException(this + " isn't MouseEventArgs");
            MouseEventArgs em = (MouseEventArgs)e;
            _view.SetLabelMouseLocation = String.Format("{0}:{1}", origin.X + em.X, origin.Y - em.Y);
        }

        private void _view_DoPictureBox_MouseMove_Draw(object sender, EventArgs e)
        {
            // === ДУБЛЬ!!!! ===
            //if (!(e is MouseEventArgs))
            //    throw new ArgumentException(this + " isn't MouseEventArgs");
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
            //if (!(e is MouseEventArgs))
            //    throw new ArgumentException(this + " isn't MouseEventArgs");
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
                        RemoveLastVertex(2);
                    else
                    {
                        polygon2D = Polygon2D.GetPolygonFromCoordinateSystem(points, origin);
                        polygons.Add(polygon2D);

                        _view.Image = lastAngleFigureBmp;
                        bmp = new Bitmap(lastAngleFigureBmp);
                        graph = Graphics.FromImage(bmp);

                        graph.DrawLine(pen, points[points.Count - 1], points[0]);

                        points.Clear();

                        _view.Image = bmp;
                        lastAngleFigureBmp = new Bitmap(bmp);
                    }
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
                RemoveLastVertex();
        }

        private void _view_DoShift_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null)
            {
                polygons.Remove(selectedPolygon2D);
                selectedPolygon2D = selectedPolygon2D.Shift(_view.DeltaX, _view.DeltaY);
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox(bmp.Width, bmp.Height);
                //graph = Graphics.FromImage(bmp);
                //selectedPolygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);
                _view.Image = bmp;
                lastAngleFigureBmp = new Bitmap(bmp);
            }
        }

        private void _view_DoRotate_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null)
            {
                polygons.Remove(selectedPolygon2D);
                selectedPolygon2D = selectedPolygon2D.Rotate((double)_view.Angle,
                    new Point2D(_view.DeltaX, _view.DeltaY));
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox(bmp.Width, bmp.Height);

                _view.Image = bmp;
                lastAngleFigureBmp = new Bitmap(bmp);
            }
        }

        private void _view_DoSymmetry_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null)
            {
                polygons.Remove(selectedPolygon2D);
                selectedPolygon2D = selectedPolygon2D.Symmetry(new Point2D(_view.DeltaX, _view.DeltaY));
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox(bmp.Width, bmp.Height);
                _view.Image = bmp;
                lastAngleFigureBmp = new Bitmap(bmp);
            }
        }

        private void _view_DoSelect_Click(object sender, EventArgs e)
        {
            if (polygon2D != null)
            {
                selectedPolygon2D = polygon2D;
                //beforeSelectedFigureBmp = new Bitmap(beforeFigureBmp);
                _view.OutputText = selectedPolygon2D.VerticesToString(Environment.NewLine) + Environment.NewLine
                    + "QuantityVertices: " + selectedPolygon2D.QuantityVertices + Environment.NewLine
                    + "Center: " + selectedPolygon2D.Center + Environment.NewLine
                    + "Perimeter: " + selectedPolygon2D.Perimeter;
                polygon2D = null;
            }
        }

        private void _view_DoDraw_Click(object sender, EventArgs e)
        {
            polygon2D = new Polygon2D(new Point2D(100, 100), new Point2D(200, 100), new Point2D(200, 200), new Point2D(100, 200));
            polygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);

            _view.Image = bmp;
            _view.OutputText = polygon2D.VerticesToString(Environment.NewLine);
        }

        private bool RemoveLastVertex(int qty = 1)
        {
            if (points.Count == 0)
                return false;
            qty = qty > points.Count ? points.Count : qty;
            points.RemoveRange(points.Count - qty, qty);
            //points.RemoveAt(points.Count - 1);
            bmp = new Bitmap(beforeFigureBmp);
            graph = Graphics.FromImage(bmp);
            DrawPoints(graph, pen);

            _view.Image = bmp;
            lastAngleFigureBmp = new Bitmap(bmp);
            return true;
        }

        private void DrawCoordinateAxes(Graphics graph)
        {
            Pen _pen = new Pen(color) { CustomEndCap = new AdjustableArrowCap(5, 5, false) };
            DrawLineInCoordinateSystem(graph, _pen, new Point(0, origin.Y - _view.GetImageHeight), new Point(0, origin.Y));
            DrawLineInCoordinateSystem(graph, _pen, new Point(0, 0), new Point(_view.GetImageWidth - origin.X - deltaBmp, 0));

            //DrawLineInCoordinateSystem(graph, _pen, new Point(0, origin.Y-bmp.Height), new Point(0, origin.Y));
            //DrawLineInCoordinateSystem(graph, _pen, new Point(0, 0), new Point(bmp.Width - origin.X - deltaBmp, 0));
        }
        private void DrawLineInCoordinateSystem(Graphics graph, Pen pen, Point p1, Point p2)
        {
            graph.DrawLine(pen, new Point(origin.X + p1.X, origin.Y - p1.Y),
                                new Point(origin.X + p2.X, origin.Y - p2.Y));
        }
        private void DrawShapes(Graphics graph, Pen pen)
        {
            for (int i = 0; i < polygons.Count; i++)
                polygons[i].GetPolygonInCoordinateSystem(origin).Draw(graph, pen);
        }
        private void DrawPoints(Graphics graph, Pen pen)
        {
            if (points.Count > 1)
                for (int i = 0; i < points.Count - 1; i++)
                    graph.DrawLine(pen, points[i], points[i + 1]);
        }

        public void Dispose()
        {
            bmp.Dispose();
            beforeFigureBmp.Dispose();
            lastAngleFigureBmp.Dispose();
            pen.Dispose();
        }
    }
}
