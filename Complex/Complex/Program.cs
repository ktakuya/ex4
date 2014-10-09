using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex x = new Complex(1.0, 2.0);
            Complex y = new Complex("4.0+1.0i");
            Complex w = x.Add(y); // 5.0 + 3.0i
            Complex z = x.Multiply(y); // 2.0 + 9.0i
            Console.WriteLine(w.ToString()); // 5.0 + 3.0i
            Console.WriteLine(z.ToString());
        }
    }
}
