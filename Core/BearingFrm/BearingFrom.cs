using ProjectMS.Core.SignalProcess.FFT;
using ProjectMS.Core.SignalProcess.Filter;
using ProjectMS.Core.SignalProcess.RUL;
using ProjectMS.Core.SignalProcess.STFT;
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
            initChart();
        }

        FilterCollection filtercollection;
        fft fftProcess;
        STFTProcess stftProcess;
        rmsProcess rmsprocess;
        rulProcess rulprocess;
        private void initChart()
        {
            ChartSplitContainer1.BorderStyle = BorderStyle.Fixed3D;
            ChartSplitContainer2.BorderStyle = BorderStyle.Fixed3D;
            ChartSplitContainer3.BorderStyle = BorderStyle.Fixed3D;
            ChartSplitContainer4.BorderStyle = BorderStyle.Fixed3D;

            filtercollection = new FilterCollection();
            Form filterform = filtercollection.Process(new List<Tuple<string, double[], double[]>>());
            filterform.TopLevel = false;
            filterform.FormBorderStyle = FormBorderStyle.None;
            filterform.Dock = DockStyle.Fill;
            ChartSplitContainer1.Panel1.Controls.Add(filterform);
            filterform.Show();

            fftProcess = new fft();
            Form fftfrom = fftProcess.Process(new List<Tuple<string, double[], double[]>>());
            fftfrom.TopLevel = false;
            fftfrom.FormBorderStyle = FormBorderStyle.None;
            fftfrom.Dock = DockStyle.Fill;
            ChartSplitContainer3.Panel1.Controls.Add(fftfrom);
            fftfrom.Show();

            stftProcess = new STFTProcess();
            Form stftform = stftProcess.Process(new List<Tuple<string, double[], double[]>>());
            stftform.TopLevel = false;
            stftform.FormBorderStyle = FormBorderStyle.None;
            stftform.Dock = DockStyle.Fill;
            ChartSplitContainer3.Panel2.Controls.Add(stftform);
            stftform.Show();

            rmsprocess = new rmsProcess();
            Form rmsform = rmsprocess.Process(new List<Tuple<string, double[], double[]>>());
            rmsform.TopLevel = false;
            rmsform.FormBorderStyle = FormBorderStyle.None;
            rmsform.Dock = DockStyle.Fill;
            ChartSplitContainer4.Panel1.Controls.Add(rmsform);
            rmsform.Show();

            rulprocess = new rulProcess();
            Form rulform = rulprocess.Process(new List<Tuple<string, double[], double[]>>());
            rulform.TopLevel = false;
            rulform.FormBorderStyle = FormBorderStyle.None;
            rulform.Dock = DockStyle.Fill;
            ChartSplitContainer4.Panel2.Controls.Add(rulform);
            rulform.Show();
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
