using System;
using Utilities;

namespace Lab4
{
    public abstract class Descent {
        protected Matrix A;
        protected Matrix B;
        protected double Constant;
        protected double MinEig; //минимальное собственное число Matrix 'a' для оценки погрешности

        public Descent(Matrix a, Matrix b, double constant, double minEig) {
            this.A = a;
            this.B = b;
            this.Constant = constant;
            this.MinEig = minEig;
        }

        public void SetA(Matrix a) {
            this.A = a;
        }

        public void SetB(Matrix b) {
            this.B = b;
        }

        public void SetConstant(double constant) {
            this.Constant = constant;
        }

        public double GetMinEig() {
            return MinEig;
        }

        public void SetMinEig(double minEig) {
            this.MinEig = minEig;
        }

        public virtual void Minimize(double eps) {
        }

        protected double Function(Matrix x) {
            return (x.Times(0.5).Transpose().Times(A).Times(x)).Plus((x.Transpose().Times(B))).Get(0, 0) + Constant;
        }

        protected double GetMinimizationError(Matrix xCurrent) {
            var m = 3.267; //TODO: заменить методом нахождения мин. собственного числа
            return (A.Times(xCurrent).Plus(B)).Norm2() * (1 / m);
        }

        protected void PrintResult(Matrix x) {
            var result = Function(x);
            var array = x.GetArray();
            //System.out.printf("%8.8f %12.8f %12.8f %12.8f\n",array[0][0], array[1][0], array[2][0], result);
            Console.WriteLine("{0} {1} {2} {3}\n",array[0][0], array[1][0], array[2][0], result);
        }

        protected void CheckDimensions() {
            if (A.GetColumnDimension() != A.GetRowDimension()) {
                throw new Exception("Matrix 'a' must be square");
            }
            if (B.GetColumnDimension() != 1) {
                throw new Exception("Matrix 'b' must be a Nx1 vector");
            }
            if (A.GetRowDimension() != B.GetRowDimension()) {
                throw new Exception("Incompatible matrices 'a' and 'b' dimensions. Expected NxN , Nx1");
            }
        }
    }
}