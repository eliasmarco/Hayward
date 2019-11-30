using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Facet.Combinatorics;


namespace Permutator
{
    public static class Core
    {
        //static void Main()
        //{
            //string basepatternfn = "basepatterns.txt";
            //string rarepatternfn = "rarepatterns.txt";

            //GenerateFileToFile(basepatternfn, rarepatternfn);

        //}

        public static void GenerateFileToFile(string in_filename, string out_filename)
        {
            List<string> sBasePatterns = new List<string>();
            List<string> sAllPatterns = new List<string>();

            Permutations<char> P1;

            if (File.Exists(in_filename))
            {
                sBasePatterns = Utils.ReadFromFile(in_filename);

                for (int i = 0; i < sBasePatterns.Count; ++i)
                {
                    P1 = new Permutations<char>(sBasePatterns[i].ToCharArray(), GenerateOption.WithoutRepetition);
                    foreach (IList<char> p in P1)
                    {
                        sAllPatterns.Add(new string(p.ToArray<char>()));
                    }
                }

                Utils.WriteToFile(sAllPatterns, out_filename);
            }
            else
            {
                throw new FileNotFoundException("Base Pattern File not found");
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
