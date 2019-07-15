using Plane2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Draw2D
{
    public class Presenter : IDisposable
    {
        private IMainForm _view;
        private Bitmap mainBmp;
        private Bitmap beforeFigureBmp;
        private Bitmap lastAngleFigureBmp;
        private Graphics graph;
        private Pen pen;
        //int scale=1;

        private List<Point> points;
        private int deltaDraw = 5;
        private int deltaBmp = 100;
        private List<Polygon2D> polygons;
        private Polygon2D selectedPolygon2D;

        const int minimumQuantityOfVertices = 3;

        private Utility util;

        public Presenter(IMainForm view)
        {
            _view = view;
            mainBmp = new Bitmap(_view.GetImageWidth, _view.GetImageHeight);
            graph = Graphics.FromImage(mainBmp); // _view.Graph;
            pen = new Pen(Color.DarkRed);
            Point origin = new Point(0, 400);
            util = new Utility(pen, origin);
            util.DrawCoordinateAxes(graph, _view.GetImageWidth, _view.GetImageHeight);

            polygons = new List<Polygon2D>();
            Polygon2D polygon2D = new Polygon2D(new Point2D(100, 10), new Point2D(50, 100), new Point2D(150, 100));

            polygons.Add(polygon2D);
            SelectPolygon(polygon2D);
            graph.DrawPolygon(pen, util.GetPolygonInCoordinateSystem(polygon2D));

            points = new List<Point>();
            _view.Image = mainBmp;

            beforeFigureBmp = new Bitmap(mainBmp);
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
            _view.About = "Draw2D 2018-2019" + Environment.NewLine + "© Sn7a7ke";

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
                    mi[i] = new ToolStripMenuItem(polygons[i].ToString(), null, ChooseShape, i.ToString());
                ((ToolStripMenuItem)((ToolStripMenuItem)_view.MenuS.Items["Tools"]).DropDownItems["Choose"]).DropDownItems.AddRange(mi);
            }
        }

        private void _view_DoClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearPictureBox(mainBmp.Width, mainBmp.Height);
            mainBmp = lastAngleFigureBmp;
            _view.Image = mainBmp;
            _view.OutputText = string.Empty;
            polygons.Clear();
        }

        private void _view_DoPictureBox_Resize(object sender, EventArgs e)
        {
            if (_view.GetImageHeight > mainBmp.Height || _view.GetImageWidth >= mainBmp.Width)
                RefreshPictureBox(mainBmp.Width + deltaBmp, mainBmp.Height + deltaBmp);
        }

        private void _view_DoPictureBox_MouseMove(object sender, EventArgs e)
        {
            //if (!(e is MouseEventArgs))
            //    throw new ArgumentException(this + " isn't MouseEventArgs");
            MouseEventArgs em = (MouseEventArgs)e;
            _view.SetLabelMouseLocation = String.Format("{0}:{1}", util.Origin.X + em.X, util.Origin.Y - em.Y);
        }

        private void _view_DoPictureBox_MouseMove_Draw(object sender, EventArgs e)
        {
            // === ДУБЛЬ!!!! ===
            //if (!(e is MouseEventArgs))
            //    throw new ArgumentException(this + " isn't MouseEventArgs");
            MouseEventArgs em = (MouseEventArgs)e;

            if (points.Count != 0)
            {
                //_view.Image = lastAngleFigureBmp;
                mainBmp = new Bitmap(lastAngleFigureBmp);
                graph = Graphics.FromImage(mainBmp);
                graph.DrawLine(pen, points[points.Count - 1], em.Location);
                _view.Image = mainBmp;
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
                    beforeFigureBmp = new Bitmap(mainBmp);
                    points.Add(mouseLocation);

                    // COMMENT
                    _view.Image = mainBmp;

                    lastAngleFigureBmp = new Bitmap(mainBmp);
                    _view.DoPictureBox_MouseMove += _view_DoPictureBox_MouseMove_Draw;
                }
                else if (Point2D.Distance(mouseLocation, points[0]) <= deltaDraw)
                {
                    if (points.Count < minimumQuantityOfVertices)
                        RemoveLastVertex(minimumQuantityOfVertices - 1);
                    else
                    {
                        Polygon2D polygon2D = util.GetPolygonFromCoordinateSystem(points);
                        polygons.Add(polygon2D);
                        _view.Image = lastAngleFigureBmp;
                        mainBmp = new Bitmap(lastAngleFigureBmp);
                        graph = Graphics.FromImage(mainBmp);
                        graph.DrawLine(pen, points[points.Count - 1], points[0]);
                        points.Clear();
                        SelectPolygon(polygon2D);
                        _view.Image = mainBmp;
                        lastAngleFigureBmp = new Bitmap(mainBmp);
                    }
                    _view.SetCursorImage = Cursors.Cross;
                    _view.DoPictureBox_MouseMove -= _view_DoPictureBox_MouseMove_Draw;

                    GC.Collect(0, GCCollectionMode.Forced); // === а надо ли??
                }
                else
                {
                    points.Add(mouseLocation);
                    graph.DrawLine(pen, points[points.Count - 2], points[points.Count - 1]);
                    _view.Image = mainBmp;
                    lastAngleFigureBmp = new Bitmap(mainBmp);
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
                lastAngleFigureBmp = new Bitmap(mainBmp);
            }
        }

        private void _view_DoRotate_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null && _view.Angle != 0)
            {
                polygons.Remove(selectedPolygon2D);
                selectedPolygon2D = (Polygon2D)selectedPolygon2D.RotateAroundThePoint((double)_view.Angle,
                    new Point2D(_view.DeltaX, _view.DeltaY));
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox();
                lastAngleFigureBmp = new Bitmap(mainBmp);
            }
        }

        private void _view_DoSymmetry_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D != null)
            {
                polygons.Remove(selectedPolygon2D);
                selectedPolygon2D = (Polygon2D)selectedPolygon2D.SymmetryAboutPoint(new Point2D(_view.DeltaX, _view.DeltaY));
                polygons.Add(selectedPolygon2D);
                RefreshPictureBox();
                lastAngleFigureBmp = new Bitmap(mainBmp);
            }
        }

        private void _view_DoSelect_Click(object sender, EventArgs e)
        {
            if (polygons?.Count > 1)
            {
                int numberOfSelectedPolygon = polygons.IndexOf(selectedPolygon2D);
                if (numberOfSelectedPolygon == -1)
                    return;

                selectedPolygon2D = (numberOfSelectedPolygon == polygons.Count - 1) ? polygons[0] : polygons[numberOfSelectedPolygon + 1];
            }
            RefreshPictureBox();
            lastAngleFigureBmp = new Bitmap(mainBmp);
        }

        private void SelectPolygon(Polygon2D p)
        {
            if (p != null)
            {
                selectedPolygon2D = p;
                RefreshPictureBox();
                _view.OutputText = GetPolygonDescription(selectedPolygon2D);
            }
        }

        private string GetPolygonDescription(Polygon2D polygon2D)
        {
            StringBuilder sb = new StringBuilder(polygon2D.ToString() + Environment.NewLine);
            for (int i = 0; i < polygon2D.QuantityVertices; i++)
                sb.Append("Vertex " + i + ": (" + polygon2D[i].X + ", " + polygon2D[i].Y + ")" + Environment.NewLine);
            for (int i = 0; i < polygon2D.QuantityVertices; i++)
                sb.Append("Angle " + i + ": " + polygon2D[i].AngleDegree + Environment.NewLine);
            sb.Append("QuantityVertices: " + polygon2D.QuantityVertices + Environment.NewLine);
            sb.Append("Center: " + polygon2D.Center + Environment.NewLine);
            sb.Append("Perimeter: " + polygon2D.Perimeter + Environment.NewLine);
            sb.Append("Is convex: " + polygon2D.IsConvex);
            return sb.ToString();
        }

        private void _view_DoInfo_Click(object sender, EventArgs e)
        {
            if (selectedPolygon2D == null)
            {
                MessageBox.Show("Please, You must select polygon");
                return;
            }
            InfoForm infoForm = new InfoForm();
            InfoPresenter infoPresenter = new InfoPresenter(infoForm, selectedPolygon2D);
            infoForm.Show();
        }
        #endregion


        #region MenuHandler
        private void ClearPictureBox(int width, int height)
        {
            Bitmap newBmp = new Bitmap(width, height);
            Graphics newGraph;
            newGraph = Graphics.FromImage(newBmp);
            util.DrawCoordinateAxes(newGraph, _view.GetImageWidth, _view.GetImageHeight);
            mainBmp = newBmp;
            beforeFigureBmp = new Bitmap(newBmp);
            lastAngleFigureBmp = new Bitmap(newBmp);
        }

        private void ChooseShape(object sender, EventArgs e)
        {
            if (int.TryParse(((ToolStripMenuItem)sender).Name, out int nn))
            {
                selectedPolygon2D = polygons[nn];
                RefreshPictureBox();

                _view.OutputText = GetPolygonDescription(selectedPolygon2D);
            }
        }
        #endregion


        private void RefreshPictureBox() => RefreshPictureBox(mainBmp.Width, mainBmp.Height);

        private void RefreshPictureBox(int width, int height)
        {
            mainBmp = new Bitmap(width, height);
            Graphics newGraph;
            newGraph = Graphics.FromImage(mainBmp);
            util.DrawCoordinateAxes(newGraph, _view.GetImageWidth, _view.GetImageHeight);
            util.DrawPolygons(newGraph, polygons);
            if (selectedPolygon2D != null)
            {
                float penWidth = pen.Width;
                pen.Width *= 2;

                newGraph.DrawPolygon(pen, util.GetPolygonInCoordinateSystem(selectedPolygon2D));

                pen.Width = penWidth;
            }
            _view.OutputText = mainBmp.Width.ToString() + " " + mainBmp.Height.ToString();
            beforeFigureBmp = new Bitmap(mainBmp);
            lastAngleFigureBmp = new Bitmap(mainBmp);
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
            mainBmp = new Bitmap(beforeFigureBmp);
            graph = Graphics.FromImage(mainBmp);
            util.DrawPoints(graph, points);
            _view.Image = mainBmp;
            lastAngleFigureBmp = new Bitmap(mainBmp);
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
                    mainBmp.Dispose();
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
