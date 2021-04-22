using System;

namespace Lab2
{
    public static class QrMethod
    {
        static decimal[][] matrix_new(int m, int n)
        {
            var x = new decimal[m][];
            for (int i = 0; i < m; i++)
                x[i] = new decimal[n];
            return x;
        }


        static void matrix_transpose(decimal[][] m)
        {
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    decimal t = m[i][j];
                    m[i][j] = m[j][i];
                    m[j][i] = t;
                }
            }
        }

        static decimal[][] matrix_mul(decimal[][] x, decimal[][] y)
        {
            if (x.Length != y.Length)
                return default;
            decimal[][] r = matrix_new(x.Length, y.Length);
            for (int i = 0; i < x.Length; i++)
            for (int j = 0; j < y.Length; j++)
            for (int k = 0; k < x.Length; k++)
                r[i][j] += x[i][k] * y[k][j];
            return r;
        }

        static decimal[][] matrix_minor(decimal[][] x, int d)
        {
            decimal[][] m = matrix_new(x.Length, x.Length);
            for (int i = 0; i < d; i++)
                m[i][i] = 1;
            for (int i = d; i < x.Length; i++)
            for (int j = d; j < x.Length; j++)
                m[i][j] = x[i][j];
            return m;
        }

/* c = a + b * s */
        static decimal[] vmadd(decimal[] a, decimal[] b, decimal s, decimal[] c, int n)
        {
            for (int i = 0; i < n; i++)
                c[i] = a[i] + s * b[i];
            return c;
        }

/* m = I - v v^T */
        static decimal[][] vmul(decimal[] v, int n)
        {
            decimal[][] x = matrix_new(n, n);
            for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                x[i][j] = -2 * v[i] * v[j];
            for (int i = 0; i < n; i++)
                x[i][i] += 1;

            return x;
        }

        static decimal vnorm(decimal[] x, int n)
        {
            decimal sum = 0;
            for (int i = 0; i < n; i++) sum += x[i] * x[i];
            return (decimal) Math.Sqrt((double) sum);
        }

        static decimal[] vdiv(decimal[] x, decimal d, decimal[] y, int n)
        {
            for (int i = 0; i < n; i++) y[i] = x[i] / d;
            return y;
        }

/* take c-th column of m, put in v */
        static decimal[] mcol(decimal[][] m, decimal[] v, int c)
        {
            for (int i = 0; i < m.Length; i++)
                v[i] = m[i][c];
            return v;
        }


        public static void householder(decimal[][] m, ref decimal[][] R, ref decimal[][] Q)
        {
            var q = new decimal[m.Length][][];
            decimal[][] z = m, z1;
            for (int k = 0; k < m.Length && k < m.Length - 1; k++)
            {
                decimal[] e = new decimal[m.Length];
                decimal[] x = new decimal[m.Length];
                decimal a;
                z1 = matrix_minor(z, k);
                z = z1;

                mcol(z, x, k);
                a = vnorm(x, m.Length);
                if (m[k][k] > 0) a = -a;

                for (int i = 0; i < m.Length; i++)
                    e[i] = (i == k) ? 1 : 0;

                vmadd(x, e, a, e, m.Length);
                vdiv(e, vnorm(e, m.Length), e, m.Length);
                q[k] = vmul(e, m.Length);
                z1 = matrix_mul(q[k], z);
                z = z1;
            }

            Q = q[0];
            R = matrix_mul(q[0], m);
            for (int i = 1; i < m.Length && i < m.Length - 1; i++)
            {
                z1 = matrix_mul(q[i], Q);
                Q = z1;
            }

            z = matrix_mul(Q, m);
            R = z;
            matrix_transpose(Q);
        }

        public static decimal[] Solve(decimal[][] a, decimal[] b)
        {
            var q = new decimal[0][];
            var r = new decimal[0][];
            householder(a, ref r, ref q);
            
            var transposedQ = q;
            var x = new decimal[b.Length];
            matrix_transpose(transposedQ);
            decimal[] y = SimpleIterativeMethod.MultiplyMatrix(transposedQ, b);

            for (var i = b.Length - 1; i >= 0; i--)
            {
                var sum = 0M;
                for (var j = b.Length - 1; j >= i; j--)
                    sum += r[i][j] * x[j];
                x[i] = (y[i] - sum) / r[i][i];
            }

            return x;
        }
    }
}