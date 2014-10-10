using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Complex
{
    /// <summary>
    /// 複素数をあらわすクラス
    /// 
    /// 優 [tanaka]
    /// </summary>
    class Complex
    {
        private double real;
        private double imaginary;
        private double z;
        private double arg;

        /// <summary>
        /// プロパティReal
        /// </summary>
        public double Real{
            get { return real; }
        }

        /// <summary>
        /// プロパティImaginary
        /// </summary>
        public double Imaginary
        {
            get { return imaginary; }
        }

        /// <summary>
        /// プロパティZ
        /// </summary>
        public double Z
        {
            get { return z; }
        }

        /// <summary>
        /// プロパティArg
        /// </summary>
        public double Arg
        {
            get { return arg; }
        }

        /// <summary>
        /// コンストラクタ　"a+bi"の形式
        /// </summary>
        /// <param name="str">"a+bi"といった文字列</param>
        public Complex(string str)
        {
            // 正規表現で実部と虚部を抽出する
            Match m = Regex.Match(str, "(-?[0-9.]+)([-+][0-9.]+)i");

            real = Convert.ToDouble(m.Groups[1].Value);
            imaginary = Convert.ToDouble(m.Groups[2].Value);
            z = Math.Sqrt(real * real + imaginary * imaginary);
            arg = Math.Atan2(real, imaginary);
        }

        /// <summary>
        /// 実部と虚部でインスタンスを生成する
        /// </summary>
        /// <param name="re">実部</param>
        /// <param name="im">虚部</param>
        public Complex(double re, double im)
        {
            real = re;
            imaginary = im;
            z = Math.Sqrt(real * real + imaginary * imaginary);
            arg = Math.Atan2(real, imaginary);
        }

        /// <summary>
        /// 絶対値と偏角でインスタンスを作成する
        /// </summary>
        /// <param name="z">絶対値</param>
        /// <param name="arg">偏角</param>
        public Complex(double _z, float _arg)
        {
            z = _z;
            arg = (double)_arg;
            real = z * Math.Cos(arg);
            imaginary = z * Math.Sin(arg);
        }

        /// <summary>
        /// 和を計算するメソッド
        /// </summary>
        /// <param name="other">足されるインスタンス</param>
        /// <returns>計算結果</returns>
        public Complex Add(Complex other)
        {
            return new Complex(real + other.Real, imaginary + other.Imaginary);
        }
        
        /// <summary>
        /// 差を計算するメソッド
        /// </summary>
        /// <param name="other">引くインスタンス</param>
        /// <returns>計算結果</returns>
        public Complex Subtract(Complex other)
        {
            return new Complex(real - other.Real, imaginary - other.Imaginary);
        }

        /// <summary>
        /// 実数を乗算するメソッド
        /// </summary>
        /// <param name="d">実数</param>
        /// <returns>計算結果</returns>
        public Complex Multiply(double d)
        {
            return new Complex(d * real, d * imaginary);
        }

        /// <summary>
        /// Complexインスタンス同士の積を計算するメソッド
        /// </summary>
        /// <param name="other">乗算されるインスタンス</param>
        /// <returns>計算結果のインスタンス</returns>
        public Complex Multiply(Complex other)
        {
            double re = real * other.Real - imaginary * other.Imaginary;
            double img = imaginary * other.Real + real * other.Imaginary;
            return new Complex(re, img);
        }

        /// <summary>
        /// 複製したインスタンスを返すメソッド
        /// </summary>
        /// <returns>Complexのインスタンス</returns>
        public Complex Clone()
        {
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// "a+bi"の形式の文字列を返すメソッド
        /// </summary>
        /// <returns>"a+bi"の形式の文字列</returns>
        public override string ToString()
        {
            string ret;
            
            // 虚部によって場合わけする
            if (imaginary > 0.0)
            {
                ret = string.Format("{0}+{1}i", real, imaginary);
            }
            else
            {
                ret = string.Format("{0}{1}i", real, imaginary);
            }

            return ret;
        }
    }
}
