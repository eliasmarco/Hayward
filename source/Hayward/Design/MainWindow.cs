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
        // List of Form Labels for Patterns and its count
        private List<Label> lCommonPatternLabelList;
        private List<Label> lCommonPatternLabelCountList;

        private List<string> slSequence;
        private List<string> lListOfAllPatterns;

        private int[] iCountOfCommonPatterns;
        private readonly int nNumOfCommonPatterns = 14;

        private readonly Color[] cColorsForCommonPatterns = {
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

        private readonly string[] sCommonPatterns = {
            "FFFSS",
            "FFFFSSS",
            "FFFFFSSSS",
            "FFFFFFSSSSS",
            "FFFFFFFSSSSSS",
            "FFFFFFFFSSSSSSS",
            "FFFFFFFFFSSSSSSSS",
            "FFFFSS",
            "FFFFFSSS",
            "FFFFFFSSSS",
            "FFFFFFFSSSSS",
            "FFFFFFFFSSSSSS",
            "FFFFFFFFFSSSSSSS",
            "FFFFFFFFFFSSSSSSSS",
        };

        public MainWindow()
        {
            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            this.slSequence = new List<string>();
            this.iCountOfCommonPatterns = new int[nNumOfCommonPatterns];
            this.lCommonPatternLabelCountList = new List<Label>
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

            this.lCommonPatternLabelList = new List<Label>
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

            Reset_Data();
            lListOfAllPatterns = Permutator.Core.GenerateAllPatterns(sCommonPatterns.ToList<string>());
        }

        private void Button_ohho_Click(object sender, EventArgs e)
        {
            this.slSequence.Add("S");
            pictureBox_express.Image = Properties.Resources.thumbsup;
            pictureBox_express.Refresh();
            UpdateSequenceTextBox();
        }

        private void Button_ahhh_Click(object sender, EventArgs e)
        {
            this.slSequence.Add("F");
            pictureBox_express.Image = Properties.Resources.fail;
            pictureBox_express.Refresh();
            UpdateSequenceTextBox();
        }

        private void UpdateSequenceTextBox()
        {
            RichTextBox_sequence.Text = string.Join("", slSequence);

            SearchSequenceForPatterns();
        }

        private void SearchSequenceForPatterns()
        {
            string distinctpattern;
            // Count Common Pattern Occurences
            for (int i = 0; i < nNumOfCommonPatterns; ++i)
            {
                distinctpattern = (sCommonPatterns[i][0] == 'F') ? "S" + sCommonPatterns[i] : "F" + sCommonPatterns[i];
                this.iCountOfCommonPatterns[i] = Utils.CountPattern(RichTextBox_sequence, distinctpattern);
                if (this.lCommonPatternLabelCountList[i].Text != Utils.CountPattern(RichTextBox_sequence, distinctpattern).ToString())
                {
                    this.lCommonPatternLabelCountList[i].BackColor = Color.LightPink;
                }
                else
                {
                    this.lCommonPatternLabelCountList[i].BackColor = Color.LightGray;
                }
                this.lCommonPatternLabelCountList[i].Text = Utils.CountPattern(RichTextBox_sequence, distinctpattern).ToString();
                this.lCommonPatternLabelList[i].BackColor = Color.LightGray;
            }

            int maxValue = this.iCountOfCommonPatterns.Max();
            if (maxValue != 0)
            {
                int maxIndex = this.iCountOfCommonPatterns.ToList().IndexOf(maxValue);
                distinctpattern = (sCommonPatterns[maxIndex][0] == 'F') ? "S" + sCommonPatterns[maxIndex] : "F" + sCommonPatterns[maxIndex];
                Utils.HighlightPattern(RichTextBox_sequence, distinctpattern, this.cColorsForCommonPatterns[maxIndex]);
                lCommonPatternLabelCountList[maxIndex].BackColor = Color.LawnGreen;
                lCommonPatternLabelList[maxIndex].BackColor = Color.LawnGreen;
                Update_Result(this.sCommonPatterns[maxIndex], maxValue);
            }
        }

        private void Reset_Data()
        {
            for (int i = 0; i < nNumOfCommonPatterns; ++i)
            {
                this.lCommonPatternLabelCountList[i].BackColor = Color.LightGray;
                this.lCommonPatternLabelCountList[i].Text = "0";
                this.lCommonPatternLabelList[i].BackColor = Color.LightGray;
            }
            this.slSequence.Clear();
            RichTextBox_sequence.Text = "";
            Update_Result("--------", 0);
        }

        private void PictureBox_holgi_DoubleClick(object sender, EventArgs e)
        {
            Form About = new Hayward.Design.About();
            About.ShowDialog();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
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

        private void Button_Permute_Click(object sender, EventArgs e)
        {
            Process_Permutations();
        }

        private void Process_Permutations()
        {

            List<int> perpatternCount = new List<int>();
            string distinctpattern;

            // Count Pattern Occurences
            for (int i = 0; i < lListOfAllPatterns.Count; ++i)
            {
                distinctpattern = (lListOfAllPatterns[i][0] == 'F') ? "S" + lListOfAllPatterns[i] : "F" + lListOfAllPatterns[i];
                perpatternCount.Add(Utils.CountPattern(RichTextBox_sequence, distinctpattern));
            }

            int maxValue = perpatternCount.Max();
            if (maxValue != 0)
            {
                int maxIndex = perpatternCount.ToList().IndexOf(maxValue);
                distinctpattern = (lListOfAllPatterns[maxIndex][0] == 'F') ? "S" + lListOfAllPatterns[maxIndex] : "F" + lListOfAllPatterns[maxIndex];
                Utils.HighlightPattern(RichTextBox_sequence, distinctpattern, Color.LawnGreen); // dummy initial char

                Update_Result(lListOfAllPatterns[maxIndex], maxValue);
            }
    }

        public void Update_Result(string pattern, int count)
        {
            RichTextBox_WinPattern.Text = pattern;
            RichTextBox_WinCount.Text = count.ToString();
            RichTextBox_WinPattern.SelectAll();
            RichTextBox_WinPattern.SelectionAlignment = HorizontalAlignment.Center;
            RichTextBox_WinCount.SelectAll();
            RichTextBox_WinCount.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
