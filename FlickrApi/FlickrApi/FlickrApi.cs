using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlickrApi
{
    class FlickrApi
    {
        /*
         * タグから画像の検索は flickr.photos.search 
         * owneridから名前の検索は flickr.people.getInfo
         * 画像のurlは自分で整形
         * 画像に付与されているタグの検索は flickr.tags.getListPhoto
         */

        private string _apiKey;
        private List<FlickrImage> _flickrImageList;
        private Dictionary<string, string> _ownerNameById;
        private Dictionary<string, List<string>> _tagsById;
        private Dictionary<string, string> _imageUrlById;
        private Dictionary<string, string> _titleById;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">APIキー</param>
        public FlickrApi(string key)
        {
            _apiKey = key;
            _flickrImageList = new List<FlickrImage>();
            _ownerNameById = new Dictionary<string, string>();
            _tagsById = new Dictionary<string, List<string>>();
            _imageUrlById = new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public List<FlickrImage> Search(string tags)
        {
            // コンマ区切りの文字列を生成する
            string tag = SplitTags(tags);
            GetFlickrPhotos(tag);
            return _flickrImageList;
        }

        /// <summary>
        /// デリミタが空白できた場合apiに合うように結合するメソッド
        /// </summary>
        /// <param name="tags">"a b c"のような文字列</param>
        /// <returns>"a,b,c"のような文字列</returns>
        private string SplitTags(string tags)
        {
            string result = "";
            string[] split = tags.Split(new char[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries);
            result = String.Join(",", split);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetFlickrPhotos(string tag){
            string url = PhotosSearchUrlBuilder(tag);

            // urlからXmlを取得
            XmlDocument xml = GetXmlDocumentFromUrl(url);

            // photoタグをまわしてownerとtagsを取得してくる
            // Console.WriteLine(xml.GetElementsByTagName("photo")[0].Attributes[0].InnerText);
            XmlNodeList photos = xml.GetElementsByTagName("photo");
            foreach (XmlNode photo in photos)
            {
                // [0] : id, [1] : owner, [2] : secret, [3] : server, [4] : farm, [5] : title, 

            }
        }

        /// <summary>
        /// urlからxmlを取得してそのまま返すメソッド
        /// </summary>
        /// <param name="url">取得してくるurl</param>
        /// <returns>取得したxml</returns>
        private XmlDocument GetXmlDocumentFromUrl(string url)
        {
            XmlDocument xml = new XmlDocument();

            // Httpによる要求を生成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // 要求を送信し、応答を受信
            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            using (Stream st = res.GetResponseStream())
            {
                // XMLを読み込み xmlに書き込まれる
                xml.Load(st);
            }

            return xml;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetOwner()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private List<string> GetTags(string id)
        {
            List<string> result = new List<string>();

            return result;
        }

        /// <summary>
        /// flickr.photos.search 用のurlを生成するメソッド
        /// </summary>
        /// <returns>url</returns>
        private string PhotosSearchUrlBuilder(string tag) {
            return string.Format("https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&format=rest&tags={1}", 
                                                _apiKey,
                                                tag);
        }

        /// <summary>
        /// flickr.tags.getListPhoto 用のurlを生成するメソッド
        /// </summary>
        /// <param name="photoid">photo_id</param>
        /// <returns>url</returns>
        private string TagsGetListPhotoUrlBuilder(string photoid)
        {
            return string.Format("https://api.flickr.com/services/rest/?method=flickr.tags.getListPhoto&api_key={0}&photo_id={1}&format=rest", 
                                                _apiKey,
                                                photoid);
        }

    }

    /// <summary>
    /// Flickrの画像情報
    /// </summary>
    class FlickrImage
    {
        private string _imageUrl;
        private string _ownerName;
        private string _title;
        private List<string> _tags;

        /// <summary>
        /// プロパティ
        /// </summary>
        public string ImageUrl{
            get { return _imageUrl; }
        }

        /// <summary>
        /// プロパティ
        /// </summary>
        public string OwnerName
        {
            get { return _ownerName; }
        }

        /// <summary>
        /// プロパティ
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// プロパティ
        /// </summary>
        public List<string> Tags
        {
            get { return _tags; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="imageUrl">画像のURL</param>
        /// <param name="ownerName">投稿者</param>
        /// <param name="title">画像のタイトル</param>
        /// <param name="tags">画像に付与されているタグ</param>
        public FlickrImage(string imageUrl, string ownerName, string title, List<string> tags)
        {
            _imageUrl = imageUrl;
            _ownerName = ownerName;
            _title = title;
            _tags = tags;
        }
    }
}
