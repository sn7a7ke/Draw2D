using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw2D
{
    public interface IView
    {
        Image Image { get; set; }
        int GetImageHeight { get; }
        int GetImageWidth { get; }
        int DeltaX { get; }
        int DeltaY { get; }

        string SetLabelMouseLocation { set; }

        string OutputText { get; set; }
        Cursor SetCursorImage { set; }



        event EventHandler DoDraw_Click;
        event EventHandler DoSelect_Click;
        event EventHandler DoShift_Click;
        event EventHandler DoRotate_Click;
        event EventHandler DoSymmetry_Click;

        event EventHandler DoPictureBox_Resize;
        event EventHandler DoPictureBox_MouseClick;
        event EventHandler DoPictureBox_MouseMove;

        event EventHandler DoClearToolStripMenuItem_Click;
    }
}