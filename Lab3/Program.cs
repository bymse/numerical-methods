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

        static void Main(string[] args)
        {
            var (left, right) = NewtonMethod.LocalizeSolution(Func, 0, 10);
            Console.WriteLine(left);
            Console.WriteLine(right);
        }
    }
}