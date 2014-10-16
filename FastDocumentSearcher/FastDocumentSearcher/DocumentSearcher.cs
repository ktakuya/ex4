using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TkVector;
using NMeCab;
using MeCabMorphologicalAnalyzer;

namespace FastDocumentSearcher
{
    /// <summary>
    /// 文書群に対する高速な検索クラス
    /// </summary>
    class DocumentSearcher
    {

        private List<Document> _docs;
        private Dictionary<string, Dictionary<int, int>> invertedIndex;

        /// <summary>
        /// コンストラクタ 
        /// </summary>
        /// <param name="docs">検索対象であるDocumentクラスからなる文書のリスト</param>
        public DocumentSearcher(List<Document> docs)
        {
            _docs = docs;
            InitInverseIndex();
        }

        /// <summary>
        /// 指定されたクエリによる検索結果を返すメソッド
        /// </summary>
        /// <param name="query">検索クエリ</param>
        /// <returns>関連性が高い順にランキングされたDocumentクラスのリスト</returns>
        public List<Document> Search(string query)
        {
            List<Document> documentList = new List<Document>();
            
            return documentList;
        }

        private void InitInverseIndex()
        {
            // 転置インデックスを生成する
            invertedIndex = new Dictionary<string, Dictionary<int, int>>();
            
            // 形態素解析クラスのインスタンス
            MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer analyzer = new MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer();
            
            // すべてのDocumentに対してbodyをMeCabで解析してある単語の出現するインデックスを抽出する
            for (int i = 0; i < _docs.Count; i++ )
            {
                List<Morpheme> morphemes = analyzer.Analyse(_docs[i].Body);
                for (int j = 0; j < morphemes.Count; j++)
                {
                    string surface = morphemes[j].Surface;
                    if (invertedIndex.ContainsKey(surface))
                    {
                        if (!invertedIndex[surface].ContainsKey(i))
                        {
                            invertedIndex[surface][i] = 0;
                        }
                        invertedIndex[surface][i] += 1;
                    }
                    else
                    {
                        invertedIndex[surface] = new Dictionary<int, int>();
                        invertedIndex[surface][i] = 1;
                    }
                }
            }

            foreach (string k in invertedIndex.Keys)
            {
                Console.WriteLine("-----\n{0}", k);
                foreach (KeyValuePair<int, int> pair in invertedIndex[k])
                {
                    Console.WriteLine("{0} - 回数 {1}", pair.Key, pair.Value);
                }
                Console.WriteLine("-----");
            }
        }

    }

    /// <summary>
    /// ベクトル空間での類似度計算クラス
    /// </summary>
    class VectorSimilarity
    {
        Vector _v1, _v2;

        public VectorSimilarity(double[] v1, double[] v2){
            _v1 = new Vector(v1);
            _v2 = new Vector(v2);
        }

        public double Cosine()
        {
            return _v1.InnerProduct(_v2) / (_v1.Norm() * _v2.Norm());
        }
    }
}
