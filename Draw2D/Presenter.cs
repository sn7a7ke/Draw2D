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
        private IMainForm _view;
        private Bitmap bmp;
        private Bitmap beforeFigureBmp;
        private Bitmap lastAngleFigureBmp;
        private Graphics graph;
        private Color color;
        private Pen pen;
        //int scale=1;

        private List<Point> points;
        private int deltaDraw = 5;
        private int deltaBmp = 100;
        private List<Polygon2D> polygons;
        private Polygon2D polygon2D;
        private Polygon2D selectedPolygon2D;
        private Point origin;

        const int minimumQuantityOfVertices = 3;

        private Utility util;

        public Presenter(IMainForm view)
        {
            _view = view;
            bmp = new Bitmap(_view.GetImageWidth, _view.GetImageHeight);
            graph = Graphics.FromImage(bmp); // _view.Graph;
            color = Color.DarkRed;
            pen = new Pen(color);
            origin = new Point(0, 400);
            util = new Utility(pen, origin);
            util.DrawCoordinateAxes(graph, _view.GetImageWidth, _view.GetImageHeight);

            polygons = new List<Polygon2D>();
            polygon2D = new Polygon2D(new Point2D(100, 10), new Point2D(50, 100), new Point2D(150, 100));
            //TriangleVertex2D tri = new TriangleVertex2D();            
            //polygon2D[1].

            polygons.Add(polygon2D);


            graph.DrawPolygon(pen, polygon2D.GetPolygonInCoordinateSystem(origin).VerticesToPoint);
            //polygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);



            points = new List<Point>();
            _view.Image = bmp;

            beforeFigureBmp = new Bitmap(bmp);
            //lastAngleFigureBmp = new Bitmap(bmp);

            _view.DoDraw_Click += _view_DoInfo_Click;
            _view.DoSelect_Click += _view_DoSelect_Click;
            _view.DoShift_Click += _view_DoShift_Click;
            _view.DoRotate_Click += _view_DoRotate_Click;
            _view.DoSymmetry_Click += _view_DoSymmetry_Click;

            _view.DoPictureBox_Resize += _view_DoPictureBox_Resize;
            _view.DoPictureBox_MouseMove += _view_DoPictureBox_MouseMove;
            _view.DoPictureBox_MouseClick += _view_DoPictureBox_MouseClick;

            _view.DoClearToolStripMenuItem_Click += _view_DoClearToolStripMenuItem_Click;
            _view.DoToolsToolStripMenuItem_Click += _view_DoToolsToolStripMenuItem_Click;
            _view.About = "Draw2D 2018" + Environment.NewLine + "© Sn7a7ke";

            //Line2D l1 = new Line2D(1, 2, 3);
            //Line2D l2 = new Line2D(2, 4, 6);
            //Line2D l3 = new Line2D(2, 4, 5);
            //MessageBox.Show((l1 == l2).ToString() + (l1 == l3).ToString());
            //MessageBox.Show((l1.Equals(l2)).ToString() + (l1.Equals(l3)).ToString());
            //MessageBox.Show(polygon2D.Name);
            //MessageBox.Show(new Point2D(10, 10).Rotate(Math.PI / 2, new Point2D(20, 20)).ToString());
        }

        #region EventHandlers
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

        private void _view_DoClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearPictureBox(bmp.Width, bmp.Height);
            bmp = lastAngleFigureBmp;
            _view.Image = bmp;
            polygons.Clear();
        }

        private void _view_DoPictureBox_Resize(object sender, EventArgs e)
        {
            if (_view.GetImageHeight > bmp.Height || _view.GetImageWidth >= bmp.Width)
                RefreshPictureBox(bmp.Width + deltaBmp, bmp.Height + deltaBmp);
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

                    // COMMENT
                    _view.Image = bmp;

                    lastAngleFigureBmp = new Bitmap(bmp);
                    _view.DoPictureBox_MouseMove += _view_DoPictureBox_MouseMove_Draw;
                }
                else if (Point2D.Distance(mouseLocation, points[0]) <= deltaDraw)
                {
                    if (points.Count < minimumQuantityOfVertices)
                        RemoveLastVertex(minimumQuantityOfVertices - 1);
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

                    GC.Collect(0, GCCollectionMode.Forced); // === а надо ли??
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
                selectedPolygon2D = (Polygon2D)selectedPolygon2D.Shift(_view.DeltaX, _view.DeltaY);
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox();
                lastAngleFigureBmp = new Bitmap(bmp);
            }
        }

        private void _view_DoRotate_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null && _view.Angle != 0)
            {
                polygons.Remove(selectedPolygon2D);
                selectedPolygon2D = (Polygon2D)selectedPolygon2D.Rotate((double)_view.Angle,
                    new Point2D(_view.DeltaX, _view.DeltaY));
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox();
                lastAngleFigureBmp = new Bitmap(bmp);
            }
        }

        private void _view_DoSymmetry_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null)
            {
                polygons.Remove(selectedPolygon2D);
                selectedPolygon2D = (Polygon2D)selectedPolygon2D.Symmetry(new Point2D(_view.DeltaX, _view.DeltaY));
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox();
                lastAngleFigureBmp = new Bitmap(bmp);
            }
        }

        private void _view_DoSelect_Click(object sender, EventArgs e)
        {
            SelectPolygon(polygon2D);
            polygon2D = null;
        }

        private void SelectPolygon(Polygon2D p)
        {
            if (p != null)
            {
                selectedPolygon2D = p;
                RefreshPictureBox();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < selectedPolygon2D.QuantityVertices; i++)
                {
                    sb.Append("Vertex " + i + ": (" + selectedPolygon2D[i].X + ", " + selectedPolygon2D[i].Y + ")" + Environment.NewLine);
                    //sb.Append(Environment.NewLine);
                }

                for (int i = 0; i < selectedPolygon2D.QuantityVertices; i++)
                    sb.Append("Angle " + i + ": " + selectedPolygon2D[i].AngleDegree + Environment.NewLine);
                sb.Append("QuantityVertices: " + selectedPolygon2D.QuantityVertices + Environment.NewLine);
                sb.Append("Center: " + selectedPolygon2D.Center + Environment.NewLine);
                sb.Append("Perimeter: " + selectedPolygon2D.Perimeter + Environment.NewLine);
                sb.Append("Is convex: " + selectedPolygon2D.IsConvex);
                _view.OutputText = sb.ToString();
            }
        }

        private void _view_DoInfo_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D == null)
            {
                MessageBox.Show("Please, You must select polygon");
                return;
            }

            //Test


            InfoForm infoForm = new InfoForm();
            InfoPresenter infoPresenter = new InfoPresenter(infoForm, selectedPolygon2D);
            infoForm.Show();

            //polygon2D = new Polygon2D(new Point2D(100, 100), new Point2D(200, 100), new Point2D(200, 200), new Point2D(100, 200));
            //polygons.Add(polygon2D);
            //// ОШИБКА!!!))) раскомментировать и перенести в метод Clear
            ////graph = Graphics.FromImage(bmp);
            //polygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen); 
            //_view.Image = bmp;
            //_view.OutputText = polygon2D.VerticesToString(Environment.NewLine);
        }
        #endregion

        #region MenuHandler
        private void ClearPictureBox(int width, int height)
        {
            Bitmap newBmp = new Bitmap(width, height);
            Graphics newGraph;
            newGraph = Graphics.FromImage(newBmp);
            util.DrawCoordinateAxes(newGraph, _view.GetImageWidth, _view.GetImageHeight);
            bmp = newBmp;
            beforeFigureBmp = new Bitmap(newBmp);
            lastAngleFigureBmp = new Bitmap(newBmp);
        }
        private void ChooseShape(object sender, EventArgs e)
        {
            if (int.TryParse(((ToolStripMenuItem)sender).Name, out int nn))
            {
                selectedPolygon2D = polygons[nn];
                RefreshPictureBox();

                _view.OutputText = selectedPolygon2D.VerticesToString(Environment.NewLine) + Environment.NewLine
                    + "QuantityVertices: " + selectedPolygon2D.QuantityVertices + Environment.NewLine
                    + "Center: " + selectedPolygon2D.Center + Environment.NewLine
                    + "Perimeter: " + selectedPolygon2D.Perimeter;
                // + Environment.NewLine + "Angle1: " + selectedPolygon2D.;
            }
        }
        #endregion


        private void RefreshPictureBox() => RefreshPictureBox(bmp.Width, bmp.Height);
        private void RefreshPictureBox(int width, int height)
        {
            bmp = new Bitmap(width, height);
            Graphics newGraph;
            newGraph = Graphics.FromImage(bmp);
            util.DrawCoordinateAxes(newGraph, _view.GetImageWidth, _view.GetImageHeight);
            util.DrawShapes(newGraph, polygons);
            if (selectedPolygon2D != null)
            {
                float penWidth = pen.Width;
                pen.Width *= 2;


                newGraph.DrawPolygon(pen, selectedPolygon2D.GetPolygonInCoordinateSystem(origin).VerticesToPoint);
                //selectedPolygon2D.GetPolygonInCoordinateSystem(origin).Draw(newGraph, pen);


                pen.Width = penWidth;
            }
            _view.OutputText = bmp.Width.ToString() + " " + bmp.Height.ToString();
            beforeFigureBmp = new Bitmap(bmp);
            lastAngleFigureBmp = new Bitmap(bmp);
            newGraph = Graphics.FromImage(lastAngleFigureBmp);
            util.DrawPoints(newGraph, points);
            _view.Image = lastAngleFigureBmp;
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
            util.DrawPoints(graph, points);
            _view.Image = bmp;
            lastAngleFigureBmp = new Bitmap(bmp);
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    bmp.Dispose();
                    beforeFigureBmp.Dispose();
                    lastAngleFigureBmp.Dispose();
                    graph.Dispose();
                    pen.Dispose();

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Presenter() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
