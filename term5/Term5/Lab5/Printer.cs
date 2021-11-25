using System;

namespace Lab5
{
    public static class Printer
    {
        public static void Print(
            Func<double, double> f, 
            Func<double, double> equalPoly, 
            Func<double, double> optimalPoly,
            double[] x
            )
        {
            Console.WriteLine("x equalP optimalP f(x) equalDelta optimalDelta");
            foreach (var t in x)
            {
                var equalPolyVal = equalPoly(t);
                var optimalPolyVal = optimalPoly(t);
                var functionVal = f(t);
                var equalDifference = Math.Abs(functionVal - equalPolyVal);
                var optimalDifference = Math.Abs(functionVal - optimalPolyVal);
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", t, equalPolyVal, optimalPolyVal, functionVal, equalDifference, optimalDifference);
            }
        }
    }
}