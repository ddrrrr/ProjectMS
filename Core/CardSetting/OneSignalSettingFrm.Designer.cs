namespace ProjectMS.Core.CardSetting
{
    partial class OneSignalSettingFrm
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
            this.OneSignalSettingPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.CheckButton = new System.Windows.Forms.Button();
            this.CancelSettingButton = new System.Windows.Forms.Button();
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OneSignalSettingPropertyGrid
            // 
            this.OneSignalSettingPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OneSignalSettingPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.OneSignalSettingPropertyGrid.Name = "OneSignalSettingPropertyGrid";
            this.OneSignalSettingPropertyGrid.Size = new System.Drawing.Size(394, 372);
            this.OneSignalSettingPropertyGrid.TabIndex = 0;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.ButtonPanel.Controls.Add(this.CheckButton);
            this.ButtonPanel.Controls.Add(this.CancelSettingButton);
            this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 342);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(394, 30);
            this.ButtonPanel.TabIndex = 1;
            // 
            // CheckButton
            // 
            this.CheckButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckButton.Location = new System.Drawing.Point(226, 4);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(75, 23);
            this.CheckButton.TabIndex = 1;
            this.CheckButton.Text = "确认";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // CancelSettingButton
            // 
            this.CancelSettingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelSettingButton.Location = new System.Drawing.Point(307, 4);
            this.CancelSettingButton.Name = "CancelSettingButton";
            this.CancelSettingButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSettingButton.TabIndex = 0;
            this.CancelSettingButton.Text = "取消";
            this.CancelSettingButton.UseVisualStyleBackColor = true;
            this.CancelSettingButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OneSignalSettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 372);
            this.Controls.Add(this.ButtonPanel);
            this.Controls.Add(this.OneSignalSettingPropertyGrid);
            this.Name = "OneSignalSettingFrm";
            this.Text = "OneSignalSettingFrm";
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid OneSignalSettingPropertyGrid;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.Button CancelSettingButton;
    }
}