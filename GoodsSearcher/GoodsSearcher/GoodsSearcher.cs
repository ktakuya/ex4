using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;

namespace GoodsSearcher
{
    class GoodsSearcher
    {
        /// <summary>
        /// APPID
        /// </summary>
        private const string APP_ID = "1016035326264529117";
        private string requestUrl = "https://app.rakuten.co.jp/services/api/IchibaItem/Search/20140222?";

        private int MAX_PAGE = 100;
        private int MAX_HITS = 30;

        /// <summary>
        /// プロパティQuery
        /// </summary>
        private string _query;
        public string Query
        {
            set { _query = value; }
            get { return _query; }
        }

        public GoodsSearcher(string query)
        {
            Query = query;
        }

        /// <summary>
        /// リクエストURLを整形するメソッド   
        /// </summary>
        /// <param name="hits">1ページあたりの取得件数</param>
        /// <param name="page">取得ページ</param>
        /// <returns>整形されたURL</returns>
        private string RequestUrlBuilder(int hits, int page)
        {
            return string.Format("{0}format=xml&applicationId={1}&keyword={2}&hits={3}&page={4}",
                                                requestUrl,
                                                APP_ID,
                                                Query,
                                                hits,
                                                page);
        }
        
        /// <summary>
        /// hits,pageを指定してXMLを取得してきてそのまま返す
        /// </summary>
        /// <param name="hits">ヒット件数</param>
        /// <param name="page">ページ</param>
        /// <returns>XmlDocument</returns>
        private XmlDocument GetXmlDocument(int hits, int page)
        {
            XmlDocument xml = new XmlDocument();

            string url = RequestUrlBuilder(hits, page);

            // Httpによる要求を生成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // 要求を送信し、応答を受信
            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            using (Stream st = res.GetResponseStream())
            {
                // XMLを読み込み xmlに書き込まれる
                xml.Load(st);
            }

            return xml;
        }

        /// <summary>
        /// クエリで実際に検索を行うメソッド
        /// </summary>
        /// <param name="num">取得する検索結果数</param>
        /// <returns>キーが商品名で値が価格である辞書</returns>
        public Dictionary<string, int> Search(int num)
        {
            // 結果を格納する辞書
            Dictionary<string, int> result = new Dictionary<string, int>();

            // １ページあたりの最大取得数が３０, 最大ページ数が100
            if (num > MAX_PAGE * MAX_HITS) {
                throw new ArgumentException();
            }

            int maxpage = num / 30;
            int lasthits = num % 30;

            if (lasthits != 0) maxpage++;

            int count = 0;
            for (int i = 1; i <= maxpage; i++)
            {
                int hits = 30;
                int page = i;

                XmlDocument xmld = GetXmlDocument(hits, page);
                XmlNodeList items = xmld.GetElementsByTagName("Item");
                foreach (XmlNode item in items)
                {
                    XmlNode itemNameNode = item["itemName"];
                    XmlNode itemPriceNode = item["itemPrice"];
                    // resultに商品名と価格を追加する
                    result[itemNameNode.InnerText] = Convert.ToInt32(itemPriceNode.InnerText);
                    // num分取得できたら終了
                    if (count++ == num) break;
                }
            }

            return result;
        }
    }

}
