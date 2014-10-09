using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = "ゲーム"; //検索クエリ
            int num = 30; // 検索件数
            GoodsSearcher searcher = new GoodsSearcher(query);
            Dictionary<string, int> result = searcher.Search(num);
            
            // foreach (KeyValuePair<string, int> pair in result){
            //    Console.WriteLine(string.Format("{0} - {1}", pair.Key, pair.Value));
            // }
        }
    }
}
