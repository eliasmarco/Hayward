using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Hayward
{
    public partial class MainWindow : Form
    {
        private List<string> slPattern;
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
            this.slPattern = new List<string>();
            this.iPatternCount = new int[this.nPatterns];
            this.lLabelCountList = new List<Label>
            {
                label_PCount_1_1,
                label_PCount_1_2,
                label_PCount_1_3,
                label_PCount_1_4,
                label_PCount_1_5,
                label_PCount_1_6,
                label_PCount_1_7,
                label_PCount_2_1,
                label_PCount_2_2,
                label_PCount_2_3,
                label_PCount_2_4,
                label_PCount_2_5,
                label_PCount_2_6,
                label_PCount_2_7
            };

            this.lLabelList = new List<Label>
            {
                label_P1_1,
                label_P1_2,
                label_P1_3,
                label_P1_4,
                label_P1_5,
                label_P1_6,
                label_P1_7,
                label_P2_1,
                label_P2_2,
                label_P2_3,
                label_P2_4,
                label_P2_5,
                label_P2_6,
                label_P2_7
            };
        }

        private void Button_ohho_Click(object sender, EventArgs e)
        {
            this.slPattern.Add("S");
            pictureBox_express.Image = Properties.Resources.thumbsup;
            pictureBox_express.Refresh();
            UpdateText();
        }

        private void Button_ahhh_Click(object sender, EventArgs e)
        {
            this.slPattern.Add("F");
            pictureBox_express.Image = Properties.Resources.fail;
            pictureBox_express.Refresh();
            UpdateText();
        }

        private void UpdateText()
        {
            RichTextBox_sequence.Text = string.Join("", slPattern);

            SearchPatterns();
        }

        private void SearchPatterns()
        {
            // Count Pattern Occurences
            for (int i = 0; i < nPatterns; ++i)
            {
                this.iPatternCount[i] = Utils.CountPattern(RichTextBox_sequence, sPatterns[i]);
                if (this.lLabelCountList[i].Text != Utils.CountPattern(RichTextBox_sequence, this.sPatterns[i]).ToString())
                {
                    this.lLabelCountList[i].BackColor = Color.LightPink;
                }
                else
                {
                    this.lLabelCountList[i].BackColor = Color.LightGray;
                }
                this.lLabelCountList[i].Text = Utils.CountPattern(RichTextBox_sequence, this.sPatterns[i]).ToString();
                this.lLabelList[i].BackColor = Color.LightGray;
            }

            int maxValue = this.iPatternCount.Max();
            int maxIndex = this.iPatternCount.ToList().IndexOf(maxValue);
            Utils.HighlightPattern(RichTextBox_sequence, this.sPatterns[maxIndex], this.cColors[maxIndex]);

            if (maxValue != 0)
            {
                lLabelCountList[maxIndex].BackColor = Color.LawnGreen;
                lLabelList[maxIndex].BackColor = Color.LawnGreen;
            }
        }

        private void Reset_Data()
        {
            for (int i = 0; i < nPatterns; ++i)
            {
                this.lLabelCountList[i].BackColor = Color.LightGray;
                this.lLabelCountList[i].Text = "0";
                this.lLabelList[i].BackColor = Color.LightGray;
            }
            this.slPattern.Clear();
        }

        private void PictureBox_holgi_DoubleClick(object sender, EventArgs e)
        {
            Form About = new Hayward.Design.About();
            About.ShowDialog();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            RichTextBox_sequence.Text = "";
            Reset_Data();
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
                    writer.WriteLine(RichTextBox_sequence.Text);
                    writer.Flush();
                    writer.Close();
                }
            }
        }
    }
}
