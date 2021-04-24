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
    }
}