using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.SignalProcess.Filter
{
    class 均值滤波 : IFilter
    {
        [CategoryAttribute("滤波参数"), DisplayName("连续采样长度")]
        public int MaxLen { get; set; }
        public 均值滤波()
        {
            MaxLen = 2;
        }
        public 均值滤波(int maxlen)
        {
            if (maxlen > 2)
                MaxLen = maxlen;
            else
                MaxLen = 2;
        }
        public void Process(double[] intime, double[] invalue, out double[] xvalue, out double[] yvalue)
        {
            xvalue = intime;
            yvalue = invalue;
            double sum = 0;
            for (int i = 0; i < MaxLen - 1; i++)
                sum += yvalue[i];
            for (int i = MaxLen - 1; i < yvalue.Length; i++)
            {
                sum += yvalue[i];
                yvalue[i] = sum / MaxLen;
                sum -= yvalue[i + 1 - MaxLen];
            }
        }
    }
}
