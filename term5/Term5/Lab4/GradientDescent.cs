using System;
using Utilities;

namespace Lab4
{
    public class GradientDescent : Descent
    {
        public GradientDescent(Matrix a, Matrix b, double constant, double minEig) :
            base(a, b, constant, minEig)
        {
        }

        public virtual void Minimize(double eps)
        {
            CheckDimensions();
            //Set start approximation of minimum with 0 vector
            var xCurrent = new Matrix(A.GetRowDimension(), 1);
            Matrix xPrev;

            Console.WriteLine("x;y;z;f(z,y,z)");
            do
            {
                xPrev = xCurrent.Copy();
                xCurrent = GetNextX(xPrev);
                PrintResult(xCurrent);
            } while (GetMinimizationError(xCurrent) > eps);
        }

        private Matrix GetNextX(Matrix xPrev)
        {
            var q = A.Times(xPrev).Plus(B);
            var mu = -1 * q.Norm2() / (q.Transpose().Times(A).Times(q)).Get(0, 0);
            return xPrev.Plus(q.Times(mu));
        }
    }
}