using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.CardSetting
{
    class CardDemo:ICard
    {
        class channelInfo
        {
            public bool IsUse;
            public int SampleRate;
            public channelInfo(bool isuse, int samplerate)
            {
                IsUse = isuse;
                SampleRate = samplerate;
            }
        }

        private DateTime startTime;
        private DateTime stopTime;
        Dictionary<string, List<short>> tmpexportdata;
        public event Action<string, Dictionary<string, List<short>>, DateTime, DateTime> DataExport;

        private System.Timers.Timer timer;
        private Dictionary<string, channelInfo> SignalChannels;
        private string[] ValueRanges;
        public CardDemo()
        {
            SignalChannels = new Dictionary<string, channelInfo>();
            foreach (string x in new string[] {"正弦波","方波","三角波",
                    "正弦扫频","方波扫频",
                    "变幅值正弦波","白噪声","温度"})
                SignalChannels.Add(x, new channelInfo(false, 0));
            ValueRanges = new string[] { "+/-625mV", "+/-1.25V", "+/-2.5V",
                "+/-5V", "+/-10V", "0-2.5V", "0-5V", "0-10V" };

            timer = new System.Timers.Timer();
            timer.Enabled = false;
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            tmpexportdata = new Dictionary<string, List<short>>();
        }

        private void getdata()
        {
            tmpexportdata.Clear();
            foreach (string k in SignalChannels.Keys)
            {
                if (SignalChannels[k].IsUse)
                {
                    List<short> tmpdata = getonechanneldata(k, SignalChannels[k]);
                    tmpexportdata.Add(k, tmpdata);
                }
            }
        }
        private uint count;
        private List<short> getonechanneldata(string k, channelInfo channelInfo)
        {
            List<short> r = new List<short>();
            double cha = 1d / channelInfo.SampleRate;
            double tmpcount = 0;
            short[] onechanneldata = new short[channelInfo.SampleRate];
            unsafe
            {
                ushort* p = (ushort*)Marshal.UnsafeAddrOfPinnedArrayElement(onechanneldata, 0);
                switch (k)
                {
                    case "正弦波":
                        for (int i = 0; i < channelInfo.SampleRate; i++)
                        {
                            *(p + i) = Convert.ToUInt16((Math.Sin(count + tmpcount) + 1) * short.MaxValue);
                            tmpcount += cha;
                        }
                        break;
                    case "方波":
                        if (count % 2 == 0)
                            for (int i = 0; i < channelInfo.SampleRate; i++)
                                *(p + i) = ushort.MinValue;
                        else
                            for (int i = 0; i < channelInfo.SampleRate; i++)
                                *(p + i) = ushort.MaxValue;
                        break;
                    case "三角波":
                        for (int i = 0; i < channelInfo.SampleRate; i++)
                        {
                            if (tmpcount < 0.5)
                                *(p + i) = Convert.ToUInt16(tmpcount * 2 * ushort.MaxValue);
                            else
                                *(p + i) = Convert.ToUInt16((1 - tmpcount) * 2 * ushort.MaxValue);
                            tmpcount += cha;
                        }
                        break;
                    case "正弦扫频":
                        for (int i = 0; i < channelInfo.SampleRate; i++)
                        {
                            *(p + i) = Convert.ToUInt16((Math.Sin((Math.Sin((count + tmpcount) * 0.1) + 1.001) * (count + tmpcount)) + 1) * short.MaxValue);
                            tmpcount += cha;
                        }
                        break;
                    case "方波扫频":
                        double f;
                        for (int i = 0; i < channelInfo.SampleRate; i++)
                        {
                            f = Math.Sin((count + tmpcount) * 1e-2);
                            if ((count + tmpcount) % (2 * f) <= f)
                                *(p + i) = ushort.MinValue;
                            else
                                *(p + i) = ushort.MaxValue;
                            tmpcount += cha;
                        }
                        break;
                    case "变幅值正弦波":
                        for (int i = 0; i < channelInfo.SampleRate; i++)
                        {
                            *(p + i) = Convert.ToUInt16(((Math.Sin((count + tmpcount) * 1e-2) + 1.2) * 0.4 * Math.Sin(count + tmpcount) + 1) * short.MaxValue);
                            tmpcount += cha;
                        }
                        break;
                    case "白噪声":
                        Random ra = new Random();
                        for (int i = 0; i < channelInfo.SampleRate; i++)
                        {
                            *(p + i) = Convert.ToUInt16(ra.NextDouble() * ushort.MaxValue);
                            tmpcount += cha;
                        }
                        break;
                    case "温度":
                        for (int i = 0; i < channelInfo.SampleRate; i++)
                        {
                            *(p + i) = Convert.ToUInt16((Math.Sin((count + tmpcount) * 1e-7) * 5 + 25) * 1e-2 * ushort.MaxValue);
                            tmpcount += cha;
                        }
                        break;
                }
            }
            r.AddRange(onechanneldata);
            return r;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            stopTime = DateTime.Now;
            DataExport.Invoke("CardDemo", tmpexportdata, startTime, stopTime);
            count++;
            startTime = stopTime;
            getdata();
        }

        public bool DelChannel(string channel)
        {
            if (SignalChannels.Keys.Contains(channel))
            {
                SignalChannels[channel].IsUse = false;
                return true;
            }
            return false;
        }

        public string[] GetDiffChanels()
        {
            return new string[0];
        }

        public double GetDoubleResolution(string range, out double basevalue)
        {
            double r = 0;
            basevalue = 0;
            switch (range)
            {
                case "+/-625mV":
                    r = 0.625 * 2 / Math.Pow(2, 16);
                    basevalue = -0.625;
                    break;
                case "+/-1.25V":
                    r = 1.25 * 2 / Math.Pow(2, 16);
                    basevalue = -1.25;
                    break;
                case "+/-2.5V":
                    r = 2.5 * 2 / Math.Pow(2, 16);
                    basevalue = -2.5;
                    break;
                case "+/-5V":
                    r = 5 * 2 / Math.Pow(2, 16);
                    basevalue = -5;
                    break;
                case "+/-10V":
                    r = 10 * 2 / Math.Pow(2, 16);
                    basevalue = -10;
                    break;
                case "0-2.5V":
                    r = 2.5 / Math.Pow(2, 16);
                    basevalue = 0;
                    break;
                case "0-5V":
                    r = 5 / Math.Pow(2, 16);
                    basevalue = 0;
                    break;
                case "0-10V":
                    r = 10 / Math.Pow(2, 16);
                    basevalue = 0;
                    break;
            }

            return r;
        }

        public int GetResolution()
        {
            return 16;
        }

        public string[] GetSingleChannels()
        {
            List<string> r = new List<string>();
            foreach (var k in SignalChannels.Keys)
            {
                if (!SignalChannels[k].IsUse)
                    r.Add(k);
            }
            return r.ToArray();
        }

        public string[] GetValueRanges()
        {
            return ValueRanges;
        }

        public bool IsExist()
        {
            return true;
        }

        public bool IsSynchro()
        {
            return false;
        }

        public bool SetSamplerate(string channel, int rate)
        {
            if (SignalChannels.Keys.Contains(channel))
            {
                SignalChannels[channel].SampleRate = rate;
                SignalChannels[channel].IsUse = true;
                return true;
            }
            return false;
        }

        public bool SetValuesRange(string channel, string range)
        {
            return true;
        }

        public bool StartRecord()
        {
            count = 0;
            startTime = DateTime.Now;
            timer.Start();
            getdata();
            return true;
        }

        public bool StopRecord()
        {
            timer.Stop();
            return true;
        }
    }
}
