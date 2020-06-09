using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.SignalProcess.Filter
{
    class 限幅滤波 : IFilter
    {
        [CategoryAttribute("滤波参数"), DisplayName("信号跳动允许的最大差值")]
        public double MaxBias { get; set; }

        public 限幅滤波()
        {
            MaxBias = 1;
        }
        public 限幅滤波(double maxbias)
        {
            MaxBias = maxbias;
        }
        public void Process(double[] intime, double[] invalue, out double[] xvalue, out double[] yvalue)
        {
            xvalue = intime;
            yvalue = invalue;
            for (int i=1;i<yvalue.Length;i++)
            {
                if (Math.Abs(yvalue[i] - yvalue[i - 1]) > MaxBias)
                    yvalue[i] = yvalue[i - 1];
            }
        }
    }
}
