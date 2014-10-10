using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeCabMorphologicalAnalyzer;

namespace MeCabMorphologicalAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "今日はいい天気です";
            MeCabMorphologicalAnalyzer analyser = new MeCabMorphologicalAnalyzer();
            List<Morpheme> result = analyser.Analyse(str);

            /*
            foreach (Morpheme m in result)
            {
                Console.WriteLine(m.ToString());
            }
             */
        }
    }
}
