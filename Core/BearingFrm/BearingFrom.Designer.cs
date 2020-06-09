namespace ProjectMS.Core.BearingFrm
{
    partial class BearingFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BearingFrom));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.BearingpicMask = new ProjectMS.Core.BearingFrm.PicMask();
            this.Editbutton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.ChartSplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ChartSplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ChartSplitContainer3 = new System.Windows.Forms.SplitContainer();
            this.ChartSplitContainer4 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer1)).BeginInit();
            this.ChartSplitContainer1.Panel2.SuspendLayout();
            this.ChartSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer2)).BeginInit();
            this.ChartSplitContainer2.Panel1.SuspendLayout();
            this.ChartSplitContainer2.Panel2.SuspendLayout();
            this.ChartSplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer3)).BeginInit();
            this.ChartSplitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer4)).BeginInit();
            this.ChartSplitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.BearingpicMask);
            this.splitContainer.Panel1.Controls.Add(this.Editbutton);
            this.splitContainer.Panel1.Controls.Add(this.pictureBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.ChartSplitContainer1);
            this.splitContainer.Size = new System.Drawing.Size(800, 450);
            this.splitContainer.SplitterDistance = 342;
            this.splitContainer.TabIndex = 0;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
            // 
            // BearingpicMask
            // 
            this.BearingpicMask.BackColor = System.Drawing.Color.Transparent;
            this.BearingpicMask.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BearingpicMask.BackgroundImage")));
            this.BearingpicMask.Cursor = System.Windows.Forms.Cursors.Default;
            this.BearingpicMask.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BearingpicMask.IsEditing = false;
            this.BearingpicMask.Location = new System.Drawing.Point(77, 79);
            this.BearingpicMask.Name = "BearingpicMask";
            this.BearingpicMask.Size = new System.Drawing.Size(150, 150);
            this.BearingpicMask.TabIndex = 2;
            // 
            // Editbutton
            // 
            this.Editbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Editbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Editbutton.Location = new System.Drawing.Point(260, 420);
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(75, 23);
            this.Editbutton.TabIndex = 1;
            this.Editbutton.Text = "编辑";
            this.Editbutton.UseVisualStyleBackColor = true;
            this.Editbutton.Click += new System.EventHandler(this.Editbutton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox.BackgroundImage")));
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(500, 411);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // ChartSplitContainer1
            // 
            this.ChartSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.ChartSplitContainer1.Name = "ChartSplitContainer1";
            this.ChartSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ChartSplitContainer1.Panel2
            // 
            this.ChartSplitContainer1.Panel2.Controls.Add(this.ChartSplitContainer2);
            this.ChartSplitContainer1.Size = new System.Drawing.Size(450, 446);
            this.ChartSplitContainer1.SplitterDistance = 150;
            this.ChartSplitContainer1.TabIndex = 0;
            // 
            // ChartSplitContainer2
            // 
            this.ChartSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartSplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.ChartSplitContainer2.Name = "ChartSplitContainer2";
            this.ChartSplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ChartSplitContainer2.Panel1
            // 
            this.ChartSplitContainer2.Panel1.Controls.Add(this.ChartSplitContainer3);
            // 
            // ChartSplitContainer2.Panel2
            // 
            this.ChartSplitContainer2.Panel2.Controls.Add(this.ChartSplitContainer4);
            this.ChartSplitContainer2.Size = new System.Drawing.Size(450, 292);
            this.ChartSplitContainer2.SplitterDistance = 150;
            this.ChartSplitContainer2.TabIndex = 0;
            // 
            // ChartSplitContainer3
            // 
            this.ChartSplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartSplitContainer3.Location = new System.Drawing.Point(0, 0);
            this.ChartSplitContainer3.Name = "ChartSplitContainer3";
            this.ChartSplitContainer3.Size = new System.Drawing.Size(450, 150);
            this.ChartSplitContainer3.SplitterDistance = 214;
            this.ChartSplitContainer3.TabIndex = 0;
            // 
            // ChartSplitContainer4
            // 
            this.ChartSplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartSplitContainer4.Location = new System.Drawing.Point(0, 0);
            this.ChartSplitContainer4.Name = "ChartSplitContainer4";
            this.ChartSplitContainer4.Size = new System.Drawing.Size(450, 138);
            this.ChartSplitContainer4.SplitterDistance = 150;
            this.ChartSplitContainer4.TabIndex = 0;
            // 
            // BearingFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Name = "BearingFrom";
            this.Text = "BearingFrom";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ChartSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer1)).EndInit();
            this.ChartSplitContainer1.ResumeLayout(false);
            this.ChartSplitContainer2.Panel1.ResumeLayout(false);
            this.ChartSplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer2)).EndInit();
            this.ChartSplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer3)).EndInit();
            this.ChartSplitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer4)).EndInit();
            this.ChartSplitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button Editbutton;
        private PicMask BearingpicMask;
        private System.Windows.Forms.SplitContainer ChartSplitContainer1;
        private System.Windows.Forms.SplitContainer ChartSplitContainer2;
        private System.Windows.Forms.SplitContainer ChartSplitContainer3;
        private System.Windows.Forms.SplitContainer ChartSplitContainer4;
    }
}