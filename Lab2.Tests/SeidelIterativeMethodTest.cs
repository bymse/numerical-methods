namespace Lab2.Tests
{
    public class SeidelIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override decimal[] Calculate(decimal[,] a, decimal[] b)
        {
            return SeidelIterativeMethod.Solve(a, b, ACCURACY).solution;
        }
    }
}