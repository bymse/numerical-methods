using System;
using FluentAssertions;
using NUnit.Framework;

namespace Lab3.Tests
{
    [TestFixture]
    public class NewtonMethodTest
    {
        private static decimal Func(decimal x)
        {
            var ln = (decimal) Math.Log((double) (x + 1));
            return x * ln - 0.3M;
        }

        private static decimal Derivative(decimal x)
        {
            var ln = (decimal) Math.Log((double) (x + 1));
            return x / (x + 1) + ln;
        } 
        
        private const decimal EXPECTED_ANSWER = 0.621035M;
        private const decimal PRECISION = 0.0001M;
        
        [Test]
        public void Test()
        {
            var solution = NewtonMethod.Solve(
                 Func,
                 Derivative,
                 (0, 10),
                 PRECISION);
            
            TestContext.WriteLine($"Solution: {solution}");
            
            solution.Should().BeApproximately(EXPECTED_ANSWER, PRECISION);
        }
    }
}