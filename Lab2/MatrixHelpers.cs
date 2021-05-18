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
            return Sqrt(sum);
        }

        public static decimal Sqrt(decimal n)
        {
            var x = n;
            decimal y = 1;

            var error = n * 10e-8M;
            while (Math.Abs(x - y) > error)
            {
                x = (x + y) / 2;
                y = n / x;
            }

            return x;
        }

        public static decimal[,] Transpose(this decimal[,] matrix)
        {
            var transposed = new decimal[matrix.GetLength(0), matrix.GetLength(0)];
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(0); j++)
                {
                    transposed[i, j] = matrix[j, i];
                }
            }

            return transposed;
        }

        public static (decimal[,] a, decimal[] b) MultiplyOnTransposed(decimal[,] a, decimal[] b)
        {
            var transposed = a.Transpose();
            var outputA = transposed.Multiply(a);
            var outputB = transposed.Multiply(b);
            return (outputA, outputB);
        }

        public static bool IsDiagonallyDominant(this decimal[,] matrix)
        {
            var wasStrict = false;
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var sum = 0M;
                for (var j = 0; j < matrix.GetLength(0); j++)
                {
                    sum += Math.Abs(matrix[i, j]);
                }
            
                if (Math.Abs(matrix[i, i]) < sum)
                    return false;
            
                wasStrict = Math.Abs(matrix[i, i]) > sum;
            }
            
            return wasStrict;
        }
    }
}