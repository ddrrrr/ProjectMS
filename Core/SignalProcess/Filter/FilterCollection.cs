using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjectMS.Core.SignalProcess.Filter
{
    class FilterCollection : ISignalProcess
    {
        public FilterCollection()
        {
            initfilters();
            initfilterform();
        }

        /// <summary>
        /// 所有的滤波器
        /// </summary>
        Dictionary<string, IFilter> filters;
        FilterForm filterform;
        string selectfiltername;
        /// <summary>
        /// 初始化，扫描满足IFilter接口的程序
        /// </summary>
        private void initfilters()
        {
            filters = new Dictionary<string, IFilter>();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IFilter))))
                        .ToArray();
            foreach (var t in types)
            {
                IFilter a = (IFilter)Activator.CreateInstance(t, true);
                filters.Add(t.Name, a);
            }
            if (filters.ContainsKey("无滤波器"))
                selectfiltername = "无滤波器";
        }
        /// <summary>
        /// 初始化滤波窗口的一系列参数
        /// </summary>
        private void initfilterform()
        {
            filterform = new FilterForm();
            filterform.FilterSelectComboBox.Items.AddRange(filters.Keys.ToArray());
            filterform.FilterSelectComboBox.Text = selectfiltername;
            filterform.FilterSelectComboBox.SelectedIndexChanged += FilterSelectComboBox_SelectedIndexChanged;
            filterform.DisplayChart1.Titles.Add("z轴振动信号");
            filterform.DisplayChart1.Series.Add("原始信号");
            filterform.DisplayChart1.Series["原始信号"].ChartType = SeriesChartType.FastLine;
            filterform.DisplayChart1.Series["原始信号"].Color = Color.LightBlue;
            filterform.DisplayChart1.Series.Add("滤波后");
            filterform.DisplayChart1.Series["滤波后"].ChartType = SeriesChartType.FastLine;
            filterform.DisplayChart1.Series["滤波后"].Color = Color.DarkGreen;
            filterform.DisplayChart1.Series["滤波后"].IsVisibleInLegend = false;
            filterform.DisplayChart1.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";
            filterform.DisplayChart2.Titles.Add("x轴振动信号");
            filterform.DisplayChart2.Series.Add("原始信号");
            filterform.DisplayChart2.Series["原始信号"].ChartType = SeriesChartType.FastLine;
            filterform.DisplayChart2.Series["原始信号"].Color = Color.LightBlue;
            filterform.DisplayChart2.Series.Add("滤波后");
            filterform.DisplayChart2.Series["滤波后"].ChartType = SeriesChartType.FastLine;
            filterform.DisplayChart2.Series["滤波后"].Color = Color.DarkGreen;
            filterform.DisplayChart2.Series["滤波后"].IsVisibleInLegend = false;
            filterform.DisplayChart2.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";
        }

        /// <summary>
        /// 滤波窗口中滤波器选择改变时的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filterselectcombobox = (ComboBox)sender;
            string filtername = filterselectcombobox.Text;
            if (filters.ContainsKey(filtername))
            {
                filterform.FilterPropertyGrid.SelectedObject = filters[filtername];
                selectfiltername = filtername;
                if (filtername != "无滤波器")
                {
                    filterform.DisplayChart1.Series["滤波后"].IsVisibleInLegend = true;
                    filterform.DisplayChart2.Series["滤波后"].IsVisibleInLegend = true;
                }
                else
                {
                    filterform.DisplayChart1.Series["滤波后"].IsVisibleInLegend = false;
                    filterform.DisplayChart2.Series["滤波后"].IsVisibleInLegend = false;
                }
                    
            }
                
        }

        public Form Process(List<Tuple<string, double[], double[]>> indata)
        {
            foreach(var onedata in indata)
            {
                if (onedata.Item2.Length != 0)
                {
                    Chart tmpchart;
                    if (onedata.Item1 == "z轴信号")
                        tmpchart = filterform.DisplayChart1;
                    else if (onedata.Item1 == "x轴信号")
                        tmpchart = filterform.DisplayChart2;
                    else
                        continue;

                    tmpchart.Series["原始信号"].Points.AddXY(onedata.Item2, onedata.Item3);
                    if (selectfiltername != "无滤波器")
                    {
                        double[] xvalue;
                        double[] yvalue;
                        filters[selectfiltername].Process(onedata.Item2, onedata.Item3, out xvalue, out yvalue);
                        tmpchart.Series["滤波后"].Points.AddXY(xvalue, yvalue);
                    }
                }
            }
            
            return filterform;
        }

        public bool Update(List<Tuple<string, double[], double[]>> indata)
        {
            bool flag = false;
            foreach (var onedata in indata)
            {
                if (onedata.Item2.Length != 0)
                {
                    Chart tmpchart;
                    if (onedata.Item1 == "z轴信号")
                        tmpchart = filterform.DisplayChart1;
                    else if (onedata.Item1 == "x轴信号")
                        tmpchart = filterform.DisplayChart2;
                    else
                        continue;

                    tmpchart.Series["原始信号"].Points.AddXY(onedata.Item2, onedata.Item3);
                    if (selectfiltername != "无滤波器")
                    {
                        double[] xvalue;
                        double[] yvalue;
                        filters[selectfiltername].Process(onedata.Item2, onedata.Item3, out xvalue, out yvalue);
                        tmpchart.Series["滤波后"].Points.AddXY(xvalue, yvalue);
                    }
                    flag = true;
                }
            }
            return flag;
        }
    }
}
