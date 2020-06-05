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
            // 标题
            Top_panel.Height = 80;
            Title.Font = new Font("宋体", 24);
            Title.Text = "轴承健康监控及剩余寿命预测";
            Title.ForeColor = Color.White;
            
        }
    }
}
