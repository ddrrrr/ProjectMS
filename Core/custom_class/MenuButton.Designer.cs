namespace ProjectMS.Core.custom_class
{
    partial class MenuButton
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
            this.ButtonText = new System.Windows.Forms.Label();
            this.ButtonBackPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonBackPic)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonText
            // 
            this.ButtonText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonText.Location = new System.Drawing.Point(0, 0);
            this.ButtonText.Name = "ButtonText";
            this.ButtonText.Size = new System.Drawing.Size(150, 150);
            this.ButtonText.TabIndex = 0;
            this.ButtonText.Text = "label1";
            this.ButtonText.Click += new System.EventHandler(this.buttonText_Click);
            this.ButtonText.MouseLeave += new System.EventHandler(this.ButtonBackPic_MouseLeave);
            this.ButtonText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ButtonBackPic_MouseMove);
            // 
            // ButtonBackPic
            // 
            this.ButtonBackPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonBackPic.Location = new System.Drawing.Point(0, 0);
            this.ButtonBackPic.Name = "ButtonBackPic";
            this.ButtonBackPic.Size = new System.Drawing.Size(150, 150);
            this.ButtonBackPic.TabIndex = 1;
            this.ButtonBackPic.TabStop = false;
            this.ButtonBackPic.Click += new System.EventHandler(this.buttonText_Click);
            this.ButtonBackPic.MouseLeave += new System.EventHandler(this.ButtonBackPic_MouseLeave);
            this.ButtonBackPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ButtonBackPic_MouseMove);
            // 
            // MenuButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButtonText);
            this.Controls.Add(this.ButtonBackPic);
            this.Name = "MenuButton";
            this.Load += new System.EventHandler(this.MenuButton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonBackPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ButtonText;
        private System.Windows.Forms.PictureBox ButtonBackPic;
    }
}
