using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextInputValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            TextInputValidator validator = new TextInputValidator();
            validator.EmailValidate("xxx@dl.kuis.kyoto-u.ac.jp"); // true
            validator.EmailValidate("x@xx@dl.kuis.kyoto-u.ac.jp"); // false
            validator.EmailValidate("a@b"); // false
            validator.EmailValidate("a@hoge.com");  // true
            validator.EmailValidate("hoge@foo.bar."); // false
            validator.UrlValidate("http://www.dl.kuis.kyoto-u.ac.jp/"); // true
            validator.UrlValidate("www.dl.kuis.kyoto-u.ac.jp/"); // false
            validator.UrlValidate("http://"); // false
            validator.UrlValidate("http://www.dl://kuis.kyoto-u.ac.jp"); // false
            validator.UrlValidate("http://127.0.0.1/"); // true
            validator.TelValidate("075-753-5959"); // true
            validator.TelValidate("119"); // true
            validator.TelValidate("1-1-1-1"); // false
            validator.TelValidate("03-333-333"); // false
            validator.TelValidate("090-111-11111111"); // false
            validator.TelValidate("9999999999");  // false
        }
    }
}
