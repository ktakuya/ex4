using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMorphologicalAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "今日はいい天気です";
            MorphologicalAnalyzer analyser = new MorphologicalAnalyzer();
            Morpheme[] morphemes = analyser.Analyse(str);

            foreach (Morpheme m in morphemes)
            {
                Console.WriteLine(m.ToString());
            }

        }
    }
}
