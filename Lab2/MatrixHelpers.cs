using System;
using System.Linq;

namespace Lab2
{
    public static class MatrixHelpers
    {
        public static decimal Norm(this decimal[,] arr)
        {
            var max = 0M;
            var r = arr.GetLength(0);
            for (var j = 0; j < r; j++)
            {
                var sum = 0M;
                for (var i = 0; i < r; i++)
                {
                    sum += Math.Abs(arr[i, j]);
                }

                max = Math.Max(max, sum);
            }

            return max;
        }


        public static decimal[] Multiply(this decimal[,] first, decimal[] second)
        {
            var n = first.GetLength(0);
            var res = new decimal[n];
            for (var i = 0; i < n; i++)
            {
                var temp = 0M;
                for (var k = 0; k < n; k++)
                {
                    temp += first[i, k] * second[k];
                }

                res[i] = temp;
            }

            return res;
        }

        public static decimal[,] Multiply(this decimal[,] first, decimal[,] second)
        {
            var n = first.GetLength(0);
            var res = new decimal[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var temp = 0M;
                    for (var k = 0; k < n; k++)
                    {
                        temp += first[i, k] * second[k, j];
                    }

                    res[i, j] = temp;
                }
            }

            return res;
        }

        public static decimal[] Sum(this decimal[] first, decimal[] second)
        {
            return first.Zip(second, (arg1, arg2) => arg1 + arg2).ToArray();
        }

        public static decimal Norm(this decimal[] vector)
        {
            var sum = vector.Sum(t => t * t);
            return (decimal) Math.Sqrt((double) sum);
        }

        public static decimal[,] Transpose(this decimal[,] matrix)
        {
            var transponed = new decimal[matrix.GetLength(0), matrix.GetLength(0)];
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(0); j++)
                {
                    transponed[i, j] = matrix[j, i];
                }
            }

            return transponed;
        }
    }
}