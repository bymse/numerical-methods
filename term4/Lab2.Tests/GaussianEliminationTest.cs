using System.Linq;

namespace Lab2.Tests
{
    public class GaussianEliminationTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case)
        {
            return (GaussianElimination.Solve(@case.Coefficients, @case.RightPart), default);
        }
    }
}