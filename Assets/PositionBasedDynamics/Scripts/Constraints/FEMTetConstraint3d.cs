using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Constraints
{
    public class FEMTetConstraint3d : Constraint3d
    {

        public bool HandleInversion { get; set; }

        private readonly int P0, P1, P2, P3;

        private Matrix3x3d InvRestMatrix;

        private double YoungsModulus;

        private double PoissonRatio;

        private double RestVolume;

        private Vector3d[] Correction;

        internal FEMTetConstraint3d(Body3d body, int p0, int p1, int p2, int p3, double youngsModulus) : base(body)
        {

            P0 = p0;
            P1 = p1;
            P2 = p2;
            P3 = p3;

            YoungsModulus = youngsModulus;
            PoissonRatio = 0.3;
            Correction = new Vector3d[4];

            Vector3d x0 = Body.Positions[P0];
            Vector3d x1 = Body.Positions[P1];
            Vector3d x2 = Body.Positions[P2];
            Vector3d x3 = Body.Positions[P3];

            RestVolume = Math.Abs((1.0f / 6.0) * Vector3d.Dot(x3 - x0, Vector3d.Cross(x2 - x0, x1 - x0)));

            Matrix3x3d restMatrix = new Matrix3x3d();

            restMatrix.SetColumn(0, x0 - x3);
            restMatrix.SetColumn(1, x1 - x3);
            restMatrix.SetColumn(2, x2 - x3);

            InvRestMatrix = restMatrix.Inverse;

        }

        internal override void ConstrainPositions(double di)
        {

            Vector3d x0 = Body.Predicted[P0];
            Vector3d x1 = Body.Predicted[P1];
            Vector3d x2 = Body.Predicted[P2];
            Vector3d x3 = Body.Predicted[P3];

            double mass = Body.ParticleMass;
            double invMass = 1.0 / mass;

            double currentVolume = -(1.0f / 6.0) * Vector3d.Dot(x3 - x0, Vector3d.Cross(x2 - x0, x1 - x0));

            HandleInversion = false;
            if (currentVolume / RestVolume < 0.2) // Only 20% of initial volume left
                HandleInversion = true;

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
            double eps = 1e-6;

            Correction[0] = Vector3d.Zero;
            Correction[1] = Vector3d.Zero;
            Correction[2] = Vector3d.Zero;
            Correction[3] = Vector3d.Zero;

            //if (youngsModulus <= 0.0f)
            //   return true;

            double C = 0.0f;
            Vector3d[] gradC = new Vector3d[4];
            Matrix3x3d epsilon = new Matrix3x3d();
            Matrix3x3d sigma = new Matrix3x3d();
            double volume = Vector3d.Dot(p3 - p0, Vector3d.Cross(p1 - p0, p2 - p0)) / 6.0;

            double mu = YoungsModulus / 2.0 / (1.0 + PoissonRatio);
            double lambda = YoungsModulus * PoissonRatio / (1.0 + PoissonRatio) / (1.0 - 2.0 * PoissonRatio);

            if (!HandleInversion || volume > 0.0)
            {
                ComputeGreenStrainAndPiolaStress(p0, p1, p2, p3, mu, lambda, ref epsilon, ref sigma, ref C);
                ComputeGradCGreen(sigma, gradC);
            }
            else
            {
                ComputeGreenStrainAndPiolaStressInversion(p0, p1, p2, p3, mu, lambda, ref epsilon, ref sigma, ref C);
                ComputeGradCGreen(sigma, gradC);
            }

            double sumNormGradC = gradC[0].SqrMagnitude + gradC[1].SqrMagnitude + gradC[2].SqrMagnitude + gradC[3].SqrMagnitude;
            sumNormGradC *= invMass;

            if (sumNormGradC < eps)
                return false;

            // compute scaling factor
            double s = C / sumNormGradC;

            Correction[0] = -s * invMass * gradC[0];
            Correction[1] = -s * invMass * gradC[1];
            Correction[2] = -s * invMass * gradC[2];
            Correction[3] = -s * invMass * gradC[3];

            return true;
        }

        private void ComputeGradCGreen(Matrix3x3d sigma, Vector3d[] J)
        {

            Matrix3x3d T = InvRestMatrix.Transpose;
            Matrix3x3d H = sigma * T * RestVolume;

            J[0].x = H.m00;
            J[1].x = H.m01;
            J[2].x = H.m02;

            J[0].y = H.m10;
            J[1].y = H.m11;
            J[2].y = H.m12;

            J[0].z = H.m20;
            J[1].z = H.m21;
            J[2].z = H.m22;

            J[3] = (J[0] * -1.0f) - J[1] - J[2];
        }

        private void ComputeGreenStrainAndPiolaStress(Vector3d x1, Vector3d x2, Vector3d x3, Vector3d x4, double mu, double lambda, ref Matrix3x3d epsilon, ref Matrix3x3d sigma, ref double energy)
        {
            // Determine \partial x/\partial m_i
            Matrix3x3d F = new Matrix3x3d();
            Vector3d p14 = x1 - x4;
            Vector3d p24 = x2 - x4;
            Vector3d p34 = x3 - x4;

            F.m00 = p14.x * InvRestMatrix.m00 + p24.x * InvRestMatrix.m10 + p34.x * InvRestMatrix.m20;
            F.m01 = p14.x * InvRestMatrix.m01 + p24.x * InvRestMatrix.m11 + p34.x * InvRestMatrix.m21;
            F.m02 = p14.x * InvRestMatrix.m02 + p24.x * InvRestMatrix.m12 + p34.x * InvRestMatrix.m22;

            F.m10 = p14.y * InvRestMatrix.m00 + p24.y * InvRestMatrix.m10 + p34.y * InvRestMatrix.m20;
            F.m11 = p14.y * InvRestMatrix.m01 + p24.y * InvRestMatrix.m11 + p34.y * InvRestMatrix.m21;
            F.m12 = p14.y * InvRestMatrix.m02 + p24.y * InvRestMatrix.m12 + p34.y * InvRestMatrix.m22;

            F.m20 = p14.z * InvRestMatrix.m00 + p24.z * InvRestMatrix.m10 + p34.z * InvRestMatrix.m20;
            F.m21 = p14.z * InvRestMatrix.m01 + p24.z * InvRestMatrix.m11 + p34.z * InvRestMatrix.m21;
            F.m22 = p14.z * InvRestMatrix.m02 + p24.z * InvRestMatrix.m12 + p34.z * InvRestMatrix.m22;

            // epsilon = 1/2 F^T F - I

            epsilon.m00 = 0.5 * (F.m00 * F.m00 + F.m10 * F.m10 + F.m20 * F.m20 - 1.0);        // xx
            epsilon.m11 = 0.5 * (F.m01 * F.m01 + F.m11 * F.m11 + F.m21 * F.m21 - 1.0);        // yy
            epsilon.m22 = 0.5 * (F.m02 * F.m02 + F.m12 * F.m12 + F.m22 * F.m22 - 1.0);        // zz
            epsilon.m01 = 0.5 * (F.m00 * F.m01 + F.m10 * F.m11 + F.m20 * F.m21);              // xy
            epsilon.m02 = 0.5 * (F.m00 * F.m02 + F.m10 * F.m12 + F.m20 * F.m22);              // xz
            epsilon.m12 = 0.5 * (F.m01 * F.m02 + F.m11 * F.m12 + F.m21 * F.m22);              // yz
            epsilon.m10 = epsilon.m01;
            epsilon.m20 = epsilon.m02;
            epsilon.m21 = epsilon.m12;

            // P(F) = F(2 mu E + lambda tr(E)I) => E = green strain
            double trace = epsilon.m00 + epsilon.m11 + epsilon.m22;
            double ltrace = lambda * trace;
            sigma = epsilon * 2.0 * mu;
            sigma.m00 += ltrace;
            sigma.m11 += ltrace;
            sigma.m22 += ltrace;
            sigma = F * sigma;

            double psi = 0.0;
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                    psi += epsilon[j, k] * epsilon[j, k];

            psi = mu * psi + 0.5 * lambda * trace * trace;
            energy = RestVolume * psi;
        }

        private void ComputeGreenStrainAndPiolaStressInversion(Vector3d x1, Vector3d x2, Vector3d x3, Vector3d x4, double mu, double lambda, ref Matrix3x3d epsilon, ref Matrix3x3d sigma, ref double energy)
        {
            // Determine \partial x/\partial m_i
            Matrix3x3d F = new Matrix3x3d();
            Vector3d p14 = x1 - x4;
            Vector3d p24 = x2 - x4;
            Vector3d p34 = x3 - x4;

            F.m00 = p14.x * InvRestMatrix.m00 + p24.x * InvRestMatrix.m10 + p34.x * InvRestMatrix.m20;
            F.m01 = p14.x * InvRestMatrix.m01 + p24.x * InvRestMatrix.m11 + p34.x * InvRestMatrix.m21;
            F.m02 = p14.x * InvRestMatrix.m02 + p24.x * InvRestMatrix.m12 + p34.x * InvRestMatrix.m22;

            F.m10 = p14.y * InvRestMatrix.m00 + p24.y * InvRestMatrix.m10 + p34.y * InvRestMatrix.m20;
            F.m11 = p14.y * InvRestMatrix.m01 + p24.y * InvRestMatrix.m11 + p34.y * InvRestMatrix.m21;
            F.m12 = p14.y * InvRestMatrix.m02 + p24.y * InvRestMatrix.m12 + p34.y * InvRestMatrix.m22;

            F.m20 = p14.z * InvRestMatrix.m00 + p24.z * InvRestMatrix.m10 + p34.z * InvRestMatrix.m20;
            F.m21 = p14.z * InvRestMatrix.m01 + p24.z * InvRestMatrix.m11 + p34.z * InvRestMatrix.m21;
            F.m22 = p14.z * InvRestMatrix.m02 + p24.z * InvRestMatrix.m12 + p34.z * InvRestMatrix.m22;

            Matrix3x3d U, VT;
            Vector3d hatF;
            Matrix3x3dDecomposition.SVDWithInversionHandling(F, out hatF, out U, out VT);

            // Clamp small singular values
            double minXVal = 0.577;

            if (hatF.x < minXVal) hatF.x = minXVal;
            if (hatF.y < minXVal) hatF.y = minXVal;
            if (hatF.z < minXVal) hatF.z = minXVal;

            // epsilon for hatF
            Vector3d epsilonHatF = new Vector3d(0.5 * (hatF.x * hatF.x - 1.0), 0.5 * (hatF.y * hatF.y - 1.0), 0.5 * (hatF.z * hatF.z - 1.0));

            double trace = epsilonHatF.x + epsilonHatF.y + epsilonHatF.z;
            double ltrace = lambda * trace;
            Vector3d sigmaVec = epsilonHatF * 2.0 * mu;
            sigmaVec.x += ltrace;
            sigmaVec.y += ltrace;
            sigmaVec.z += ltrace;
            sigmaVec.x = hatF.x * sigmaVec.x;
            sigmaVec.y = hatF.y * sigmaVec.y;
            sigmaVec.z = hatF.z * sigmaVec.z;

            Matrix3x3d sigmaDiag = new Matrix3x3d();
            Matrix3x3d epsDiag = new Matrix3x3d();

            sigmaDiag.SetRow(0, new Vector3d(sigmaVec.x, 0.0, 0.0));
            sigmaDiag.SetRow(1, new Vector3d(0.0, sigmaVec.y, 0.0));
            sigmaDiag.SetRow(2, new Vector3d(0.0, 0.0, sigmaVec.z));

            epsDiag.SetRow(0, new Vector3d(epsilonHatF.x, 0.0, 0.0));
            epsDiag.SetRow(1, new Vector3d(0.0, epsilonHatF.y, 0.0));
            epsDiag.SetRow(2, new Vector3d(0.0, 0.0f, epsilonHatF.z));

            epsilon = U * epsDiag * VT;
            sigma = U * sigmaDiag * VT;

            double psi = 0.0f;
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                    psi += epsilon[j, k] * epsilon[j, k];

            psi = mu * psi + 0.5 * lambda * trace * trace;
            energy = RestVolume * psi;
        }

    }

}