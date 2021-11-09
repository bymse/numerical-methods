using System;
using FluentAssertions;
using NUnit.Framework;

namespace Lab3.Tests
{
    [TestFixture]
    public class NewtonMethodForSystemsTest
    {
        private static readonly Function First = new Function
        {
            Func = (x, y, k) => x - k*(decimal) Math.Sin((double) (y + 1)) - 0.8M,
            FirstArgDerivative = (x, y, k) => 1,
            SecondArgDerivative = (x, y, k) => -k*(decimal) Math.Cos((double) (y + 1))
        };
        
        private static readonly Function Second = new Function
        {
            Func = (x, y, k) => k*(decimal) Math.Sin((double) (x - 1)) + y - 1.3M,
            FirstArgDerivative = (x, y, k) => k*(decimal) Math.Cos((double) (1 - x)),
            SecondArgDerivative = (x, y, k) => 1,
        };

        private const decimal PRECISION = 0.0001M;
        private static readonly decimal[] Expected = { 1.79993M, 0.582693M };
        
        [Test]
        public void TestWithStaticApproximation()
        {
            var solution = NewtonMethodForSystems.Solve(
                First,
                Second,
                new[] {0.0M, 0.0M},
                PRECISION
            );
            
            Assert(solution);
        }

        [Test]
        public void TestWithApproximationFinding()
        {
            var solution = NewtonMethodForSystems.SolveWithApproximationFinding(
                First,
                Second,
                PRECISION
            );
            
            Assert(solution);
        }

        private static void Assert(decimal[] solution)
        {
            TestContext.WriteLine("solution");
            for (var i = 0; i < solution.Length; i++)
            {
                TestContext.WriteLine(solution[i]);
                solution[i].Should().BeApproximately(Expected[i], PRECISION);
            }
        }
    }
}