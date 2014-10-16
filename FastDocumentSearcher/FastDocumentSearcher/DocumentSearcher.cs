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
            double[] originalVector;
            string[] keywords;

            // query を形態素解析
            MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer mcma = new MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer();
            List<Morpheme> morphemes = mcma.Analyse(query);

            // Queryのベクトルを生成する
            Dictionary<string, double> queryVector = new Dictionary<string, double>();
            for (int i = 0; i < morphemes.Count; i++)
            {
                string surface = morphemes[i].Surface;
                if (queryVector.ContainsKey(surface))
                {
                    queryVector[surface] += 1.0;
                }
                else
                {
                    queryVector[surface] = 1.0;
                }
             }
            // doubleの配列に変換する
            originalVector = new double[queryVector.Keys.Count];
            keywords = new string[queryVector.Keys.Count];
            
            // keywordsにはqueryの単語 originalVectorにはそのkeywordsの順に出現回数
            int cnt = 0;
            foreach (KeyValuePair<string, double> pair in queryVector)
            {
                originalVector[cnt] = pair.Value;
                keywords[cnt] = pair.Key;
                cnt++;
            }

            // 文書同士の類似度比較用のリスト vectors[i][j] := i番目の文書のj番目の品詞の出現回数
            double[,] vectors = new double[_docs.Count, morphemes.Count];
 
            // 全文書についてqueryのワードを含むかチェック
            for (int i = 0; i < _docs.Count; i++)
            {
                for (int j = 0; j < morphemes.Count; j++)
                {
                    string surface = morphemes[j].Surface;
                    // 含む場合、出現回数をベクトルの要素にする. 含まない場合0.0
                    if (invertedIndex[surface].ContainsKey(i))
                    {
                        vectors[i, j] = (double)invertedIndex[surface][i];
                    }
                    else
                    {
                        vectors[i, j] = 0.0;
                    }
                }
            }
            
            // ベクトル同士の類似度を計算して
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
                Console.WriteLine(i);
                // 本文を形態素解析
                List<Morpheme> morphemes = analyzer.Analyse(_docs[i].Body);
                
                // 得られた形態素から転置インデックスを作成
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

            // 以下デバッグコード
            foreach (string k in invertedIndex.Keys)
            {
                Console.WriteLine("-----\n{0}", k);
                foreach (KeyValuePair<int, int> pair in invertedIndex[k])
                {
                    Console.WriteLine("{0} - 回数 {1}", pair.Key, pair.Value);
                }
                Console.WriteLine("-----");
            }
            //

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
