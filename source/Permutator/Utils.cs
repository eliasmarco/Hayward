using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Permutator
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

        public static List<string> GeneratePatterns(string basepattern)
        {
            List<string> allcombinations = new List<string>();

            allcombinations.AddRange(permute(basepattern, 0, basepattern.Length - 1));

            List<string> alldistinct = allcombinations.Distinct().ToList<string>();

            return alldistinct;
        }

        public static List<string> permute(string str,
                                    int l, int r)
        {
            List<string> ret = new List<string>();

            Console.WriteLine("Permutation: L=" + l + " R=" + r);

            if (l == r)
                ret.Add(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = swap(str, l, i);
                    ret.AddRange(permute(str, l + 1, r));
                    str = swap(str, l, i);
                }
            }
            return ret;
        }

        public static string swap(string a,
                                  int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }
    }
}
