using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

namespace PositionBasedDynamics.Sources
{

    public abstract class ParticleSource
    {
        public int NumParticles { get { return Positions.Count; } }

        public IList<Vector3d> Positions { get; protected set; }

        public double Spacing { get; private set; }

        public double Diameter {  get { return Spacing * 2.0; } }

        public ParticleSource(double spacing)
        {
            Spacing = spacing;
        }

    }

}