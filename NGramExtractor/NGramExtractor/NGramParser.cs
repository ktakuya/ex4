using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramExtractor
{
    /// <summary>
    /// 優 [yamamoto]
    /// 文章からn-gramを抽出するクラス
    /// </summary>
    class NGramParser
    {
        private int GramNum;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gramNum">n-gramのnに相当</param>
        public NGramParser(int gramNum)
        {
            this.GramNum = gramNum;
        }

        /// <summary>
        /// n-gramを抽出したstringの配列を返す
        /// </summary>
        /// <param name="str">パースする文字列</param>
        /// <returns>抽出された文字列の配列</returns>
        public string[] Parse(string str)
        {
            // 返す文字列を格納しておくためのリスト
            List<string> list = new List<string>();

            // 文字列を区切ってリストに格納していく
            for (int i = 0; i < str.Length - this.GramNum + 1; i++)
            {
                list.Add(str.Substring(i, this.GramNum));
            }

            // 文字列の配列を返す
            return list.ToArray();
        }
    }
}
