using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMeCab;

namespace MeCabMorphologicalAnalyzer
{
    class MeCabMorphologicalAnalyzer
    {
        public MeCabMorphologicalAnalyzer() { }

        public List<Morpheme> Analyse(string str)
        {
            List<Morpheme> morphemeList = new List<Morpheme>();

            MeCabTagger mct = MeCabTagger.Create();
            MeCabNode node = mct.ParseToNode(str);

            while (node != null)
            {
                if (node.CharType > 0)
                {
                    Console.WriteLine(node.Surface + " - " + node.Feature);
                }
                node = node.Next;
            }
            return morphemeList;
        }
    }
}
