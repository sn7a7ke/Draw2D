namespace Draw2D
{
    partial class InfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxInfo = new System.Windows.Forms.PictureBox();
            this.Output = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMouseLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLeftBottom = new System.Windows.Forms.Label();
            this.lblRightTop = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxInfo
            // 
            this.pictureBoxInfo.BackgroundImage = global::Draw2D.Properties.Resources.Background10x10;
            this.pictureBoxInfo.Location = new System.Drawing.Point(11, 54);
            this.pictureBoxInfo.Name = "pictureBoxInfo";
            this.pictureBoxInfo.Size = new System.Drawing.Size(481, 321);
            this.pictureBoxInfo.TabIndex = 0;
            this.pictureBoxInfo.TabStop = false;
            this.pictureBoxInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxInfo_MouseMove);
            // 
            // Output
            // 
            this.Output.AutoSize = true;
            this.Output.Location = new System.Drawing.Point(504, 31);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(10, 13);
            this.Output.TabIndex = 1;
            this.Output.Text = ".";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblMouseLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 436);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabel1.Text = "X,Y:";
            // 
            // lblMouseLocation
            // 
            this.lblMouseLocation.Name = "lblMouseLocation";
            this.lblMouseLocation.Size = new System.Drawing.Size(22, 17);
            this.lblMouseLocation.Text = "0,0";
            // 
            // lblLeftBottom
            // 
            this.lblLeftBottom.AutoSize = true;
            this.lblLeftBottom.Location = new System.Drawing.Point(13, 378);
            this.lblLeftBottom.Name = "lblLeftBottom";
            this.lblLeftBottom.Size = new System.Drawing.Size(10, 13);
            this.lblLeftBottom.TabIndex = 11;
            this.lblLeftBottom.Text = ".";
            // 
            // lblRightTop
            // 
            this.lblRightTop.Location = new System.Drawing.Point(392, 38);
            this.lblRightTop.Name = "lblRightTop";
            this.lblRightTop.Size = new System.Drawing.Size(100, 13);
            this.lblRightTop.TabIndex = 12;
            this.lblRightTop.Text = ".";
            this.lblRightTop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 458);
            this.Controls.Add(this.lblRightTop);
            this.Controls.Add(this.lblLeftBottom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.pictureBoxInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 496);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 496);
            this.Name = "InfoForm";
            this.Text = "InfoForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxInfo;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblMouseLocation;
        private System.Windows.Forms.Label lblLeftBottom;
        private System.Windows.Forms.Label lblRightTop;
    }
}