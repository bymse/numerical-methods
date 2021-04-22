using System;
using System.Linq;

namespace Lab2
{
    public class SimpleIterativeMethod
    {
        public static decimal[] Solve(decimal[][] a, decimal[] b, decimal e)
        {
            var n = b.Length;
            var x = new decimal[n];
            
            var temp = 2 / (Norm(a) + e);
            var bCoeffs = CalculateBCoeffs(a, temp);
            var bNorm = Norm(bCoeffs);
            if (bNorm >= 1)
            {
                (a, b) = Fix(a, b);
                temp = 2 / (Norm(a) + e);
                bCoeffs = CalculateBCoeffs(a, temp);
                bNorm = Norm(bCoeffs);
            }

            if (bNorm >= 1)
                throw new Exception();
            
            var target = e * (1 - bNorm) / bNorm;
            
            var xPrevious = new decimal[n];
            var c = b.Select(r => temp * r).ToArray();
            c.CopyTo(xPrevious, 0);

            while (true)
            {
                var newX = Sum(MultiplyMatrix(bCoeffs, xPrevious), c);

                for (var i = 0; i < x.Length; i++)
                {
                    xPrevious[i] = x[i];
                    x[i] = newX[i];
                }

                var cur = x.Select((val, i) => Math.Abs(val - xPrevious[i])).Max();
                if (cur <= target)
                    return x;
            }
        }

        private static decimal[][] CalculateBCoeffs(decimal[][] a, decimal temp)
        {
            var n = a.Length;
            var bCoeffs = new decimal[n][];
            for (var i = 0; i < n; i++)
            {
                bCoeffs[i] = new decimal[n];
                for (var j = 0; j < n; j++)
                {
                    var product = temp * a[i][j];
                    bCoeffs[i][j] = i == j
                        ? 1 - product
                        : -product;
                }
            }

            return bCoeffs;
        }

        private static (decimal[][] a, decimal[] b) Fix(decimal[][] a, decimal[] b)
        {
            var transponed = new decimal[a.Length][];
            for (var i = 0; i < a.Length; i++)
            {
                transponed[i] = new decimal[a.Length];
                for (var j = 0; j < a.Length; j++)
                {
                    transponed[i][j] = a[j][i];
                }
            }

            var outputA = MultiplyMatrix(transponed, a);
            var outputB = MultiplyMatrix(transponed, b);

            return (outputA, outputB);
        }

        private static decimal Norm(decimal[][] arr)
        {
            var max = 0M;
            var r = arr.GetLength(0);
            for (var j = 0; j < r; j++)
            {
                var sum = 0M;
                for (var i = 0; i < r; i++)
                {
                    sum += Math.Abs(arr[i][j]);
                }

                max = Math.Max(max, sum);
            }

            return max;
        }

        public static decimal[][] MultiplyMatrix(decimal[][] first, decimal[][] second)
        {
            int rA = first.Length;
            int cA = first.Length;
            int cB = second.Length;
            decimal temp = 0;
            var kHasil = new decimal[rA][];
            for (int i = 0; i < rA; i++)
            {
                kHasil[i] = new decimal[cB];
                for (int j = 0; j < cB; j++)
                {
                    temp = 0;
                    for (int k = 0; k < cA; k++)
                    {
                        temp += first[i][k] * second[k][j];
                    }

                    kHasil[i][j] = temp;
                }
            }

            return kHasil;
        }

        public static decimal[] MultiplyMatrix(decimal[][] first, decimal[] second)
        {
            var rA = first.GetLength(0);
            var cA = first.GetLength(0);
            decimal temp;
            var kHasil = new decimal[rA];
            for (var i = 0; i < rA; i++)
            {
                temp = 0;
                for (var k = 0; k < cA; k++)
                {
                    temp += first[i][k] * second[k];
                }

                kHasil[i] = temp;
            }

            return kHasil;
        }

        private static decimal[] Sum(decimal[] first, decimal[] second)
        {
            return first.Select((e, i) => e + second[i]).ToArray();
        }
    }
}