using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDocumentSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Document> docs = new List<Document>();
            docs.Add(new Document("タイトル", "本文")); // 文書群作成
            DocumentSearcher ds = new DocumentSearcher(docs);
            string query = "京都";
            List<Document> rankedDoc = ds.Search(query);
        }
    }
}
