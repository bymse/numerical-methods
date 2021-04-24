namespace Lab2.Tests
{
    public class QrMethodTest : SystemOfEquationsTestBase
    {
        protected override (decimal[] Solution, int IterationsCount) Calculate(decimal[,] a, decimal[] b, decimal accuracy)
        {
            return (QrMethod.Solve(a, b), default);
        }
    }
}