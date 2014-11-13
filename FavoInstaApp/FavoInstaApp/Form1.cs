using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlickrApi;
using FastDocumentSearcher;
using FavoInsta;

namespace FavoInstaApp
{
    public partial class Form1 : Form
    {
        private List<PhotoUrl> docs;
        private System.Windows.Forms.PictureBox[] picForm;
        private List<Document> searchDocs;
        private FastDocumentSearcher.DocumentSearcher fds;
        private String[] textForm;

        public Form1()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            // picForm に PictureBoxのインスタンスをいれてインデックスで指定できるようにしておく
            picForm = new System.Windows.Forms.PictureBox[9] {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5,
                                                                                                     pictureBox6, pictureBox7, pictureBox8, pictureBox9};
            textForm = new String[9];

            // 画像のUrlとテキストを持つオブジェクトを取得する
            SQLiteManager sqm = new SQLiteManager();
            docs = sqm.GetData();

            searchDocs = new List<Document>();
            foreach (var obj in docs)
            {
                searchDocs.Add(new Document(obj.Url, obj.Text));
            }

            fds = new FastDocumentSearcher.DocumentSearcher(searchDocs);

            for (int i = 0; i < 9; i++)
            {
                picForm[i].Image = GetImgByUrl(docs[i].Url);
                textForm[i] = docs[i].Text;
            }
            
        }

        private Bitmap GetImgByUrl(string url)
        {
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url);
            Bitmap bitmap = new Bitmap(stream);
            stream.Close();
            bitmap = ResizeImage(bitmap, 111, 104);
            return bitmap;
        }

        private Bitmap ResizeImage(Bitmap image, double dw, double dh)
        {
            double hi;
            double imagew = image.Width;
            double imageh = image.Height;

            if ((dh / dw) <= (imageh / imagew))
            {
                hi = dh / imageh;
            }
            else
            {
                hi = dw / imagew;
            }
            int w = (int)(imagew * hi);
            int h = (int)(imageh * hi);

            Bitmap result = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, result.Width, result.Height);

            return result;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
