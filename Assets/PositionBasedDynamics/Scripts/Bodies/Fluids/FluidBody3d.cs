using System;
using System.Collections.Generic;

using Common.Geometry.Shapes;
using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Collisions;
using PositionBasedDynamics.Sources;
using PositionBasedDynamics.Constraints;

namespace PositionBasedDynamics.Bodies.Fluids
{

    public class FluidBody3d : Body3d
    {

        public double Density { get; set; }

        public double Viscosity { get; set; }

        public double[] Lambda { get; private set; }

        internal CubicKernel3d Kernel { get; private set; }

        internal ParticleHash3d Hash { get; private set; }

        internal double[] Densities { get; private set; }

        public FluidBody3d(ParticleSource source, double radius, double density, Matrix4x4d RTS)
            : base(source.NumParticles, radius, 1.0)
        {
            Density = density;
            Viscosity = 0.02;
            Dampning = 0;

            double d = ParticleDiameter;
            ParticleMass = 0.8 * d * d * d * Density;

            CreateParticles(source, RTS);

            double cellSize = ParticleRadius * 4.0;
            Kernel = new CubicKernel3d(cellSize);
            Hash = new ParticleHash3d(NumParticles, cellSize);

            Lambda = new double[NumParticles];
            Densities = new double[NumParticles];
        }

        public void AddBoundry(FluidBoundary3d boundry)
        {
            FluidConstraint3d constraint = new FluidConstraint3d(this, boundry);
            Constraints.Add(constraint);
        }

        internal void Reset()
        {
            for (int i = 0; i < NumParticles; i++)
            {
                Lambda[i] = 0.0;
                Densities[i] = 0.0;
            }
        }

        public void RandomizePositionOrder(Random rnd)
        {
            for(int i = 0; i < NumParticles; i++)
            {
                Vector3d tmp = Positions[i];

                int idx = rnd.Next(0, NumParticles - 1);
                Positions[i] = Positions[idx];
                Positions[idx] = tmp;
            }

            Array.Copy(Positions, Predicted, NumParticles);
        }

        internal void ComputeViscosity()
        {
            int[,] neighbors = Hash.Neighbors;
            int[] numNeighbors = Hash.NumNeighbors;

            double viscosityMulMass = Viscosity * ParticleMass;

            // Compute viscosity forces (XSPH) 
            for (int i = 0; i < NumParticles; i++)
            {
                //Viscosity for particle Pi. Modifies the velocity.
                //Vi = Vi + c * SUMj Vij * W(Pi - Pj, h)
                Vector3d pi = Predicted[i];

                for (int j = 0; j < numNeighbors[i]; j++)
                {
                    int neighborIndex = neighbors[i, j];
                    if (neighborIndex < NumParticles) // Test if fluid particle
                    {
                        double invDensity = 1.0 / Densities[neighborIndex];
                        Vector3d pn = Predicted[neighborIndex];

                        double k = Kernel.W(pi.x - pn.x, pi.y - pn.y, pi.z - pn.z) * viscosityMulMass * invDensity;
                        Velocities[i] -= k * (Velocities[i] - Velocities[neighborIndex]);
                    }
                }
            }

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