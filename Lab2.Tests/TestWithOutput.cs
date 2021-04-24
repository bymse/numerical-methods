using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lab2.Tests
{
    [Explicit]
    [Parallelizable(ParallelScope.None)]
    public class TestWithOutput : SystemOfEquationsTestBase
    {
        private readonly StringBuilder stringBuilder = new();
        
        [OneTimeSetUp]
        public void Initialize()
        {
            stringBuilder.Append("№ Теста,n,E,x',e,");
            stringBuilder.Append("МПИ: x, МПИ: delta, МПИ: k,");
            stringBuilder.Append("М-д Зейделя: x, М-д Зейделя: delta, М-д Зейделя: k,");
            stringBuilder.Append("М-д Гаусса: x, М-д Гаусса: delta,");
            stringBuilder.AppendLine("М-д Хаусхолдера: x, М-д Хаусхолдера: delta");
        }

        [OneTimeTearDown]
        public void WriteToFile()
        {
            File.WriteAllText(@"C:\Users\bymse\Desktop\чм\out.csv",
                stringBuilder.ToString(),
                Encoding.UTF8);
        }

        protected override (decimal[] Solution, int? IterationsCount) Calculate(TestCase @case)
        {
            var results = new List<(decimal[], int?)>
            {
                SimpleIterativeMethod.Solve(@case.Coefficients, @case.RightPart, @case.Accuracy),
                SeidelIterativeMethod.Solve(@case.Coefficients, @case.RightPart, @case.Accuracy),
                (GaussianElimination.Solve(@case.Coefficients, @case.RightPart), default),
                (HouseholderMethod.Solve(@case.Coefficients, @case.RightPart), default),
            };

            for (var index = 0; index < @case.Solution.Length; index++)
            {
                var expected = @case.Solution[index];
                stringBuilder.Append($",-,-,{expected},{@case.Accuracy},");
                
                foreach (var (solution, iterationsCount) in results)
                {
                    var actual = solution[index];
                    var delta = Math.Abs(actual - expected);
                    stringBuilder.Append($"{actual},{delta},");
                    if (iterationsCount.HasValue)
                    {
                        stringBuilder.Append($"{iterationsCount},");
                    }
                    
                    //Assert.True(delta <= @case.Accuracy);
                }

                stringBuilder.AppendLine();
            }

            return default;
        }


        protected override void Test(TestCase @case)
        {
            Calculate(@case);
        }

        #region Tests
        
        [Order(1)]
        [TestCase(0.001)]
        public override void Test0(decimal accuracy)
        {
            stringBuilder.Append('0');
            base.Test0(accuracy);
        }

        [Order(2)]
        [TestCase(0.001)]
        public override void Test1(decimal accuracy)
        {
            stringBuilder.Append('1');
            base.Test1(accuracy);
        }
        
        [Order(3)]
        [TestCase(0.001)]
        public override void Test2(decimal accuracy)
        {
            stringBuilder.Append('2');
            base.Test2(accuracy);
        }
        
        [Order(4)]
        [TestCase(0.001)]
        public override void Test3(decimal accuracy)
        {
            stringBuilder.Append('3');
            base.Test3(accuracy);
        }
        
        [Order(5)]
        [TestCase(0.001)]
        public override void Test4(decimal accuracy)
        {
            stringBuilder.Append('4');
            base.Test4(accuracy);
        }
        
        #endregion
    }
}