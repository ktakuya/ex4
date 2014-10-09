using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "今日はいい天気です";
            int gramNum = 4;
            NGramParser parser = new NGramParser(gramNum);
            string[] result = parser.Parse(str);
            foreach (string s in result)
            {
                Console.WriteLine(s);
            }

        }
    }
}
