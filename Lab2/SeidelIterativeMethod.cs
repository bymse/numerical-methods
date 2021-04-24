using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    public class SeidelIterativeMethod
    {
        public static (decimal[] Solution, int IterationsCount) Solve(decimal[,] a, decimal[] b, decimal accuracy)
        {
            accuracy /= 1000;
            var iterationsCount = 0;
            var n = b.Length;

            if (!IsEqualToTransposed(a) || !a.IsDiagonallyDominant())
            {
                (a, b) = MatrixHelpers.MultiplyOnTransposed(a, b);
            }
            
            var solution = b.Select((e, i) => e / a[i, i]).ToArray();
            var stopValue = 1e10M;
            while (stopValue >= accuracy)
            {
                iterationsCount++;
                if (iterationsCount > 1000000)
                    break;
                
                for (var i = 0; i < n; i++)
                {
                    var sum = 0M;
                    for (var j = 0; j < n; j++)
                    {
                        if (j != i && iterationsCount > 1)
                            sum += a[i, j] * solution[j];
                    }

                    solution[i] = b[i] / a[i, i] - sum / a[i, i];
                }

                stopValue = ComputeStopCondition(a, b, solution);

            }

            return (solution, iterationsCount);
        }

        public static decimal ComputeStopCondition(decimal[,] a, decimal[] b, decimal[] solution)
        {
            var vector = a.Multiply(solution);
            for (var i = 0; i < b.Length; i++)
            {
                vector[i] = vector[i] - b[i];
            }

            return vector.Norm();
        }

        private static bool IsEqualToTransposed(decimal[,] a)
        {
            var transposed = a.Transpose();

            for (var i = 0; i < a.GetLength(0); i++)
            {
                for (var j = 0; j < a.GetLength(0); j++)
                {
                    if (a[i, j] != transposed[i, j])
                        return false;
                }
            }

            return true;
        }
    }
}