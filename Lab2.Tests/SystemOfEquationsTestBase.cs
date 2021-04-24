using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Lab2.Tests
{
    public abstract class SystemOfEquationsTestBase
    {
        protected abstract (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case);

        [TestCase(0.01)]
        public virtual void Test0(decimal accuracy)
        {
            Test(new TestCase()
            {
                Coefficients = new decimal[,]
                {
                    {1, 2, 4},
                    {4, 5, 6},
                    {0, 2, 3},
                },
                RightPart = new decimal[] {17, 32, 13},
                Solution = new decimal[] {1, 2, 3},
                
            }, accuracy);
        }

        [TestCase(0.01)]
        public virtual void Test1(decimal accuracy)
        {
            Test(new TestCase
            {
                RightPart = new decimal[] {17, 19, 21},
                Coefficients = new decimal[,]
                {
                    {15, 1, 1},
                    {1, 17, 1},
                    {1, 1, 19},
                },
                Solution = new decimal[] {1, 1, 1}
            }, accuracy);
        }

        [TestCase(0.01)]
        public virtual void Test2(decimal accuracy)
        {
            Test(new TestCase
            {
                Coefficients = new decimal[,]
                {
                    {-15, 1, 1},
                    {1, -17, 1},
                    {1, 1, -19},
                },
                RightPart = new decimal[] {-17, -19, -21},
                Solution = new[] {779M / 599M, 759M / 599M, 743M / 599M}
            }, accuracy);
        }

        [TestCase(0.01)]
        public virtual void Test3(decimal accuracy)
        {
            Test(new TestCase
            {
                Coefficients = new decimal[,]
                {
                    {-15, 16, 17},
                    {18, -17, 14},
                    {17, 18, -19}
                },
                RightPart = new decimal[] {17, 19, 21},
                Solution = new[] {11785M / 9318M, 5155M / 4659M, 10013M / 9318M}
            }, accuracy);
        }

        [TestCase(0.01)]
        public virtual void Test4(decimal accuracy)
        {
            Test(new TestCase
            {
                Coefficients = new decimal[,]
                {
                    {15, 14, 14},
                    {14, 17, 14},
                    {14, 14, 19}
                },
                RightPart = new decimal[] {17, 19, 21},
                Solution = new[] {-53M / 337M, 207M / 337M, 259M / 337M}
            }, accuracy);
        }

        public virtual void Test5()
        {
            
        }

        private void Test(TestCase @case, decimal accuracy)
        {
            var result = Calculate(@case);
            AssertResult(@case.Solution, result.Solution, accuracy);
        }

        private static void AssertResult(decimal[] expected, decimal[] actual, decimal accuracy)
        {
            for (var i = 0; i < expected.Length; i++)
            {
                actual[i].Should().BeInRange(expected[i] - accuracy, expected[i] + accuracy);
            }
        }
    }
}