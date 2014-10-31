using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FaceDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap src = new Bitmap("face1.jpg");
            FaceDetection extract = new FaceDetection();
            Bitmap dst = extract.Extract(src);
            dst.Save("output.jpg");
        }
    }
}
