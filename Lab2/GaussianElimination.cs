namespace Lab2
{
    public static class GaussianElimination
    {
        public static decimal[] Solve(decimal[,] a, decimal[] b)
        {
            var n = b.Length;
            var y = new decimal[n];
            var x = new decimal[n];

            var (l, u) = LuDecomposition(a);

            for (var i = 0; i < n; i++)
            {
                var sum = 0M;
                for (var j = 0; j < i; j++)
                    sum += l[i, j] * y[j];
                y[i] = (b[i] - sum) / l[i, i];
            }

            for (var i = n - 1; i >= 0; i--)
            {
                var sum = 0M;
                for (var j = n - 1; j >= i; j--)
                    sum += u[i, j] * x[j];
                x[i] = (y[i] - sum) / u[i, i];
            }

            return x;
        }

        private static (decimal[,] l, decimal[,] u) LuDecomposition(decimal[,] a)
        {
            var n = a.GetLength(0);
            var l = new decimal[n, n];
            var u = new decimal[n, n];

            int i, j, k;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (j < i)
                        l[j, i] = 0;
                    else
                    {
                        l[j, i] = a[j, i];
                        for (k = 0; k < i; k++)
                        {
                            l[j, i] = l[j, i] - l[j, k] * u[k, i];
                        }
                    }
                }

                for (j = 0; j < n; j++)
                {
                    if (j < i)
                        u[i, j] = 0;
                    else if (j == i)
                        u[i, j] = 1;
                    else
                    {
                        u[i, j] = a[i, j] / l[i, i];
                        for (k = 0; k < i; k++)
                        {
                            u[i, j] = u[i, j] - l[i, k] * u[k, j] / l[i, i];
                        }
                    }
                }
            }

            return (l, u);
        }
    }
}