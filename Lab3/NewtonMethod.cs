using System;
using System.Linq;

namespace Lab3
{
    public static class NewtonMethod
    {

        public static decimal Solve(
            Func<decimal, decimal> func,
            Func<decimal, decimal> derivative,
            (decimal left, decimal right) startBoundaries,
            decimal precision
            )
        {
            var boundaries = LocalizeSolution(func, startBoundaries.left, startBoundaries.right);

            decimal prevSolution;
            var currentSolution = boundaries.left;
            do
            {
                prevSolution = currentSolution;
                var funcVal = func(currentSolution);
                var derivativeVal = derivative(currentSolution);
                
                if(funcVal != 0 && derivativeVal != 0)
                {
                    currentSolution = currentSolution - funcVal / derivativeVal;
                }

                var boundariesHalf = (boundaries.left + boundaries.right) / 2;
                if (currentSolution < boundaries.left || currentSolution > boundaries.right)
                {
                    currentSolution = boundariesHalf;
                }

                if (currentSolution < boundariesHalf)
                {
                    boundaries.left = currentSolution;
                }
                else
                {
                    boundaries.right = currentSolution;
                }
            } while (Math.Abs(currentSolution - prevSolution) > precision);

            return currentSolution;
        }

        private const int N = 1000;

        private static (decimal left, decimal right) LocalizeSolution(
            Func<decimal, decimal> func,
            decimal baseLeft,
            decimal baseRight)
        {
            var h = (baseRight - baseLeft) / N;
            var points = Enumerable.Range(0, N)
                .Select(k => baseLeft + k * h)
                .ToArray();

            for (var index = 0; index < points.Length - 1; index++)
            {
                var left = points[index];
                var right = points[index + 1];
                if (func(right) * func(left) < 0)
                    return (left, right);
            }

            throw new Exception();
        }
    }
}