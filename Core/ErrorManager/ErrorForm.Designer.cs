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
            this.Settingpanel = new System.Windows.Forms.Panel();
            this.Visiblebutton = new System.Windows.Forms.Button();
            this.Cleanbutton = new System.Windows.Forms.Button();
            this.SelectgroupBox = new System.Windows.Forms.GroupBox();
            this.SelectcheckedListBox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridview)).BeginInit();
            this.Settingpanel.SuspendLayout();
            this.SelectgroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ErrorGridview
            // 
            this.ErrorGridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ErrorGridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorGridview.Location = new System.Drawing.Point(0, 0);
            this.ErrorGridview.Name = "ErrorGridview";
            this.ErrorGridview.RowTemplate.Height = 23;
            this.ErrorGridview.Size = new System.Drawing.Size(684, 450);
            this.ErrorGridview.TabIndex = 0;
            // 
            // Settingpanel
            // 
            this.Settingpanel.Controls.Add(this.SelectgroupBox);
            this.Settingpanel.Controls.Add(this.Cleanbutton);
            this.Settingpanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.Settingpanel.Location = new System.Drawing.Point(700, 0);
            this.Settingpanel.Name = "Settingpanel";
            this.Settingpanel.Size = new System.Drawing.Size(100, 450);
            this.Settingpanel.TabIndex = 1;
            // 
            // Visiblebutton
            // 
            this.Visiblebutton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Visiblebutton.Dock = System.Windows.Forms.DockStyle.Right;
            this.Visiblebutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Visiblebutton.Location = new System.Drawing.Point(684, 0);
            this.Visiblebutton.Name = "Visiblebutton";
            this.Visiblebutton.Size = new System.Drawing.Size(16, 450);
            this.Visiblebutton.TabIndex = 2;
            this.Visiblebutton.Text = "▶";
            this.Visiblebutton.UseVisualStyleBackColor = true;
            this.Visiblebutton.Click += new System.EventHandler(this.Visiblebutton_Click);
            // 
            // Cleanbutton
            // 
            this.Cleanbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Cleanbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cleanbutton.Location = new System.Drawing.Point(0, 427);
            this.Cleanbutton.Name = "Cleanbutton";
            this.Cleanbutton.Size = new System.Drawing.Size(100, 23);
            this.Cleanbutton.TabIndex = 0;
            this.Cleanbutton.Text = "清空所有";
            this.Cleanbutton.UseVisualStyleBackColor = true;
            this.Cleanbutton.Click += new System.EventHandler(this.Cleanbutton_Click);
            // 
            // SelectgroupBox
            // 
            this.SelectgroupBox.Controls.Add(this.SelectcheckedListBox);
            this.SelectgroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectgroupBox.Location = new System.Drawing.Point(0, 0);
            this.SelectgroupBox.Name = "SelectgroupBox";
            this.SelectgroupBox.Size = new System.Drawing.Size(100, 427);
            this.SelectgroupBox.TabIndex = 1;
            this.SelectgroupBox.TabStop = false;
            this.SelectgroupBox.Text = "筛选";
            // 
            // SelectcheckedListBox
            // 
            this.SelectcheckedListBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SelectcheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectcheckedListBox.FormattingEnabled = true;
            this.SelectcheckedListBox.Location = new System.Drawing.Point(3, 17);
            this.SelectcheckedListBox.Name = "SelectcheckedListBox";
            this.SelectcheckedListBox.Size = new System.Drawing.Size(94, 407);
            this.SelectcheckedListBox.TabIndex = 0;
            this.SelectcheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectcheckedListBox_ItemCheck);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ErrorGridview);
            this.Controls.Add(this.Visiblebutton);
            this.Controls.Add(this.Settingpanel);
            this.Name = "ErrorForm";
            this.Text = "ErrorForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridview)).EndInit();
            this.Settingpanel.ResumeLayout(false);
            this.SelectgroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ErrorGridview;
        private System.Windows.Forms.Panel Settingpanel;
        private System.Windows.Forms.Button Visiblebutton;
        private System.Windows.Forms.Button Cleanbutton;
        private System.Windows.Forms.GroupBox SelectgroupBox;
        private System.Windows.Forms.CheckedListBox SelectcheckedListBox;
    }
}