using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new  decimal[][]
            {
                new decimal[]{1, 2, 4},
                new decimal[]{4, 5, 6},
                new decimal[]{0, 2, 3},
            };


            var res = SeidelIterativeMethod.Solve(a, new decimal[]{17,32,13}, 0.0001M);
            
            foreach (var @decimal in res.solution)
            {
                Console.WriteLine(@decimal);
            }
        }

        private static void Show(List<List<decimal>> arr)
        {
            for (var index0 = 0; index0 < arr.Count; index0++)
            {
                for (var index1 = 0; index1 < arr.Count; index1++)
                {
                    var val = arr[index0][index1];
                    Console.Write(val);
                    Console.Write("   ");
                }

                Console.WriteLine();
            }
        }
    }
}