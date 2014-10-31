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
    /// <summary>
    /// Flickr API を利用した写真検索クラス
    /// 優 [yamamoto]
    /// </summary>
    class FlickrApi
    {
        /*
         * タグから画像の検索は flickr.photos.search 
         * owneridから名前の検索は flickr.people.getInfo
         * 画像のurlは自分で整形
         * 画像に付与されているタグの検索は flickr.tags.getListPhoto
         */

        private string _apiKey;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">APIキー</param>
        public FlickrApi(string key)
        {
            _apiKey = key;
        }

        /// <summary>
        /// 検索する
        /// </summary>
        /// <param name="tags">スペース区切りのタグ</param>
        /// <returns>FlickrImageのリスト</returns>
        public List<FlickrImage> Search(string tags)
        {
            // コンマ区切りの文字列を生成する
            // FIXME: 今のところ空白区切りでタグが指定されるのを前提にしている
            string tag = SplitTags(tags);
            List<FlickrImage> result = GetFlickrPhotos(tag);
            return result;
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
       /// FlickrImageのリストを生成する
       /// またここからタグ、ユーザ名、タイトル、画像URLを生成するメソッドを呼び出す
       /// </summary>
       /// <param name="tag">tag</param>
       /// <returns>FlickrImageのリスト</returns>
        private List<FlickrImage> GetFlickrPhotos(string tag){
            // 返すListを生成する
            List<FlickrImage> flickrImageList = new List<FlickrImage>();

            string url = PhotosSearchUrlBuilder(tag);

            // urlからXmlを取得
            XmlDocument xml = GetXmlDocumentFromUrl(url);

            // photoタグをまわしてownerとtagsを取得してくる
            XmlNodeList photos = xml.GetElementsByTagName("photo");
            foreach (XmlNode photo in photos)
            {
                // attributeからidとownerを取り出してきてFlickrImageインスタンスを生成する
                XmlAttributeCollection xac = photo.Attributes;
                string photoId = xac.GetNamedItem("id").Value;
                string ownerId = xac.GetNamedItem("owner").Value;
                string farmId = xac.GetNamedItem("farm").Value;
                string serverId = xac.GetNamedItem("server").Value;
                string title = xac.GetNamedItem("title").Value;
                string secret = xac.GetNamedItem("secret").Value;

                // photo_id から タグを取得する
                List<string> tags = GetTags(photoId);
                
                // useridからusernameを取り出す
                string userName = GetOwner(ownerId);

                // 画像のURLを生成する
                string imageUrl = PhotoSourceUrlBuilder(farmId, serverId, photoId, secret);

                flickrImageList.Add(new FlickrImage(imageUrl, userName, title, tags));
            }

            return flickrImageList;
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
        /// useridからuserについての詳細を取得してくるメソッド
        /// </summary>
        /// <param name="userid">user_id</param>
        /// <returns>username</returns>
        private string GetOwner(string userid)
        {
            // urlを生成してxmlを取得
            string url = PeopleGetInfoUrlBuilder(userid);
            XmlDocument xml = GetXmlDocumentFromUrl(url);
            
            // usernameタグの中身をとりだして返す
            return xml.GetElementsByTagName("username")[0].InnerText;
        }

        /// <summary>
        /// photo_idを指定してその画像に付随するタグをリストにして返すメソッド
        /// </summary>
        /// <param name="photoid">photo_id</param>
        /// <returns>タグをstringにしたリスト</returns>
        private List<string> GetTags(string photoid)
        {
            List<string> result = new List<string>();

            string url = TagsGetListPhotoUrlBuilder(photoid);
            XmlDocument xml = GetXmlDocumentFromUrl(url);
            XmlNodeList tagsList = xml.GetElementsByTagName("tag");

            // foreachでtag内のraw属性をListに突っ込む
            foreach (XmlNode tag in tagsList)
            {
                // raw の属性値を取得
                XmlAttributeCollection xac = tag.Attributes;
                result.Add(xac.GetNamedItem("raw").Value);
            }
            return result;
        }

        /// <summary>
        /// Photo Source URLを生成するメソッド
        /// </summary>
        /// <param name="farmId">farm-id</param>
        /// <param name="serverId">server-id</param>
        /// <param name="id">id</param>
        /// <param name="secret">secret</param>
        /// <returns>url</returns>
        private string PhotoSourceUrlBuilder(string farmId, string serverId, string id, string secret)
        {
            return string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg",
                                                farmId,
                                                serverId,
                                                id,
                                                secret);
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

        /// <summary>
        /// flickr.people.getinfo 用のurlを生成するメソッド
        /// </summary>
        /// <param name="userid">userid</param>
        /// <returns>url</returns>
        private string PeopleGetInfoUrlBuilder(string userid)
        {
            return string.Format("https://api.flickr.com/services/rest/?method=flickr.people.getInfo&api_key={0}&user_id={1}&format=rest",
                                                _apiKey,
                                                userid);
        }
    }

}
