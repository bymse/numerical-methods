namespace Lab2.Tests
{
    public class SimpleIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case)
        {
            return SimpleIterativeMethod.Solve(@case.Coefficients, @case.RightPart, @case.Accuracy);
        }
    }
}