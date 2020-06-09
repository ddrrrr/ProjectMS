using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.SignalProcess.Filter
{
    class 无滤波器 : IFilter
    {
        public void Process(double[] intime, double[] invalue, out double[] xvalue, out double[] yvalue)
        {
            xvalue = intime;
            yvalue = invalue;
        }
    }
}
