using ProjectMS.Core.DataSet;
using ProjectMS.Core.ErrorManager;
using ProjectMS.Core.PropertyBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.CardSetting
{
    /// <summary>
    /// 静态类，管理设置的采集卡信息
    /// </summary>
    static class CardSettingManager
    {
        /// <summary>
        /// 采集卡设置警报选项
        /// </summary>
        static ErrorManager.ErrorManager CardSettingError;
        /// <summary>
        /// 信号设置列表
        /// </summary>
        static List<OneListSetting> _Settings;
        /// <summary>
        /// 对外可查询的设置列表，不可写入
        /// </summary>
        public static List<OneListSetting> Settings { get { return _Settings; } }
        /// <summary>
        /// 采集卡设置保存路劲
        /// </summary>
        static string SettingsPath = "CardSetting.dat";
        /// <summary>
        /// 程序中已经支持的采集卡列表且连接正常（即已经编写好ICard接口的程序）
        /// </summary>
        static Dictionary<string, ICard> cards;

        /// <summary>
        /// Settings改变事件
        /// </summary>
        public static event Action<string, OneListSetting> ListChanged;

        static CardSettingManager()
        {
            _Settings = new List<OneListSetting>();
            initCartSettingError();
            cards = new Dictionary<string, ICard>();
            initcard();
            LoadSettings();
            _isRecording = false;
        }

        /// <summary>
        /// 初始化，扫描满足ICard接口的程序
        /// </summary>
        private static void initcard()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICard))))
                        .ToArray();
            foreach (var t in types)
            {
                ICard a = (ICard)Activator.CreateInstance(t, true);
                //a.DataExport += CardSettingManager_DataExport;
                if (a.IsExist())
                    cards.Add(t.Name, a);
                else
                    CardSettingError.RaiseError(201);
            }
        }

        /// <summary>
        /// 设置采集卡设置过程中可能出现的错误，并列出警报列表
        /// </summary>
        private static void initCartSettingError()
        {
            CardSettingError = new ErrorManager.ErrorManager("Crd");
            /// 进行该类各种方法时可能出现的错误
            /// 查询错误
            CardSettingError.AddError(001, "未找到对应的信号名称", "请选择存在的信号名称", ErrorSerious.提示);
            CardSettingError.AddError(002, "未找到对应属性名称", "请选择正确的属性名称", ErrorSerious.提示);
            /// 设置错误
            CardSettingError.AddError(101, "采集卡通道格式错误", "请设置为非负整数", ErrorSerious.提示);
            CardSettingError.AddError(102, "设置采集卡通道数大于采集卡最大通道数", "请设置合理通道数", ErrorSerious.提示);
            CardSettingError.AddError(103, "命名格式错误", "请从新设置格式", ErrorSerious.提示);
            CardSettingError.AddError(104, "命名重复", "请设置不重复重命名", ErrorSerious.提示);
            CardSettingError.AddError(105, "采样频率格式错误", "请设置为正整数", ErrorSerious.提示);
            CardSettingError.AddError(106, "电信号范围格式错误", "请选择正确项", ErrorSerious.提示);
            CardSettingError.AddError(107, "阈值格式错误", "", ErrorSerious.提示);
            CardSettingError.AddError(108, "传感器位置格式错误", "请选择正确位置", ErrorSerious.提示);

            CardSettingError.AddError(201, "未检测到数据采集卡", "请检测数据采集卡是否正确接入并重启软件", ErrorSerious.提示);
        }
        /// <summary>
        /// 添加单行信号设置
        /// </summary>
        /// <param name="onesetting">单个信号设置</param>
        /// <returns>是否添加成功</returns>
        public static bool AddSetting(OneListSetting onesetting)
        {
            //  setting添加
            _Settings.Add(onesetting);

            //  采集卡进行设置采样频率以及电信号范围
            cards[onesetting.OneCardSetting.cardname.Value].SetSamplerate(onesetting.OneCardSetting.channel.channel.Value, onesetting.OneCardSetting.samplerate);
            cards[onesetting.OneCardSetting.cardname.Value].SetValuesRange(onesetting.OneCardSetting.channel.channel.Value, onesetting.OneCardSetting.range.Value);

            //  将该行信号设置添加到数据库母表，并建立对应子表
            SignalInfo info = new SignalInfo();
            info.cardname = onesetting.OneCardSetting.cardname.Value;
            info.name = onesetting.SignalName;
            info.position = onesetting.SensorPosition;
            info.resolution = (ushort)cards[onesetting.OneCardSetting.cardname.Value].GetResolution();
            info.sensortype = onesetting.SensorType;
            info.signalrange = onesetting.OneCardSetting.range.Value;
            info.detectequipment = onesetting.DetectEquipment;
            info.resolutiondouble = cards[onesetting.OneCardSetting.cardname.Value].GetDoubleResolution(onesetting.OneCardSetting.range.Value, out info.basevalue);
            CollectDataSet.AddSignal(info);

            //  事件
            ListChanged?.Invoke("Add", onesetting);
            return true;
        }

        /// <summary>
        /// 删除单个信号设置
        /// </summary>
        /// <param name="name"></param>
        public static bool DelSetting(string name)
        {
            var onesetting = _Settings.Find(x => x.SignalName == name);
            if (onesetting != null)
            {
                //  采集卡删除
                cards[onesetting.OneCardSetting.cardname.Value].DelChannel(onesetting.OneCardSetting.channel.channel.Value);
                //  setting删除
                _Settings.Remove(onesetting);

                //  数据库删除
                CollectDataSet.DelSignal(onesetting.SignalName);
                //  事件
                ListChanged?.Invoke("Del", onesetting);
                return true;
            }
            else
            {
                CardSettingError.RaiseError(001);
                return false;
            }
        }

        /// <summary>
        /// 修改单个信号设置信息
        /// </summary>
        /// <param name="name">原信号名称</param>
        /// <param name="changesetting">修改后的信号设置信息</param>
        /// <returns></returns>
        public static bool ChangeValue(string name, OneListSetting changesetting)
        {
            var onesetting = _Settings.Find((x) => x.SignalName == name);
            if (onesetting == null)
            {
                cards[onesetting.OneCardSetting.cardname.Value].DelChannel(onesetting.OneCardSetting.channel.channel.Value);
                CardSettingError.RaiseError(001);
                return false;
            }
            //  setting修改
            onesetting = changesetting;
            //  采集卡修改
            cards[changesetting.OneCardSetting.cardname.Value].SetSamplerate(changesetting.OneCardSetting.channel.channel.Value, changesetting.OneCardSetting.samplerate);
            cards[changesetting.OneCardSetting.cardname.Value].SetValuesRange(changesetting.OneCardSetting.channel.channel.Value, changesetting.OneCardSetting.range.Value);
            //  数据库修改
            var info = CollectDataSet.GetSignal(name);
            SignalInfo new_info = new SignalInfo();
            new_info.cardname = changesetting.OneCardSetting.cardname.Value;
            new_info.name = changesetting.SignalName;
            new_info.position = changesetting.SensorPosition;
            new_info.resolution = (ushort)cards[changesetting.OneCardSetting.cardname.Value].GetResolution();
            new_info.sensortype = changesetting.SensorType;
            new_info.signalrange = changesetting.OneCardSetting.range.Value;
            new_info.detectequipment = changesetting.DetectEquipment;
            new_info.resolutiondouble = cards[changesetting.OneCardSetting.cardname.Value].GetDoubleResolution(changesetting.OneCardSetting.range.Value, out new_info.basevalue);
            CollectDataSet.ChgSignal(info, new_info);

            ListChanged?.Invoke("Chg", changesetting);
            return true;
        }

        /// <summary>
        /// 根据采集卡接口程序获取采集卡可用通道名称
        /// </summary>
        /// <param name="cardname">采集卡名称</param>
        /// <param name="value">单通道还是差分式</param>
        /// <returns></returns>
        internal static List<string> GetChannelAllowList(string cardname, ChannelType value)
        {
            List<string> channels = new List<string>();
            if (cardname == null)
                return channels;
            if (cards.ContainsKey(cardname))
            {
                if (value == ChannelType.单通道)
                    channels.AddRange(cards[cardname].GetSingleChannels());
                else
                    channels.AddRange(cards[cardname].GetDiffChanels());
            }
            else
                CardSettingError.RaiseError(001);
            return channels;
        }
        /// <summary>
        /// 根据采集卡接口程序获取采集卡可用电信号范围
        /// </summary>
        /// <param name="cardname">采集卡名称</param>
        /// <returns></returns>
        public static string[] GetRangeList(string cardname)
        {
            List<string> rangelist = new List<string>();
            if (cardname == null)
                return rangelist.ToArray();
            if (cards.ContainsKey(cardname))
            {
                rangelist.AddRange(cards[cardname].GetValueRanges());
            }
            else
                CardSettingError.RaiseError(001);
            return rangelist.ToArray();
        }
        /// <summary>
        /// 选择添加采集卡设置后，所需要的空模板
        /// </summary>
        /// <returns></returns>
        public static OneListSetting GetNewCardSetting()
        {
            string name = "信号";
            int count = 0;
            while (_Settings.Exists(x => x.SignalName == name + count.ToString()))
                count++;
            var onesetting = new OneListSetting(name + count.ToString());
            onesetting.OneCardSetting.cardname.AllowList.AddRange(cards.Keys);
            onesetting.OneCardSetting.channel.channeltype = ChannelType.单通道;
            return onesetting;
        }
        /// <summary>
        /// 更新信号设置信息中可选选项
        /// </summary>
        /// <param name="onesetting"></param>
        public static void GetCardAllowSetting(OneCardSetting onesetting)
        {
            string selectcardname = onesetting.cardname.Value;
            onesetting.channel.channel.AllowList.Clear();
            onesetting.channel.channel.AllowList.AddRange(GetChannelAllowList(selectcardname, onesetting.channel.channeltype));
            onesetting.range.AllowList.Clear();
            onesetting.range.AllowList.AddRange(GetRangeList(selectcardname));
            return;
        }
        /// <summary>
        /// 查询单个信号设置
        /// </summary>
        /// <param name="name">信号名称</param>
        /// <returns></returns>
        public static OneListSetting GetOneCardSetting(string name)
        {
            var onesetting = _Settings.Find(x => x.SignalName == name);
            return onesetting;
        }

        /// <summary>
        /// 是否正在进行信号采集
        /// </summary>
        private static bool _isRecording;
        /// <summary>
        /// 是否正在进行信号采集（只读）
        /// </summary>
        public static bool IsRecording { get { return _isRecording; } }
        static double starttime, stoptime;
        public static event Action<bool> RecordStageChanged;
        /// <summary>
        /// 开始采集信号的时间
        /// </summary>
        /// <returns></returns>
        public static double GetStarttime()
        {
            if (IsRecording)
                return starttime;
            else
                return 0;
        }
        /// <summary>
        /// 开始信号采集
        /// </summary>
        public static void StartRecord()
        {
            if (!IsRecording)
            {
                starttime = DateTime.Now.ToOADate();
                foreach (var k in cards.Keys)
                {
                    if (_Settings.Exists(x => x.OneCardSetting.cardname.Value == k))
                    {
                        cards[k].StartRecord();
                    }
                }
                _isRecording = true;
                RecordStageChanged?.Invoke(true);
            }
        }
        /// <summary>
        /// 停止信号采集
        /// </summary>
        public static void StopRecord()
        {
            if (IsRecording)
            {
                stoptime = DateTime.Now.ToOADate();
                foreach (var k in cards.Keys)
                {
                    cards[k].StopRecord();
                }
                foreach (var x in Settings)
                {
                    CollectDataSet.AddTime(x.SignalName, starttime, stoptime);
                }
                _isRecording = false;
                RecordStageChanged?.Invoke(false);
            }
        }

        //private static void CardSettingManager_DataExport(string cardname, Dictionary<string, List<short>> arg1, DateTime arg2, DateTime arg3)
        //{
        //    foreach (var k in arg1.Keys)
        //    {
        //        var onesetting = _Settings.Find(x => x.cardname.Value == cardname && x.channel.channel.Value == k);
        //        if (onesetting != null)
        //        {
        //            List<DateTime> times = new List<DateTime>();
        //            List<double> values = new List<double>();
        //            double basevalue;
        //            double resolution = cards[cardname].GetDoubleResolution(onesetting.range.Value, out basevalue);
        //            var cha = (arg3.ToOADate() - arg2.ToOADate()) / (arg1[k].Count);
        //            var counttime = arg2.ToOADate();
        //            for (int i = 0; i < arg1[k].Count; i++)
        //            {
        //                times.Add(DateTime.FromOADate(counttime));
        //                counttime += cha;
        //                values.Add((ushort)arg1[k][i] * resolution + basevalue);
        //            }
        //            //  添加到数据库
        //            CollectDataSet.AddData(onesetting.name, arg2.ToOADate(), arg3.ToOADate(), cha, arg1[k].ToArray());
        //            //  添加到图表数据中
        //            var onechartdata = chartdata.Find(x => x.Name == onesetting.name);
        //            onechartdata.AddRange(times, values);
        //            onechartdata.Weight = Math.Abs((values.Sum() / values.Count) - (onesetting.threshold.UpThresHold + onesetting.threshold.DownThresHold) / 2)
        //                / ((onesetting.threshold.UpThresHold - onesetting.threshold.DownThresHold) / 2);
        //        }
        //    }
        //}

        #region 设置文件存储与读写
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <returns>是否保存成功</returns>
        public static bool SaveSettings()
        {
            Stream fStream = new FileStream(SettingsPath, FileMode.Create);
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            binFormat.Serialize(fStream, _Settings);
            fStream.Flush();
            fStream.Close();
            return true;
        }
        /// <summary>
        /// 读取设置
        /// </summary>
        /// <returns>是否读取成功</returns>
        public static bool LoadSettings()
        {
            if (File.Exists(SettingsPath))
            {
                Stream fStream = new FileStream(SettingsPath, FileMode.Open);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                var loadsetting = (List<OneListSetting>)binFormat.Deserialize(fStream);
                foreach (var a in loadsetting)
                {
                    LoadOneSetting(a);
                }
                return true;
            }
            return false;
        }

        private static bool LoadOneSetting(OneListSetting onesetting)
        {
            if (cards.ContainsKey(onesetting.OneCardSetting.cardname.Value))
            {
                //  setting添加
                _Settings.Add(onesetting);

                //  采集卡进行设置
                cards[onesetting.OneCardSetting.cardname.Value].SetSamplerate(onesetting.OneCardSetting.channel.channel.Value, onesetting.OneCardSetting.samplerate);
                cards[onesetting.OneCardSetting.cardname.Value].SetValuesRange(onesetting.OneCardSetting.channel.channel.Value, onesetting.OneCardSetting.range.Value);

                //  事件
                ListChanged?.Invoke("Add", onesetting);
                return true;
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// 在属性设置列表上可修改的属性
    /// </summary>
    [Serializable]
    class OneListSetting
    {
        /// <summary>
        /// 信号名称
        /// </summary>
        [CategoryAttribute("信号信息"), DisplayName("信号名称")]
        public string SignalName { set; get; }
        /// <summary>
        /// 传感器位置
        /// </summary>
        [CategoryAttribute("信号信息"), DisplayName("传感器位置")]
        public string SensorPosition { set; get; } = "未设定";
        /// <summary>
        /// 传感器类型
        /// </summary>
        [CategoryAttribute("信号信息"), DisplayName("传感器类型")]
        public string SensorType { set; get; } = "未设定";
        /// <summary>
        /// 传感器检测的设备
        /// </summary>
        [CategoryAttribute("信号信息"), DisplayName("检测的设备")]
        public string DetectEquipment { set; get; } = "未指定";


        /// <summary>
        /// 单张信号采集卡上单个通道信号信息
        /// </summary>
        OneCardSetting _onecardsetting;
        [TypeConverter(typeof(ExpandableObjectConverter)),
            CategoryAttribute("信号信息"), DisplayName("采集卡信息")]
        public OneCardSetting OneCardSetting
        {
            set
            {
                _onecardsetting = value;
            }
            get { return _onecardsetting; }
        }

        public OneListSetting(string name)
        {
            SignalName = name;
            _onecardsetting = new OneCardSetting();
        }
    }
}
