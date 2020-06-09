using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.SignalProcess.Filter
{
    /// <summary>
    /// 滤波器接口
    /// </summary>
    interface IFilter
    {
        void Process(double[] intime, double[] invalue, out double[] xvalue, out double[] yvalue);
    }
}
