using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw2D
{
    public interface IMainForm
    {
        Image Image { get; set; }
        //Graphics Graph { get; }
        int GetImageHeight { get; }
        int GetImageWidth { get; }
        int DeltaX { get; }
        int DeltaY { get; }
        decimal Angle { get; }

        MenuStrip MenuS { get; set; }

        string SetLabelMouseLocation { set; }

        string OutputText { get; set; }
        Cursor SetCursorImage { set; }
        string About { get; set; }



        // Mouse Event
        event EventHandler DoDraw_Click;
        event EventHandler DoSelect_Click;
        event EventHandler DoShift_Click;
        event EventHandler DoRotate_Click;
        event EventHandler DoSymmetry_Click;

        //PictureBox Event
        event EventHandler DoPictureBox_Resize;
        event EventHandler DoPictureBox_MouseClick;
        event EventHandler DoPictureBox_MouseMove;

        //Menu Event
        event EventHandler DoClearToolStripMenuItem_Click;
        event EventHandler DoToolsToolStripMenuItem_Click;

    }
}