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
       /// <summary>
       /// 優[imamori]
       /// 
       /// コンストラクタ
       /// </summary>
        public MeCabMorphologicalAnalyzer() { }

        /// <summary>
        /// 解析メソッド
        /// </summary>
        /// <param name="str">解析する文字列</param>
        /// <returns>解析された結果をまとめたリスト</returns>
        public List<Morpheme> Analyse(string str)
        {
            // 結果を格納するリストを生成
            List<Morpheme> morphemeList = new List<Morpheme>();

            // MeCab
            MeCabTagger mct = MeCabTagger.Create();
            MeCabNode node = mct.ParseToNode(str);

            while (node != null)
            {
                if (node.CharType > 0)
                {
                    morphemeList.Add(new Morpheme(node.Surface, GetPos(node.Feature)));
                }
                node = node.Next;
            }
            return morphemeList;
        }

        /// <summary>
        /// 品詞を返すメソッド
        /// </summary>
        /// <param name="feature">,で区切られた特徴を表す文字列</param>
        /// <returns>品詞</returns>
        private string GetPos(string feature)
        {
            return feature.Split(',')[0];
        }
    }
}
