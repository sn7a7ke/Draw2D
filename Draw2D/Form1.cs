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
    public partial class Form1 : Form, IView
    {
        #region IView
        public Image Image { get { return pictureBox.Image; } set { pictureBox.Image = value; } } //Bitmap
        public int GetImageHeight => pictureBox.Height;
        public int GetImageWidth => pictureBox.Width;
        //public int GetImageHeight => pictureBox.Size.Height;
        //public int GetImageWidth => pictureBox.Size.Width;

        public string SetLabelMouseLocation { set { labelMouseLocation.Text = value; } }
        public string OutputText { get { return Output.Text; } set { Output.Text = value; } }
        public Cursor SetCursorImage { set { pictureBox.Cursor = value; } }

        public event EventHandler DoDraw_Click;
        public event EventHandler DoSelect_Click;
        public event EventHandler DoShift_Click;
        public event EventHandler DoRotate_Click;
        public event EventHandler DoSymmetry_Click;

        public event EventHandler DoPictureBox_Resize;
        public event EventHandler DoPictureBox_MouseClick;
        public event EventHandler DoPictureBox_MouseMove;


        #endregion

        #region Проброс событий
        void Draw_Click(object sender, EventArgs e) => DoDraw_Click?.Invoke(this, EventArgs.Empty);
        void Select_Click(object sender, EventArgs e) => DoSelect_Click?.Invoke(this, EventArgs.Empty);
        void Shift_Click(object sender, EventArgs e) => DoShift_Click?.Invoke(this, EventArgs.Empty);
        void Rotate_Click(object sender, EventArgs e) => DoRotate_Click?.Invoke(this, EventArgs.Empty);
        void Symmetry_Click(object sender, EventArgs e) => DoSymmetry_Click?.Invoke(this, EventArgs.Empty);


        private void PictureBox_Resize(object sender, EventArgs e) => DoPictureBox_Resize?.Invoke(this, EventArgs.Empty);
        private void PictureBox_MouseClick(object sender, MouseEventArgs e) => DoPictureBox_MouseClick?.Invoke(this, e);
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //labelMouseLocation.Text = String.Format("{0}:{1}", e.X, e.Y);
            DoPictureBox_MouseMove?.Invoke(this, e);
        }

        #endregion

        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (pictureBox.Focused)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        Cursor.Position.Offset(-1, 0);
                        break;
                    case Keys.Up:
                        Cursor.Position.Offset(0, -1);
                        break;
                    case Keys.Right:
                        Cursor.Position.Offset(1, 0);
                        break;
                    case Keys.Down:
                        Cursor.Position.Offset(0, 1);                        
                        break;
                }
            }
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (pictureBox.Focused)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        Cursor.Position.Offset(-1, 0);
                        break;
                    case Keys.Up:
                        Cursor.Position.Offset(0, -1);
                        break;
                    case Keys.Right:
                        Cursor.Position.Offset(1, 0);
                        break;
                    case Keys.Down:
                        Cursor.Position.Offset(0, 1);
                        break;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
        }

    }
}
