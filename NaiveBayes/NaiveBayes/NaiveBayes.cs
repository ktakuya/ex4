using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector;

namespace NaiveBayes
{
    /// <summary>
    /// ナイーブベイズを用いてコンストラクタで訓練データを受け取り，Classifyメソッドでデータを分類するクラス
    /// </summary>
    class NaiveBayes
    {
        private int POS = 0, NEG = 1; 
        private int _wordCount;
        private List<Vector.Vector>[] _class;
        private int[][] _wordsFreq;
        private int[] _classCount;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="positives">ポジティブな訓練データ</param>
        /// <param name="negatives">ネガティブな訓練データ</param>
        public NaiveBayes(List<Vector.Vector> positives, List<Vector.Vector> negatives)
        {
            train(positives, negatives);
        }

        /// <summary>
        /// ネガティブな訓練データの確率
        /// </summary>
        /// <returns>確率</returns>
        private double prob_neg()
        {
            return _class[NEG].Count / (_class[POS].Count + _class[NEG].Count);
        }

        /// <summary>
        /// ポジティブな訓練データの確率
        /// </summary>
        /// <returns>確率</returns>
        private double prob_pos()
        {
            return _class[POS].Count / (_class[POS].Count + _class[NEG].Count);
        }

        /// <summary>
        /// あるクラスの各単語の出現頻度を計算する
        /// </summary>
        /// <param name="c">クラス（POS or NEG）</param>
        private void CountWords(int c)
        {
            for (int i = 0; i < _class[c].Count; i++)
            {
                for (int j = 0; j < _wordCount; j++) {
                    _wordsFreq[c][j] += (int)_class[c][i].GetValue(j+1);
                    _classCount[c] += (int)_class[c][i].GetValue(j+1);
                }
            }
        }

        /// <summary>
        /// 訓練データを使って訓練する
        /// </summary>
        /// <param name="pos">ポジティブな訓練データ</param>
        /// <param name="neg">ネガティブな訓練データ</param>
        private void train(List<Vector.Vector> pos, List<Vector.Vector> neg)
        {
            _class = new List<Vector.Vector>[2];
            _class[POS] = pos;
            _class[NEG] = neg;
            _wordCount = pos[0].Dimension;
            _wordsFreq = new int[2][]  {
                new int[_wordCount],
                new int[_wordCount]
            };
            _classCount = new int[2] {0, 0};

            CountWords(POS);
            CountWords(NEG);
        }

        /// <summary>
        /// あるクラスの各単語の確率を計算する
        /// </summary>
        /// <param name="c">クラス（POS or NEG）</param>
        /// <param name="i">何ワード目か</param>
        /// <returns>確率</returns>
        private double probForWord(int c, int i) 
        {
            return _wordsFreq[c][i] / _classCount[c];
        }

        /// <summary>
        /// 指定されたデータがポジティブかネガティブかをナイーブベイズによって分類するメソッド
        /// </summary>
        /// <param name="data">分類したいデータ</param>
        /// <returns>分類結果（ポジティブなら+1，ネガティブなら-1を返す）</returns>
        public int Classify(Vector.Vector data)
        {
            double ans = 0.0;
            ans += Math.Log(prob_pos() / prob_neg());

            for (int i = 0; i < data.Dimension; i++)
            {
                ans += data.GetValue(i+1) * Math.Log(probForWord(POS, i) / probForWord(NEG, i));
            }

            if (ans > 0)
                return 1;
            else
                return -1;
        }
    }
}
