using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Lab2.Tests
{
    public abstract class SystemOfEquationsTestBase
    {
        private const decimal ACCURACY = 0.0001M;
        
        private readonly decimal[,] a = {
            {1, 2, 4},
            {4, 5, 6},
            {0, 2, 3},
        };

        private readonly decimal[] b = {17, 32, 13};

        private readonly decimal[] expected = {1, 2, 3};

        protected abstract decimal[] Calculate(decimal[,] a, decimal[] b);

        [Test]
        public void Test()
        {
            var result = Calculate(a, b);
            for (var i = 0; i < result.Length; i++)
            {
                result[i].Should().BeInRange(expected[i] - ACCURACY, expected[i] + ACCURACY);
            }
        }
    }
}