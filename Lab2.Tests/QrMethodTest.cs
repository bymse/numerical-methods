namespace Lab2.Tests
{
    public class QrMethodTest : SystemOfEquationsTestBase
    {
        protected override decimal[] Calculate(decimal[,] a, decimal[] b)
        {
            return QrMethod.Solve(a, b);
        }
    }
}