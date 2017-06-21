using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Constraints
{

    public class DistanceConstraint3d : Constraint3d
    {

        private double RestLength;

        private double CompressionStiffness;

        private double StretchStiffness;

        private readonly int i0, i1;

        internal DistanceConstraint3d(Body3d body, int i0, int i1, double stiffness) : base(body)
        {
            this.i0 = i0;
            this.i1 = i1;

            CompressionStiffness = stiffness;
            StretchStiffness = stiffness;
            RestLength = (Body.Positions[i0] - Body.Positions[i1]).Magnitude;
        }

        internal override void ConstrainPositions(double di)
        {
            double mass = Body.ParticleMass;
            double invMass = 1.0 / mass;
            double sum = mass * 2.0;

            Vector3d n = Body.Predicted[i1] - Body.Predicted[i0];
            double d = n.Magnitude;
            n.Normalize();

            Vector3d corr;
            if (d < RestLength)
                corr = CompressionStiffness * n * (d - RestLength) * sum;
            else
                corr = StretchStiffness * n * (d - RestLength) * sum;

            Body.Predicted[i0] += invMass * corr * di;

            Body.Predicted[i1] -= invMass * corr * di;

        }

    }

}
