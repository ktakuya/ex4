using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace FaceDetection
{
    class FaceDetection
    {
        public FaceDetection() { }

        public Bitmap Extract(Bitmap src) {
            Bitmap ret;
            const double Scale = 1.14;
            const double ScaleFactor = 1.0850;
            const int MinNeighbors = 2;
            using (IplImage img = BitmapConverter.ToIplImage(src))
            using (IplImage smallImg = new IplImage(new CvSize(Cv.Round(img.Width / Scale), Cv.Round(img.Height / Scale)), BitDepth.U8, 1))
            {
                // 顔検出用の画像の生成
                using (IplImage gray = new IplImage(img.Size, BitDepth.U8, 1))
                {
                    Cv.CvtColor(img, gray, ColorConversion.BgrToGray);
                    Cv.Resize(gray, smallImg, Interpolation.Linear);
                    Cv.EqualizeHist(smallImg, smallImg);
                }

                //using (CvHaarClassifierCascade cascade = Cv.Load<CvHaarClassifierCascade>(Const.XmlHaarcascade))  // どっちでも可
                using (CvHaarClassifierCascade cascade = CvHaarClassifierCascade.FromFile("haarcascade_frontalface_default.xml"))    //
                using (CvMemStorage storage = new CvMemStorage())
                {
                    storage.Clear();

                    // 顔の検出
                    CvSeq<CvAvgComp> faces = Cv.HaarDetectObjects(smallImg, cascade, storage, ScaleFactor, MinNeighbors, 0, new CvSize(30, 30));

                    // 検出した箇所にまるをつける
                    for (int i = 0; i < faces.Total; i++)
                    {
                        CvRect r = faces[i].Value.Rect;
                        CvPoint center = new CvPoint
                        {
                            X = Cv.Round((r.X + r.Width * 0.5) * Scale),
                            Y = Cv.Round((r.Y + r.Height * 0.5) * Scale)
                        };
                        int radius = Cv.Round((r.Width + r.Height) * 0.25 * Scale);
                        img.Circle(center, radius, new CvColor(0,0,255), 3, LineType.AntiAlias, 0);
                    }
                }
                CvWindow.ShowImages(img);
                ret = BitmapConverter.ToBitmap(img);
            }
            return ret;
        }
    }
}
