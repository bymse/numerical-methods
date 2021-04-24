using System;
using NUnit.Framework;

namespace Lab2.Tests
{
    public class SeidelIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case)
        {
            return SeidelIterativeMethod.Solve(@case.Coefficients, @case.RightPart, @case.Accuracy);
       }

        [TestCase(0.01)]
        [TestCase(0.001)]
        public override void Test3(decimal accuracy)
        {
            Assert.Throws<OverflowException>(() => base.Test3(accuracy));
        }
    }
}