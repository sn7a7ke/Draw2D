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
    public partial class MainForm : Form, IMainForm
    {
        #region IView
        public Image Image { get { return pictureBox.Image; } set { pictureBox.Image = value; } } //Bitmap
        //public Graphics Graph => pictureBox. CreateGraphics();
        public int GetImageHeight => pictureBox.Height;
        public int GetImageWidth => pictureBox.Width;
        public int DeltaX => (int)nUD_DeltaX.Value;
        public int DeltaY => (int)nUD_DeltaY.Value;
        public decimal Angle=> nUD_Angle.Value;

        //public List<string> Shapes { get;  set; }
        //public int GetImageHeight => pictureBox.Size.Height;
        //public int GetImageWidth => pictureBox.Size.Width;
        public MenuStrip MenuS { get => menuStrip1; set => menuStrip1=value; }

        public string SetLabelMouseLocation { set { labelMouseLocation.Text = value; } }
        public string OutputText { get { return Output.Text; } set { Output.Text = value; } }
        public Cursor SetCursorImage { set { pictureBox.Cursor = value; } }

        public string About { get; set; }

        // Mouse Event
        public event EventHandler DoDraw_Click;
        public event EventHandler DoSelect_Click;
        public event EventHandler DoShift_Click;
        public event EventHandler DoRotate_Click;
        public event EventHandler DoSymmetry_Click;

        //PictureBox Event
        public event EventHandler DoPictureBox_Resize;
        public event EventHandler DoPictureBox_MouseClick;
        public event EventHandler DoPictureBox_MouseMove;

        //Menu Event
        public event EventHandler DoClearToolStripMenuItem_Click;
        public event EventHandler DoToolsToolStripMenuItem_Click;

        #endregion


        #region Проброс событий
        // Mouse Event
        void Draw_Click(object sender, EventArgs e) => DoDraw_Click?.Invoke(this, EventArgs.Empty);
        void Select_Click(object sender, EventArgs e) => DoSelect_Click?.Invoke(this, EventArgs.Empty);
        void Shift_Click(object sender, EventArgs e) => DoShift_Click?.Invoke(this, EventArgs.Empty);
        void Rotate_Click(object sender, EventArgs e) => DoRotate_Click?.Invoke(this, EventArgs.Empty);
        void Symmetry_Click(object sender, EventArgs e) => DoSymmetry_Click?.Invoke(this, EventArgs.Empty);

        //PictureBox Event
        private void PictureBox_Resize(object sender, EventArgs e) => DoPictureBox_Resize?.Invoke(this, EventArgs.Empty);
        private void PictureBox_MouseClick(object sender, MouseEventArgs e) => DoPictureBox_MouseClick?.Invoke(this, e);
        private void PictureBox_MouseMove(object sender, MouseEventArgs e) => DoPictureBox_MouseMove?.Invoke(this, e);

        //Menu Event
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e) => DoClearToolStripMenuItem_Click?.Invoke(this, e);
        private void ToolsToolStripMenuItem_Click(object sender, EventArgs e) => DoToolsToolStripMenuItem_Click?.Invoke(this, e);


        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(About, "About");
        }

        private void tSBPolygon_Click(object sender, EventArgs e)
        {
            tSBPolygon.Checked = true;
            tSBCircle.Checked = false;

        }

        private void tSBCircle_Click(object sender, EventArgs e)
        {
            tSBCircle.Checked = true;
            tSBPolygon.Checked = false;
        }
    }
}
