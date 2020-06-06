using ProjectMS.Core.PropertyBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.CardSetting
{
    /// <summary>
    /// 单个数据采集卡上单个信号信息，包括
    /// 采集卡名称
    /// 通道
    /// 电信号范围
    /// 采集频率
    /// </summary>
    [Serializable]
    class OneCardSetting
    {
        CustomString _cardname;
        Channel _channel;
        CustomString _range;

        /// <summary>
        /// 采集卡型号
        /// </summary>
        [TypeConverter(typeof(CustomStringConverter)), DisplayName("采集卡型号")]
        public CustomString cardname
        {
            set
            {
                _cardname = value;
            }
            get { return _cardname; }
        }
        /// <summary>
        /// 信号通道
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter)), DisplayName("通道")]
        public Channel channel { set { _channel = value; } get { return _channel; } }

        /// <summary>
        /// 信号名称
        /// </summary>
        //[DisplayName("信号名称")]
        //public string name { set; get; }

        /// <summary>
        /// 采样频率
        /// </summary>
        [DisplayName("采样频率")]
        public int samplerate { set; get; }

        /// <summary>
        /// 电信号范围
        /// </summary>
        [TypeConverter(typeof(CustomStringConverter)), DisplayName("电信号范围")]
        public CustomString range { set { _range = value; } get { return _range; } }

        public OneCardSetting()
        {
            _cardname = new CustomString();
            _cardname.ValueChange += _cardname_ValueChange;
            _range = new CustomString();
            _channel = new Channel();
            _channel.channeltypechane += _channel_channeltypechane;
        }

        private void _cardname_ValueChange(string obj)
        {
            CardSettingManager.GetCardAllowSetting(this);
        }

        private void _channel_channeltypechane(ChannelType obj)
        {
            _channel.channel.AllowList.Clear();
            _channel.channel.AllowList.AddRange(CardSettingManager.GetChannelAllowList(cardname.Value, obj));
        }
    }

    //[Serializable]
    //class ThresHold
    //{
    //    [DisplayName("上限")]
    //    public double UpThresHold { set; get; }
    //    [DisplayName("下限")]
    //    public double DownThresHold { set; get; }
    //    public override string ToString()
    //    {
    //        return string.Format("({0},{1})", DownThresHold, UpThresHold);
    //    }
    //}

    [Serializable]
    class Channel
    {
        public event Action<ChannelType> channeltypechane;
        ChannelType _channeltype;
        [DisplayName("模式")]
        public ChannelType channeltype
        {
            get { return _channeltype; }
            set { _channeltype = value; channeltypechane?.Invoke(value); }
        }

        CustomString _channel;
        [TypeConverter(typeof(CustomStringConverter)), DisplayName("通道选择")]
        public CustomString channel { get { return _channel; } set { _channel = value; } }

        public Channel()
        {
            _channeltype = ChannelType.单通道;
            _channel = new CustomString();
        }
        public override string ToString()
        {
            return _channel.ToString();
        }
    }

    public enum ChannelType { 单通道, 差分式 };
}
