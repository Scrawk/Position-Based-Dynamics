using System;
using System.Collections.Generic;

using Common.Geometry.Shapes;
using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Collisions;
using PositionBasedDynamics.Sources;

namespace PositionBasedDynamics.Bodies.Fluids
{

    public class FluidBoundary3d
    {

        public Vector3d[] Positions { get; private set; }

        public double[] Psi { get; private set; } 

        public double ParticleRadius { get; private set; }

        public double ParticleDiameter { get { return ParticleRadius * 2.0; } }

        public double Density { get; private set; }

        public int NumParticles { get; private set;  }

        public FluidBoundary3d(ParticleSource source, double radius, double density, Matrix4x4d RTS)
        {
            ParticleRadius = radius;
            Density = density;

            CreateParticles(source, RTS);
            CreateBoundryPsi();
        }

        private void CreateParticles(ParticleSource source, Matrix4x4d RTS)
        {
            NumParticles = source.NumParticles;

            Positions = new Vector3d[NumParticles];
  
            for (int i = 0; i < NumParticles; i++)
            {
                Vector4d pos = RTS * source.Positions[i].xyz1;
                Positions[i] = new Vector3d(pos.x, pos.y, pos.z);
            }

        }

        private void CreateBoundryPsi()
        {

            Psi = new double[NumParticles];

            double cellSize = ParticleRadius * 4.0;

            ParticleHash3d hash = new ParticleHash3d(NumParticles, cellSize);
            hash.NeighborhoodSearch(Positions);

            int[,] neighbors = hash.Neighbors;
            int[] numNeighbors = hash.NumNeighbors;

            CubicKernel3d kernel = new CubicKernel3d(cellSize);

            for (int i = 0; i < NumParticles; i++)
            {
                double delta = kernel.W_zero;

                for (int j = 0; j < numNeighbors[i]; j++)
                {
                    int neighborIndex = neighbors[i, j];

                    Vector3d p = Positions[i] - Positions[neighborIndex];

                    delta += kernel.W(p.x, p.y, p.z);
                }

                double volume = 1.0 / delta;

                Psi[i] = Density * volume;
            }

        }

    }

}