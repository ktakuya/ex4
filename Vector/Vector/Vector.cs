using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    /// <summary>
    /// 優 [tanaka]
    /// </summary>
    class Vector
    {
        private int _dimension;
        private double[] _elements;

        public int Dimension
        {
            get { return _dimension; }
        }

        public double[] Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vector">double配列で表現されたベクトル</param>
        public Vector(double[] vector)
        {
            _dimension = vector.Length;
            _elements = vector;
        }

        /// <summary>
        /// 0ベクトルインスタンスを生成する
        /// </summary>
        /// <param name="dimension">次元</param>
        public Vector(int dimension)
        {
            _dimension = dimension;
            _elements = new double[Dimension];
            for (int i = 0; i < Dimension; i++) Elements[i] = 0.0;
        }

        /// <summary>
        /// 指定された次元の要素地を返すメソッド
        /// </summary>
        /// <param name="dimension">次元</param>
        /// <returns>要素地</returns>
        public double GetValue(int dimension)
        {
            return Elements[dimension - 1];
        }

        /// <summary>
        /// ベクトルを"(x1, x2, ...)"の形式の文字列で返すメソッド
        /// </summary>
        /// <returns>"(x1, x2, ...)"の形式の文字列</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for (int i = 0; i < Dimension; i++)
            {
                sb.Append(Elements[i].ToString());
                // 最後の次元でなければ , をいれる
                if (i != Dimension - 1) sb.Append(", "); 
            }
            sb.Append(")");
            return sb.ToString();
        }

        /// <summary>
        /// 自身と指定されたベクトルの和を返すメソッド
        /// </summary>
        /// <param name="other">加えるベクトル（Vectorクラス）</param>
        /// <returns>計算結果</returns>
        public Vector Add(Vector other)
        {
            if (Dimension != other.Dimension)
            {
                throw new ArgumentException();
            }

            // 計算結果を格納する
            List<double> result = new List<double>(); 
            for (int i = 0; i < Dimension; i++)
            {
                result.Add(this.GetValue(i + 1) + other.GetValue(i + 1));
            }
            // 新しいVectorを生成して返す
            return new Vector(result.ToArray());
        }

        /// <summary>
        /// 自身と指定されたベクトルの差を返すメソッド
        /// </summary>
        /// <param name="other">引くベクトル（Vectorクラス）</param>
        /// <returns>計算結果</returns>
        public Vector Sub(Vector other)
        {
            if (Dimension != other.Dimension)
            {
                throw new ArgumentException();
            }

            // 計算結果を格納する
            List<double> result = new List<double>();
            for (int i = 0; i < Dimension; i++)
            {
                result.Add(this.GetValue(i + 1) - other.GetValue(i + 1));
            }
            // 新しいVectorを生成して返す
            return new Vector(result.ToArray());
        }

        /// <summary>
        /// 自身と指定されたベクトルの和を返すメソッド
        /// </summary>
        /// <param name="other">加えるベクトル（doubleの配列）</param>
        /// <returns>計算結果</returns>
        public Vector Add(double[] other)
        {
            if (Dimension != other.Length)
            {
                throw new ArgumentException();
            }

            // 計算結果を格納する
            List<double> result = new List<double>();
            for (int i = 0; i < Dimension; i++)
            {
                result.Add(this.GetValue(i + 1) + other[i]);
            }
            // 新しいVectorを生成して返す
            return new Vector(result.ToArray());
        }

        /// <summary>
        /// 自身と指定されたベクトルの差を返すメソッド
        /// </summary>
        /// <param name="other">引くベクトル（doubleの配列）</param>
        /// <returns>計算結果</returns>
        public Vector Sub(double[] other)
        {
            if (Dimension != other.Length)
            {
                throw new ArgumentException();
            }

            // 計算結果を格納する
            List<double> result = new List<double>();
            for (int i = 0; i < Dimension; i++)
            {
                result.Add(this.GetValue(i + 1) - other[i]);
            }
            // 新しいVectorを生成して返す
            return new Vector(result.ToArray());
        }

        /// <summary>
        /// 自身と指定されたスカラ値の積を返すメソッド
        /// </summary>
        /// <param name="d">掛けるスカラ値</param>
        /// <returns>計算結果</returns>
        public Vector ScalarMultiply(double d)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < Dimension; i++)
            {
                result.Add(d * GetValue(i + 1));
            }
            return new Vector(result.ToArray());
        }

        /// <summary>
        /// 自身と指定されたベクトルの内積を返すメソッド
        /// </summary>
        /// <param name="other">自身との内積を求めたいベクトル</param>
        /// <returns>内積</returns>
        public double InnerProduct(Vector other)
        {
            if (Dimension != other.Dimension)
            {
                throw new ArgumentException();
            }

            double result = 0.0;
            for (int i = 0; i < Dimension; i++)
            {
                result += this.GetValue(i + 1) * other.GetValue(i + 1);
            }
            return result;
        }

        /// <summary>
        /// ノルムを返すメソッド
        /// </summary>
        /// <returns>ノルム</returns>
        public double Norm()
        {
            double result = 0.0;
            for (int i = 0; i < Dimension; i++)
            {
                result += this.GetValue(i + 1) * this.GetValue(i + 1);
            }
            return Math.Sqrt(result);
        }


    }
}
