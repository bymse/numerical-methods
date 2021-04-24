using System;
using NUnit.Framework;

namespace Lab2.Tests
{
    public class SeidelIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int IterationsCount) Calculate(decimal[,] a, decimal[] b, decimal accuracy)
        {
            return SeidelIterativeMethod.Solve(a, b, accuracy);
       }

        [TestCase(0.01)]
        [TestCase(0.001)]
        public override void Test3(decimal accuracy)
        {
            Assert.Throws<OverflowException>(() => base.Test3(accuracy));
        }
    }
}