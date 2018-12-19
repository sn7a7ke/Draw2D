using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw2D
{
    public partial class InfoForm : Form, IInfoForm
    {
        #region IView

        public Image Image { get { return pictureBoxInfo.Image; } set { pictureBoxInfo.Image = value; } } //Bitmap
        public string LeftBottomText { get { return lblLeftBottom.Text; } set { lblLeftBottom.Text= value; } }
        public string RightTopText { get { return lblRightTop.Text; } set { lblRightTop.Text = value; } }

        public int GetImageHeight => pictureBoxInfo.Height;
        public int GetImageWidth => pictureBoxInfo.Width;

        public string SetMouseLocation { set {  lblMouseLocation.Text = value; } }
        public string OutputText { get { return Output.Text; } set { Output.Text = value; } }

        //PictureBox Event
        public event EventHandler DoPictureBoxInfo_MouseMove;


        #endregion



        #region Проброс событий
        private void pictureBoxInfo_MouseMove(object sender, MouseEventArgs e) => DoPictureBoxInfo_MouseMove?.Invoke(this, e);



        #endregion

        public InfoForm()
        {
            InitializeComponent();
        }

    }
}
