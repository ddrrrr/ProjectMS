namespace ProjectMS.Core.CardSetting
{
    partial class CardSettingFrm
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
            this.Toppanel = new System.Windows.Forms.Panel();
            this.Settinglabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.CardSettingDataGridView = new System.Windows.Forms.DataGridView();
            this.Toppanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CardSettingDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Toppanel
            // 
            this.Toppanel.Controls.Add(this.AddButton);
            this.Toppanel.Controls.Add(this.DeleteButton);
            this.Toppanel.Controls.Add(this.ChangeButton);
            this.Toppanel.Controls.Add(this.SaveButton);
            this.Toppanel.Controls.Add(this.Settinglabel);
            this.Toppanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Toppanel.Location = new System.Drawing.Point(0, 0);
            this.Toppanel.Name = "Toppanel";
            this.Toppanel.Size = new System.Drawing.Size(800, 30);
            this.Toppanel.TabIndex = 0;
            // 
            // Settinglabel
            // 
            this.Settinglabel.BackColor = System.Drawing.Color.Transparent;
            this.Settinglabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Settinglabel.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Settinglabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Settinglabel.Location = new System.Drawing.Point(0, 0);
            this.Settinglabel.Name = "Settinglabel";
            this.Settinglabel.Size = new System.Drawing.Size(118, 30);
            this.Settinglabel.TabIndex = 0;
            this.Settinglabel.Text = "采集卡设置";
            // 
            // SaveButton
            // 
            this.SaveButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveButton.Location = new System.Drawing.Point(725, 0);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 30);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ChangeButton
            // 
            this.ChangeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ChangeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChangeButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChangeButton.Location = new System.Drawing.Point(650, 0);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(75, 30);
            this.ChangeButton.TabIndex = 2;
            this.ChangeButton.Text = "修改";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeleteButton.Location = new System.Drawing.Point(575, 0);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 30);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "删除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddButton.Location = new System.Drawing.Point(500, 0);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 30);
            this.AddButton.TabIndex = 4;
            this.AddButton.Text = "添加";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // CardSettingDataGridView
            // 
            this.CardSettingDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CardSettingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CardSettingDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardSettingDataGridView.Location = new System.Drawing.Point(0, 30);
            this.CardSettingDataGridView.Name = "CardSettingDataGridView";
            this.CardSettingDataGridView.RowTemplate.Height = 23;
            this.CardSettingDataGridView.Size = new System.Drawing.Size(800, 420);
            this.CardSettingDataGridView.TabIndex = 1;
            // 
            // CardSettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CardSettingDataGridView);
            this.Controls.Add(this.Toppanel);
            this.Name = "CardSettingFrm";
            this.Text = "CardSettingFrm";
            this.Toppanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CardSettingDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Toppanel;
        private System.Windows.Forms.Label Settinglabel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView CardSettingDataGridView;
    }
}