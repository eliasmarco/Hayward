using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Facet.Combinatorics;


namespace Permutator
{
    class Program
    {
        static void Main()
        {
            List<string> sBasePatterns = new List<string>();
            List<string> sRarePatterns = new List<string>();
            string basepatternfn = "basepatterns.txt";
            string rareepatternfn = "rarepatterns.txt";

            if (File.Exists(basepatternfn))
            {
                sBasePatterns = Utils.ReadFromFile(basepatternfn);

                for (int i = 0; i < sBasePatterns.Count; ++i)
                {
                    sRarePatterns.AddRange(Utils.GeneratePatterns(sBasePatterns[i]));
                }

                Utils.WriteToFile(sRarePatterns, rareepatternfn);
            }
            else 
            {
                throw new FileNotFoundException("Base Pattern File not found");
            }
        }
    }
}
