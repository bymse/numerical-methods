using System;
using System.Collections.Generic;

namespace Lab2
{
    public class SeidelIterativeMethod
    {
        public static decimal[] Solve(decimal[,] a, decimal[] b, decimal accuracy)
        {
            var iterationsCount = 0;
            var solution = new decimal[b.Length];
            var n = b.Length;

            var stopValue = 1e10M;
            while (stopValue >= accuracy)
            {
                iterationsCount++;
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

            return solution;
        }

        private static decimal ComputeStopCondition(decimal[,] a, decimal[] b, decimal[] solution)
        {
            var n = b.Length;
            var vector = new decimal[n];
            for (var i = 0; i < n; i++)
            {
                vector[i] = 0.0M;
                for (var j = 0; j < n; j++)
                {
                    vector[i] = vector[i] + a[i, j] * solution[j];
                }
            }

            for (var i = 0; i < n; i++)
            {
                vector[i] = vector[i] - b[i];
            }
            
            return vector.Norm();
        }
    }
}