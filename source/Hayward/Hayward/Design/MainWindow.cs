using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Hayward
{
    public partial class MainWindow : Form
    {
        private List<string> slPattern = new List<string>();
        private List<Label> lLabelCountList;
        private List<Label> lLabelList;
        private int[] iPatternCount;
        private readonly int nPatterns = 14;
        private readonly Color[] cColors = {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Cyan,
            Color.Magenta,
            Color.Yellow,
            Color.Purple,
            Color.Pink,
            Color.LightGreen,
            Color.LightBlue,
            Color.DarkCyan,
            Color.DarkMagenta,
            Color.Gold,
            Color.MediumPurple,
        };
        private readonly string[] sPatterns = {
            "SFFFSS",
            "SFFFFSSS",
            "SFFFFFSSSS",
            "SFFFFFFSSSSS",
            "SFFFFFFFSSSSSS",
            "SFFFFFFFFSSSSSSS",
            "SFFFFFFFFFSSSSSSSS",
            "SFFFFSS",
            "SFFFFFSSS",
            "SFFFFFFSSSS",
            "SFFFFFFFSSSSS",
            "SFFFFFFFFSSSSSS",
            "SFFFFFFFFFSSSSSSS",
            "SFFFFFFFFFFSSSSSSSS",
        };

        public MainWindow()
        {
            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            this.iPatternCount = new int[this.nPatterns];
            lLabelCountList = new List<Label>();
            lLabelCountList.Add(label_PCount_1_1);
            lLabelCountList.Add(label_PCount_1_2);
            lLabelCountList.Add(label_PCount_1_3);
            lLabelCountList.Add(label_PCount_1_4);
            lLabelCountList.Add(label_PCount_1_5);
            lLabelCountList.Add(label_PCount_1_6);
            lLabelCountList.Add(label_PCount_1_7);
            lLabelCountList.Add(label_PCount_2_1);
            lLabelCountList.Add(label_PCount_2_2);
            lLabelCountList.Add(label_PCount_2_3);
            lLabelCountList.Add(label_PCount_2_4);
            lLabelCountList.Add(label_PCount_2_5);
            lLabelCountList.Add(label_PCount_2_6);
            lLabelCountList.Add(label_PCount_2_7);

            lLabelList = new List<Label>();
            lLabelList.Add(label_P1_1);
            lLabelList.Add(label_P1_2);
            lLabelList.Add(label_P1_3);
            lLabelList.Add(label_P1_4);
            lLabelList.Add(label_P1_5);
            lLabelList.Add(label_P1_6);
            lLabelList.Add(label_P1_7);
            lLabelList.Add(label_P2_1);
            lLabelList.Add(label_P2_2);
            lLabelList.Add(label_P2_3);
            lLabelList.Add(label_P2_4);
            lLabelList.Add(label_P2_5);
            lLabelList.Add(label_P2_6);
            lLabelList.Add(label_P2_7);
        }

        private void btn_ohho_Click(object sender, EventArgs e)
        {
            slPattern.Add("S");
            pictureBox_express.Image = Properties.Resources.thumbsup;
            pictureBox_express.Refresh();
            vUpdateText();
        }

        private void btn_ahhh_Click(object sender, EventArgs e)
        {
            slPattern.Add("F");
            pictureBox_express.Image = Properties.Resources.fail;
            pictureBox_express.Refresh();
            vUpdateText();
        }

        private void vUpdateText()
        {
            richTextBox_sequence.Text = string.Join("", slPattern);

            vSearchPatterns();
        }

        private void vSearchPatterns()
        {
            // Count Pattern Occurences
            for (int i = 0; i < nPatterns; ++i)
            {
                iPatternCount[i] = Utils.CountPattern(richTextBox_sequence, sPatterns[i]);
                if (lLabelCountList[i].Text != Utils.CountPattern(richTextBox_sequence, sPatterns[i]).ToString())
                {
                    lLabelCountList[i].BackColor = Color.LightPink;
                }
                else 
                {
                    lLabelCountList[i].BackColor = Color.LightGray;
                }
                lLabelCountList[i].Text = Utils.CountPattern(richTextBox_sequence, sPatterns[i]).ToString();
                lLabelList[i].BackColor = Color.LightGray;
            }

            int maxValue = iPatternCount.Max();
            int maxIndex = iPatternCount.ToList().IndexOf(maxValue);
            Utils.HighlightPattern(richTextBox_sequence, sPatterns[maxIndex], cColors[maxIndex]);

            if (maxValue != 0)
            {
                lLabelCountList[maxIndex].BackColor = Color.LawnGreen;
                lLabelList[maxIndex].BackColor = Color.LawnGreen;
            }
        }

        private void pictureBox_holgi_DoubleClick(object sender, EventArgs e)
        {
            Form About = new Hayward.Design.About();
            About.ShowDialog();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            richTextBox_sequence.Text = "";
            for (int i = 0; i < nPatterns; ++i)
            {
                lLabelCountList[i].BackColor = Color.LightGray;
                lLabelCountList[i].Text = "0";
                lLabelList[i].BackColor = Color.LightGray;
            }
            slPattern.Clear();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Export Sequence Data";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile()))
                {
                    writer.WriteLine(richTextBox_sequence.Text);
                    writer.Flush();
                    writer.Close();
                }
            }
        }
    }
}
