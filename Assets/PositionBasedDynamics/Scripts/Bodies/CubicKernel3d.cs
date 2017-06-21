using System;

using Common.Mathematics.LinearAlgebra;

namespace PositionBasedDynamics.Bodies
{

    public class CubicKernel3d
    {

        public double W_zero;

        double K;

        double L;

        double Radius;

        double InvRadius;

        public CubicKernel3d(double radius)
        {
            Radius = radius;
            InvRadius = 1.0 / Radius;

            double h3 = Radius * Radius * Radius;

            K = 8.0 / (Math.PI * h3);
            L = 48.0f / (Math.PI * h3);

            W_zero = W(0,0,0);
        }

        double Pow3(double v)
        {
            return v * v * v;
        }

        public double W(Vector3d r)
        {

            double res = 0.0;
            double rl = r.Magnitude; // r.norm();
            double q = rl / Radius;

            if (q <= 1.0f)
            {
                if (q <= 0.5f)
                {
                    double q2 = q * q;
                    double q3 = q2 * q;
                    res = K * (6.0 * q3 - 6.0 * q2 + 1.0);
                }
                else
                {
                    res = K * 2.0 * Pow3(1.0 - q);
                }
            }
            return res;

        }

        public double W(double x, double y, double z)
        {

            double res = 0.0;
            double rl = Math.Sqrt(x * x + y * y + z * z);
            double q = rl * InvRadius;

            if (q <= 1.0)
            {
                if (q <= 0.5)
                {
                    double q2 = q * q;
                    double q3 = q2 * q;
                    res = K * (6.0 * q3 - 6.0f * q2 + 1.0);
                }
                else
                {
                    double v = 1.0 - q;
                    res = K * 2.0 * v*v*v;
                }
            }
            return res;

        }

        public Vector3d GradW(Vector3d r)
        {

            Vector3d res = Vector3d.Zero;
            double rl = r.Magnitude;
            double q = rl / Radius;

            if (q <= 1.0)
            {
                if (rl > 1.0e-6)
                {
                    Vector3d gradq = r * (1.0 / (rl * Radius));
                    if (q <= 0.5f)
                    {
                        res = L * q * (3.0 * q - 2.0) * gradq;
                    }
                    else
                    {
                        double factor = 1.0 - q;
                        res = L * (-factor * factor) * gradq;
                    }
                }
            }

            return res;
        }


        public Vector3d GradW(double x, double y, double z)
        {

            Vector3d res = Vector3d.Zero;
            double rl = Math.Sqrt(x*x + y*y + z*z);
            double q = rl * InvRadius;
            double factor;

            if (q <= 1.0)
            {
                if (rl > 1.0e-6)
                {
                    Vector3d gradq;

                    factor = 1.0 / (rl * Radius);

                    gradq.x = x * factor;
                    gradq.y = y * factor;
                    gradq.z = z * factor;

                    if (q <= 0.5)
                    {
                        factor = L * q * (3.0 * q - 2.0);

                        res.x = gradq.x * factor;
                        res.y = gradq.y * factor;
                        res.z = gradq.z * factor;
                    }
                    else
                    {
                        factor = 1.0 - q;
                        factor = L * (-factor * factor);

                        res.x = gradq.x * factor;
                        res.y = gradq.y * factor;
                        res.z = gradq.z * factor;
                    }
                }
            }

            return res;
        }

    }

}