using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TFVectorGenerator
{
    /// <summary>
    /// 優 [yamamoto]
    /// 英語文章からTFベクトルを生成するクラス
    /// </summary>
    class TFVectorGenerator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TFVectorGenerator()
        {

        }

        /// <summary>
        /// 指定された文字列のTFベクトルを生成するメソッド
        /// </summary>
        /// <param name="str">TFベクトルにする文字列</param>
        /// <returns>キーが単語で値が出現頻度の辞書</returns>
        public Dictionary<string, int> Generate(string str)
        {
            // 文字列を小文字に変換しておく
            string inputString = str.ToLower();

            // アルファベット文字以外を削除する 
            Regex re = new Regex("[^a-z ]", RegexOptions.Singleline);
            inputString = re.Replace(inputString, " ");

            // スペースごとに文字列を区切る
            string[] splitString = inputString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            // 結果を格納する辞書を作成する
            Dictionary<string, int> result = new Dictionary<string, int>();

            // 各単語の出現回数をカウントする
            foreach (string s in splitString)
            {
                if (result.ContainsKey(s))
                {
                    result[s]++;
                }
                else
                {
                    result[s] = 1;
                }
            }

            return result;
        }
    }
}
