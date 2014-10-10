using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private string _tags;
        private List<FlickrImage> _flickrImageList;
        private Dictionary<string, string> _ownerNameById;
        private Dictionary<string, List<string>> _tagsById;
        private Dictionary<string, string> _imageUrlById;

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
            
            return _flickrImageList;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetFlickrPhotos(){

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
        private void GetTags()
        {

        }

        /// <summary>
        /// flickr.photos.search 用のurlを生成するメソッド
        /// </summary>
        /// <returns>url</returns>
        private string PhotosSearchUrlBuilder() {
            return string.Format("https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&format=xmlrpc&tags={1}", 
                                                _apiKey,
                                                _tags);
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
