namespace Lab2.Tests
{
    public class SimpleIterativeMethodTest : SystemOfEquationsTestBase
    {
        protected override decimal[] Calculate(decimal[,] a, decimal[] b)
        {
            var arr = new decimal[b.Length][];
            
            for (var i = 0; i < a.GetLength(0); i++)
            {
                arr[i] = new decimal[b.Length];
                for (var i1 = 0; i1 < arr.GetLength(0); i1++)
                {
                    arr[i][i1] = a[i, i1];
                }
            }
            return SimpleIterativeMethod.Solve(arr, b, ACCURACY);
        }
    }
}