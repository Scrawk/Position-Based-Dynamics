using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Constraints;
using PositionBasedDynamics.Sources;

namespace PositionBasedDynamics.Bodies.Ridgid
{

    public class RidgidBody3d : Body3d
    {

        public double Stiffness { get; private set; }

        public RidgidBody3d(ParticleSource source, double radius, double mass, Matrix4x4d RTS)
            : base(source.NumParticles, radius, mass)
        {
            Stiffness = 1.0;

            CreateParticles(source, RTS);
            Constraints.Add(new ShapeMatchingConstraint3d(this, mass, Stiffness));
        }

        private void CreateParticles(ParticleSource source, Matrix4x4d RTS)
        {

            for (int i = 0; i < NumParticles; i++)
            {
                Vector4d pos = RTS * source.Positions[i].xyz1;
                Positions[i] = new Vector3d(pos.x, pos.y, pos.z);
                Predicted[i] = Positions[i];
            }

        }

    }

}