using System;
using System.Collections.Generic;

namespace Common.Mathematics.LinearAlgebra
{
    public static class Matrix2x2fDecomposition
    {

        public static Matrix2x2f QRDecomposition(Matrix2x2f m)
        {
            Vector2f a = m.GetColumn(0).Normalized;
            Matrix2x2f q = new Matrix2x2f();

            q.SetColumn(0, a);
            q.SetColumn(1, a.PerpendicularCCW);

            return q;
        }

        public static Matrix2x2f PolarDecomposition(Matrix2x2f m)
        {
            Matrix2x2f q = m + new Matrix2x2f(m.m11, -m.m10, -m.m01, m.m00);

            Vector2f c0 = q.GetColumn(0);
            Vector2f c1 = q.GetColumn(1);

            float s = c0.Magnitude;
            q.SetColumn(0, c0 / s);
            q.SetColumn(1, c1 / s);

            return q;
        }

        public static void EigenDecomposition(Matrix2x2f m, out float e1, out float e2)
        {
            // solve the characteristic polynomial
            float a = 1.0f;
            float b = -(m.m00 + m.m11);
            float c = m.m00 * m.m11 - m.m01 * m.m10;

            SolveQuadratic(a, b, c, out e1, out e2);
        }

        private static bool SolveQuadratic(float a, float b, float c, out float min, out float max)
        {
            min = max = 0.0f;

            if (a == 0.0f && b == 0.0f)
                return true;

            float discriminant = b * b - 4.0f * a * c;

            if (discriminant < 0.0f)
                return false;

            // numerical receipes 5.6 (this method ensures numerical accuracy is preserved)
            float t = (float)(-0.5 * (b + Math.Sign(b) * Math.Sqrt(discriminant)));
            min = t / a;
            max = c / t;

            if (min > max)
            {
                float tmp = min;
                min = max;
                max = tmp;
            }

            return true;
        }
    }
}
