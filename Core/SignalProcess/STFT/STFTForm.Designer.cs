namespace ProjectMS.Core.SignalProcess.STFT
{
    partial class STFTForm
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
            this.ParaPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.ParaVisibleButton = new System.Windows.Forms.Button();
            this.ColorBarPanel = new System.Windows.Forms.Panel();
            this.ColorBarHighlabel = new System.Windows.Forms.Label();
            this.ColorBarLowlabel = new System.Windows.Forms.Label();
            this.ColorBarPictureBox = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.STFTPictureBox1 = new System.Windows.Forms.PictureBox();
            this.STFTTitleLabel1 = new System.Windows.Forms.Label();
            this.STFTPictureBox2 = new System.Windows.Forms.PictureBox();
            this.STFTTitleLabel2 = new System.Windows.Forms.Label();
            this.ColorBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBarPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.STFTPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.STFTPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ParaPropertyGrid
            // 
            this.ParaPropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.ParaPropertyGrid.Location = new System.Drawing.Point(605, 0);
            this.ParaPropertyGrid.Name = "ParaPropertyGrid";
            this.ParaPropertyGrid.Size = new System.Drawing.Size(195, 450);
            this.ParaPropertyGrid.TabIndex = 0;
            // 
            // ParaVisibleButton
            // 
            this.ParaVisibleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ParaVisibleButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ParaVisibleButton.Location = new System.Drawing.Point(589, 0);
            this.ParaVisibleButton.Name = "ParaVisibleButton";
            this.ParaVisibleButton.Size = new System.Drawing.Size(16, 450);
            this.ParaVisibleButton.TabIndex = 1;
            this.ParaVisibleButton.Text = "▶";
            this.ParaVisibleButton.UseVisualStyleBackColor = true;
            this.ParaVisibleButton.Click += new System.EventHandler(this.ParaVisibleButton_Click);
            // 
            // ColorBarPanel
            // 
            this.ColorBarPanel.Controls.Add(this.ColorBarHighlabel);
            this.ColorBarPanel.Controls.Add(this.ColorBarLowlabel);
            this.ColorBarPanel.Controls.Add(this.ColorBarPictureBox);
            this.ColorBarPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ColorBarPanel.Location = new System.Drawing.Point(0, 400);
            this.ColorBarPanel.Name = "ColorBarPanel";
            this.ColorBarPanel.Size = new System.Drawing.Size(589, 50);
            this.ColorBarPanel.TabIndex = 2;
            // 
            // ColorBarHighlabel
            // 
            this.ColorBarHighlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorBarHighlabel.Location = new System.Drawing.Point(553, 35);
            this.ColorBarHighlabel.Name = "ColorBarHighlabel";
            this.ColorBarHighlabel.Size = new System.Drawing.Size(30, 12);
            this.ColorBarHighlabel.TabIndex = 2;
            this.ColorBarHighlabel.Text = "高";
            this.ColorBarHighlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorBarLowlabel
            // 
            this.ColorBarLowlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ColorBarLowlabel.AutoSize = true;
            this.ColorBarLowlabel.Location = new System.Drawing.Point(12, 35);
            this.ColorBarLowlabel.Name = "ColorBarLowlabel";
            this.ColorBarLowlabel.Size = new System.Drawing.Size(17, 12);
            this.ColorBarLowlabel.TabIndex = 1;
            this.ColorBarLowlabel.Text = "低";
            // 
            // ColorBarPictureBox
            // 
            this.ColorBarPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorBarPictureBox.Location = new System.Drawing.Point(12, 6);
            this.ColorBarPictureBox.Name = "ColorBarPictureBox";
            this.ColorBarPictureBox.Size = new System.Drawing.Size(571, 22);
            this.ColorBarPictureBox.TabIndex = 0;
            this.ColorBarPictureBox.TabStop = false;
            this.ColorBarPictureBox.Resize += new System.EventHandler(this.ColorBarPictureBox_Resize);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.STFTPictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.STFTTitleLabel1);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.STFTPictureBox2);
            this.splitContainer1.Panel2.Controls.Add(this.STFTTitleLabel2);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.splitContainer1_Panel2_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(589, 400);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 3;
            // 
            // STFTPictureBox1
            // 
            this.STFTPictureBox1.Location = new System.Drawing.Point(3, 40);
            this.STFTPictureBox1.Name = "STFTPictureBox1";
            this.STFTPictureBox1.Size = new System.Drawing.Size(274, 336);
            this.STFTPictureBox1.TabIndex = 1;
            this.STFTPictureBox1.TabStop = false;
            this.STFTPictureBox1.Resize += new System.EventHandler(this.STFTPictureBox1_Resize);
            // 
            // STFTTitleLabel1
            // 
            this.STFTTitleLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.STFTTitleLabel1.Location = new System.Drawing.Point(0, 0);
            this.STFTTitleLabel1.Name = "STFTTitleLabel1";
            this.STFTTitleLabel1.Size = new System.Drawing.Size(302, 25);
            this.STFTTitleLabel1.TabIndex = 0;
            this.STFTTitleLabel1.Text = "z轴时频图";
            this.STFTTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // STFTPictureBox2
            // 
            this.STFTPictureBox2.Location = new System.Drawing.Point(90, 176);
            this.STFTPictureBox2.Name = "STFTPictureBox2";
            this.STFTPictureBox2.Size = new System.Drawing.Size(100, 50);
            this.STFTPictureBox2.TabIndex = 1;
            this.STFTPictureBox2.TabStop = false;
            this.STFTPictureBox2.Resize += new System.EventHandler(this.STFTPictureBox2_Resize);
            // 
            // STFTTitleLabel2
            // 
            this.STFTTitleLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.STFTTitleLabel2.Location = new System.Drawing.Point(0, 0);
            this.STFTTitleLabel2.Name = "STFTTitleLabel2";
            this.STFTTitleLabel2.Size = new System.Drawing.Size(283, 25);
            this.STFTTitleLabel2.TabIndex = 0;
            this.STFTTitleLabel2.Text = "x轴时频图";
            this.STFTTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // STFTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ColorBarPanel);
            this.Controls.Add(this.ParaVisibleButton);
            this.Controls.Add(this.ParaPropertyGrid);
            this.Name = "STFTForm";
            this.Text = "STFTForm";
            this.ColorBarPanel.ResumeLayout(false);
            this.ColorBarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBarPictureBox)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.STFTPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.STFTPictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ParaVisibleButton;
        private System.Windows.Forms.Panel ColorBarPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label STFTTitleLabel1;
        private System.Windows.Forms.Label STFTTitleLabel2;
        private System.Windows.Forms.PictureBox STFTPictureBox1;
        private System.Windows.Forms.PictureBox STFTPictureBox2;
        private System.Windows.Forms.PictureBox ColorBarPictureBox;
        private System.Windows.Forms.Label ColorBarHighlabel;
        private System.Windows.Forms.Label ColorBarLowlabel;
        public System.Windows.Forms.PropertyGrid ParaPropertyGrid;
    }
}