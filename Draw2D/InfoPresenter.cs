using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Plane2D;

namespace Draw2D
{
    public class InfoPresenter : IDisposable
    {
        private IInfoForm infoForm;
        private Polygon2D selectedPolygon2D;
        private Point2D LeftBottomPoint;
        private Point2D RightTopPoint;
        private Point2D CenterPoint;
        private Point origin;
        private Point2D NewLeftBottomPoint;
        private Point2D NewRightTopPoint;
        private Point2D NewCenterPoint;
        //private int width;
        //private int height;
        private int widthPoly;
        private int heightPoly;
        private int[] scales = { 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000 };
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
            this.selectedPolygon2D = selectedPolygon2D;
            LeftBottomPoint = selectedPolygon2D.LeftBottomRectangleVertex;
            RightTopPoint = selectedPolygon2D.RightTopRectangleVertex;
            CenterPoint = Point2D.Middle(LeftBottomPoint, RightTopPoint);
            widthPoly = (int)(RightTopPoint.X - LeftBottomPoint.X);
            heightPoly = (int)(RightTopPoint.Y - LeftBottomPoint.Y);
            //width = infoForm.GetImageWidth;
            //height = infoForm.GetImageHeight;
            scale = GetScale();

            bmp = new Bitmap(infoForm.GetImageWidth, infoForm.GetImageHeight);
            //_view.Image = new Bitmap(_view.GetImageWidth, _view.GetImageHeight);
            graph = Graphics.FromImage(bmp); // _view.Graph;
            color = Color.DarkRed;
            pen = new Pen(color);

            origin = new Point(0, 320);

            util = new Utility(pen, origin);
            util.DrawCoordinateAxes(graph, infoForm.GetImageWidth, infoForm.GetImageHeight);
            selectedPolygon2D.GetPolygonInCoordinateSystem(origin).Draw(graph, pen);



            infoForm.Image = bmp;

        }
        private int GetScale()
        {
            // КОРЯВО
            double scX = infoForm.GetImageWidth / widthPoly;
            double scY = infoForm.GetImageHeight / heightPoly;
            double sc = Math.Min(scX, scY) * stepScale;
            int num = 0;
            for (int i = 0; i < scales.Length; i++)
                if (scales[i] > sc)
                {
                    num = i - 1;
                    break;
                }
            if (num == 0)
                num = scales.Length-1;
            if (num == -1)
                num = 0;            
            return scales[num];
        }

        // ДУБЛЬ
        //private void DrawCoordinateAxes(Graphics graph)
        //{
        //    Pen _pen = new Pen(color) { CustomEndCap = new AdjustableArrowCap(5, 5, false) };
        //    DrawLineInCoordinateSystem(graph, _pen, new Point(0, origin.Y - _view.GetImageHeight), new Point(0, origin.Y));
        //    DrawLineInCoordinateSystem(graph, _pen, new Point(0, 0), new Point(_view.GetImageWidth - origin.X - deltaBmp, 0));
        //}



        public void Dispose()
        {
            //bmp.Dispose();
            //beforeFigureBmp.Dispose();
            //lastAngleFigureBmp.Dispose();
            //pen.Dispose();
        }

        //enum Scale
        //{
        //    s100to1 = 1,
        //    s50to1 = 2,
        //    s20to1 = 5,
        //    s10to1 = 10,
        //    s5to1 = 20,
        //    s2to1 = 50,
        //    s1to1 = 100,
        //    s1to2 = 200,
        //    s1to5 = 500,
        //    s1to10 = 1000
        //}
    }
}