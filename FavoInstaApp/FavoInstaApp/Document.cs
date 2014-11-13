using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDocumentSearcher
{
    /// <summary>
    /// タイトルと本文を持つドキュメントクラス
    /// </summary>
    class Document
    {

        private string _title;
        private string _body;

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
        public string Body
        {
            get { return _body; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title">文書のタイトル</param>
        /// <param name="body">文書の本文</param>
        public Document(string title, string body)
        {
            _title = title;
            _body = body;
        }
    }
}
