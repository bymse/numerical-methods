using System;
using System.Linq;
using Lab2;

namespace Lab3
{
    public static class NewtonMethodForSystems
    {
        public static decimal[] SolveWithApproximationFinding(Function first, Function second, decimal e)
        {
            var solution = new decimal[2];
            foreach (var index in Enumerable.Range(0, 10))
            {
                solution = SolveInner(first, second, solution[0], solution[1], (decimal) index / 10);
            }

            return Solve(first, second, solution, e);
        }

        public static decimal[] Solve(
            Function first,
            Function second,
            decimal[] startArgs,
            decimal e,
            decimal k = 1)
        {
            var currentSolution = startArgs;
            decimal[] previousSolution;
            do
            {
                var coeffs = SolveInner(first, second, currentSolution[0], currentSolution[1], k);
                previousSolution = currentSolution;
                currentSolution = currentSolution.Sum(coeffs);
            } while (CalculateStopCondition(currentSolution, previousSolution) >= e);

            return currentSolution;
        }

        private static decimal CalculateStopCondition(decimal[] curSln, decimal[] prevSln)
        {
            return curSln.Select((e, i) => e - prevSln[i]).ToArray().Norm();
        }

        private static decimal[] SolveInner(
            Function first,
            Function second,
            decimal firstArg,
            decimal secondArg,
            decimal k)
        {
            var a = new[,]
            {
                {first.FirstArgDerivative(firstArg, secondArg, k), first.SecondArgDerivative(firstArg, secondArg, k)},
                {second.FirstArgDerivative(firstArg, secondArg, k), second.SecondArgDerivative(firstArg, secondArg, k)},
            };

            var b = new[]
            {
                -first.Func(firstArg, secondArg, k),
                -second.Func(firstArg, secondArg, k),
            };

            return GaussianElimination.Solve(a, b);
        }
    }
}