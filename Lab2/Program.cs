using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new  List<List<decimal>>()
            {
                new List<decimal>{1, 2, 4},
                new List<decimal>{4, 5, 6},
                new List<decimal>{0, 2, 3},
            };

            var l = new List<List<decimal>>()
            {
                new List<decimal> {0, 0, 0},
                new List<decimal> {0, 0, 0},
                new List<decimal> {0, 0, 0},
            };

            var u = new List<List<decimal>>()
            {
                new List<decimal> {0, 0, 0},
                new List<decimal> {0, 0, 0},
                new List<decimal> {0, 0, 0},
            };
            
            GaussianElimination.LuDecomposition(a, l, u, 3);
            
            Show(l);
            Console.WriteLine("----------");
            Show(u);
            Console.WriteLine("----------");

            var res = GaussianElimination.Solve(l, u, new List<decimal>(){13,17,32});
            
            foreach (var @decimal in res)
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