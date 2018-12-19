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
        public Image Image { get { return pictureBoxInfo.Image; } set { pictureBoxInfo.Image = value; } } //Bitmap
        public int GetImageHeight => pictureBoxInfo.Height;
        public int GetImageWidth => pictureBoxInfo.Width;

        public string SetLabelMouseLocation { set { labelMouseLocation.Text = value; } }
        public string OutputText { get { return Output.Text; } set { Output.Text = value; } }

        public InfoForm()
        {
            InitializeComponent();
        }
    }
}
