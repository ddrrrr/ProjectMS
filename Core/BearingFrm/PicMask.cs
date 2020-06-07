using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using ProjectMS.Core.CardSetting;
using System.Security.Permissions;

namespace ProjectMS.Core.BearingFrm
{
    public partial class PicMask : UserControl
    {
        private List<DectectPointInfo> _sensorlist;
        /// <summary>
        /// 标记点的list
        /// </summary>
        public List<DectectPointInfo> SensorList { get { return _sensorlist; } set { _sensorlist = value; } }
        public PicMask()
        {
            InitializeComponent();
            this.ForeColor = SystemColors.ControlLightLight;
            _sensorlist = new List<DectectPointInfo>();
            InitColorBar("jet");
            Bitmap bp = new Bitmap(Properties.Resources.icons8_add_32);
            editcursor = new Cursor(bp.GetHicon());
            _isEditing = false;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        private bool _isEditing;

        /// <summary>
        /// 编辑状态下的鼠标样式
        /// </summary>
        private Cursor editcursor;

        /// <summary>
        /// 是否在编辑状态中
        /// </summary>
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                if (value)
                {
                    this.Cursor = editcursor;
                    ReSet();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    ReSet();
                }
            }
        }

        /// <summary>
        /// 绘制云图或者传感器标记图
        /// </summary>
        public void ReSet()
        {
            Bitmap bp = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            if (IsEditing)
            {
                Graphics g = Graphics.FromImage(bp);
                Pen p = new Pen(Color.Red, 2);
                foreach (var x in SensorList)
                {
                    g.DrawEllipse(p, x.Location.Item1 - 6, x.Location.Item2 - 6, 12, 12);
                    g.DrawLine(p, x.Location.Item1 - 5, x.Location.Item2, x.Location.Item1 + 5, x.Location.Item2);
                    g.DrawLine(p, x.Location.Item1, x.Location.Item2 - 5, x.Location.Item1, x.Location.Item2 + 5);
                }
                this.BackgroundImage = bp;
            }
            else
            {
                BitmapData bData = bp.LockBits(new Rectangle(0, 0, bp.Width, bp.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format32bppArgb);

                short maxweight = 255;
                Color[] colors = ColorBar.GetRGB("jet", maxweight);
                byte firstdivice = 48;
                short[,,] weightwindow = new short[2,firstdivice, firstdivice];
                double weightfenduzhi = 1d / (firstdivice - 1);
                for (int i = 0; i < firstdivice; i++)
                {
                    for (int j = 0; j < firstdivice; j++)
                    {
                        double max_weight_dis = 0d;
                        double max_dis = 0;
                        foreach (var x in SensorList)
                        {
                            double dis = (Math.Pow(x.Position.Item1 - i * weightfenduzhi, 2) + Math.Pow(x.Position.Item2 - j * weightfenduzhi, 2));
                            max_dis = Math.Max(max_dis, 1d - dis * 200);
                            max_weight_dis = Math.Max(max_weight_dis, x.Weight - dis * 80);
                        }
                        weightwindow[0, i, j] = (short)Math.Round(max_weight_dis * maxweight);
                        weightwindow[1, i, j] = (short)Math.Round(max_dis * maxweight);
                    }
                }

                unsafe
                {
                    double scalewidth = (double)(firstdivice - 1) / (bp.Width - 1);
                    double tmpwidth = 0;
                    double scaleheight = (double)(firstdivice - 1) / (bp.Height - 1);
                    double tmpheight = 0;

                    short weight16x = -1, weight16y = -1;
                    double u, v;
                    byte* p = (byte*)bData.Scan0;
                    short tmpweight;
                    short tmpAlphaweight;
                    for (int i = 0; i < bp.Width; i++)
                    {
                        tmpheight = 0;
                        weight16x = (short)Math.Floor(tmpwidth);
                        u = tmpwidth - weight16x;
                        for (int j = 0; j < bp.Height; j++)
                        {
                            weight16y = (short)Math.Floor(tmpheight);
                            v = tmpheight - weight16y;
                            if ((weightwindow[1, weight16x, weight16y] +
                                weightwindow[1, Math.Min(firstdivice - 1, weight16x + 1), weight16y] +
                                weightwindow[1, weight16x, Math.Min(firstdivice - 1, weight16y + 1)] +
                                weightwindow[1, Math.Min(firstdivice - 1, weight16x + 1), Math.Min(firstdivice - 1, weight16y + 1)]) < 1e-4)
                            {
                                tmpweight = 0;
                                tmpAlphaweight = 0;
                            }
                            else
                            {
                                tmpweight = (short)Math.Round(weightwindow[0, weight16x, weight16y] * (1 - u) * (1 - v)
                                + weightwindow[0, Math.Min(firstdivice - 1, weight16x + 1), weight16y] * u * (1 - v)
                                + weightwindow[0, weight16x, Math.Min(firstdivice - 1, weight16y + 1)] * (1 - u) * v
                                + weightwindow[0, Math.Min(firstdivice - 1, weight16x + 1), Math.Min(firstdivice - 1, weight16y + 1)] * u * v);
                                tmpAlphaweight = (short)Math.Round(weightwindow[1, weight16x, weight16y] * (1 - u) * (1 - v)
                                    + weightwindow[1, Math.Min(firstdivice - 1, weight16x + 1), weight16y] * u * (1 - v)
                                    + weightwindow[1, weight16x, Math.Min(firstdivice - 1, weight16y + 1)] * (1 - u) * v
                                    + weightwindow[1, Math.Min(firstdivice - 1, weight16x + 1), Math.Min(firstdivice - 1, weight16y + 1)] * u * v);
                            }
                            Color c = colors[tmpweight];
                            int offset = i * 4 + j * bData.Stride;
                            p[offset] = c.B;
                            p[offset + 1] = c.G;
                            p[offset + 2] = c.R;
                            p[offset + 3] = (byte)tmpAlphaweight;

                            tmpheight += scaleheight;
                        }
                        tmpwidth += scalewidth;
                    }
                }
                bp.UnlockBits(bData);

                Graphics g = Graphics.FromImage(bp);
                Pen pen = new Pen(Color.Red, 2);
                foreach (var x in SensorList)
                {
                    g.DrawEllipse(pen, x.Location.Item1 - 6, x.Location.Item2 - 6, 12, 12);
                    g.DrawLine(pen, x.Location.Item1 - 5, x.Location.Item2, x.Location.Item1 + 5, x.Location.Item2);
                    g.DrawLine(pen, x.Location.Item1, x.Location.Item2 - 5, x.Location.Item1, x.Location.Item2 + 5);
                }
                this.BackgroundImage = bp;
            }

        }

        /// <summary>
        /// 画出色柱
        /// </summary>
        /// <param name="select">渐变颜色选择，具体看ColorBar</param>
        private void InitColorBar(string select)
        {
            Bitmap bp = new Bitmap(ColorBarpictureBox.Width, ColorBarpictureBox.Height, PixelFormat.Format32bppArgb);
            BitmapData bData = bp.LockBits(new Rectangle(0, 0, bp.Width, bp.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format32bppArgb);
            Color[] colors = ColorBar.GetRGB(select, bp.Height);
            unsafe
            {
                Color c;
                byte* p = (byte*)bData.Scan0;
                for (int i=0;i<bp.Height;i++)
                {
                    c = colors[bp.Height-i-1];
                    for (int j=0;j<bp.Width;j++)
                    {
                        int offset = j * 4 + i * bData.Stride;
                        p[offset] = c.B;
                        p[offset + 1] = c.G;
                        p[offset + 2] = c.R;
                        p[offset + 3] = (byte)255;
                    }
                }
            }
            bp.UnlockBits(bData);
            ColorBarpictureBox.BackgroundImage = bp;
        }

        /// <summary>
        /// 设置色柱上下限数值
        /// </summary>
        /// <param name="HighLowValue"></param>
        public void SetColorBarHighLow(Tuple<string,string> HighLowValue)
        {
            ColorBarHighLabel.Text = HighLowValue.Item1;
            ColorBarLowLabel.Text = HighLowValue.Item2;
        }

        /// <summary>
        /// 求曼哈顿距离
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        private int getdistance(Tuple<int, int> x1, Tuple<int, int> x2)
        {
            return Math.Abs(x1.Item1 - x2.Item1) + Math.Abs(x1.Item2 - x2.Item2);
        }

        /// <summary>
        /// 蒙版缩放事件，调整各标记的位置，并重绘云图
        /// </summary>
        private void PicMask_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < _sensorlist.Count; i++)
            {
                _sensorlist[i].Location = new Tuple<int, int>(
                    Convert.ToInt32(_sensorlist[i].Position.Item1 * this.Width),
                    Convert.ToInt32(_sensorlist[i].Position.Item2 * this.Height));
            }
            this.ReSet();
        }

        /// <summary>
        /// 鼠标单击事件，3种情况
        /// 1：右键菜单取消
        /// 2：添加新传感器
        /// 3：打开右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsEditing)
            {
                if (SelectSensor != null)
                {
                    SelectSensor = null;
                }
                else if (e.Button == MouseButtons.Left && MostNearSensor == null)
                {
                    DectectPointInfo new_sensor = new DectectPointInfo();
                    new_sensor.Location = new Tuple<int, int>(e.X, e.Y);
                    new_sensor.Position = new Tuple<double, double>((double)e.X / this.Width, (double)e.Y / this.Height);
                    new_sensor.Weight = 0;
                    SensorList.Add(new_sensor);
                    ReSet();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (MostNearSensor != null)
                        RightClickcontextMenuStrip.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        /// <summary>
        /// 蒙版上进行鼠标事件时最靠近鼠标的传感器标记
        /// </summary>
        private DectectPointInfo MostNearSensor = null;

        /// <summary>
        /// 右键菜单时的传感器标记
        /// </summary>
        private DectectPointInfo SelectSensor = null;

        /// <summary>
        /// 是否在拖拽过程中
        /// </summary>
        private bool isDragging = false;

        /// <summary>
        /// 鼠标移动事件
        /// 鼠标是否移动到传感器标记附近
        /// 若在编辑状态下则触发拖动
        /// 否则显示传感器信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicMask_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                int dis = Math.Max(this.Width, this.Height);
                DectectPointInfo selectsensor = null;
                foreach (var x in SensorList)
                {
                    int tmpdis = getdistance(new Tuple<int, int>(e.X, e.Y), x.Location);
                    if (tmpdis < dis)
                    {
                        dis = tmpdis;
                        selectsensor = x;
                    }
                }
                if (dis < 8)
                {
                    MostNearSensor = selectsensor;
                }
                else
                {
                    MostNearSensor = null;
                }
            }
            

            if (IsEditing)
            {
                if (isDragging)
                {
                    MostNearSensor.Location = new Tuple<int, int>(e.X, e.Y);
                    ReSet();
                }
                else
                {
                    if (MostNearSensor != null)
                        this.Cursor = Cursors.SizeAll;
                    else
                        this.Cursor = editcursor;
                }
            }
            else
            {
                this.Cursor = this.Parent.Cursor;
                if (MostNearSensor != null)
                {
                    if (MostNearSensor.SignalName == null)
                        Tiplabel.Text = string.Format("{0}\n权重{1:0.0000}", "未选择传感器", MostNearSensor.Weight);
                    else
                        Tiplabel.Text = string.Format("{0}\n权重{1:0.0000}", MostNearSensor.SignalName, MostNearSensor.Weight);
                    Tiplabel.Location = e.Location;
                    Tiplabel.Visible = true;
                }
                else
                {
                    Tiplabel.Visible = false;
                }
            }
        }

        /// <summary>
        /// 删除传感器标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SensorList.Remove(SelectSensor);
            SelectSensor = null;
            ReSet();
        }

        /// <summary>
        /// 鼠标按下事件
        /// 只有当处于编辑状态下，且鼠标附近有传感器标记，且按下的是左键时才会进入拖动状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicMask_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsEditing && MostNearSensor != null && e.Button == MouseButtons.Left)
                isDragging = true;
        }

        /// <summary>
        /// 离开拖动状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicMask_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                MostNearSensor.Position = new Tuple<double, double>(
                    (double)MostNearSensor.Location.Item1 / this.Width,
                    (double)MostNearSensor.Location.Item2 / this.Height
                    );
            }
        }

        /// <summary>
        /// 新增可选传感器选项
        /// </summary>
        /// <param name="name"></param>
        public void AddSignalSelection(string name)
        {
            ToolStripComboBox comboBoxSelectSignals = (ToolStripComboBox)RightClickcontextMenuStrip.Items[2];
            comboBoxSelectSignals.Items.Add(name);
        }

        /// <summary>
        /// 删除可选传感器选项
        /// </summary>
        /// <param name="name"></param>
        public void DelSignalSelection(string name)
        {
            ToolStripComboBox comboBoxSelectSignals = (ToolStripComboBox)RightClickcontextMenuStrip.Items[2];
            comboBoxSelectSignals.Items.Remove(name);
        }

        /// <summary>
        /// 修改可选传感器选项
        /// </summary>
        /// <param name="oriname"></param>
        /// <param name="name"></param>
        public void ChgSignalSelection(string oriname, string name)
        {
            ToolStripComboBox comboBoxSelectSignals = (ToolStripComboBox)RightClickcontextMenuStrip.Items[2];
            int idx = comboBoxSelectSignals.Items.IndexOf(oriname);
            comboBoxSelectSignals.Items[idx] = name;
        }

        /// <summary>
        /// 右键菜单打开时，
        /// 1：选择传感器标记
        /// 2：显示该标记所绑定的传感器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightClickcontextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SelectSensor = MostNearSensor;
            MostNearSensor = null;
            if (SelectSensor.SignalName == null)
            {
                ToolStripComboBox comboBoxSelectSignals = (ToolStripComboBox)RightClickcontextMenuStrip.Items[2];
                comboBoxSelectSignals.Text = "选择传感信号";
            }
            else
            {
                ToolStripComboBox comboBoxSelectSignals = (ToolStripComboBox)RightClickcontextMenuStrip.Items[2];
                comboBoxSelectSignals.Text = SelectSensor.SignalName;
            }
        }

        /// <summary>
        /// 修改标记所绑定的传感器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectSensortoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBoxSelectSignals = (ToolStripComboBox)sender;
            SelectSensor.SignalName = comboBoxSelectSignals.Text;
        }

        /// <summary>
        /// 更新显示传感器的数值
        /// </summary>
        /// <param name="DetectPointWeights">字典：信号名字，weight</param>
        public void UpdateSensorWeight(Dictionary<string, double> DetectPointWeights)
        {
            for (int i=0;i<SensorList.Count;i++)
            {
                if (DetectPointWeights.ContainsKey(SensorList[i].SignalName))
                {
                    SensorList[i].Weight = DetectPointWeights[SensorList[i].SignalName];
                }
            }
            this.ReSet();
        }

        #region 信息保存及读取
        public static string LogPath = "PicPosiLog";
        private string logfilename = "";

        public bool LoadSensorInfo(string logname)
        {
            logfilename = logname;
            SensorList.Clear();
            if (File.Exists(string.Format("{0}\\{1}.log", LogPath, logfilename)))
            {
                using (StreamReader sr = new StreamReader(string.Format("{0}\\{1}.log", LogPath, logfilename), Encoding.UTF8))
                {
                    String line;
                    double x;
                    double y;
                    Random ra = new Random();
                    while ((line = sr.ReadLine()) != null)
                    {
                        var onesensor = new DectectPointInfo();
                        x = double.Parse(line.Substring(1, 6));
                        y = double.Parse(line.Substring(8, 6));
                        onesensor.Position = new Tuple<double, double>(x, y);
                        onesensor.Location = new Tuple<int, int>(Convert.ToInt32(x * this.Width), Convert.ToInt32(y * this.Height));
                        //onesensor.Weight = ra.NextDouble();
                        onesensor.Weight = 0d;
                        //  查询该点是否绑定检测信号
                        if (line.Length > 16)
                        {
                            onesensor.SignalName = line.Substring(15, line.Length - 15);    //  获取之前绑定的信号名称
                            //  检测该信号名称是否已经加载，若无则清空该监测点绑定的信号名称
                            var onelistsignal = CardSettingManager.Settings.Find(onesetting => onesetting.SignalName == onesensor.SignalName);
                            if (onelistsignal == null)
                                onesensor.SignalName = null;
                        }
                        else
                            onesensor.SignalName = null;
                        SensorList.Add(onesensor);
                    }
                }
                ReSet();
                return true;
            }
            ReSet();
            return false;
        }

        public bool SaveSensorInfo()
        {
            if (logfilename != "" && SensorList.Count > 0)
            {
                if (!Directory.Exists("PicPosiLog"))//如果不存在就创建 dir 文件夹  
                    Directory.CreateDirectory("PicPosiLog");

                FileStream fs = new FileStream(string.Format("PicPosiLog\\{0}.log", logfilename), FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                foreach (var x in SensorList)
                {
                    if (x.SignalName == null)
                        sw.WriteLine(string.Format("({0:0.0000},{1:0.0000})", x.Position.Item1, x.Position.Item2));
                    else
                        sw.WriteLine(string.Format("({0:0.0000},{1:0.0000}){2}", x.Position.Item1, x.Position.Item2, x.SignalName));
                }
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                return true;
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// PicMask中显示的传感器数据
    /// </summary>
    [Serializable]
    public partial class DectectPointInfo
    {
        /// <summary>
        /// 传感器在图中的像素坐标
        /// </summary>
        private Tuple<int, int> _location = new Tuple<int, int>(0, 0);
        /// <summary>
        /// 传感器在图中的像素坐标
        /// </summary>
        public Tuple<int, int> Location { get { return _location; } set { _location = value; } }
        /// <summary>
        /// 传感器在图中的相对坐标，取值范围为[0,1]
        /// </summary>
        private Tuple<double, double> _position = new Tuple<double, double>(0, 0);
        /// <summary>
        /// 传感器在图中的相对坐标，取值范围为[0,1]
        /// </summary>
        public Tuple<double, double> Position { get { return _position; } set { _position = value; } }
        /// <summary>
        /// 传感器数值，取值范围为[0,1]
        /// </summary>
        private double _weight;
        /// <summary>
        /// 传感器数值，取值范围为[0,1]
        /// </summary>
        public double Weight { get { return _weight; } set { _weight = value; } }
        /// <summary>
        /// 对应的信号名称
        /// </summary>
        private string _SignalName;
        /// <summary>
        /// 对应的信号名称
        /// </summary>
        public string SignalName { get { return _SignalName; } set { _SignalName = value; } }
    }
}
