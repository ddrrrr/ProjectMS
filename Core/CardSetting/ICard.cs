using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.CardSetting
{
    interface ICard
    {
        /// <summary>
        /// 采集完数据后的事件
        /// string:cardname,
        /// dictionary:[channel:data],
        /// datetime:starttime,
        /// datetime:stoptime
        /// </summary>
        event Action<string, Dictionary<string, List<short>>, DateTime, DateTime> DataExport;
        /// <summary>
        /// 板卡是否已经插入并能够被检测出
        /// </summary>
        /// <returns></returns>
        bool IsExist();
        /// <summary>
        /// 采集卡是否是同步采集
        /// </summary>
        /// <returns></returns>
        bool IsSynchro();
        /// <summary>
        /// 采集卡AD转换精度（分辨率）
        /// </summary>
        /// <returns>多少位</returns>
        int GetResolution();
        /// <summary>
        /// 采集卡在某个电信号范围时的最小分辨率
        /// </summary>
        /// <param name="range">电信号范围</param>
        /// <param name="basevalue">基准值（最小值）</param>
        /// <returns>最小分辨率</returns>
        double GetDoubleResolution(string range, out double basevalue);
        /// <summary>
        /// 采集卡可选单通道数值
        /// </summary>
        /// <returns>如：{"0","1"}</returns>
        string[] GetSingleChannels();
        /// <summary>
        /// 采集卡可选的差分式通道
        /// </summary>
        /// <returns>如：{"0-1","2-3"}</returns>
        string[] GetDiffChanels();
        /// <summary>
        /// 采集卡量程可选范围
        /// </summary>
        /// <returns></returns>
        string[] GetValueRanges();
        /// <summary>
        /// 设置采样频率
        /// </summary>
        /// <param name="channel">设置的通道</param>
        /// <param name="rate">采样频率</param>
        /// <returns>是否设置成功</returns>
        bool SetSamplerate(string channel, int rate);
        /// <summary>
        /// 设置量程范围
        /// </summary>
        /// <param name="channel">设置的通道</param>
        /// <param name="range">量程</param>
        /// <returns>是否设置成功</returns>
        bool SetValuesRange(string channel, string range);
        /// <summary>
        /// 开始对单通道进行采集
        /// </summary>
        /// <param name="channel">通道名字</param>
        /// <returns>是否设置成功</returns>
        bool StartRecord();
        /// <summary>
        /// 停止单通道进行采集
        /// </summary>
        /// <param name="channel">通道名字</param>
        /// <returns>是否设置成功</returns>
        bool StopRecord();
        /// <summary>
        /// 删除已设置通道
        /// </summary>
        /// <param name="channel">通道名字</param>
        /// <returns>是否设置成功</returns>
        bool DelChannel(string channel);
    }
}
