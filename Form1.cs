﻿using ProjectMS.Core.custom_control;
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
            InitMenu();
        }

        #region 程序可设置内容
        #region 窗口
        private void InitForm()
        {
            this.Text = "轴承健康监控及剩余寿命预测系统";
            this.Icon = Icon.FromHandle(Properties.Resources.窗口图标_32.GetHicon());
            this.BackColor = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlLightLight;
        }
        #endregion
        #region 标题
        private void InitTitle()
        {
            Top_panel.Height = 80;
            Title.AutoSize = false;
            Title.TextAlign = ContentAlignment.MiddleLeft;
            Title.Font = new Font("宋体", 24);
            Title.Text = "轴承健康监控及剩余寿命预测系统";
            Title.ForeColor = Color.White;
        }
        #endregion

        #region 菜单栏
        private List<Tuple<MenuButton, bool>> MenuButtons;
        private void InitMenu()
        {
            Left_panel.Width = 200;
            int menu_height = 35;
            Font menu_font = new Font("宋体", 14);
            MenuButtons = new List<Tuple<MenuButton, bool>>();

            //  添加菜单按钮
            var 主界面Menu = new MenuButton(menu_font, "主界面", menu_height);
            MenuButtons.Add(new Tuple<MenuButton, bool>(主界面Menu,true));
            var 帮助Menu = new MenuButton(menu_font, "帮助", menu_height);
            MenuButtons.Add(new Tuple<MenuButton, bool>(帮助Menu, false));

            //  往菜单上添加菜单按钮
            foreach (var x in MenuButtons)
            {
                x.Item1.Click += MenuButton_Click;
                if (x.Item2)
                {
                    x.Item1.Dock = DockStyle.Top;
                    Left_panel.Controls.Add(x.Item1);
                }
                else
                {
                    x.Item1.Dock = DockStyle.Bottom;
                    Left_panel.Controls.Add(x.Item1);
                }
            }

        }
        /// <summary>
        /// 当随便一个菜单按钮按下之后，取消其他已经按下的菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuButton_Click(object sender, EventArgs e)
        {
            MenuButton a = (MenuButton)sender;
            if (a.Btnchecked)
            {
                a.isLock = true;
                for (int i = 0; i < MenuButtons.Count; i++)
                    if (MenuButtons[i].Item1 != a && MenuButtons[i].Item1.Btnchecked)
                    {
                        MenuButtons[i].Item1.isLock = false;
                        MenuButtons[i].Item1.PerformClick();
                    }
            }
        }
        /// <summary>
        /// 主窗口更改画面
        /// </summary>
        /// <param name="frm"></param>
        //private void ChgMainFrm(Form frm)
        //{
        //    frm.TopLevel = false;
        //    frm.FormBorderStyle = FormBorderStyle.None;
        //    frm.Dock = DockStyle.Fill;
        //    splitContainer1.Panel1.Controls.Clear();
        //    splitContainer1.Panel1.Controls.Add(frm);
        //    frm.Show();
        //}
        #endregion
        #endregion



    }
}
