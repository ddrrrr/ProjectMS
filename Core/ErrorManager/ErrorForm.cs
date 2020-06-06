using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.ErrorManager
{
    public partial class ErrorForm : Form
    {
        Dictionary<string, bool> StateSelect;
        public ErrorForm()
        {
            InitializeComponent();
            this.BackColor = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlLightLight;
            StateSelect = new Dictionary<string, bool>();
            InitSelectWindow();
            InitErrorGrid();
            ErrorLogManager.ErrorRasied += ErrorLogManager_ErrorRasied;
            var historyerror = ErrorLogManager.GetHistoryError();
            foreach (var x in historyerror)
                AddError(x.Item2, x.Item1);
        }
        private void InitSelectWindow()
        {
            SelectgroupBox.ForeColor = this.ForeColor;
            SelectgroupBox.BackColor = this.BackColor;
            SelectcheckedListBox.BackColor = this.BackColor;
            SelectcheckedListBox.ForeColor = this.ForeColor;
            SelectcheckedListBox.BorderStyle = BorderStyle.None;
            foreach (string s in Enum.GetNames(typeof(ErrorSerious)))
                SelectcheckedListBox.Items.Add(s,true);
        }

        private void ErrorLogManager_ErrorRasied(ErrorBase obj)
        {
            AddError(DateTime.Now, obj);
        }

        private void AddError(DateTime time, ErrorBase err)
        {
            ErrorGridview.Rows.Add();
            int idx = ErrorGridview.Rows.Count - 1;
            ErrorGridview[0, idx].Value = time.ToString("yyyy年MM月dd日 HH:mm:ss");
            
            ErrorGridview[1, idx].Value = err.Serious;
            switch (err.Serious)
            {
                case ErrorSerious.一般:
                    ErrorGridview[1, idx].Style.ForeColor = Color.Yellow;
                    break;
                case ErrorSerious.严重:
                    ErrorGridview[1, idx].Style.ForeColor = Color.OrangeRed;
                    break;
                case ErrorSerious.致命:
                    ErrorGridview[1, idx].Style.ForeColor = Color.Red;
                    break;
            }
            ErrorGridview[2, idx].Value = err.ErrorReason;
            ErrorGridview[3, idx].Value = err.Resolution;
            if (!StateSelect[err.Serious.ToString()])
                ErrorGridview.Rows[idx].Visible = false;
        }

        private void InitErrorGrid()
        {
            ErrorGridview.ColumnCount = 4;
            ErrorGridview.Columns[0].Name = "时间";
            ErrorGridview.Columns[0].FillWeight = 100;
            ErrorGridview.Columns[0].MinimumWidth = 160;
            ErrorGridview.Columns[1].Name = "严重程度";
            ErrorGridview.Columns[1].FillWeight = 40;
            ErrorGridview.Columns[1].MinimumWidth = 80;
            ErrorGridview.Columns[2].Name = "可能原因";
            ErrorGridview.Columns[2].FillWeight = 200;
            ErrorGridview.Columns[3].Name = "可能解决措施";
            ErrorGridview.Columns[3].FillWeight = 200;

            ErrorGridview.EnableHeadersVisualStyles = false;
            ErrorGridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80,80,80);
            ErrorGridview.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlLightLight;
            ErrorGridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            ErrorGridview.ReadOnly = true;
            ErrorGridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ErrorGridview.BackgroundColor = this.BackColor;
            ErrorGridview.CellBorderStyle = DataGridViewCellBorderStyle.None;
            ErrorGridview.AllowUserToAddRows = false;
            ErrorGridview.AllowUserToDeleteRows = false;
            ErrorGridview.AllowUserToResizeRows = false;
            ErrorGridview.AllowUserToResizeColumns = false;
            ErrorGridview.RowHeadersVisible = false;

            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = SystemColors.ControlDark;
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlLightLight;
            ErrorGridview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.BackColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlLightLight;
            ErrorGridview.RowsDefaultCellStyle = dataGridViewCellStyle2;
        }

        private void Visiblebutton_Click(object sender, EventArgs e)
        {
            Settingpanel.Visible = !Settingpanel.Visible;
            if (Settingpanel.Visible)
                Visiblebutton.Text = "▶";
            else
                Visiblebutton.Text = "◀";
        }

        private void SelectcheckedListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var items = SelectcheckedListBox.CheckedItems;
        }

        private void SelectcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var statechange = e.NewValue;
            var value = SelectcheckedListBox.Items[e.Index];
            if (StateSelect.ContainsKey(value.ToString()))
                StateSelect[value.ToString()] = !StateSelect[value.ToString()];
            else
                StateSelect.Add(value.ToString(), statechange == CheckState.Checked ? true : false);
            
            for (int i = 0; i < ErrorGridview.Rows.Count; i++)
                if (StateSelect[ErrorGridview[1, i].Value.ToString()])
                    ErrorGridview.Rows[i].Visible = true;
                else
                    ErrorGridview.Rows[i].Visible = false;
        }

        private void Cleanbutton_Click(object sender, EventArgs e)
        {
            while (ErrorGridview.Rows.Count != 0)
                ErrorGridview.Rows.RemoveAt(0);
        }
    }
}
