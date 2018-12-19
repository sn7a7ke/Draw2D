using System;
using System.Drawing;

namespace Draw2D
{
    public interface IInfoForm
    {
        int GetImageHeight { get; }
        int GetImageWidth { get; }
        Image Image { get; set; }

        string LeftBottomText { get; set; }
        string RightTopText { get; set; }

        string OutputText { get; set; }
        string SetMouseLocation { set; }


        event EventHandler DoPictureBoxInfo_MouseMove;
    }
}