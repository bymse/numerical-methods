using System;
using System.Linq;

namespace NumericalMethods
{
    public static class ElementaryFunctions
    {
        private const int MAX = 10;

        public static decimal Arctg(decimal val)
        {
            if (Math.Abs(val) < 1)
            {
                return Enumerable.Range(0, MAX)
                    .Sum(k =>
                        (decimal) Math.Pow(-1, k) *
                        (decimal) Math.Pow((double) val, 2 * k + 1)
                        / (2 * k + 1));
            }

            return (decimal) Math.PI / 2 * Math.Sign(val)
                   - Enumerable.Range(0, MAX)
                       .Sum(k =>
                           (decimal) Math.Pow(-1, k) *
                           (decimal) Math.Pow((double) val, -(2 * k + 1))
                           / (2 * k + 1));
        }

        public static decimal Sin(decimal val)
        {
            return Enumerable.Range(0, MAX)
                .Sum(k =>
                {
                    var factorial = Factorial(2 * k + 1);
                    return (decimal) Math.Pow(-1, k) *
                           (decimal) Math.Pow((double) val, 2 * k + 1)
                           / factorial;
                });
        }

        public static int Factorial(int val) => Enumerable.Range(1, val).Aggregate(1, (p, item) => p * item);

        public static decimal Sqrt(decimal n) 
        {
            var x = n; 
            decimal y = 1; 
  
            const decimal e = 0.000001M; 
            while (x - y > e) { 
                x = (x + y) / 2; 
                y = n / x; 
            } 
            return x; 
        }
    }
}