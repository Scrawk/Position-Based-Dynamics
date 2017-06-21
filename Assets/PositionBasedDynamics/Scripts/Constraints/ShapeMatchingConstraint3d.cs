using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Constraints
{

    public class ShapeMatchingConstraint3d : Constraint3d
    {

        private Matrix3x3d InvRestMatrix;

        private Vector3d RestCm;

        private Vector3d[] RestPositions;

        private double Stiffness;

        internal ShapeMatchingConstraint3d(Body3d body, double mass, double stiffness) : base(body)
        {
            Stiffness = stiffness;
            InvRestMatrix = Matrix3x3d.Identity;
            RestCm = Vector3d.Zero;
            double wsum = 0.0;
            int numParticles = Body.NumParticles;

            for (int i = 0; i < numParticles; i++)
            {
                RestCm += Body.Positions[i] * mass;
                wsum += mass;
            }

            RestCm /= wsum;

            Matrix3x3d A = new Matrix3x3d();

            RestPositions = new Vector3d[numParticles];

            for (int i = 0; i < numParticles; i++)
            {
                Vector3d q = Body.Positions[i] - RestCm;

                A[0, 0] += mass * q.x * q.x;
                A[0, 1] += mass * q.x * q.y;
                A[0, 2] += mass * q.x * q.z;

                A[1, 0] += mass * q.y * q.x;
                A[1, 1] += mass * q.y * q.y;
                A[1, 2] += mass * q.y * q.z;

                A[2, 0] += mass * q.z * q.x;
                A[2, 1] += mass * q.z * q.y;
                A[2, 2] += mass * q.z * q.z;

                RestPositions[i] = q;
            }

            InvRestMatrix = A.Inverse;

        }

        internal override void ConstrainPositions(double di)
        {
     
            Vector3d cm = new Vector3d(0.0, 0.0, 0.0);
            double wsum = 0.0;
            double mass = Body.ParticleMass;
            int numParticles = Body.NumParticles;

            for (int i = 0; i < numParticles; i++)
            {
                cm += Body.Predicted[i] * mass;
                wsum += mass;
            }

            cm /= wsum;

            Matrix3x3d A = new Matrix3x3d();

            for (int i = 0; i < numParticles; i++)
            {
                Vector3d q = RestPositions[i];
                Vector3d p = Body.Positions[i] - cm;

                A[0, 0] += mass * p.x * q.x;
                A[0, 1] += mass * p.x * q.y;
                A[0, 2] += mass * p.x * q.z;

                A[1, 0] += mass * p.y * q.x;
                A[1, 1] += mass * p.y * q.y;
                A[1, 2] += mass * p.y * q.z;

                A[2, 0] += mass * p.z * q.x;
                A[2, 1] += mass * p.z * q.y;
                A[2, 2] += mass * p.z * q.z;
            }

            A = A * InvRestMatrix;

            double eps = 1e-6;
            Matrix3x3d R;
            Matrix3x3dDecomposition.PolarDecompositionStable(A, eps, out R);

            //Matrix3x3d R = A;

            //Matrix3x3d R, U, D;
            //Matrix3x3dDecomposition.PolarDecomposition(A, out R, out U, out D);

            for (int i = 0; i < numParticles; i++)
            {
                Vector3d goal = cm + R * RestPositions[i];
                Body.Predicted[i] += (goal - Body.Predicted[i]) * Stiffness * di;
            }
        }

    }

}
