using System;

namespace Lab5
{
    public class InterpolationPoly {
        private InterpolationPoly() {
        }

        public static Func<Double, Double> Build(double[] x, double[] y) {
            return t => {
                var value = 0.0;
                var l = new double[x.Length];
                Array.Fill(l, 1);
                for (var i = 0; i < x.Length; i++) {
                    for (var j = 0; j < x.Length; j++) {
                        if (i != j) {
                            l[i] *= (t - x[j]) / (x[i] - x[j]);
                        }
                    }
                    value += l[i] * y[i];
                }
                return value;
            };
        }
    }
}