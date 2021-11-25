using System;
using System.IO;
using Utilities;

namespace Lab4
{
    class Program
    {
        public static void Main(string[] args)
        {
            var n = 11;
            var eps = 10E-6;
            var minEig = 3.55501;


            // using var writer = new StreamWriter("C:\\Users\\bymse\\Desktop\\Prog\\csharp\\numerical-methods\\out.csv"); 
            // Console.SetOut(writer);
            
            var a = new Matrix(new[]
            {
                new double[] {4, 1, 1},
                new[] {1, 2 * (3 + 0.1 * n), -1},
                new[] {1, -1, 2 * (4 + 0.1 * n)}
            });
            var b = new Matrix(new[]
            {
                new double[] {1},
                new double[] {-2},
                new double[] {3}
            });

            var gradientDescent = new GradientDescent(a, b, n, minEig);
            Console.WriteLine("GradientDescend;;;");
            gradientDescent.Minimize(eps);

            var coordinateDescent = new CoordinateDescent(a, b, n, minEig);
            Console.WriteLine("CoordinateDescent;;;");
            coordinateDescent.Minimize(eps);
        }
    }
}