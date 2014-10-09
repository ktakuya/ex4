using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector x = new Vector(new double[] { 1.0, 2.0 });
            Vector y = new Vector(new double[] { 3.0, 1.0 });
            Vector z = x.Add(y); // (4.0, 3.0)
            Vector a = new Vector(2);
            a = a.Add(x); // (1.0, 2.0)
            Vector b = a.ScalarMultiply(2.0); // (2.0, 4.0)

            // Console.WriteLine(z.ToString());
            // Console.WriteLine(a.ToString());
            // Console.WriteLine(b.ToString());
        }
    }
}
