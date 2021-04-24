using System;
using System.Linq;

namespace Lab2
{
    public class SimpleIterativeMethod
    {
        public static (decimal[] x, int iterationsCount) Solve(decimal[,] sourceA, decimal[] sourceB, decimal e)
        {
            e /= 100;
            var a = (decimal[,]) sourceA.Clone();
            var b = (decimal[]) sourceB.Clone();

            var n = b.Length;
            var x = new decimal[n];

            var temp = 2 / (a.Norm() - e);
            var bCoeffs = CalculateBCoeffs(a, temp);
            var bNorm = bCoeffs.Norm();
            if (bNorm >= 1)
            {
                (a, b) = MatrixHelpers.MultiplyOnTransposed(a, b);
                temp = 2 / (a.Norm() - e);
                bCoeffs = CalculateBCoeffs(a, temp);
                bNorm = bCoeffs.Norm();
            }

            var xPrevious = new decimal[n];
            var c = b.Select(r => temp * r).ToArray();
            c.CopyTo(xPrevious, 0);

            var iterationsCount = 0;
            while (iterationsCount < 100000)
            {
                iterationsCount++;
                var newX = bCoeffs.Multiply(xPrevious).Sum(c);

                for (var i = 0; i < x.Length; i++)
                {
                    xPrevious[i] = x[i];
                    x[i] = newX[i];
                }

                var stop = CalculateStopCondition(bNorm, x, xPrevious, sourceA, sourceB);
                if (stop < e)
                    return (x, iterationsCount);
            }

            throw new Exception();
        }

        private static decimal CalculateStopCondition(
            decimal bNorm,
            decimal[] x,
            decimal[] xPrevious,
            decimal[,] sourceA,
            decimal[] sourceB)
        {
            if (bNorm < 1)
            {
                var diff = x.Select((val, i) => val - xPrevious[i]).ToArray();
                return diff.Norm() * bNorm / (1 - bNorm);
            }

            return SeidelIterativeMethod.ComputeStopCondition(sourceA, sourceB, x);
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
    }
}