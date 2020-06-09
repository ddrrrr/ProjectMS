using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjectMS.Core.SignalProcess.RUL
{
    class rmsProcess : ISignalProcess
    {
        public rmsProcess()
        {
            initrmsform();
        }
        ChartForm rmsform;
        private void initrmsform()
        {
            rmsform = new ChartForm();
            rmsform.chart1.BackColor = SystemColors.ControlDarkDark;
            rmsform.chart1.ForeColor = SystemColors.ControlLightLight;
            rmsform.chart1.Titles.Add("RMS");
            rmsform.chart1.ChartAreas[0].AxisX.Name = "时间";
            rmsform.chart1.ChartAreas[0].AxisY.Name = "幅值";

            rmsform.chart1.Series.Add("z轴RMS");
            rmsform.chart1.Series["z轴RMS"].ChartType = SeriesChartType.FastLine;
            rmsform.chart1.Series["z轴RMS"].Color = Color.Red;
            rmsform.chart1.Series.Add("x轴RMS");
            rmsform.chart1.Series["x轴RMS"].ChartType = SeriesChartType.FastLine;
            rmsform.chart1.Series["x轴RMS"].Color = Color.Yellow;
        }

        public Form Process(List<Tuple<string, double[], double[]>> indata)
        {
            foreach (var onedata in indata)
            {
                if (onedata.Item2.Length != 0)
                {
                    string tmpsignal;
                    if (onedata.Item1 == "z轴信号")
                        tmpsignal = "z轴RMS";
                    else if (onedata.Item1 == "x轴信号")
                        tmpsignal = "x轴RMS";
                    else
                        continue;

                    double time = onedata.Item2[onedata.Item2.Length-1];
                    double value = 0;
                    for (int i = 0; i < onedata.Item3.Length; i++)
                        value += Math.Pow(onedata.Item3[i], 2);
                    value = Math.Sqrt(value / onedata.Item3.Length);

                    rmsform.chart1.Series[tmpsignal].Points.AddXY(time, value);
                }
            }
            return rmsform;
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
                        tmpsignal = "z轴RMS";
                    else if (onedata.Item1 == "x轴信号")
                        tmpsignal = "x轴RMS";
                    else
                        continue;

                    double time = onedata.Item2[onedata.Item2.Length - 1];
                    double value = 0;
                    for (int i = 0; i < onedata.Item3.Length; i++)
                        value += Math.Pow(onedata.Item3[i], 2);
                    value = Math.Sqrt(value / onedata.Item3.Length);

                    rmsform.chart1.Series[tmpsignal].Points.AddXY(time, value);
                    flag = true;
                }
            }
            return flag;
        }
    }
}
