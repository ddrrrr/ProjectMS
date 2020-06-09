using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;

namespace ProjectMS.Core.SignalProcess.RUL
{
    class rulProcess : ISignalProcess
    {
        List<FeaRam> features;
        FeatureNet featurenet;
        RegLSTM reglstm;
        public rulProcess()
        {
            features = new List<FeaRam>();
            for (int i = 0; i < 7; i++)
                features.Add(new FeaRam(16));
            featurenet = new FeatureNet();
            reglstm = new RegLSTM();
            initrulform();
        }

        ChartForm rulform;
        private void initrulform()
        {
            rulform = new ChartForm();
            rulform.chart1.BackColor = SystemColors.ControlDarkDark;
            rulform.chart1.ForeColor = SystemColors.ControlLightLight;
            rulform.chart1.Titles.Add("RUL");
            rulform.chart1.ChartAreas[0].AxisX.Name = "时间";
            rulform.chart1.ChartAreas[0].AxisY.Name = "剩余寿命";

            rulform.chart1.Series.Add("剩余寿命");
            rulform.chart1.Series["剩余寿命"].ChartType = SeriesChartType.FastLine;
            rulform.chart1.Series["剩余寿命"].Color = Color.Blue;
        }

        public Form Process(List<Tuple<string, double[], double[]>> indata)
        {
            var z_data = indata.Find(one_data => one_data.Item1 == "z轴信号");
            var x_data = indata.Find(one_data => one_data.Item1 == "x轴信号");
            if (z_data!=null && x_data!=null)
            {
                double[] onefea = featurenet.Process(z_data.Item3, x_data.Item3);
                UpdateFeatures(onefea);
                List<double[]> all_fea = GetFeatures();
                double rul = reglstm.Process(all_fea);
                double time = z_data.Item2[z_data.Item2.Length - 1];
                rulform.chart1.Series["剩余寿命"].Points.AddXY(time, rul);
            }
            return rulform;
        }

        public bool Update(List<Tuple<string, double[], double[]>> indata)
        {
            bool flag = false;
            var z_data = indata.Find(one_data => one_data.Item1 == "z轴信号");
            var x_data = indata.Find(one_data => one_data.Item1 == "x轴信号");
            if (z_data != null && x_data != null)
            {
                double[] onefea = featurenet.Process(z_data.Item3, x_data.Item3);
                UpdateFeatures(onefea);
                List<double[]> all_fea = GetFeatures();
                double rul = reglstm.Process(all_fea);
                double time = z_data.Item2[z_data.Item2.Length - 1];
                rulform.chart1.Series["剩余寿命"].Points.AddXY(time, rul);
                flag = true;
            }
            return flag;
        }

        private void UpdateFeatures(double[] fea)
        {
            bool flag = true;
            int i = 0;
            while (flag && i < features.Count)
            {
                flag = features[i].Update(fea, out double[] outfea);
                if (flag)
                    fea = outfea;
                i++;
            }
        }

        private List<double[]> GetFeatures()
        {
            int i = 0;
            List<double[]> r_list = new List<double[]>();
            while (features[i].HasData() && i < features.Count)
            {
                r_list.AddRange(features[i].GetData());
                i++;
            }
                
            return r_list;
        }
    }

    class FeaRam
    {
        List<double[]> feature;
        int maxlen;
        bool flag;
        public FeaRam(int maxlen)
        {
            this.maxlen = maxlen;
            flag = true;
            feature = new List<double[]>();
        }

        public FeaRam() : this(16) {}

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="indata"></param>
        /// <param name="outdata"></param>
        /// <returns>若为真则有数据传出</returns>
        public bool Update(double[] indata, out double[] outdata)
        {
            outdata = new double[indata.Length];
            if (feature.Count > maxlen)
            {
                if (flag)
                {
                    outdata = feature[0];
                    flag = !flag;
                }
                feature.RemoveAt(0);
            }
            feature.Add(indata);
            return !flag;
        }
        /// <summary>
        /// 对features进行深拷贝传递出去
        /// </summary>
        /// <returns></returns>
        public List<double[]> GetData()
        {
            var r_list = new List<double[]>();
            feature.ForEach(i => r_list.Add(i));
            r_list.Reverse();
            return r_list;
        }
        /// <summary>
        /// 查询是否有数据
        /// </summary>
        /// <returns></returns>
        public bool HasData()
        {
            if (feature.Count > 0)
                return true;
            else
                return false;
        }
    }
}
