using System;
using System.Globalization;
using System.IO;

namespace NumericalMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            ElementaryFunctions.EpsilonForArctg = ;
            ElementaryFunctions.EpsilonForSin = ;
            ElementaryFunctions.EpsilonForSqrt = ;
            PrintTable();
        }

        private static void PrintTable()
        {
            const decimal start = 0.2M;
            const decimal end = 0.3M;
            const decimal shift = 0.01M;
            var writer = new StringWriter(CultureInfo.InvariantCulture);
            writer.WriteLine("step,g,g',delta(g),fi,fi',delta(fi),psi,psi',delta(psi),z,z',delta(z)");
            for (var val = start; val <= end; val += shift)
            {
                writer.Write(val);
                writer.Write(",");

                PrintNativeCustomDiff(val, ExecuteG_Custom, ExecuteG_Native, writer);
                PrintNativeCustomDiff(val, ExecuteFi_Custom, ExecuteFi_Native, writer);
                PrintNativeCustomDiff(val, ExecutePsi_Custom, ExecutePsi_Native, writer);
                PrintNativeCustomDiff(val, Execute_Custom, Execute_Native, writer);
                writer.WriteLine();
            }
            File.WriteAllText("out.csv",writer.ToString());
        }

        private static void PrintNativeCustomDiff(decimal val,
            Func<decimal, decimal> custom,
            Func<decimal, decimal> native, TextWriter writer)
        {
            var customVal = custom(val);
            var nativeVal = native(val);
            var diff = Math.Abs(customVal - nativeVal);
            var line = $"{customVal},{nativeVal},{diff},";
            writer.Write(line);
        }

        private static decimal Execute_Native(decimal val)
        {
            return ExecuteFi_Native(val) + ExecutePsi_Native(val);
        }

        private static decimal Execute_Custom(decimal val)
        {
            return ExecuteFi_Custom(val) + ExecutePsi_Custom(val);
        }

        private static decimal ExecuteG_Custom(decimal val) => ExecuteG(val, ElementaryFunctions.Sqrt);
        private static decimal ExecuteG_Native(decimal val) => ExecuteG(val, arg => (decimal) Math.Sqrt((double) arg));
        
        private static decimal ExecuteFi_Native(decimal val)
        {
            return ExecuteFi(val,
                arg => (decimal) Math.Sqrt((double) arg),
                arg => (decimal) Math.Atan((double) arg));
        }

        private static decimal ExecutePsi_Native(decimal val)
        {
            return  ExecutePsi(val, arg => (decimal) Math.Sin((double) arg));
        }
        
        private static decimal ExecuteFi_Custom(decimal val)
        {
            return ExecuteFi(val,
                ElementaryFunctions.Sqrt,
                ElementaryFunctions.Arctg);
        }

        private static decimal ExecutePsi_Custom(decimal val)
        {
            return ExecutePsi(val, ElementaryFunctions.Sin);
        }
        

        private static decimal ExecuteFi(decimal val,
            Func<decimal, decimal> sqrt,
            Func<decimal, decimal> arctg)
        {
            return arctg(ExecuteG(val, sqrt));
        }

        private static decimal ExecutePsi(decimal val, Func<decimal, decimal> sin)
        {
            return sin(3 * val + 0.6M);
        }

        private static decimal ExecuteG(decimal val, Func<decimal, decimal> sqrt)
        {
            return sqrt(0.9M * val + 1) / (1 - val * val);
        }
    }
}