using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.BearingFrm
{
    public partial class BearingFrom : Form
    {
        public BearingFrom()
        {
            InitializeComponent();
            PictureBoxResize();
            this.BackColor = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlLightLight;
            _isEditing = false;
            BearingpicMask.Dock = DockStyle.Fill;
            pictureBox.Controls.Add(BearingpicMask);
            BearingpicMask.SetColorBarHighLow(new Tuple<string, string>("8.8g", "0g"));
            BearingpicMask.LoadSensorInfo("Bearing");
        }

        /// <summary>
        /// 图片自适应大小
        /// </summary>
        private void PictureBoxResize()
        {
            double picRatio = Convert.ToDouble(pictureBox.BackgroundImage.Width) / pictureBox.BackgroundImage.Height;
            double panelRatio = Convert.ToDouble(splitContainer.Panel1.Width) / splitContainer.Panel1.Height;
            if (picRatio >= panelRatio)
            {
                pictureBox.Width = splitContainer.Panel1.Width;
                pictureBox.Height = Convert.ToInt32(pictureBox.Width / picRatio);
            }
            else
            {
                pictureBox.Height = splitContainer.Panel1.Height;
                pictureBox.Width = Convert.ToInt32(pictureBox.Height * picRatio);
            }
        }

        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            PictureBoxResize();
        }

        private bool _isEditing;
        private void Editbutton_Click(object sender, EventArgs e)
        {
            _isEditing = !_isEditing;
            BearingpicMask.IsEditing = _isEditing;
            if (!_isEditing)
            {
                BearingpicMask.SaveSensorInfo();
            }
        }
    }
}
