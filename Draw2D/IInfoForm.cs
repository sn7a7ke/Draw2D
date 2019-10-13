using System;
using System.Drawing;
using System.Windows.Forms;

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
        TreeView TreeViewInfo { get; set; }

        event EventHandler DoPictureBoxInfo_MouseMove;
        event EventHandler DotVInfo_BeforeSelect;
    }
}