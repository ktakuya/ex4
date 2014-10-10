using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextInputValidator
{
    /// <summary>
    /// いろいろなテキスト入力に対する検証を行えるクラス
    /// 
    /// 優 [tanaka]
    /// </summary>
    class TextInputValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextInputValidator() { }

        /// <summary>
        /// eメールの検証を行うメソッド
        /// </summary>
        /// <param name="str">検証を行うeメールアドレスの文字列</param>
        /// <returns>検証結果</returns>
        public bool EmailValidate(string str)
        {
            Regex emaiRegex = new Regex(@"(^[0-9a-zA-Z][-0-9a-zA-Z_\.]*)@([-0-9a-zA-Z_]+\.[-0-9a-zA-Z_\.]*[0-9a-zA-Z]$)");
            return emaiRegex.IsMatch(str);
        }

        /// <summary>
        /// URLの検証を行うメソッド
        /// </summary>
        /// <param name="str">検証を行うURL文字列</param>
        /// <returns>検証結果</returns>
        public bool UrlValidate(string str)
        {
            Regex urlRegex = new Regex(@"^http://([\w-]+\.)+[\w-]+/[\w-/]*");
            return urlRegex.IsMatch(str);
        }

        /// <summary>
        /// 電話番号の検証を行うメソッド
        /// </summary>
        /// <param name="str">検証を行う電話番号の文字列</param>
        /// <returns>検証結果</returns>
        public bool TelValidate(string str)
        {
            Regex telRegex = new Regex(@"(^\d{3}$)|(^0\d{1,4}-\d{1,4}-{4}$)");
            return telRegex.IsMatch(str);
        }
    }
}
