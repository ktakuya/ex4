using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;

namespace FastDocumentSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Document> docs = new List<Document>();
            docs.Add(new Document("タイトル", "ソート済みのハッシュテーブルの利用")); // 文書群作成
            docs.Add(new Document("title2", "すべてのソートを利用"));
            DocumentSearcher ds = new DocumentSearcher(docs);
            string query = "京都";
            List<Document> rankedDoc = ds.Search(query);
            Test t = new Test();
        }

        class Test { 
            public Test()
            {
                List<Document> documents= new List<Document>();

                //XmlDocument xml = GetXmlDocumentFromUrl("http://www.dl.kuis.kyoto-u.ac.jp/~tyamamot/files/select10000.xml");
                XmlDocument xml = new XmlDocument();
                xml.Load("select10000.xml");
                XmlNodeList docs = xml.GetElementsByTagName("doc");

                foreach (XmlNode doc in docs)
                {
                    IEnumerator ienum = doc.GetEnumerator();
                    XmlNode item;
                    string body = "", title = "";
                    while (ienum.MoveNext())
                    {
                        item = (XmlNode)ienum.Current;
                        if (item.Attributes.GetNamedItem("name").Value == "body")
                        {
                            body = item.OuterXml;
                        }
                        if (item.Attributes.GetNamedItem("name").Value == "title")
                        {
                            title = item.OuterXml;
                        }
                    }
                    documents.Add(new Document(title, body));
                }

                DocumentSearcher ds = new DocumentSearcher(documents);

            }

            private XmlDocument GetXmlDocumentFromUrl(string url)
            {
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
                return xml;
            }
        }
    }
}
