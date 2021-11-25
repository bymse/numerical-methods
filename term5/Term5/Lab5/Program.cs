using System;

namespace Lab5
{
    class Program
    {
        static double eps = 10E-4;
        
        private const int N = 10;
        private const int M = 30;

        public static void Main(string[] args)
        {
            var left = eps;
            var right = Math.PI - eps;
            var equalPoly = Poly.GetEqualPoly(left, right, N, Func);
            var optimalPoly = Poly.GetOptimalPoly(left, right, N, Func);
            var x = Poly.GetEqualNodes(left, right, M);
            Printer.Print(Func, equalPoly, optimalPoly, x);
        }

        private static double Func(double t) => 1 / Math.Tan(t) - t;
    }
}