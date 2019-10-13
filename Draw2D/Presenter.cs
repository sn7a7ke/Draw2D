using Plane2D;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw2D
{
    public class Presenter
    {
        private IMainForm _view;
        private int _deltaDraw = 5;
        private const int _minimumQuantityOfVertices = 3;
        private Canvas.Canvas _canvas;

        public Presenter(IMainForm view)
        {
            _view = view;
            Configuration.GetAppConfig();
            DoubleExtended.Epsilon = Configuration.Epsilon;

            Point origin = new Point(200, 400);
            _canvas = new Canvas.Canvas(origin, _view.GetImageWidth, _view.GetImageHeight);
            _canvas.OutputNumberFormat = Configuration.OutputNumberFormat;
            Polygon2D polygon2D = new Polygon2D(new Point2D(100, 10, "A"), new Point2D(50, 100, "B"), new Point2D(150, 100, "C"));
            _canvas.Polygons2D.Add(polygon2D);
            RefreshPictureBox();

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
        }


        #region EventHandlers
        private void _view_DoToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)((ToolStripMenuItem)_view.MenuS.Items["Tools"]).DropDownItems["Choose"]).DropDownItems.Clear();
            int polyCount = _canvas.Polygons2D.Count;
            if (polyCount != 0)
            {
                ToolStripMenuItem[] mi = new ToolStripMenuItem[polyCount];
                for (int i = 0; i < polyCount; i++)
                    mi[i] = new ToolStripMenuItem(_canvas.Polygons2D[i].ToString(), null, ChooseShape, i.ToString());
                ((ToolStripMenuItem)((ToolStripMenuItem)_view.MenuS.Items["Tools"]).DropDownItems["Choose"]).DropDownItems.AddRange(mi);
            }
        }

        private void ChooseShape(object sender, EventArgs e)
        {
            if (int.TryParse(((ToolStripMenuItem)sender).Name, out int nn))
            {
                _canvas.Polygons2D.Select(nn);
                RefreshPictureBox();
            }
        }

        private void _view_DoClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _canvas.Clear();
            RefreshPictureBox();
        }

        private void _view_DoPictureBox_Resize(object sender, EventArgs e)
        {
            int newWidth = _canvas.Width;
            int newHeight = _canvas.Height;
            if (_view.GetImageWidth >= _canvas.Width)
                newWidth = _view.GetImageWidth;
            if (_view.GetImageHeight > _canvas.Height)
                newHeight = _view.GetImageHeight;
            _canvas.Resize(newWidth, newHeight);
            RefreshPictureBox();
        }

        private void _view_DoPictureBox_MouseMove(object sender, EventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs)e;
            _view.SetLabelMouseLocation = String.Format($"{_canvas.Origin.X + em.X}:{_canvas.Origin.Y - em.Y}");
        }

        private void _view_DoPictureBox_MouseMove_Draw(object sender, EventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs)e;
            _canvas.Points.Temporary = em.Location;
            if (_canvas.Points.Count > 1 && Point2D.Distance(em.Location, _canvas.Points.First) <= _deltaDraw)
                _view.SetCursorImage = Cursors.WaitCursor;
            else
                _view.SetCursorImage = Cursors.Cross;
            RefreshPictureBox();
        }

        private void _view_DoPictureBox_MouseClick(object sender, EventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs)e;
            MouseButtons mouseButton = em.Button;
            if (mouseButton == MouseButtons.Left)
            {
                Point mouseLocation = em.Location;
                if (_canvas.Points.Count == 0)
                {
                    _canvas.Points.Add(mouseLocation);
                    _view.DoPictureBox_MouseMove += _view_DoPictureBox_MouseMove_Draw;
                }
                else if (Point2D.Distance(mouseLocation, _canvas.Points.First) <= _deltaDraw)
                {
                    if (_canvas.Points.Count < _minimumQuantityOfVertices)
                        _canvas.Points.Clear();
                    else
                        _canvas.AddPolygon();
                    _view.SetCursorImage = Cursors.Cross;
                    _view.DoPictureBox_MouseMove -= _view_DoPictureBox_MouseMove_Draw;
                    GC.Collect(0, GCCollectionMode.Forced); // === а надо ли??
                }
                else
                    _canvas.Points.Add(mouseLocation);
            }
            if (mouseButton == MouseButtons.Right)
                _canvas.Points.RemoveLast();
            RefreshPictureBox();
        }

        private void _view_DoShift_Click(object sender, EventArgs e)
        {
            Polygon2D poly = (Polygon2D)_canvas.Polygons2D.Selected.Shift(_view.DeltaX, _view.DeltaY);
            _canvas.Polygons2D.ChangeSelected(poly);
            RefreshPictureBox();
        }

        private void _view_DoRotate_Click(object sender, EventArgs e)
        {
            Polygon2D poly = (Polygon2D)_canvas.Polygons2D.Selected.RotateAroundThePoint((double)_view.Angle,
                new Point2D(_view.DeltaX, _view.DeltaY));
            _canvas.Polygons2D.ChangeSelected(poly);
            RefreshPictureBox();
        }

        private void _view_DoSymmetry_Click(object sender, EventArgs e)
        {
            Polygon2D poly = (Polygon2D)_canvas.Polygons2D.Selected.RotateAroundThePoint((double)_view.Angle,
                new Point2D(_view.DeltaX, _view.DeltaY));
            _canvas.Polygons2D.ChangeSelected(poly);
            RefreshPictureBox();
        }

        private void _view_DoSelect_Click(object sender, EventArgs e)
        {
            _canvas.Polygons2D.SelectNext();
            RefreshPictureBox();
        }

        private void _view_DoInfo_Click(object sender, EventArgs e)
        {
            if (_canvas.Polygons2D.Selected == null)
                MessageBox.Show("Please, You must select polygon");
            else
            {
                InfoForm infoForm = new InfoForm();
                InfoPresenter infoPresenter = new InfoPresenter(infoForm, _canvas.Polygons2D.Selected);
                infoForm.Show();
            }
        }
        #endregion


        private void RefreshPictureBox()
        {
            _canvas.Refresh();
            _view.Image = _canvas.MainBmp;
            if (_canvas.Polygons2D.Selected != null)
                _view.OutputText = _canvas.Summary(_canvas.Polygons2D.Selected);
            else
                _view.OutputText = _canvas.Width.ToString() + " " + _canvas.Height.ToString();
        }
    }
}
