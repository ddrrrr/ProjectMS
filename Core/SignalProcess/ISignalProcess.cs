using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.SignalProcess
{
    /// <summary>
    /// 信号处理接口
    /// </summary>
    interface ISignalProcess
    {
        /// <summary>
        /// 得到该信号处理的可视化窗口
        /// </summary>
        /// <param name="intime"></param>
        /// <param name="invalue"></param>
        /// <returns></returns>
        Form Process(List<Tuple<string,double[],double[]>> indata);
        /// <summary>
        /// 更新该信号处理结果
        /// </summary>
        /// <param name="intime"></param>
        /// <param name="invalue"></param>
        /// <returns></returns>
        bool Update(List<Tuple<string, double[], double[]>> indata);
    }
}
