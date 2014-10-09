using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TFVectorGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "To be, or not to be::;\nto be";
            TFVectorGenerator generator = new TFVectorGenerator();
            Dictionary<string, int> result = generator.Generate(str);

            foreach (KeyValuePair<string, int> pair in result)
            {
                Console.WriteLine(string.Format("Key : {0} / Value : {1}", pair.Key, pair.Value));
            }
        }
    }
}
