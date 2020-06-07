using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.BearingFrm
{
    class ColorBar
    {
        static public Dictionary<string, byte[,]> ColorDict;

        static ColorBar()
        {
            ColorDict = new Dictionary<string, byte[,]>();
            InitColorBar();
        }

        static void InitColorBar()
        {
            string file = Properties.Resources.ColorBar;
            string[] allline = file.Split('\n');

            string colorname = null;
            int count = 0;
            foreach (var oneline in allline)
            {
                string line = oneline;
                if (oneline.Contains('\r'))
                    line = oneline.Remove(oneline.Length - 1);
                if (line.First() == '#')
                {
                    colorname = line.Remove(0, 1);
                    ColorDict.Add(colorname, new byte[64, 3]);
                    count = 0;
                }
                else
                {
                    var nums = line.Split('\t');
                    if (nums.Length == 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ColorDict[colorname][count, i] = byte.Parse(nums[i]);
                        }
                        count++;
                    }
                }
            }
        }

        static public Color GetRGB(string colorname, double value)
        {
            Color rColor = Color.FromArgb(255, 255, 255);
            if (ColorDict.ContainsKey(colorname) && value >= 0 && value <= 1)
            {
                int[] ColorRGB = new int[3];
                value = value * 63;
                double t = value - Math.Floor(value);
                for (int i = 0; i < 3; i++)
                {
                    ColorRGB[i] = (int)Math.Round((1d - t) * ColorDict[colorname][(int)Math.Floor(value), i] + t * ColorDict[colorname][(int)Math.Ceiling(value), i]);
                }
                rColor = Color.FromArgb(ColorRGB[0], ColorRGB[1], ColorRGB[2]);
            }
            return rColor;
        }

        static public Color[] GetRGB(string colorname, int length)
        {
            Color[] rcolor = new Color[length];
            if (ColorDict.ContainsKey(colorname))
            {
                if (length == 64)
                {
                    for (int i = 0; i < 64; i++)
                        rcolor[i] = Color.FromArgb(ColorDict[colorname][i, 0], ColorDict[colorname][i, 1], ColorDict[colorname][i, 2]);
                }
                else
                {
                    double fenduzhi = 63d / (length - 1);
                    double value = 0;
                    int[] ColorRGB = new int[3];
                    double t;
                    for (int i = 0; i < length; i++)
                    {
                        t = value - Math.Floor(value);
                        for (int j = 0; j < 3; j++)
                            ColorRGB[j] = (int)Math.Round((1d - t) * ColorDict[colorname][(int)Math.Floor(value), j] +
                                t * ColorDict[colorname][(int)Math.Min(63, Math.Ceiling(value)), j]);
                        rcolor[i] = Color.FromArgb(ColorRGB[0], ColorRGB[1], ColorRGB[2]);

                        value += fenduzhi;
                    }
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                    rcolor[i] = Color.FromArgb(255, 255, 255);
            }
            return rcolor;
        }
    }
}
