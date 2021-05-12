using System;

namespace Lab3
{
    class Program
    {
        private static decimal Func(decimal x)
        {
            var ln = (decimal) Math.Log((double) (x + 1));
            return x * ln - 0.3M;
        }
        
        private static decimal Derivative(decimal x)
        {
            var ln = (decimal) Math.Log((double) (x + 1));
            return x / (x + 1) + ln;
        }

        static void Main(string[] args)
        {
            var solution = NewtonMethod.Solve(
                Func,
                Derivative,
                (0, 10),
                0.0001M);
            Console.WriteLine(solution);
        }
    }
}