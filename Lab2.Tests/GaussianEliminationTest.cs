using System.Linq;

namespace Lab2.Tests
{
    public class GaussianEliminationTest : SystemOfEquationsTestBase
    {
        protected override decimal[] Calculate(decimal[][] a, decimal[] b)
        {
            return GaussianElimination.Solve(a.Select(e => e.ToList()).ToList(), b.ToList());
        }
    }
}