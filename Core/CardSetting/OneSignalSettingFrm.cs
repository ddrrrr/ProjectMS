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
    partial class OneSignalSettingFrm : Form
    {
        public event Action<OneListSetting> ConfirmEvent;
        private OneListSetting setting;
        public OneSignalSettingFrm(OneListSetting onesetting)
        {
            InitializeComponent();
            this.setting = onesetting;
            OneSignalSettingPropertyGrid.SelectedObject = this.setting;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            ConfirmEvent?.Invoke(setting);
            this.Close();
        }
    }
}
