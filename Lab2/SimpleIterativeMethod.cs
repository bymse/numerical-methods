using System;
using System.Linq;

namespace Lab2
{
    public class SimpleIterativeMethod
    {
        public static decimal[] Solve(decimal[,] a, decimal[] b, decimal e)
        {
            var n = b.Length;
            var x = new decimal[n];

            var temp = 2 / (a.Norm() + e);
            var bCoeffs = CalculateBCoeffs(a, temp);
            var bNorm = bCoeffs.Norm();
            if (bNorm >= 1)
            {
                (a, b) = Fix(a, b);
                temp = 2 / (a.Norm() + e);
                bCoeffs = CalculateBCoeffs(a, temp);
                bNorm = bCoeffs.Norm();
            }

            if (bNorm >= 1)
                throw new Exception();

            var target = e * (1 - bNorm) / bNorm;

            var xPrevious = new decimal[n];
            var c = b.Select(r => temp * r).ToArray();
            c.CopyTo(xPrevious, 0);

            while (true)
            {
                var newX = bCoeffs.Multiply(xPrevious).Sum(c);

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

        private static decimal[,] CalculateBCoeffs(decimal[,] a, decimal temp)
        {
            var n = a.GetLength(0);
            var bCoeffs = new decimal[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var product = temp * a[i, j];
                    bCoeffs[i, j] = i == j
                        ? 1 - product
                        : -product;
                }
            }

            return bCoeffs;
        }

        private static (decimal[,] a, decimal[] b) Fix(decimal[,] a, decimal[] b)
        {
            var transponed = new decimal[a.GetLength(0), a.GetLength(0)];
            for (var i = 0; i < a.GetLength(0); i++)
            {
                for (var j = 0; j < a.GetLength(0); j++)
                {
                    transponed[i, j] = a[j, i];
                }
            }

            var outputA = transponed.Multiply(a);
            var outputB = transponed.Multiply(b);

            return (outputA, outputB);
        }
    }
}