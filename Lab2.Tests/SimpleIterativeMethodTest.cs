using System;
using NUnit.Framework;

namespace Lab2.Tests
{
    public class SimpleIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case)
        {
            return SimpleIterativeMethod.Solve(@case.Coefficients, @case.RightPart, @case.Accuracy);
        }

        [TestCase(0.01)]
        [TestCase(0.001)]
        public override void Test0(decimal accuracy)
        {
            Assert.Throws<Exception>(() => base.Test0(accuracy));
        }

        [TestCase(0.01)]
        [TestCase(0.001)]
        public override void Test4(decimal accuracy)
        {
            Assert.Throws<Exception>(() => base.Test4(accuracy));
        }
    }
}