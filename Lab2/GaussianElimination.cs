using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    public static class GaussianElimination
    {
        public static void LuDecomposition(List<List<decimal>> a, List<List<decimal>> l,
            List<List<decimal>> u, int n)
        {
            int i, j, k;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (j < i)
                        l[j][i] = 0;
                    else
                    {
                        l[j][i] = a[j][i];
                        for (k = 0; k < i; k++)
                        {
                            l[j][i] = l[j][i] - l[j][k] * u[k][i];
                        }
                    }
                }

                for (j = 0; j < n; j++)
                {
                    if (j < i)
                        u[i][j] = 0;
                    else if (j == i)
                        u[i][j] = 1;
                    else
                    {
                        u[i][j] = a[i][j] / (l[i][i]);
                        for (k = 0; k < i; k++)
                        {
                            u[i][j] = u[i][j] - ((l[i][k] * u[k][j]) / (l[i][i]));
                        }
                    }
                }
            }
        }

        public static decimal[] Solve(List<List<decimal>> a, List<decimal> b)
        {
            int i;
            decimal sum;
            var y = new decimal[b.Count];
            var x = new decimal[b.Count];
            var n = b.Count;

            var l = Enumerable.Range(0, b.Count).Select(_ => new List<decimal>()).ToList();
            var u = Enumerable.Range(0, b.Count).Select(_ => new List<decimal>()).ToList();
            
            for (var i1 = 0; i1 < l.Count; i1++)
            {
                l[i1] = Enumerable.Range(0, b.Count).Select(_ => 0M).ToList();
                u[i1] = Enumerable.Range(0, b.Count).Select(_ => 0M).ToList();
            }

            LuDecomposition(a, l, u, b.Count);

            for (i = 0; i < n; i++)
            {
                sum = 0;
                for (var j = 0; j < i; j++)
                    sum += l[i][j] * y[j];
                y[i] = (b[i] - sum) / l[i][i];
            }

            for (i = n - 1; i >= 0; i--)
            {
                sum = 0;
                for (var j = n - 1; j >= i; j--)
                    sum += u[i][j] * x[j];
                x[i] = (y[i] - sum) / u[i][i];
            }

            return x;
        }
    }
}