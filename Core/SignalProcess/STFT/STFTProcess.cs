using ProjectMS.Core.SignalProcess.FFT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.SignalProcess.STFT
{
    class STFTProcess : ISignalProcess
    {
        STFTForm stftform;
        [CategoryAttribute("STFT参数"), DisplayName("连续采样长度")]
        public int datalen { set; get; }
        [CategoryAttribute("STFT参数"), DisplayName("采样间隔")]
        public int shiftlen { set; get; }
        private double samplerate;
        public void SetSamplerate(double rate)
        {
            samplerate = rate;
        }
        public STFTProcess()
        {
            stftform = new STFTForm();
            stftform.ParaPropertyGrid.SelectedObject = this;
            stftform.BackColor = SystemColors.ControlDarkDark;
            stftform.ForeColor = SystemColors.ControlLightLight;
            datalen = 128;
            shiftlen = 50;
        }

        public Form Process(List<Tuple<string, double[], double[]>> indata)
        {
            foreach (var onedata in indata)
            {
                if (onedata.Item2.Length != 0)
                {
                    if (onedata.Item1 != "z轴信号" && onedata.Item1 != "x轴信号")
                        continue;

                    int all_shift = (onedata.Item2.Length - datalen) / shiftlen + 1;
                    double[,] result_weight = new double[datalen/2, all_shift];
                    double[] one_indata = new double[datalen];
                    double[] one_result;
                    for (int i=0;i<all_shift;i++)
                    {
                        for (int j = 0; j < datalen; j++)
                            one_indata[j] = onedata.Item3[i * shiftlen + j];
                        fftbase.fftProcess(one_indata, out one_result);
                        
                        //double[] f = new double[datalen / 2];
                        //double fenduzhi = samplerate / datalen;
                        //double tmpf = 0;
                        for (int j = 0; j < datalen / 2; j++/*, tmpf += fenduzhi*/)
                        {
                            //f[j] = tmpf;
                            result_weight[datalen/2-j-1, i] = one_result[j];
                        }
                    }
                    stftform.SetWeight(onedata.Item1, result_weight);
                }
            }

            return stftform;
        }

        public bool Update(List<Tuple<string, double[], double[]>> indata)
        {
            bool flag = false;
            foreach (var onedata in indata)
            {
                if (onedata.Item2.Length != 0)
                {
                    if (onedata.Item1 != "z轴信号" && onedata.Item1 != "x轴信号")
                        continue;

                    int all_shift = (onedata.Item2.Length - datalen) / shiftlen + 1;
                    double[,] result_weight = new double[datalen / 2, all_shift];
                    double[] one_indata = new double[datalen];
                    double[] one_result;
                    for (int i = 0; i < all_shift; i++)
                    {
                        for (int j = 0; j < datalen; j++)
                            one_indata[j] = onedata.Item3[i * shiftlen + j];
                        fftbase.fftProcess(one_indata, out one_result);

                        //double[] f = new double[datalen / 2];
                        //double fenduzhi = samplerate / datalen;
                        //double tmpf = 0;
                        for (int j = 0; j < datalen / 2; j++/*, tmpf += fenduzhi*/)
                        {
                            //f[j] = tmpf;
                            result_weight[datalen / 2 - j - 1, i] = one_result[j];
                        }
                    }
                    stftform.SetWeight(onedata.Item1, result_weight);
                    flag = true;
                }
            }
            return flag;
        }
    }
}
