using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjectMS.Core.SignalProcess.FFT
{
    class fft : ISignalProcess
    {
        public fft()
        {
            initfftform();
            samplerate = 1000;
        }
        /// <summary>
        /// 数据采样频率
        /// </summary>
        double samplerate;
        public void SetSamplerate(double rate)
        {
            samplerate = rate;
        }
        /// <summary>
        /// 频谱图窗口
        /// </summary>
        ChartForm fftform;
        private void initfftform()
        {
            fftform = new ChartForm();
            fftform.chart1.BackColor = SystemColors.ControlDarkDark;
            fftform.chart1.ForeColor = SystemColors.ControlLightLight;
            fftform.chart1.Titles.Add("频谱图");
            fftform.chart1.ChartAreas[0].AxisX.Name = "频率";
            fftform.chart1.ChartAreas[0].AxisY.Name = "幅值";

            fftform.chart1.Series.Add("z轴频谱");
            fftform.chart1.Series["z轴频谱"].ChartType = SeriesChartType.FastLine;
            fftform.chart1.Series["z轴频谱"].Color = Color.Red;
            fftform.chart1.Series.Add("x轴频谱");
            fftform.chart1.Series["x轴频谱"].ChartType = SeriesChartType.FastLine;
            fftform.chart1.Series["x轴频谱"].Color = Color.Yellow;
        }
        public Form Process(List<Tuple<string, double[], double[]>> indata)
        {
            foreach (var onedata in indata)
            {
                if (onedata.Item2.Length != 0)
                {
                    string tmpsignal;
                    if (onedata.Item1 == "z轴信号")
                        tmpsignal = "z轴频谱";
                    else if (onedata.Item1 == "x轴信号")
                        tmpsignal = "x轴频谱";
                    else
                        continue;

                    fftbase.fftProcess(onedata.Item3, out double[] fft_value);
                    double[] f = new double[onedata.Item3.Length / 2];
                    double fenduzhi = samplerate / onedata.Item3.Length;
                    double tmpf = 0;
                    for (int i = 0; i < onedata.Item3.Length / 2; i++,tmpf+=fenduzhi)
                        f[i] = tmpf;
                    fftform.chart1.Series[tmpsignal].Points.Clear();
                    fftform.chart1.Series[tmpsignal].Points.AddXY(f, fft_value);
                }
            }
            return fftform;
        }

        public bool Update(List<Tuple<string, double[], double[]>> indata)
        {
            bool flag = false;
            foreach (var onedata in indata)
            {
                if (onedata.Item2.Length != 0)
                {
                    string tmpsignal;
                    if (onedata.Item1 == "z轴信号")
                        tmpsignal = "z轴频谱";
                    else if (onedata.Item1 == "x轴信号")
                        tmpsignal = "x轴频谱";
                    else
                        continue;

                    fftbase.fftProcess(onedata.Item3, out double[] fft_value);
                    double[] f = new double[onedata.Item3.Length / 2];
                    double fenduzhi = samplerate / onedata.Item3.Length;
                    double tmpf = 0;
                    for (int i = 0; i < onedata.Item3.Length / 2; i++, tmpf += fenduzhi)
                        f[i] = tmpf;
                    fftform.chart1.Series[tmpsignal].Points.Clear();
                    fftform.chart1.Series[tmpsignal].Points.AddXY(f, fft_value);
                    flag = true;
                }
            }
            return flag;
        }

        
    }

    
}
