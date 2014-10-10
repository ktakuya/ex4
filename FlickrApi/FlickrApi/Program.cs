using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "2b4f6b0fb919de79b3e9595a909d4845";
            string tags = "東京";
            FlickrApi flickr = new FlickrApi(key);
            List<FlickrImage> result = flickr.Search(tags);
            /*
            foreach (FlickrImage fl in result)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine(fl.ImageUrl);
                Console.WriteLine(fl.OwnerName);
                Console.WriteLine(fl.Title);
            }
            */
        }
    }
}
