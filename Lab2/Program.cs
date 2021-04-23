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


            // //Householder.Solve(a, new decimal[]{5,7,9});
            // var m = new mat()
            // {
            //     V = a
            // };
            //
            // var q = new mat();
            // var r = new mat();
            //     var obj = new HouseholderFromC();
            //  obj.householder(m, ref r, ref q);
            //  var res = obj.Solve(q, r, new decimal[] {17, 32, 13});
            //
            //  // Show(q.V);
            //  // Console.WriteLine("------");
            //  // Show(r.V);
            //
            //  foreach (var @decimal in res)
            //  {
            //       Console.WriteLine(@decimal);
            //  }
        }

        private static void Show(decimal[][] arr)
        {
            for (var index0 = 0; index0 < arr.Length; index0++)
            {
                for (var index1 = 0; index1 < arr.Length; index1++)
                {
                    var val = arr[index0][index1];
                    Console.Write($"{val:00.00}");
                    Console.Write("   ");
                }

                Console.WriteLine();
            }
        }
    }
}