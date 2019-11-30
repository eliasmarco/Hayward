using System.Windows.Forms;
using System.Drawing;

namespace Hayward
{
    static class Utils
    {
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
    }
}
