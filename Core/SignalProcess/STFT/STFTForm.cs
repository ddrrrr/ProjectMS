using ProjectMS.Core.BearingFrm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMS.Core.SignalProcess.STFT
{
    public partial class STFTForm : Form
    {
        public STFTForm()
        {
            InitializeComponent();
            ColorbarSelect = "jet";
            weight1 = new double[1, 1];
            weight1[0, 0] = 0;
            weight2 = new double[1, 1];
            weight2[0, 0] = 0;
            maxweight = 0;
            ColorBarLowlabel.Text = 0d.ToString("#0.00");
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            STFTPictureBox1.Width = (int)Math.Round(splitContainer1.Panel1.Width * 0.8);
            STFTPictureBox1.Height = (int)Math.Round((splitContainer1.Panel1.Height - STFTTitleLabel1.Height) * 0.8);
            STFTPictureBox1.Top = (int)Math.Round(splitContainer1.Panel1.Height * 0.1) + STFTTitleLabel1.Height;
            STFTPictureBox1.Left = (int)Math.Round(splitContainer1.Panel1.Width * 0.1);
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            STFTPictureBox2.Width = (int)Math.Round(splitContainer1.Panel2.Width * 0.8);
            STFTPictureBox2.Height = (int)Math.Round((splitContainer1.Panel2.Height - STFTTitleLabel2.Height) * 0.8);
            STFTPictureBox2.Top = (int)Math.Round(splitContainer1.Panel2.Height * 0.1) + STFTTitleLabel2.Height;
            STFTPictureBox2.Left = (int)Math.Round(splitContainer1.Panel2.Width * 0.1);
        }

        public string ColorbarSelect;
        /// <summary>
        /// 当色柱长度变化时，重画色柱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorBarPictureBox_Resize(object sender, EventArgs e)
        {
            if (ColorBarPictureBox.Width <= 0 || ColorBarPictureBox.Height <= 0)
                return;

            Bitmap bp = new Bitmap(ColorBarPictureBox.Width, ColorBarPictureBox.Height, PixelFormat.Format32bppArgb);
            BitmapData bData = bp.LockBits(new Rectangle(0, 0, bp.Width, bp.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format32bppArgb);
            Color[] colors = ColorBar.GetRGB(ColorbarSelect, bp.Width);
            unsafe
            {
                Color c;
                byte* p = (byte*)bData.Scan0;
                for (int i = 0; i < bp.Width; i++)
                {
                    c = colors[bp.Width - i - 1];
                    for (int j = 0; j < bp.Height; j++)
                    {
                        int offset = i * 4 + j * bData.Stride;
                        p[offset] = c.B;
                        p[offset + 1] = c.G;
                        p[offset + 2] = c.R;
                        p[offset + 3] = (byte)255;
                    }
                }
            }
            bp.UnlockBits(bData);
            ColorBarPictureBox.BackgroundImage = bp;
        }

        double[,] weight1;
        double[,] weight2;
        double maxweight;
        /// <summary>
        /// 重绘频谱图
        /// </summary>
        /// <param name="tmppic"></param>
        /// <param name="weight"></param>
        private void RedrawWeight(PictureBox tmppic, double[,] weight)
        {
            Bitmap bp = new Bitmap(tmppic.Width, tmppic.Height, PixelFormat.Format32bppArgb);
            BitmapData bData = bp.LockBits(new Rectangle(0, 0, bp.Width, bp.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format32bppArgb);

            short maxcolor = 255;
            Color[] colors = ColorBar.GetRGB(ColorbarSelect, maxcolor);

            unsafe
            {
                double scalewidth = (double)(weight.GetLength(0) - 1) / (bp.Width - 1);
                double tmpwidth = 0;
                double scaleheight = (double)(weight.GetLength(0) - 1) / (bp.Height - 1);
                double tmpheight = 0;

                short weight16x = -1, weight16y = -1;
                double u, v;
                byte* p = (byte*)bData.Scan0;
                short tmpweight;
                for (int i = 0; i < bp.Width; i++)
                {
                    tmpheight = 0;
                    weight16x = (short)Math.Floor(tmpwidth);
                    u = tmpwidth - weight16x;
                    for (int j = 0; j < bp.Height; j++)
                    {
                        weight16y = (short)Math.Floor(tmpheight);
                        v = tmpheight - weight16y;

                        tmpweight = (short)Math.Round((weight[weight16x, weight16y] * (1 - u) * (1 - v)
                        + weight[Math.Min(weight.GetLength(0) - 1, weight16x + 1), weight16y] * u * (1 - v)
                        + weight[weight16x, Math.Min(weight.GetLength(1) - 1, weight16y + 1)] * (1 - u) * v
                        + weight[Math.Min(weight.GetLength(0) - 1, weight16x + 1), Math.Min(weight.GetLength(1) - 1, weight16y + 1)] * u * v) / maxweight * maxcolor);

                        Color c = colors[tmpweight];
                        int offset = i * 4 + j * bData.Stride;
                        p[offset] = c.B;
                        p[offset + 1] = c.G;
                        p[offset + 2] = c.R;
                        p[offset + 3] = (byte)255;

                        tmpheight += scaleheight;
                    }
                    tmpwidth += scalewidth;
                }
            }
            bp.UnlockBits(bData);
            tmppic.BackgroundImage = bp;
        }
        public void SetWeight(string select, double[,] weight)
        {
            PictureBox tmppic;
            if (select == "z轴信号")
            {
                weight1 = weight;

                for (int i = 0; i < weight.GetLength(0); i++)
                    for (int j = 0; j < weight.GetLength(1); j++)
                        maxweight = Math.Max(maxweight, weight[i, j]);
                ColorBarHighlabel.Text = maxweight.ToString("#0.00");

                RedrawWeight(STFTPictureBox1, weight1);
            }
            else if (select == "x轴信号")
            {
                weight2 = weight;

                for (int i = 0; i < weight.GetLength(0); i++)
                    for (int j = 0; j < weight.GetLength(1); j++)
                        maxweight = Math.Max(maxweight, weight[i, j]);
                ColorBarHighlabel.Text = maxweight.ToString("#0.00");

                RedrawWeight(STFTPictureBox2, weight2);
            }
            else
                return;

        }

        private void STFTPictureBox1_Resize(object sender, EventArgs e)
        {
            RedrawWeight(STFTPictureBox1, weight1);
        }

        private void STFTPictureBox2_Resize(object sender, EventArgs e)
        {
            RedrawWeight(STFTPictureBox2, weight2);
        }

        private void ParaVisibleButton_Click(object sender, EventArgs e)
        {
            ParaPropertyGrid.Visible = !ParaPropertyGrid.Visible;
            if (ParaPropertyGrid.Visible)
                ParaVisibleButton.Text = "▶";
            else
                ParaVisibleButton.Text = "◀";
        }
    }
}
