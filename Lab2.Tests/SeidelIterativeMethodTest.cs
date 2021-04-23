using System;
using NUnit.Framework;

namespace Lab2.Tests
{
    public class SeidelIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override decimal[] Calculate(decimal[,] a, decimal[] b)
        {
            return SeidelIterativeMethod.Solve(a, b, ACCURACY);
       }

        [Test]
        public override void Test3()
        {
            Assert.Throws<OverflowException>(() => base.Test3());
        }
    }
}