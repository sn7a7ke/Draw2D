﻿namespace Draw2D
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Choose = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMouseLocation = new System.Windows.Forms.Label();
            this.Output = new System.Windows.Forms.Label();
            this.SelectPoly = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nUD_Angle = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Symmetry = new System.Windows.Forms.Button();
            this.Rotate = new System.Windows.Forms.Button();
            this.Shift = new System.Windows.Forms.Button();
            this.nUD_DeltaY = new System.Windows.Forms.NumericUpDown();
            this.nUD_DeltaX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tSBPolygon = new System.Windows.Forms.ToolStripButton();
            this.tSBCircle = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Angle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_DeltaY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_DeltaX)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox.BackgroundImage")));
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox.Location = new System.Drawing.Point(3, 56);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.MinimumSize = new System.Drawing.Size(601, 401);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(601, 401);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseClick);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.pictureBox.Resize += new System.EventHandler(this.PictureBox_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.Tools,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Tools
            // 
            this.Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.Choose});
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(48, 20);
            this.Tools.Text = "Tools";
            this.Tools.Click += new System.EventHandler(this.ToolsToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // Choose
            // 
            this.Choose.Name = "Choose";
            this.Choose.Size = new System.Drawing.Size(157, 22);
            this.Choose.Text = "Choose a shape";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 3;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(706, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Draw_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 468);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "X,Y:";
            // 
            // labelMouseLocation
            // 
            this.labelMouseLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMouseLocation.AutoSize = true;
            this.labelMouseLocation.Location = new System.Drawing.Point(24, 468);
            this.labelMouseLocation.Name = "labelMouseLocation";
            this.labelMouseLocation.Size = new System.Drawing.Size(22, 13);
            this.labelMouseLocation.TabIndex = 6;
            this.labelMouseLocation.Text = "0,0";
            // 
            // Output
            // 
            this.Output.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Output.AutoSize = true;
            this.Output.Location = new System.Drawing.Point(610, 55);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(10, 13);
            this.Output.TabIndex = 7;
            this.Output.Text = ".";
            // 
            // SelectPoly
            // 
            this.SelectPoly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectPoly.Location = new System.Drawing.Point(625, 433);
            this.SelectPoly.Name = "SelectPoly";
            this.SelectPoly.Size = new System.Drawing.Size(75, 23);
            this.SelectPoly.TabIndex = 8;
            this.SelectPoly.Text = "Select Poly";
            this.SelectPoly.UseVisualStyleBackColor = true;
            this.SelectPoly.Click += new System.EventHandler(this.Select_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.nUD_Angle);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Symmetry);
            this.panel1.Controls.Add(this.Rotate);
            this.panel1.Controls.Add(this.Shift);
            this.panel1.Controls.Add(this.nUD_DeltaY);
            this.panel1.Controls.Add(this.nUD_DeltaX);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(606, 267);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 162);
            this.panel1.TabIndex = 9;
            // 
            // nUD_Angle
            // 
            this.nUD_Angle.DecimalPlaces = 6;
            this.nUD_Angle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nUD_Angle.Location = new System.Drawing.Point(41, 30);
            this.nUD_Angle.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUD_Angle.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nUD_Angle.Name = "nUD_Angle";
            this.nUD_Angle.Size = new System.Drawing.Size(85, 20);
            this.nUD_Angle.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Angle:";
            // 
            // Symmetry
            // 
            this.Symmetry.Location = new System.Drawing.Point(102, 133);
            this.Symmetry.Name = "Symmetry";
            this.Symmetry.Size = new System.Drawing.Size(65, 23);
            this.Symmetry.TabIndex = 11;
            this.Symmetry.Text = "Symmetry";
            this.Symmetry.UseVisualStyleBackColor = true;
            this.Symmetry.Click += new System.EventHandler(this.Symmetry_Click);
            // 
            // Rotate
            // 
            this.Rotate.Location = new System.Drawing.Point(48, 133);
            this.Rotate.Name = "Rotate";
            this.Rotate.Size = new System.Drawing.Size(47, 23);
            this.Rotate.TabIndex = 10;
            this.Rotate.Text = "Rotate";
            this.Rotate.UseVisualStyleBackColor = true;
            this.Rotate.Click += new System.EventHandler(this.Rotate_Click);
            // 
            // Shift
            // 
            this.Shift.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Shift.Location = new System.Drawing.Point(5, 133);
            this.Shift.Name = "Shift";
            this.Shift.Size = new System.Drawing.Size(36, 23);
            this.Shift.TabIndex = 9;
            this.Shift.Text = "Shift";
            this.Shift.UseVisualStyleBackColor = true;
            this.Shift.Click += new System.EventHandler(this.Shift_Click);
            // 
            // nUD_DeltaY
            // 
            this.nUD_DeltaY.Location = new System.Drawing.Point(84, 3);
            this.nUD_DeltaY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nUD_DeltaY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.nUD_DeltaY.Name = "nUD_DeltaY";
            this.nUD_DeltaY.Size = new System.Drawing.Size(42, 20);
            this.nUD_DeltaY.TabIndex = 8;
            // 
            // nUD_DeltaX
            // 
            this.nUD_DeltaX.Location = new System.Drawing.Point(41, 3);
            this.nUD_DeltaX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nUD_DeltaX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.nUD_DeltaX.Name = "nUD_DeltaX";
            this.nUD_DeltaX.Size = new System.Drawing.Size(42, 20);
            this.nUD_DeltaX.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "X,Y:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSBPolygon,
            this.tSBCircle});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tSBPolygon
            // 
            this.tSBPolygon.Checked = true;
            this.tSBPolygon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tSBPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBPolygon.Image = ((System.Drawing.Image)(resources.GetObject("tSBPolygon.Image")));
            this.tSBPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBPolygon.Name = "tSBPolygon";
            this.tSBPolygon.Size = new System.Drawing.Size(23, 22);
            this.tSBPolygon.Text = "Polygon";
            this.tSBPolygon.Click += new System.EventHandler(this.tSBPolygon_Click);
            // 
            // tSBCircle
            // 
            this.tSBCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBCircle.Image = ((System.Drawing.Image)(resources.GetObject("tSBCircle.Image")));
            this.tSBCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBCircle.Name = "tSBCircle";
            this.tSBCircle.Size = new System.Drawing.Size(23, 22);
            this.tSBCircle.Text = "Circle";
            this.tSBCircle.Click += new System.EventHandler(this.tSBCircle_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 482);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SelectPoly);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.labelMouseLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 520);
            this.Name = "MainForm";
            this.Text = "Draw2D";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Angle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_DeltaY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_DeltaX)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMouseLocation;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.Button SelectPoly;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Tools;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nUD_DeltaY;
        private System.Windows.Forms.NumericUpDown nUD_DeltaX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Shift;
        private System.Windows.Forms.Button Symmetry;
        private System.Windows.Forms.Button Rotate;
        private System.Windows.Forms.NumericUpDown nUD_Angle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem Choose;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tSBPolygon;
        private System.Windows.Forms.ToolStripButton tSBCircle;
    }
}

