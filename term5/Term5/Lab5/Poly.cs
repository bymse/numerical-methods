using System;

namespace Lab5
{
    public static class Poly
    {
        public static Func<double, double> GetEqualPoly(double left, double right, int n, Func<double, double> f)
        {
            var equalNodes = GetEqualNodes(left, right, n);
            var equalNodesValues = GetFunctionValues(equalNodes, f);
            return InterpolationPoly.Build(equalNodes, equalNodesValues);   
        }

        public static Func<double, double> GetOptimalPoly(double left, double right, int n, Func<double, double> f)
        {
            var optimalNodes = GetOptimalNodes(left, right, n);
            var optimalNodesValues = GetFunctionValues(optimalNodes, f);
            return InterpolationPoly.Build(optimalNodes, optimalNodesValues);
        }
        
        private static double[] GetFunctionValues(double[] x, Func<double, double> f)
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
        
        public static double[] GetEqualNodes(double a, double b, int n)
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