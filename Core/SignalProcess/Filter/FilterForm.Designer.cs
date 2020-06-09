namespace ProjectMS.Core.SignalProcess.Filter
{
    partial class FilterForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.DisplayChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SettingPanel = new System.Windows.Forms.Panel();
            this.FilterPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.FilterSelectComboBox = new System.Windows.Forms.ComboBox();
            this.SettingPanelVisbleButton = new System.Windows.Forms.Button();
            this.ChartSplitContainer = new System.Windows.Forms.SplitContainer();
            this.DisplayChart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayChart1)).BeginInit();
            this.SettingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer)).BeginInit();
            this.ChartSplitContainer.Panel1.SuspendLayout();
            this.ChartSplitContainer.Panel2.SuspendLayout();
            this.ChartSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayChart2)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayChart1
            // 
            this.DisplayChart1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.DisplayChart1.ChartAreas.Add(chartArea1);
            this.DisplayChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.DisplayChart1.Legends.Add(legend1);
            this.DisplayChart1.Location = new System.Drawing.Point(0, 0);
            this.DisplayChart1.Name = "DisplayChart1";
            this.DisplayChart1.Size = new System.Drawing.Size(653, 144);
            this.DisplayChart1.TabIndex = 0;
            this.DisplayChart1.Text = "chart1";
            // 
            // SettingPanel
            // 
            this.SettingPanel.BackColor = System.Drawing.Color.Transparent;
            this.SettingPanel.Controls.Add(this.FilterPropertyGrid);
            this.SettingPanel.Controls.Add(this.FilterSelectComboBox);
            this.SettingPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.SettingPanel.Location = new System.Drawing.Point(669, 0);
            this.SettingPanel.Name = "SettingPanel";
            this.SettingPanel.Size = new System.Drawing.Size(131, 289);
            this.SettingPanel.TabIndex = 1;
            // 
            // FilterPropertyGrid
            // 
            this.FilterPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterPropertyGrid.Location = new System.Drawing.Point(0, 20);
            this.FilterPropertyGrid.Name = "FilterPropertyGrid";
            this.FilterPropertyGrid.Size = new System.Drawing.Size(131, 269);
            this.FilterPropertyGrid.TabIndex = 0;
            // 
            // FilterSelectComboBox
            // 
            this.FilterSelectComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.FilterSelectComboBox.FormattingEnabled = true;
            this.FilterSelectComboBox.Location = new System.Drawing.Point(0, 0);
            this.FilterSelectComboBox.Name = "FilterSelectComboBox";
            this.FilterSelectComboBox.Size = new System.Drawing.Size(131, 20);
            this.FilterSelectComboBox.TabIndex = 1;
            // 
            // SettingPanelVisbleButton
            // 
            this.SettingPanelVisbleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SettingPanelVisbleButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SettingPanelVisbleButton.Location = new System.Drawing.Point(653, 0);
            this.SettingPanelVisbleButton.Name = "SettingPanelVisbleButton";
            this.SettingPanelVisbleButton.Size = new System.Drawing.Size(16, 289);
            this.SettingPanelVisbleButton.TabIndex = 2;
            this.SettingPanelVisbleButton.Text = "▶";
            this.SettingPanelVisbleButton.UseVisualStyleBackColor = true;
            this.SettingPanelVisbleButton.Click += new System.EventHandler(this.SettingPanelVisbleButton_Click);
            // 
            // ChartSplitContainer
            // 
            this.ChartSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ChartSplitContainer.Name = "ChartSplitContainer";
            this.ChartSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ChartSplitContainer.Panel1
            // 
            this.ChartSplitContainer.Panel1.Controls.Add(this.DisplayChart1);
            // 
            // ChartSplitContainer.Panel2
            // 
            this.ChartSplitContainer.Panel2.Controls.Add(this.DisplayChart2);
            this.ChartSplitContainer.Size = new System.Drawing.Size(653, 289);
            this.ChartSplitContainer.SplitterDistance = 144;
            this.ChartSplitContainer.TabIndex = 3;
            // 
            // DisplayChart2
            // 
            this.DisplayChart2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.DisplayChart2.ChartAreas.Add(chartArea2);
            this.DisplayChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.DisplayChart2.Legends.Add(legend2);
            this.DisplayChart2.Location = new System.Drawing.Point(0, 0);
            this.DisplayChart2.Name = "DisplayChart2";
            this.DisplayChart2.Size = new System.Drawing.Size(653, 141);
            this.DisplayChart2.TabIndex = 0;
            this.DisplayChart2.Text = "chart1";
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 289);
            this.Controls.Add(this.ChartSplitContainer);
            this.Controls.Add(this.SettingPanelVisbleButton);
            this.Controls.Add(this.SettingPanel);
            this.Name = "FilterForm";
            this.Text = "FilterForm";
            ((System.ComponentModel.ISupportInitialize)(this.DisplayChart1)).EndInit();
            this.SettingPanel.ResumeLayout(false);
            this.ChartSplitContainer.Panel1.ResumeLayout(false);
            this.ChartSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer)).EndInit();
            this.ChartSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayChart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel SettingPanel;
        private System.Windows.Forms.Button SettingPanelVisbleButton;
        public System.Windows.Forms.DataVisualization.Charting.Chart DisplayChart1;
        public System.Windows.Forms.PropertyGrid FilterPropertyGrid;
        public System.Windows.Forms.ComboBox FilterSelectComboBox;
        private System.Windows.Forms.SplitContainer ChartSplitContainer;
        public System.Windows.Forms.DataVisualization.Charting.Chart DisplayChart2;
    }
}