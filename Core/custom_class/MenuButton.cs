using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.custom_class
{
    public partial class MenuButton : UserControl
    {
        public MenuButton(Font buttonfont, int buttonheight)
        {
            InitializeComponent();
            ButtonText.Parent = ButtonBackPic;
            Btncheckedchange += CustomButton_Btncheckedchange;
            this.BackColor = Color.Transparent;
            this.Height = buttonheight;
            ButtonText.Font = buttonfont;
        }

        public MenuButton(Font buttonfont, string text, int buttonheight):this(buttonfont,buttonheight)
        {
            BtnText = text;
        }

        private void MenuButton_Load(object sender, EventArgs e)
        {
            ButtonText.ForeColor = this.ForeColor;
            ButtonText.TextAlign = ContentAlignment.MiddleLeft;
            this.Btnchecked = false;
        }

        private void CustomButton_Btncheckedchange(bool obj)
        {
            if (obj)
            {
                ButtonBackPic.Image = backImageMove;
                ButtonText.ForeColor = Color.Orange;
            }
            else
            {
                ButtonBackPic.Image = backImageLeave;
                ButtonText.ForeColor = this.ForeColor;
            }
        }

        /// <summary>
        /// 鼠标移动到控件上方显示的图片
        /// </summary>
        private Image backImageMove = global::ProjectMS.Properties.Resources.菜单按钮T;
        [Description("鼠标移动到控件上方显示的图片")]
        public Image BackImageMove
        {
            get { return backImageMove; }
            set { backImageMove = value; }
        }
        /// <summary>
        /// 鼠标离开控件显示的图片
        /// </summary>
        private Image backImageLeave = global::ProjectMS.Properties.Resources.菜单按钮F;
        [Description("鼠标离开控件显示的图片")]
        public Image BackImageLeave
        {
            get { return backImageLeave; }
            set
            {
                backImageLeave = value;
                ButtonBackPic.Image = backImageLeave;
            }
        }
        /// <summary>
        /// 按钮文字显示
        /// </summary>
        private string BText = "";
        [Description("按钮显示的文字")]
        public string BtnText
        {
            get { return BText; }
            set
            {
                BText = value;
                ButtonText.Text = BText;
            }
        }

        public event Action<bool> Btncheckedchange;
        private bool _Btnchecked = false;
        [Description("按钮是否已经按下")]
        public bool Btnchecked
        {
            get { return _Btnchecked; }
            set
            {
                _Btnchecked = value;
                Btncheckedchange?.Invoke(value);
            }
        }

        private void ButtonBackPic_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isLock)
            {
                if (Btnchecked)
                {
                    ButtonText.ForeColor = this.ForeColor;
                }
                else
                {
                    ButtonText.ForeColor = Color.Orange;
                }
            }
        }

        private void ButtonBackPic_MouseLeave(object sender, EventArgs e)
        {
            if (Btnchecked)
            {
                ButtonText.ForeColor = Color.Orange;
            }
            else
            {
                ButtonText.ForeColor = this.ForeColor;
            }
        }

        private bool _isLock = false;
        [Description("锁定按下功能")]
        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }
        public void buttonText_Click(object sender, EventArgs e)
        {
            if (!isLock)
            {
                Btnchecked = !Btnchecked;
                OnClick(e);//这句话很关键!!!
            }
        }
        /// <summary>
        /// 从程序上令该按键处于按下状态
        /// </summary>
        public void PerformClick()
        {
            buttonText_Click(this, new EventArgs());
        }

    }
}
