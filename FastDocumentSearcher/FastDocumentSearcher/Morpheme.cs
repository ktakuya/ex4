using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeCabMorphologicalAnalyzer
{
    /// <summary>
    /// 解析された１つワードを表すクラス
    /// </summary>
    class Morpheme
    {
        private string _surface;
        private string _pos;

        /// <summary>
        /// プロパティ
        /// </summary>
        public string Surface
        {
            get { return _surface; }
        }

        /// <summary>
        /// プロパティ
        /// </summary>
        public string Pos
        {
            get { return _pos; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="surface">解析された文字</param>
        /// <param name="pos">その品詞</param>
        public Morpheme(string surface, string pos)
        {
            this._surface = surface;
            this._pos = pos;
        }

        /// <summary>
        /// 文字と品詞をあわせた文字列を返す
        /// </summary>
        /// <returns>整形された文字列</returns>
        public override string ToString()
        {
            return string.Format("{0} ({1})", this._surface, this._pos);
        }
    }
}
