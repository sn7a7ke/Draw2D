using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Plane2D;

namespace Draw2D
{
    public class InfoPresenter : IDisposable
    {
        private IInfoForm infoForm;
        private Polygon2D selectedPolygon2D;
        private Polygon2D poly;
        private Point2D LeftBottomPoint;
        private Point2D RightTopPoint;
        private Point2D CenterPoint;
        private Point origin;
        //private Point2D NewLeftBottomPoint;
        //private Point2D NewRightTopPoint;
        //private Point2D NewCenterPoint;
        //private int width;
        //private int height;
        private int widthPoly;
        private int heightPoly;
        private int[] scales = { 1, 2, 5, 10, 20, 50, 100};
        private int scale;
        private const int stepScale = 100;

        private Bitmap bmp;
        private Graphics graph;
        private Color color;
        private Pen pen;

        Utility util;

        public InfoPresenter(IInfoForm infoForm, Polygon2D selectedPolygon2D)
        {
            this.infoForm = infoForm;
            this.selectedPolygon2D = selectedPolygon2D ?? throw new ArgumentNullException(nameof(selectedPolygon2D));

            LeftBottomPoint = selectedPolygon2D.LeftBottomRectangleVertex;
            RightTopPoint = selectedPolygon2D.RightTopRectangleVertex;
            CenterPoint = Point2D.Middle(LeftBottomPoint, RightTopPoint);
            widthPoly = (int)(RightTopPoint.X - LeftBottomPoint.X);
            heightPoly = (int)(RightTopPoint.Y - LeftBottomPoint.Y);
            scale = GetScale();

            bmp = new Bitmap(infoForm.GetImageWidth, infoForm.GetImageHeight);            
            graph = Graphics.FromImage(bmp); // _view.Graph;
            color = Color.DarkRed;
            pen = new Pen(color);

            origin = new Point(0, 320);

            util = new Utility(pen, origin);
            util.DrawCoordinateAxes(graph, infoForm.GetImageWidth, infoForm.GetImageHeight);

            Point2D[] points = selectedPolygon2D.GetVertices;
            List<Point2D> newPoints = new List<Point2D>();
            Point2D current;
            for (int i = 0; i < points.Length; i++)
            {
                current = new Point2D(GetNewCoorinateX(points[i].X), GetNewCoorinateY(points[i].Y));
                newPoints.Add(current);
            }
            poly = new Polygon2D(newPoints.ToArray());
            poly.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);

            infoForm.Image = bmp;
            infoForm.LeftBottomText = LeftBottomPoint.ToString();
            infoForm.RightTopText = new Point2D(infoForm.GetImageWidth * 100 / scale + LeftBottomPoint.X, infoForm.GetImageHeight * 100 / scale + LeftBottomPoint.Y).ToString();

            infoForm.OutputText = LeftBottomPoint.ToString() + " " + scale.ToString()+ Environment.NewLine;
            if (selectedPolygon2D is Triangle2D tri)
            {
                infoForm.OutputText += new Line2D(tri.IntersectionAltitudes, tri.IntersectionBisectors).DistanceFromPointToLine(tri.IntersectionMedians);

            }



            infoForm.DoPictureBoxInfo_MouseMove += InfoForm_DoPictureBoxInfo_MouseMove;
            infoForm.DotVInfo_BeforeSelect += InfoForm_DotVInfo_BeforeSelect;
        }

        private void InfoForm_DotVInfo_BeforeSelect(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InfoForm_DoPictureBoxInfo_MouseMove(object sender, EventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs)e;
            infoForm.SetMouseLocation = String.Format("{0}:{1}", ((origin.X + em.X * 100 / scale) + LeftBottomPoint.X), ((origin.Y - em.Y) * 100 / scale + LeftBottomPoint.Y));
        }

        private double GetNewCoorinateX(double k) => (k - LeftBottomPoint.X) * scale / 100;
        private double GetNewCoorinateY(double k) => (k - LeftBottomPoint.Y) * scale / 100;

        private int GetScale()
        {            
            int scX = infoForm.GetImageWidth * stepScale / widthPoly;
            int scY = infoForm.GetImageHeight * stepScale / heightPoly;
            int sc = Math.Min(scX, scY);
            if (sc > stepScale)
                return sc;
            int num = 0;
            for (int i = 0; i < scales.Length; i++)
                if (scales[i] >= sc)
                {
                    num = i - 1;
                    break;
                }
            if (num == 0)
                num = scales.Length - 1;
            if (num == -1)
                num = 0;
            return scales[num];
        }

        private void SetTreeViewInfo()
        {
            infoForm.TreeViewInfo.Nodes.Clear();

        }

        public void Dispose()
        {
            //bmp.Dispose();
            //beforeFigureBmp.Dispose();
            //lastAngleFigureBmp.Dispose();
            //pen.Dispose();
        }
    }
}