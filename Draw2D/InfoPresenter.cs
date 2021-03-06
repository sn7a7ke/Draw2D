﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Point origin;
        private int widthPoly;
        private int heightPoly;
        private int[] scales = { 1, 2, 5, 10, 20, 50, 100};
        private int scale;
        private const int stepScale = 100;
        private string _outputNumberFormat = "0.##";

        private Bitmap bmp;
        private Graphics graph;
        private Color color;
        private Pen pen;

        Utility util;

        public InfoPresenter(IInfoForm infoForm, Polygon2D selectedPolygon2D)
        {
            this.infoForm = infoForm;
            this.selectedPolygon2D = selectedPolygon2D ?? throw new ArgumentNullException(nameof(selectedPolygon2D));

            LeftBottomPoint = new Point2D(selectedPolygon2D.MinX, selectedPolygon2D.MinY);
            RightTopPoint = new Point2D(selectedPolygon2D.MaxX, selectedPolygon2D.MaxY);
            widthPoly = (int)(RightTopPoint.X - LeftBottomPoint.X);
            heightPoly = (int)(RightTopPoint.Y - LeftBottomPoint.Y);
            scale = GetScale();

            bmp = new Bitmap(infoForm.GetImageWidth, infoForm.GetImageHeight);            
            graph = Graphics.FromImage(bmp);
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
            graph.DrawPolygon(pen, util.GetPolygonInCoordinateSystem(poly));

            infoForm.Image = bmp;
            infoForm.LeftBottomText = LeftBottomPoint.ToString(_outputNumberFormat);
            infoForm.RightTopText = new Point2D(infoForm.GetImageWidth * 100 / scale + LeftBottomPoint.X, infoForm.GetImageHeight * 100 / scale + LeftBottomPoint.Y).ToString(_outputNumberFormat);
            infoForm.OutputText = selectedPolygon2D.ToString() + scale.ToString();

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


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    bmp.Dispose();
                    graph.Dispose();
                    pen.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);
        #endregion
    }
}