using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FavoInsta
{
    class InstagramApi
    {
        private string EndPoint = "https://api.instagram.com/v1/media/popular?client_id=";
        private string ClientId = "86d8cf8f366f4d40b4c96feae4c00857";
        public InstagramApi()
        {

        }

        public List<InstagramPhoto> GetPhotos()
        {
            List<InstagramPhoto> result = new List<InstagramPhoto>();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(EndPoint + ClientId);
            Encoding enc = Encoding.UTF8;
            string json;
            using (WebResponse res = (WebResponse)req.GetResponse())
            using (Stream st = res.GetResponseStream())
            {
                StreamReader reader = new StreamReader(st, enc);
                json = reader.ReadToEnd();
            }

            dynamic photos = JsonConvert.DeserializeObject(json);
            dynamic data = photos.data;
            foreach (var obj in data)
            {
                if (obj.type == "image")
                {
                    try
                    {
                        string url = obj.images.standard_resolution.url;
                        string text = obj.caption.text;
                        result.Add(new InstagramPhoto(url, text));
                    } catch(Exception e){
                        Console.WriteLine(e);
                    }
                    
                }
            }

            return result;
        }
    }

    class InstagramPhoto{
        private string _photoUrl, _text;

        public string PhotoUrl {
            get { return _photoUrl;}
        }

        public string Text {
            get { return _text;}
        }

        public InstagramPhoto(string url, string text){
            _photoUrl = url;
            _text = text;
        }
    }
}
