using System;

namespace Lab3
{
    public class Function
    {
        public Func<decimal, decimal, decimal, decimal> Func { get; init; }
        public Func<decimal, decimal, decimal, decimal> FirstArgDerivative { get; init; }
        public Func<decimal, decimal, decimal, decimal> SecondArgDerivative { get; init; }
    }
}