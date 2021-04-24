using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Lab2.Tests
{
    public abstract class SystemOfEquationsTestBase
    {
        protected abstract (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case);

        [TestCase(0.001)]
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
                Accuracy = accuracy
            });
        }

        [TestCase(0.001)]
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
                Solution = new decimal[] {1, 1, 1},
                Accuracy = accuracy
            });
        }

        [TestCase(0.001)]
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
                Solution = new[] {779M / 599M, 759M / 599M, 743M / 599M},
                Accuracy = accuracy
            });
        }

        [TestCase(0.001)]
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
                Solution = new[] {11785M / 9318M, 5155M / 4659M, 10013M / 9318M},
                Accuracy = accuracy
            });
        }

        [TestCase(0.001)]
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
                Solution = new[] {-53M / 337M, 207M / 337M, 259M / 337M},
                Accuracy = accuracy
            });
        }

        [TestCaseSource(nameof(GetTest5Data))]
        public virtual void Test5(decimal accuracy, int n, decimal e, decimal[] solution)
        {
            var a = new decimal[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        a[i, j] = 1;
                        a[i, j] += e * 13 * 1;
                    }
                    else if (i - j < 0)
                    {
                        a[i, j] = -1;
                        a[i, j] += e * 13 * -1;
                    }
                    else
                    {
                        a[i, j] = 0;
                        a[i, j] += e * 13 * -1;
                    }
                }
            }

            var b = new decimal[n];
            for (var i = 0; i < b.Length - 1; i++)
            {
                b[i] = -1;
            }

            b[n - 1] = 1;

            var testCase = new TestCase
            {
                Accuracy = accuracy,
                Coefficients = a,
                RightPart = b,
                Solution = solution,
                Length = n,
                Multiplier = e
            };

            Test(testCase);
        }

        public static IEnumerable<TestCaseData> GetTest5Data()
        {
            yield return new TestCaseData(0.001M, 4, 0.001M, new[] {0, 0, 0, 1000M / 1013M});
            yield return new TestCaseData(0.001M, 4, 1M / 1000000M, new[] {0, 0, 0, 1000000M / 1000013M});

            yield return new TestCaseData(0.001M, 5, 0.001M, new[] {0, 0, 0, 0, 1000M / 1013M});
            yield return new TestCaseData(0.001M, 5, 1M / 1000000M, new[] {0, 0, 0, 0, 1000000M / 1000013M});
        }

        protected virtual void Test(TestCase @case)
        {
            var result = Calculate(@case);
            AssertResult(@case.Solution, result.Solution, @case.Accuracy);
        }

        private static void AssertResult(decimal[] expected, decimal[] actual, decimal accuracy)
        {
            for (var i = 0; i < expected.Length; i++)
            {
                actual[i].Should().BeApproximately(expected[i], accuracy);
            }
        }
    }
}