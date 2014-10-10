using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDocumentSearcher
{
    /// <summary>
    /// 文書群に対する高速な検索クラス
    /// </summary>
    class DocumentSearcher
    {
        /// <summary>
        /// コンストラクタ 
        /// </summary>
        /// <param name="docs">検索対象であるDocumentクラスからなる文書のリスト</param>
        public DocumentSearcher(List<Document> docs)
        {

        }

        /// <summary>
        /// 指定されたクエリによる検索結果を返すメソッド
        /// </summary>
        /// <param name="query">検索クエリ</param>
        /// <returns>関連性が高い順にランキングされたDocumentクラスのリスト</returns>
        public List<Document> Search(string query)
        {
            List<Document> documentList = new List<Document>();
            return documentList;
        }

    }


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

    /// <summary>
    /// ベクトル空間での類似度計算クラス
    /// </summary>
    class VectorSimilarity
    {
        private int[] _v1, _v2;

        public VectorSimilarity(int[] v1, int[] v2){
            _v1 = v1;
            _v2 = v2;
        }

        public double Cosine()
        {

        }
    }
}
