using System;
using NUnit.Framework;

namespace Lab2.Tests
{
    public class SimpleIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override decimal[] Calculate(decimal[,] a, decimal[] b)
        {
            return SimpleIterativeMethod.Solve(a, b, ACCURACY);
        }

        [Test]
        public override void Test0()
        {
            Assert.Throws<Exception>(() => base.Test0());
        }

        [Test]
        public override void Test4()
        {
            Assert.Throws<Exception>(() => base.Test4());
        }
    }
}