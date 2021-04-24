using System.Linq;

namespace Lab2.Tests
{
    public class GaussianEliminationTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int IterationsCount) Calculate(decimal[,] a, decimal[] b, decimal accuracy)
        {
            return (GaussianElimination.Solve(a, b), default);
        }
    }
}