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
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url =
    "http://www.atmarkit.co.jp/fdotnet/images/fdotnet_m.gif";
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url);
            Bitmap bitmap = new Bitmap(stream);
            stream.Close();

            pictureBox1.Image = bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SQLiteManager sqm = new SQLiteManager();
            docs = sqm.GetData();
        }
    }
}
