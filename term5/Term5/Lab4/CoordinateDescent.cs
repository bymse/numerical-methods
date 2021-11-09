using System;
using Utilities;

namespace Lab4
{
    public class CoordinateDescent : Descent
    {
        public CoordinateDescent(Matrix a, Matrix b, double constant, double minEig) : base(a, b, constant, minEig)
        {
        }

        public override void Minimize(double eps)
        {
            CheckDimensions();
            //Set start approximation with 0 vector
            var xCurrent = new Matrix(A.GetRowDimension(), 1);
            Matrix xPrev;
            var step = 0;
            Console.WriteLine("%8s %12s %12s %12s\n", "x", "y", "z", "f(x,y,z)");
            do
            {
                xPrev = xCurrent.Copy();
                xCurrent = GetNextX(xPrev, step);
                PrintResult(xCurrent);
                step = (step >= A.GetRowDimension() - 1) ? 0 : step + 1;
            } while (GetMinimizationError(xCurrent) > eps);
        }

        private Matrix GetNextX(Matrix xPrev, int step)
        {
            var ort = OrtI(step);
            var t1 = (ort.Transpose()).Times(A.Times(xPrev).Plus(B)).Get(0, 0);
            var t2 = (ort.Transpose()).Times(A.Times(ort)).Get(0, 0);
            var mu = -1 * t1 / t2;
            return xPrev.Plus(ort.Times(mu));
        }

        private Matrix OrtI(int i)
        {
            var qI = new Matrix(A.GetRowDimension(), 1);
            qI.Set(i, 0, 1);
            return qI;
        }
    }
}