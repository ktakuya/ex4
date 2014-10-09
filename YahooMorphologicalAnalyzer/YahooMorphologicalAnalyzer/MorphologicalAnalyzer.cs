using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YahooMorphologicalAnalyzer
{
    /// <summary>
    /// 優 [yamamoto]
    /// 形態素解析を行うクラス
    /// </summary>
    class MorphologicalAnalyzer
    {
        private string AppId = "dj0zaiZpPUZNYm52Ym1hR3dqVSZzPWNvbnN1bWVyc2VjcmV0Jng9NjA-";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MorphologicalAnalyzer()
        {

        }

        /// <summary>
        /// Yahoo!日本語形態素解析APIにあわせたURLを生成する
        /// </summary>
        /// <param name="sentence">解析を行う文字列</param>
        /// <returns>生成されたURL</returns>
        private string UrlBuilder(string sentence)
        {
            return string.Format("http://jlp.yahooapis.jp/MAService/V1/parse?appid={0}&sentence={1}", this.AppId, sentence);
        }

        /// <summary>
        /// 指定された文字列の形態素解析を行うメソッド
        /// </summary>
        /// <param name="str">形態素解析を行う文字列</param>
        /// <returns>Morphemeクラスのリスト</returns>
        public Morpheme[] Analyse(string str)
        {
            // Morphemeインスタンスを格納しておくリスト
            List<Morpheme> list = new List<Morpheme>();

            // URLを作成
            string url = UrlBuilder(str);

            // 空のXMLを生成
            XmlDocument xml = new XmlDocument();

            // Httpによる要求を生成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // 要求を送信し、応答を受信
            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            using (Stream st = res.GetResponseStream())
            {
                // XMLを読み込み xmlに書き込まれる
                xml.Load(st);
            }

            // XML中の word 要素をすべて取得
            XmlNodeList words = xml.GetElementsByTagName("word");
            // wordsのすべての要素に対して繰り返し
            foreach (XmlNode word in words)
            {
                // word要素内のsurface要素を取得
                XmlNode surfaceNode = word["surface"];
                XmlNode posNode = word["pos"];

                // listにMorphemeインスタンスを追加していく
                list.Add(new Morpheme(surfaceNode.InnerText, posNode.InnerText));
            }

            return list.ToArray();
        }
    }
}
