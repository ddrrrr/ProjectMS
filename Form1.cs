using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitForm();
            InitTitle();
        }

        #region 程序可设置内容
        #region 窗口
        private void InitForm()
        {
            Text = "轴承健康监控及剩余寿命预测系统";
            Icon = Icon.FromHandle(Properties.Resources.窗口图标_32.GetHicon());
        }
        #endregion
        #region 标题
        private void InitTitle()
        {
            Top_panel.Height = 80;
            Title.Font = new Font("宋体", 24);
            Title.Text = "轴承健康监控及剩余寿命预测系统";
            Title.ForeColor = Color.White;
        }
        #endregion

        #region 菜单栏
        private void InitMenu()
        {
            Left_panel.Width = 200;
            int menu_height = 35;
            Font menu_font = new Font("宋体", 14);
        }
        #endregion
        #endregion


        
    }
}
