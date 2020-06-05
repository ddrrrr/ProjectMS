namespace ProjectMS.Core.ErrorManager
{
    partial class ErrorForm
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
            this.ErrorGridview = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridview)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorGridview
            // 
            this.ErrorGridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ErrorGridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorGridview.Location = new System.Drawing.Point(0, 0);
            this.ErrorGridview.Name = "ErrorGridview";
            this.ErrorGridview.RowTemplate.Height = 23;
            this.ErrorGridview.Size = new System.Drawing.Size(800, 450);
            this.ErrorGridview.TabIndex = 0;
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ErrorGridview);
            this.Name = "ErrorForm";
            this.Text = "ErrorForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ErrorGridview;
    }
}