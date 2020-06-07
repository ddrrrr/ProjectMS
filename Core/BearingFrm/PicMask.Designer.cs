namespace ProjectMS.Core.BearingFrm
{
    partial class PicMask
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RightClickcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectSensortoolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.Tiplabel = new System.Windows.Forms.Label();
            this.ColorBarpictureBox = new System.Windows.Forms.PictureBox();
            this.ColorBarHighLabel = new System.Windows.Forms.Label();
            this.ColorBarLowLabel = new System.Windows.Forms.Label();
            this.RightClickcontextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBarpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RightClickcontextMenuStrip
            // 
            this.RightClickcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.toolStripSeparator1,
            this.SelectSensortoolStripComboBox});
            this.RightClickcontextMenuStrip.Name = "contextMenuStrip1";
            this.RightClickcontextMenuStrip.Size = new System.Drawing.Size(182, 61);
            this.RightClickcontextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.RightClickcontextMenuStrip_Opening);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // SelectSensortoolStripComboBox
            // 
            this.SelectSensortoolStripComboBox.Name = "SelectSensortoolStripComboBox";
            this.SelectSensortoolStripComboBox.Size = new System.Drawing.Size(121, 25);
            this.SelectSensortoolStripComboBox.Text = "选择传感器";
            this.SelectSensortoolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectSensortoolStripComboBox_SelectedIndexChanged);
            // 
            // Tiplabel
            // 
            this.Tiplabel.AutoSize = true;
            this.Tiplabel.Location = new System.Drawing.Point(31, 39);
            this.Tiplabel.Name = "Tiplabel";
            this.Tiplabel.Size = new System.Drawing.Size(0, 12);
            this.Tiplabel.TabIndex = 1;
            // 
            // ColorBarpictureBox
            // 
            this.ColorBarpictureBox.Location = new System.Drawing.Point(3, 3);
            this.ColorBarpictureBox.Name = "ColorBarpictureBox";
            this.ColorBarpictureBox.Size = new System.Drawing.Size(15, 100);
            this.ColorBarpictureBox.TabIndex = 2;
            this.ColorBarpictureBox.TabStop = false;
            // 
            // ColorBarHighLabel
            // 
            this.ColorBarHighLabel.AutoSize = true;
            this.ColorBarHighLabel.Location = new System.Drawing.Point(24, 3);
            this.ColorBarHighLabel.Name = "ColorBarHighLabel";
            this.ColorBarHighLabel.Size = new System.Drawing.Size(17, 12);
            this.ColorBarHighLabel.TabIndex = 3;
            this.ColorBarHighLabel.Text = "高";
            // 
            // ColorBarLowLabel
            // 
            this.ColorBarLowLabel.AutoSize = true;
            this.ColorBarLowLabel.Location = new System.Drawing.Point(24, 91);
            this.ColorBarLowLabel.Name = "ColorBarLowLabel";
            this.ColorBarLowLabel.Size = new System.Drawing.Size(17, 12);
            this.ColorBarLowLabel.TabIndex = 4;
            this.ColorBarLowLabel.Text = "低";
            // 
            // PicMask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ColorBarLowLabel);
            this.Controls.Add(this.ColorBarHighLabel);
            this.Controls.Add(this.ColorBarpictureBox);
            this.Controls.Add(this.Tiplabel);
            this.Name = "PicMask";
            this.Size = new System.Drawing.Size(290, 253);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicMask_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicMask_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicMask_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicMask_MouseUp);
            this.Resize += new System.EventHandler(this.PicMask_Resize);
            this.RightClickcontextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColorBarpictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip RightClickcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox SelectSensortoolStripComboBox;
        private System.Windows.Forms.Label Tiplabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox ColorBarpictureBox;
        private System.Windows.Forms.Label ColorBarHighLabel;
        private System.Windows.Forms.Label ColorBarLowLabel;
    }
}
