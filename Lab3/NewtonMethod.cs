using System;
using System.Linq;

namespace Lab3
{
    public static class NewtonMethod
    {
        private const int N = 1000;

        public static (decimal left, decimal right) LocalizeSolution(
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