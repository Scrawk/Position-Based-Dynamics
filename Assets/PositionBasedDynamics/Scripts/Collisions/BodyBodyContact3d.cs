using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Collisions
{

    internal class BodyBodyContact3d : CollisionContact3d
    {

        private Body3d Body0, Body1;

        private int i0, i1;

        private double Diameter, Diameter2;

        private double Mass0, Mass1;

        internal BodyBodyContact3d(Body3d body0, int i0, Body3d body1, int i1)
        {
            Body0 = body0;
            this.i0 = i0;

            Body1 = body1;
            this.i1 = i1;

            Diameter = Body0.ParticleRadius + Body1.ParticleRadius;
            Diameter2 = Diameter * Diameter;

            double sum = Body0.ParticleMass + Body1.ParticleMass;
            Mass0 = Body0.ParticleMass / sum;
            Mass1 = Body1.ParticleMass / sum;
        }

        internal override void ResolveContact(double di)
        {
            Vector3d normal = Body0.Predicted[i0] - Body1.Predicted[i1];

            double sqLen = normal.SqrMagnitude;

            if (sqLen <= Diameter2 && sqLen > 1e-9)
            {
                double len = Math.Sqrt(sqLen);
                normal /= len;

                Vector3d delta = di * (Diameter - len) * normal;

                Body0.Predicted[i0] += delta * Mass0;
                Body0.Positions[i0] += delta * Mass0;

                Body1.Predicted[i1] -= delta * Mass1;
                Body1.Positions[i1] -= delta * Mass1;
            }
        }

    }

}