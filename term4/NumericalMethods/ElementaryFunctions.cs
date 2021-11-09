using System;
using System.Linq;

namespace NumericalMethods
{
    public static class ElementaryFunctions
    {
        public static decimal EpsilonForSin = 0;
        public static decimal EpsilonForArctg = 0;
        public static decimal EpsilonForSqrt = 0;

        public static decimal Arctg(decimal val)
        {
            var k = 0;
            decimal sum = 0;
            while (true)
            {
                if (Math.Abs(val) < 1)
                {
                    var item = (decimal) Math.Pow(-1, k) *
                               (decimal) Math.Pow((double) val, 2 * k + 1)
                               / (2 * k + 1);
                    sum += item;

                    if (Math.Abs(item) < Math.Abs(EpsilonForArctg))
                    {
                        return sum;
                    }
                }
                else
                {
                    var item = (decimal) Math.Pow(-1, k) *
                               (decimal) Math.Pow((double) val, -(2 * k + 1))
                               / (2 * k + 1);

                    sum += item;
                    
                    if (Math.Abs(item) < Math.Abs(EpsilonForArctg))
                    {
                        return (decimal) Math.PI / 2 * Math.Sign(val)  - sum;
                    }
                    
                }

                k++;
            }
        }

        public static decimal Sin(decimal val)
        {
            var k = 0;
            decimal sum = 0;
            while (true)
            {
                var factorial = (decimal) Factorial(2 * k + 1);
                var item = (decimal) Math.Pow(-1, k) *
                           (decimal) Math.Pow((double) val, 2 * k + 1)
                           / factorial;
                sum += item;
                k++;

                if (Math.Abs(item) < Math.Abs(EpsilonForSin))
                {
                    return sum;
                }
            }
        }

        public static int Factorial(int val) => Enumerable.Range(1, val).Aggregate(1, (p, item) => p * item);

        public static decimal Sqrt(decimal n)
        {
            var x = n;
            decimal y = 1;

            while (x - y > EpsilonForSqrt)
            {
                x = (x + y) / 2;
                y = n / x;
            }

            return x;
        }
    }
}