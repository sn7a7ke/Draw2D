using System.Drawing;

namespace Draw2D
{
    public interface IInfoForm
    {
        int GetImageHeight { get; }
        int GetImageWidth { get; }
        Image Image { get; set; }
        string OutputText { get; set; }
        string SetLabelMouseLocation { set; }
    }
}