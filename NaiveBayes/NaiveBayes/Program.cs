using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector;

namespace NaiveBayes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vector.Vector> positives = new List<Vector.Vector>();
            List<Vector.Vector> negatives = new List<Vector.Vector>();
            // Vectorの各要素 0:無料 1:出会い 2:登録 3:参加 4:講義
            positives.Add(new Vector.Vector(new double[5]{1.0, 0, 0, 1.0, 1.0}));
            negatives.Add(new Vector.Vector(new double[5]{2.0, 1.0, 1.0, 1.0, 0}));
            NaiveBayes naiveBayes = new NaiveBayes(positives, negatives);
            int result = naiveBayes.Classify(new Vector.Vector(new double[5]{1.0, 1.0, 1.0, 0, 0}));
            Console.WriteLine(result);
        }
    }
}
