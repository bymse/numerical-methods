using System;

namespace Utilities
{
    public class Matrix
    {
        private readonly double[][] array;
        private readonly int rows;
        private readonly int columns;

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            array = new double[rows][];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = new double[columns];
            }
        }

        public Matrix(double[][] array)
        {
            rows = array.Length;
            columns = array[0].Length;
            for (var i = 0; i < rows; i++)
            {
                if (array[i].Length != columns)
                {
                    throw new Exception("All rows must have the same length.");
                }
            }

            this.array = array;
        }

        public Matrix(double[][] array, int rows, int columns)
        {
            this.array = array;
            this.rows = rows;
            this.columns = columns;
        }

        public Matrix Copy()
        {
            var x = new Matrix(rows, columns);
            var c = x.GetArray();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    c[i][j] = array[i][j];
                }
            }

            return x;
        }

        public double[][] GetArray()
        {
            return array;
        }

        public double[][] GetArrayCopy()
        {
            var c = new double[rows][];
            for (var i = 0; i < rows; i++)
            {
                c[i] = new double[columns];
                for (var j = 0; j < columns; j++)
                {
                    c[i][j] = array[i][j];
                }
            }

            return c;
        }

        public int GetRowDimension()
        {
            return rows;
        }

        public int GetColumnDimension()
        {
            return columns;
        }

        public double Get(int i, int j)
        {
            return array[i][j];
        }

        public Matrix GetMatrix(int[] r, int j0, int j1)
        {
            var x = new Matrix(r.Length, j1 - j0 + 1);
            var b = x.GetArray();
            try
            {
                for (var i = 0; i < r.Length; i++)
                {
                    for (var j = j0; j <= j1; j++)
                    {
                        b[i][j - j0] = array[r[i]][j];
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                throw new IndexOutOfRangeException("Submatrix indices");
            }

            return x;
        }

        public Matrix GetMatrix(int i0, int i1, int j0, int j1)
        {
            var x = new Matrix(i1 - i0 + 1, j1 - j0 + 1);
            var b = x.GetArray();
            try
            {
                for (var i = i0; i <= i1; i++)
                {
                    for (var j = j0; j <= j1; j++)
                    {
                        b[i - i0][j - j0] = array[i][j];
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                throw new IndexOutOfRangeException("Submatrix indices");
            }

            return x;
        }


        public void Set(int i, int j, double s)
        {
            array[i][j] = s;
        }

        public Matrix Transpose()
        {
            var x = new Matrix(columns, rows);
            var c = x.GetArray();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    c[j][i] = array[i][j];
                }
            }

            return x;
        }


        public double NormInf()
        {
            double f = 0;
            for (var i = 0; i < rows; i++)
            {
                double s = 0;
                for (var j = 0; j < columns; j++)
                {
                    s += Math.Abs(array[i][j]);
                }

                f = Math.Max(f, s);
            }

            return f;
        }

        public double Norm2()
        {
            double norm = 0;
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    norm += array[i][j] * array[i][j];
                }
            }

            return norm;
        }

        public Matrix Plus(Matrix b)
        {
            CheckMatrixDimensions(b);
            var x = new Matrix(rows, columns);
            var c = x.GetArray();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    c[i][j] = array[i][j] + b.array[i][j];
                }
            }

            return x;
        }

        public Matrix Minus(Matrix b)
        {
            CheckMatrixDimensions(b);
            var x = new Matrix(rows, columns);
            var c = x.GetArray();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    c[i][j] = array[i][j] - b.array[i][j];
                }
            }

            return x;
        }

        public Matrix Times(double s)
        {
            var x = new Matrix(rows, columns);
            var c = x.GetArray();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    c[i][j] = s * array[i][j];
                }
            }

            return x;
        }

        public Matrix Times(Matrix b)
        {
            if (b.rows != columns)
            {
                throw new Exception("Matrix.Matrix inner dimensions must agree.");
            }

            var x = new Matrix(rows, b.columns);
            var c = x.GetArray();
            var bcolj = new double[columns];
            for (var j = 0; j < b.columns; j++)
            {
                for (var k = 0; k < columns; k++)
                {
                    bcolj[k] = b.array[k][j];
                }

                for (var i = 0; i < rows; i++)
                {
                    var arowi = array[i];
                    double s = 0;
                    for (var k = 0; k < columns; k++)
                    {
                        s += arowi[k] * bcolj[k];
                    }

                    c[i][j] = s;
                }
            }

            return x;
        }

        public static Matrix Identity(int m, int n)
        {
            var a = new Matrix(m, n);
            var x = a.GetArray();
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    x[i][j] = (i == j ? 1.0 : 0.0);
                }
            }

            return a;
        }

        private void CheckMatrixDimensions(Matrix b)
        {
            if (b.rows != rows || b.columns != columns)
            {
                throw new Exception("Matrix.Matrix dimensions must agree.");
            }
        }
    }
}