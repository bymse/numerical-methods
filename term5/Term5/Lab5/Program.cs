using System;

namespace Lab5
{
    class Program
    {
        static double eps = 10E-4;

        public static void Main(String[] args)
        {
            var n = 10; // nodes number for Interp. poly
            var m = 30; // nodes number for values printing
            Func<Double, Double> f = x => 1 / Math.Tan(x) - x;
            var left = eps;
            var right = Math.PI - eps;

            var equalNodes = GetEqualNodes(left, right, n);
            var equalNodesValues = GetFunctionValues(equalNodes, f);
            var equalPoly = InterpolationPoly.Build(equalNodes, equalNodesValues);

            var optimalNodes = GetOptimalNodes(left, right, n);
            var optimalNodesValues = GetFunctionValues(optimalNodes, f);
            var optimalPoly = InterpolationPoly.Build(optimalNodes, optimalNodesValues);

            var x = GetEqualNodes(left, right, m);

            Console.WriteLine("%10s %12s %12s %12s %12s %12s\n",
                "x", "equalP", "optimalP", "f(x)", "equalDelta", "optimalDelta");
            foreach (var t in x)
            {
                var equalPolyVal = equalPoly(t);
                var optimalPolyVal = optimalPoly(t);
                var functionVal = f(t);
                var equalDifference = Math.Abs(functionVal - equalPolyVal);
                var optimalDifference = Math.Abs(functionVal - optimalPolyVal);
                //System.out.printf("%10.6f %12.6f %12.6f %12.6f %12.6f %12.6f\n", t, equalPolyVal,optimalPolyVal, functionVal, equalDifference, optimalDifference);
                Console.WriteLine("{0} {1} {2} {3} {4} {5}\n", t, equalPolyVal, optimalPolyVal, functionVal, equalDifference, optimalDifference);
            }
        }

        private static double[] GetFunctionValues(double[] x, Func<Double, Double> f)
        {
            var y = new double[x.Length];
            for (var i = 0; i < x.Length; i++)
            {
                y[i] = f(x[i]);
            }

            return y;
        }

        private static double[] GetOptimalNodes(double a, double b, int n)
        {
            var nodes = new double[n];
            for (var i = 0; i < nodes.Length; i++)
            {
                nodes[i] = 0.5 * ((b - a) * Math.Cos(Math.PI * (2 * i + 1.0) / (2 * (n + 1))) + b + a);
            }

            return nodes;
        }

        private static double[] GetEqualNodes(double a, double b, int n)
        {
            var nodes = new double[n];
            nodes[0] = a;
            var step = (b - a) / (n - 1);
            for (var i = 1; i < n; i++)
            {
                nodes[i] = nodes[i - 1] + step;
            }

            return nodes;
        }
    }
}