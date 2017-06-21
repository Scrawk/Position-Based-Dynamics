using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Constraints;
using PositionBasedDynamics.Sources;

namespace PositionBasedDynamics.Bodies.Deformable
{

    public class DeformableBody3d : Body3d
    {

        public int[] Indices { get; private set; }

        public double Stiffness { get; private set; }

        public DeformableBody3d(TetrahedronSource source, double radius, double mass, double stiffness, Matrix4x4d RTS)
            : base(source.NumParticles, radius, mass)
        {
            Stiffness = stiffness;

            CreateParticles(source, RTS);
            CreateConstraints();
        }

        private void CreateParticles(TetrahedronSource source, Matrix4x4d RTS)
        {

            for (int i = 0; i < NumParticles; i++)
            {
                Vector4d pos = RTS * source.Positions[i].xyz1;
                Positions[i] = new Vector3d(pos.x, pos.y, pos.z);
                Predicted[i] = Positions[i];
            }

            int numIndices = source.NumIndices;
            Indices = new int[numIndices];

            for (int i = 0; i < numIndices; i++)
                Indices[i] = source.Indices[i];

        }

        private void CreateConstraints()
        {

            int numTets = Indices.Length / 4;
            Constraints.Capacity = numTets;

            for (int i = 0; i < numTets; i++)
            {
                int v1 = Indices[4 * i + 0];
                int v2 = Indices[4 * i + 1];
                int v3 = Indices[4 * i + 2];
                int v4 = Indices[4 * i + 3];

                Constraints.Add(new StrainTetConstraint3d(this, v1, v2, v3, v4, Stiffness));

                //Constraints.Add( new FEMTetConstraint3d(this, v1, v2, v3, v4, Stiffness));
            }

        }

    }

}