using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Lab2.Tests
{
    public abstract class SystemOfEquationsTestBase
    {
        protected const decimal ACCURACY = 0.0001M;

        protected abstract decimal[] Calculate(decimal[,] a, decimal[] b);

        [Test]
        public void Test0()
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
            });
        }

        [Test]
        public void Test1()
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
            });
        }

        [Test]
        public void Test2()
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
            });
        }

        [Test]
        public void Test3()
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
            });
        }

        [Test]
        public void Test4()
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
            });
        }

        private void Test(TestCase @case)
        {
            var result = Calculate(@case.Coefficients, @case.RightPart);
            AssertResult(@case.Solution, result, ACCURACY);
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