using System;
using System.Collections.Generic;

namespace Lab2
{
    public class SeidelIterativeMethod
    {
        public static (decimal[] solution, int iterationsCount) Solve(decimal[,] a, decimal[] b, decimal accuracy)
        {
            decimal k;
            var iterationsCount = 0;
            var solution = new decimal[b.Length];
            var n = b.Length;
            
            k = 1e10M;
            while (k >= accuracy){
                iterationsCount++;
                for (var i = 0; i < n; i++) {
                    var sum = 0M;
                    for (var j = 0; j < n; j++) {
                        if (j!=i && iterationsCount > 1)
                            sum += a[i,j] * solution[j];
                    }
                    solution[i] = b[i]/a[i,i]-sum/a[i,i];
                }

                k = ComputeStopCondition(a, b, solution);
            }

            return (solution, iterationsCount);
        }

        private static decimal ComputeStopCondition(decimal[,] a, decimal[] b, decimal[] solution)
        {
            var n = b.Length;
            var vector = new decimal[n];
            for (var i = 0; i < n; i++)
            {
                vector[i] = 0.0M;
                for (var j = 0; j < n; j++){
                    vector[i] = vector[i] + a[i,j]*solution[j];
                }
            }
            for (var i = 0; i < n; i++){
                vector[i] = vector[i]-b[i];
            }

            var k = 0M;
            for (var i = 0;i < n;i++) {
                k += vector[i] * vector[i];
            } 
            return (decimal) Math.Sqrt((double) k);
        }
    }
}