using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMorphologicalAnalyzer
{
    /// <summary>
    /// 解析された１つワードを表すクラス
    /// </summary>
    class Morpheme
    {
        private string Surface;
        private string Pos;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="surface">解析された文字</param>
        /// <param name="pos">その品詞</param>
        public Morpheme(string surface, string pos)
        {
            this.Surface = surface;
            this.Pos = pos;
        }

        /// <summary>
        /// 文字と品詞をあわせた文字列を返す
        /// </summary>
        /// <returns>整形された文字列</returns>
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Surface, this.Pos);
        }
    }
}
