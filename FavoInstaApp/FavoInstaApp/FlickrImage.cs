using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApi
{
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
        public string ImageUrl
        {
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
