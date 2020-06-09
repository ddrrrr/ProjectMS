using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.SignalProcess.Filter
{
    public partial class FilterForm : Form
    {
        public FilterForm()
        {
            InitializeComponent();
            this.BackColor = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlLightLight;
            DisplayChart1.BackColor = this.BackColor;
            DisplayChart1.ForeColor = this.ForeColor;
            DisplayChart2.BackColor = this.BackColor;
            DisplayChart2.ForeColor = this.ForeColor;
        }

        private void SettingPanelVisbleButton_Click(object sender, EventArgs e)
        {
            SettingPanel.Visible = !SettingPanel.Visible;
            if (SettingPanel.Visible)
                SettingPanelVisbleButton.Text = "▶";
            else
                SettingPanelVisbleButton.Text = "◀";
        }
    }
}
