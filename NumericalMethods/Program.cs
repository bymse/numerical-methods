using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace NumericalMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            const decimal start = 0.2M;
            const decimal end = 0.3M;
            const decimal shift = 0.01M;

            for (var val = start; val <= end; val+=shift)
            {
                var res = Execute(val);
                Console.WriteLine(res);
            }
        }

        private static decimal Execute(decimal val)
        {
            var sqrt = ElementaryFunctions.Sqrt(0.9M * val + 1);
            var arctg = ElementaryFunctions.Arctg(sqrt / (1 - val * val));
            var sin = ElementaryFunctions.Sin(3 * val + 0.6M);
            return arctg + sin;
        }
    }
}