using System;

namespace Lab2
{
    public static class QrMethod
    {
        public static decimal[] Solve(decimal[,] a, decimal[] b)
        {
            var (q, r) = QrHouseholder(a);

            var x = new decimal[b.Length];
            var transposedQ = q.Transpose();
            var y = transposedQ.Multiply(b);

            for (var i = b.Length - 1; i >= 0; i--)
            {
                var sum = 0M;
                for (var j = b.Length - 1; j >= i; j--)
                    sum += r[i, j] * x[j];
                x[i] = (y[i] - sum) / r[i, i];
            }

            return x;
        }
        
        private static (decimal[,] Q, decimal[,] R) QrHouseholder(decimal[,] m)
        {
            var q = new decimal[m.GetLength(0)][,];
            decimal[,] z = m, z1;
            for (var k = 0; k < m.GetLength(0) && k < m.GetLength(0) - 1; k++)
            {
                z1 = matrix_minor(z, k);
                z = z1;

                var x =CopyColumn(z, k);
                var a = x.Norm();
                if (m[k, k] > 0)
                    a = -a;

                var e = new decimal[m.GetLength(0)];
                for (var i = 0; i < m.GetLength(0); i++)
                    e[i] = (i == k) ? 1 : 0;

                vmadd(x, e, a, e, m.GetLength(0));
                vdiv(e, e.Norm(), e, m.GetLength(0));
                q[k] = vmul(e, m.GetLength(0));
                z1 = q[k].Multiply(z);
                z = z1;
            }

            var resultQ = q[0];
            for (var i = 1; i < m.GetLength(0) && i < m.GetLength(0) - 1; i++)
            {
                z1 = q[i].Multiply(resultQ);
                resultQ = z1;
            }

            z = resultQ.Multiply(m);
            var resultR = z;
            resultQ = resultQ.Transpose();

            return (resultQ, resultR);
        }
        
        private static decimal[,] matrix_minor(decimal[,] x, int d)
        {
            var n = x.GetLength(0);
            var m = new decimal[n, n];
            for (var i = 0; i < d; i++)
                m[i, i] = 1;
            for (var i = d; i < n; i++)
            for (var j = d; j < n; j++)
                m[i, j] = x[i, j];
            return m;
        }

        //c = a + b * s
        private static void vmadd(decimal[] a, decimal[] b, decimal s, decimal[] c, int n)
        {
            for (var i = 0; i < n; i++)
                c[i] = a[i] + s * b[i];
        }

        /* m = I - v v^T */
        private static decimal[,] vmul(decimal[] v, int n)
        {
            var x = new decimal[n, n];
            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                x[i, j] = -2 * v[i] * v[j];
            for (var i = 0; i < n; i++)
                x[i, i] += 1;

            return x;
        }

        private static decimal[] vdiv(decimal[] x, decimal d, decimal[] y, int n)
        {
            for (var i = 0; i < n; i++) y[i] = x[i] / d;
            return y;
        }

        /* take c-th column of m, put in v */
        private static decimal[] CopyColumn(decimal[,] from, int column)
        {
            var to = new decimal[from.GetLength(0)]; 
            for (var i = 0; i < from.GetLength(0); i++)
                to[i] = from[i, column];
            return to;
        }
    }
}