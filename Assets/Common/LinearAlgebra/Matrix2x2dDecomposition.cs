using System;
using System.Collections.Generic;

namespace Common.Mathematics.LinearAlgebra
{
    public static class Matrix2x2dDecomposition
    {

        public static Matrix2x2d QRDecomposition(Matrix2x2d m)
        {
            Vector2d a = m.GetColumn(0).Normalized;
            Matrix2x2d q = new Matrix2x2d();

            q.SetColumn(0, a);
            q.SetColumn(1, a.PerpendicularCCW);

            return q;
        }

        public static Matrix2x2d PolarDecomposition(Matrix2x2d m)
        {
            Matrix2x2d q = m + new Matrix2x2d(m.m11, -m.m10, -m.m01, m.m00);

            Vector2d c0 = q.GetColumn(0);
            Vector2d c1 = q.GetColumn(1);

            double s = c0.Magnitude;
            q.SetColumn(0, c0 / s);
            q.SetColumn(1, c1 / s);

            return q;
        }

        public static void EigenDecomposition(Matrix2x2d m, out double e1, out double e2)
        {
            // solve the characteristic polynomial
            double a = 1.0;
            double b = -(m.m00 + m.m11);
            double c = m.m00 * m.m11 - m.m01 * m.m10;

            SolveQuadratic(a, b, c, out e1, out e2);
        }

        private static bool SolveQuadratic(double a, double b, double c, out double min, out double max)
        {
            min = max = 0.0;

            if (a == 0.0 && b == 0.0)
                return true;

            double discriminant = b * b - 4.0 * a * c;

            if (discriminant < 0.0)
                return false;

            // numerical receipes 5.6 (this method ensures numerical accuracy is preserved)
            double t = -0.5 * (b + Math.Sign(b) * Math.Sqrt(discriminant));
            min = t / a;
            max = c / t;

            if (min > max)
            {
                double tmp = min;
                min = max;
                max = tmp;
            }

            return true;
        }
    }
}
