using System;
using Utilities;

namespace Lab4
{
    public abstract class Descent
    {
        protected Matrix A;
        protected Matrix B;
        private double constant;
        private double minEig; //минимальное собственное число Matrix 'a' для оценки погрешности

        protected Descent(Matrix a, Matrix b, double constant, double minEig)
        {
            A = a;
            B = b;
            this.constant = constant;
            this.minEig = minEig;
        }

        private double Function(Matrix x)
        {
            return (x.Times(0.5).Transpose().Times(A).Times(x)).Plus((x.Transpose().Times(B))).Get(0, 0) + constant;
        }

        protected double GetMinimizationError(Matrix xCurrent)
        {
            return (A.Times(xCurrent).Plus(B)).Norm2() * (1 / minEig);
        }

        protected void PrintResult(Matrix x)
        {
            var result = Function(x);
            var array = x.GetArray();
            Console.WriteLine($"{array[0][0]};{array[1][0]};{array[2][0]};{result}");
        }

        protected void CheckDimensions()
        {
            if (A.GetColumnDimension() != A.GetRowDimension())
            {
                throw new Exception("Matrix 'a' must be square");
            }

            if (B.GetColumnDimension() != 1)
            {
                throw new Exception("Matrix 'b' must be a Nx1 vector");
            }

            if (A.GetRowDimension() != B.GetRowDimension())
            {
                throw new Exception("Incompatible matrices 'a' and 'b' dimensions. Expected NxN , Nx1");
            }
        }
    }
}