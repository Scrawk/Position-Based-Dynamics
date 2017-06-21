using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Constraints
{

    public class StrainTetConstraint3d : Constraint3d
    {

        public bool NormalizeStretch { get; set; }

        public bool NormalizeShear { get; set; }

        private readonly int P0, P1, P2, P3;

        private Matrix3x3d InvRestMatrix;

        private double Stiffness;

        private Vector3d[] Correction;

        internal StrainTetConstraint3d(Body3d body, int p0, int p1, int p2, int p3, double stiffness) : base(body)
        {

            P0 = p0;
            P1 = p1;
            P2 = p2;
            P3 = p3;

            Stiffness = stiffness;
            NormalizeStretch = false;
            NormalizeShear = false;

            Correction = new Vector3d[4];

            Vector3d x0 = body.Positions[P0];
            Vector3d x1 = body.Positions[P1];
            Vector3d x2 = body.Positions[P2];
            Vector3d x3 = body.Positions[P3];

            Matrix3x3d restMatrix = new Matrix3x3d();

            restMatrix.SetColumn(0, x1 - x0);
            restMatrix.SetColumn(1, x2 - x0);
            restMatrix.SetColumn(2, x3 - x0);

            InvRestMatrix = restMatrix.Inverse;

        }

        internal override void ConstrainPositions(double di)
        {

            Vector3d x0 = Body.Predicted[P0];
            Vector3d x1 = Body.Predicted[P1];
            Vector3d x2 = Body.Predicted[P2];
            Vector3d x3 = Body.Predicted[P3];

            double invMass = 1.0 / Body.ParticleMass;

            bool res = SolveConstraint(x0, x1, x2, x3, invMass);

            if (res)
            {
                Body.Predicted[P0] += Correction[0] * di;
                Body.Predicted[P1] += Correction[1] * di;
                Body.Predicted[P2] += Correction[2] * di;
                Body.Predicted[P3] += Correction[3] * di;
            }

        }

        private bool SolveConstraint(Vector3d p0, Vector3d p1, Vector3d p2, Vector3d p3, double invMass)
        {

            double eps = 1e-9;

            Correction[0] = Vector3d.Zero;
            Correction[1] = Vector3d.Zero;
            Correction[2] = Vector3d.Zero;
            Correction[3] = Vector3d.Zero;

            Vector3d[] c = new Vector3d[3];
            c[0] = InvRestMatrix.GetColumn(0);
            c[1] = InvRestMatrix.GetColumn(1);
            c[2] = InvRestMatrix.GetColumn(2);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Matrix3x3d P = new Matrix3x3d();

                    // Jacobi
                    //P.col(0) = p1 - p0;		
                    //P.col(1) = p2 - p0;
                    //P.col(2) = p3 - p0;

                    // Gauss - Seidel
                    P.SetColumn(0, (p1 + Correction[1]) - (p0 + Correction[0])); 
                    P.SetColumn(1, (p2 + Correction[2]) - (p0 + Correction[0]));
                    P.SetColumn(2, (p3 + Correction[3]) - (p0 + Correction[0]));

                    Vector3d fi = P * c[i];
                    Vector3d fj = P * c[j];

                    double Sij = Vector3d.Dot(fi, fj);

                    double wi = 0.0, wj = 0.0, s1 = 0.0, s3 = 0.0;
                    if (NormalizeShear && i != j)
                    {
                        wi = fi.Magnitude;
                        wj = fj.Magnitude;
                        s1 = 1.0 / (wi * wj);
                        s3 = s1 * s1 * s1;
                    }

                    Vector3d[] d = new Vector3d[4];
                    d[0] = new Vector3d(0.0, 0.0, 0.0);

                    for (int k = 0; k < 3; k++)
                    {
                        d[k + 1] = fj * InvRestMatrix[k, i] + fi * InvRestMatrix[k, j];

                        if (NormalizeShear && i != j)
                        {
                            d[k + 1] = s1 * d[k + 1] - Sij * s3 * (wj * wj * fi * InvRestMatrix[k, i] + wi * wi * fj * InvRestMatrix[k, j]);
                        }

                        d[0] -= d[k + 1];
                    }

                    if (NormalizeShear && i != j)
                        Sij *= s1;

                    double lambda = (d[0].SqrMagnitude + d[1].SqrMagnitude + d[2].SqrMagnitude + d[3].SqrMagnitude) * invMass;

                    if (Math.Abs(lambda) < eps) continue;

                    if (i == j)
                    {
                        // diagonal, stretch
                        if (NormalizeStretch)
                        {
                            double s = Math.Sqrt(Sij);
                            lambda = 2.0 * s * (s - 1.0) / lambda * Stiffness;
                        }
                        else
                        {
                            lambda = (Sij - 1.0) / lambda * Stiffness;
                        }
                    }
                    else
                    {
                        // off diagonal, shear
                        lambda = Sij / lambda * Stiffness;
                    }

                    Correction[0] -= lambda * invMass * d[0];
                    Correction[1] -= lambda * invMass * d[1];
                    Correction[2] -= lambda * invMass * d[2];
                    Correction[3] -= lambda * invMass * d[3];
                }
            }

            return true;
        }

    }

}