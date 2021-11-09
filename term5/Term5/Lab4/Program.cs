using System;
using Utilities;

namespace Lab4
{
    class Program
    {
        public static void Main(String[] args)
        {
            var n = 15;
            var eps = 10E-6;
            var minEig = 3.618;


            var a = new Matrix(new double[][]
            {
                new double[] {4, 1, 1},
                new double[] {1, 2 * (3 + 0.1 * n), -1},
                new double[] {1, -1, 2 * (4 + 0.1 * n)}
            });
            var b = new Matrix(new double[][]
            {
                new double[] {1},
                new double[] {-2},
                new double[] {3}
            });

            var gradientDescent = new GradientDescent(a, b, n, minEig);
            Console.WriteLine("GradientDescend:");
            gradientDescent.Minimize(eps);

            var coordinateDescent = new CoordinateDescent(a, b, n, minEig);
            Console.WriteLine("CoordinateDescent");
            coordinateDescent.Minimize(eps);
        }
    }
}