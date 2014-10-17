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
                // 品詞が名詞以外なら飛ばす
                if (morphemes[i].Pos != "名詞") continue;
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

            //debug
            for (int i = 0; i < originalVector.Length; i++ )
            {
                Console.WriteLine("{0} - {1}", keywords[i], originalVector[i]);
            }

            // 文書同士の類似度比較用のリスト vectors[i][j] := i番目のベクトルのTF-IDFによるベクトル表現
            // double[,] vectors = new double[_docs.Count, morphemes.Count];
            List<List<double>> vectors = new List<List<double>>();

            // 全文書についてqueryのワードを含むかチェック
            for (int i = 0; i < _docs.Count; i++)
            {
                vectors.Add(new List<double>());
                for (int j = 0; j < keywords.Length; j++)
                {
                    string surface = keywords[j];
                    
                    // そもそもqueryのワードをひとつも含まない場合
                    if (!invertedIndex.ContainsKey(surface))
                    {
                        vectors[i].Add(0.0);
                        continue;
                    }
                    // 含む場合、出現回数をベクトルの要素にする. 含まない場合0.0
                    if (invertedIndex[surface].ContainsKey(i))
                    {
                        // 文書 i でのsurfaceの頻度
                        double tf = (double)invertedIndex[surface][i];

                        // surfaceを含む文書数の逆
                        double idf = Math.Log((double)_docs.Count / (double)invertedIndex[surface].Keys.Count);

                        vectors[i].Add(tf * idf);
                    }
                    else
                    {
                        vectors[i].Add(0.0);
                    }
                }
            }
            
            // queryと全文書間の類似度を計算して順位付けする
            return GetRankedDocument(originalVector, vectors);
        }

        /// <summary>
        /// ランキングされたDocumentのリストを返す
        /// </summary>
        /// <param name="query">queryのベクトル</param>
        /// <param name="documents">全文書のベクトル</param>
        /// <returns>ランキングされたリスト</returns>
        private List<Document> GetRankedDocument(double[] query, List<List<double>> documents)
        {
            // 返すDocumentのリスト
            List<Document> docs = new List<Document>();

            // Key: 何番目の文書か Value: queryとの類似度
            Dictionary<int, double> similarity = new Dictionary<int, double>();

            Console.WriteLine("--- 類似度 ---");
            // 類似度計算
            for (int i = 0; i < documents.Count; i++)
            {
                VectorSimilarity vs = new VectorSimilarity(query, documents[i].ToArray());
                similarity[i] = vs.Cosine();
                Console.WriteLine("{0} - {1}", i, similarity[i]);
            }

            // Valueでソート 降順にする
            List<KeyValuePair<int, double>> list = new List<KeyValuePair<int, double>>(similarity);
            list.Sort(( kvp1, kvp2) =>
                {
                    return kvp2.Value.CompareTo(kvp1.Value);
                });

            foreach (KeyValuePair<int, double> kvp in list)
            {
                docs.Add(_docs[kvp.Key]);
            }

            return docs;
        }

        /// <summary>
        /// 転置インデックスを作成する
        /// </summary>
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
                    // 品詞が名詞以外なら飛ばす
                    if (morphemes[j].Pos != "名詞") continue;

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

            /* 以下デバッグコード
            foreach (string k in invertedIndex.Keys)
            {
                Console.WriteLine("-----\n{0}", k);
                foreach (KeyValuePair<int, int> pair in invertedIndex[k])
                {
                    Console.WriteLine("{0} - 回数 {1}", pair.Key, pair.Value);
                }
                Console.WriteLine("-----");
            }
            */

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
