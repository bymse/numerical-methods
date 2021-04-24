namespace Lab2.Tests
{
    public class HouseholderMethodTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case)
        {
            return (HouseholderMethod.Solve(@case.Coefficients, @case.RightPart), default);
        }
    }
}