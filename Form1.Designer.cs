﻿namespace ProjectMS
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Top_panel = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.Left_panel = new System.Windows.Forms.Panel();
            this.Button_panel = new System.Windows.Forms.Panel();
            this.Middle_panel = new System.Windows.Forms.Panel();
            this.Top_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Top_panel
            // 
            this.Top_panel.Controls.Add(this.Title);
            this.Top_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Top_panel.Location = new System.Drawing.Point(0, 0);
            this.Top_panel.Name = "Top_panel";
            this.Top_panel.Size = new System.Drawing.Size(784, 100);
            this.Top_panel.TabIndex = 0;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(41, 12);
            this.Title.TabIndex = 0;
            this.Title.Text = "label1";
            // 
            // Left_panel
            // 
            this.Left_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Left_panel.Location = new System.Drawing.Point(0, 100);
            this.Left_panel.Name = "Left_panel";
            this.Left_panel.Size = new System.Drawing.Size(115, 361);
            this.Left_panel.TabIndex = 1;
            // 
            // Button_panel
            // 
            this.Button_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button_panel.Location = new System.Drawing.Point(0, 461);
            this.Button_panel.Name = "Button_panel";
            this.Button_panel.Size = new System.Drawing.Size(784, 100);
            this.Button_panel.TabIndex = 2;
            // 
            // Middle_panel
            // 
            this.Middle_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Middle_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Middle_panel.Location = new System.Drawing.Point(115, 100);
            this.Middle_panel.Name = "Middle_panel";
            this.Middle_panel.Size = new System.Drawing.Size(669, 361);
            this.Middle_panel.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Middle_panel);
            this.Controls.Add(this.Left_panel);
            this.Controls.Add(this.Top_panel);
            this.Controls.Add(this.Button_panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Top_panel.ResumeLayout(false);
            this.Top_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Top_panel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Panel Left_panel;
        private System.Windows.Forms.Panel Button_panel;
        private System.Windows.Forms.Panel Middle_panel;
    }
}

