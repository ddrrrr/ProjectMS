using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.SignalProcess.RUL
{
    class FeatureNet
    {
        public double[] Process(double[] z_data, double[] x_data)
        {
            var r_fea = new double[16];
            for (int i = 0; i < 16; i++)
                r_fea[i] = 0;
            return r_fea;
        }
    }
}
