using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Facet.Combinatorics;

namespace Hayward
{
    public static class Utils
    {
        public static void WriteToFile(List<string> list, string filename)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                foreach (string s in list)
                    tw.WriteLine(s);
            }
        }

        public static List<string> ReadFromFile(string filename)
        {
            string[] file = File.ReadAllLines(filename);
            List<string> linelist = new List<string>(file);

            return linelist;
        }

        public static List<int> SubstringCount(string main, List<string> substrings)
        {
            List<int> iCount = new List<int>();

            for (int y = 0; y < substrings.Count; y++)
            {
                iCount[y] += (main.Length - main.Replace(substrings[y], String.Empty).Length) / substrings[y].Length;
            }

            return iCount;
        }

        public static int CountPattern(this RichTextBox myRtb, string word)
        {
            int iCount = 0;

            if (word != string.Empty)
            {
                int s_start = myRtb.SelectionStart, startIndex = 0, index;

                while ((index = myRtb.Text.IndexOf(word, startIndex)) != -1)
                {
                    startIndex = index + word.Length-1;
                    ++iCount;
                }

            }
            return iCount;
        }

        public static void HighlightPattern(this RichTextBox myRtb, string word, Color color)
        {

            if (word != string.Empty)
            {
                int s_start = myRtb.SelectionStart, startIndex = 0, index;

                while ((index = myRtb.Text.IndexOf(word, startIndex)) != -1)
                {
                    myRtb.Select(index + 1, word.Length-1);
                    myRtb.SelectionBackColor = color;

                    startIndex = index + word.Length-1;
                }

                myRtb.SelectionStart = s_start;
                myRtb.SelectionLength = 0;
                myRtb.SelectionColor = Color.Black;
            }
        }

        public static List<string> GenerateAllPatterns(List<string> baselist)
        {
            List<string> sAllPatterns = new List<string>();

            Permutations<char> P1;

            for (int i = 0; i < baselist.Count; ++i)
            {
                P1 = new Permutations<char>(baselist[i].ToCharArray(), GenerateOption.WithoutRepetition);
                foreach (IList<char> p in P1)
                {
                    sAllPatterns.Add(new string(p.ToArray<char>()));
                }
            }

            return sAllPatterns;
        }
    }
}
