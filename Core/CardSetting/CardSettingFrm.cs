using ProjectMS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.CardSetting
{
    public partial class CardSettingFrm : Form
    {
        public CardSettingFrm()
        {
            InitializeComponent();
            this.BackColor = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlLightLight;
            Toppanel.BackColor = this.BackColor;
            Toppanel.ForeColor = this.ForeColor;
            InitCardSettingGrid();
            var settings = CardSettingManager.Settings;
            foreach (var onesetting in settings)
            {
                int idx = CardSettingDataGridView.Rows.Add();
                _InputOneSignal(idx, onesetting);
            }
        }
        /// <summary>
        /// 表格外贸设定
        /// </summary>
        private void InitCardSettingGrid()
        {
            CardSettingDataGridView.ColumnCount = 8;
            CardSettingDataGridView.Columns[0].Name = "信号名称";
            CardSettingDataGridView.Columns[0].FillWeight = 100;
            CardSettingDataGridView.Columns[0].MinimumWidth = 160;
            CardSettingDataGridView.Columns[1].Name = "使用采集卡";
            CardSettingDataGridView.Columns[1].FillWeight = 100;
            CardSettingDataGridView.Columns[1].MinimumWidth = 160;
            CardSettingDataGridView.Columns[2].Name = "使用通道";
            CardSettingDataGridView.Columns[2].FillWeight = 100;
            CardSettingDataGridView.Columns[2].MinimumWidth = 160;
            CardSettingDataGridView.Columns[3].Name = "采样频率";
            CardSettingDataGridView.Columns[3].FillWeight = 60;
            CardSettingDataGridView.Columns[3].MinimumWidth = 100;
            CardSettingDataGridView.Columns[4].Name = "电信号范围";
            CardSettingDataGridView.Columns[4].FillWeight = 60;
            CardSettingDataGridView.Columns[4].MinimumWidth = 160;
            CardSettingDataGridView.Columns[5].Name = "传感器类型";
            CardSettingDataGridView.Columns[5].FillWeight = 100;
            CardSettingDataGridView.Columns[5].MinimumWidth = 160;
            CardSettingDataGridView.Columns[6].Name = "检测设备";
            CardSettingDataGridView.Columns[6].FillWeight = 100;
            CardSettingDataGridView.Columns[6].MinimumWidth = 250;
            CardSettingDataGridView.Columns[7].Name = "布置位置";
            CardSettingDataGridView.Columns[7].FillWeight = 100;
            CardSettingDataGridView.Columns[7].MinimumWidth = 250;

            CardSettingDataGridView.EnableHeadersVisualStyles = false;
            CardSettingDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            CardSettingDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlLightLight;
            CardSettingDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            CardSettingDataGridView.ReadOnly = true;
            CardSettingDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CardSettingDataGridView.BackgroundColor = this.BackColor;
            CardSettingDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            CardSettingDataGridView.AllowUserToAddRows = false;
            CardSettingDataGridView.AllowUserToDeleteRows = false;
            CardSettingDataGridView.AllowUserToResizeRows = false;
            CardSettingDataGridView.AllowUserToResizeColumns = false;
            CardSettingDataGridView.RowHeadersVisible = false;

            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = SystemColors.ControlDark;
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlLightLight;
            CardSettingDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.BackColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlLightLight;
            CardSettingDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
        }

        private void _InputOneSignal(int idx, OneListSetting onesetting)
        {
            CardSettingDataGridView[0, idx].Value = onesetting.SignalName;
            CardSettingDataGridView[1, idx].Value = onesetting.OneCardSetting.cardname.Value;
            CardSettingDataGridView[2, idx].Value = onesetting.OneCardSetting.channel.channel.Value;
            CardSettingDataGridView[3, idx].Value = onesetting.OneCardSetting.samplerate;
            CardSettingDataGridView[4, idx].Value = onesetting.OneCardSetting.range.Value;
            CardSettingDataGridView[5, idx].Value = onesetting.SensorType;
            CardSettingDataGridView[6, idx].Value = onesetting.DetectEquipment;
            CardSettingDataGridView[7, idx].Value = onesetting.SensorPosition;
        }

        bool _Enable;
        public bool Enable
        {
            get { return _Enable; }
            set
            {
                _Enable = value;
                AddButton.Enabled = value;
                ChangeButton.Enabled = value;
                DeleteButton.Enabled = value;
                SaveButton.Enabled = value;
                CardSettingDataGridView.Enabled = value;
            }
        }

        #region 增删改存4个按钮点击设置
        OneSignalSettingFrm settingfrm;
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            var newsetting = CardSettingManager.GetNewCardSetting();
            settingfrm = new OneSignalSettingFrm(newsetting);
            settingfrm.ConfirmEvent += Settingfrm_AddConfirmEvent;
            settingfrm.ShowDialog();
        }

        private void Settingfrm_AddConfirmEvent(OneListSetting obj)
        {
            OneListSetting a = obj;
            CardSettingManager.AddSetting(a);
            int idx = CardSettingDataGridView.Rows.Add();
            _InputOneSignal(idx, a);
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            int r = CardSettingDataGridView.CurrentRow.Index;
            string Selectmonitorname = (string)CardSettingDataGridView[0, r].Value;
            if (MessageBox.Show(string.Format("确实要删除{0}信号吗?", Selectmonitorname), "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CardSettingDataGridView.Rows.RemoveAt(r);
            }
            CardSettingManager.DelSetting(Selectmonitorname);
        }
        /// <summary>
        /// 更改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string selectname;
        private void ChangeButton_Click(object sender, EventArgs e)
        {
            int r = CardSettingDataGridView.CurrentRow.Index;
            string Selectmonitorname = (string)CardSettingDataGridView[0, r].Value;
            selectname = Selectmonitorname;
            var onesetting = CardSettingManager.GetOneCardSetting(Selectmonitorname);
            settingfrm = new OneSignalSettingFrm(onesetting);
            settingfrm.ConfirmEvent += Settingfrm_ChgConfirmEvent;
            settingfrm.ShowDialog();
        }

        private void Settingfrm_ChgConfirmEvent(OneListSetting obj)
        {
            CardSettingManager.ChangeValue(selectname, obj);
            int idx = CardSettingDataGridView.CurrentRow.Index;
            _InputOneSignal(idx, obj);
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            CardSettingManager.SaveSettings();
        }
        #endregion

        
    }
}
