using System;
using System.Collections.Generic;

using Common.Geometry.Shapes;
using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Constraints;
using PositionBasedDynamics.Sources;

namespace PositionBasedDynamics.Bodies.Cloth
{

    public class ClothBody3d : Body3d
    {

        public double StretchStiffness { get; private set;  }

		public double BendStiffness { get; private set;  }

        public int[] Indices { get; private set; }

		public ClothBody3d(TrianglesFromGrid source, double radius, double mass, double stretchStiffness, double bendStiffness, Matrix4x4d RTS)
            : base(source.NumParticles, radius, mass)
        {

			StretchStiffness = stretchStiffness;
			BendStiffness = bendStiffness;

            CreateParticles(source, RTS);
			CreateConstraints(source.Rows, source.Columns);

        }

        private void CreateParticles(TrianglesFromGrid source, Matrix4x4d RTS)
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

        private void CreateConstraints(int nRows, int nCols)
        {

            int height = nCols + 1;
            int width = nRows + 1;

            // Horizontal
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < (width - 1); x++)
                {
                    Constraints.Add(new DistanceConstraint3d(this, y * width + x, y * width + x + 1, StretchStiffness));
                }
            }

            // Vertical
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < (height - 1); y++)
                {
                    Constraints.Add(new DistanceConstraint3d(this, y * width + x, (y + 1) * width + x, StretchStiffness));
                }
            }


            // Shearing distance constraint
            for (int y = 0; y < (height - 1); y++)
            {
                for (int x = 0; x < (width - 1); x++)
                {
                    Constraints.Add(new DistanceConstraint3d(this, y * width + x, (y + 1) * width + x + 1, StretchStiffness));
                    Constraints.Add(new DistanceConstraint3d(this, (y + 1) * width + x, y * width + x + 1, StretchStiffness));
                }
            }

            //add vertical constraints
            for (int i = 0; i <= nRows; i++)
            {
                for (int j = 0; j < nCols - 1; j++)
                {
                    int i0 = j * (nRows + 1) + i;
                    int i1 = (j + 1) * (nRows + 1) + i;
                    int i2 = (j + 2) * (nRows + 1) + i;

                    Constraints.Add(new BendingConstraint3d(this, i0, i1, i2, BendStiffness));
                }
            }

            //add horizontal constraints
            for (int i = 0; i < nRows - 1; i++)
            {
                for (int j = 0; j <= nCols; j++)
                {
                    int i0 = j * (nRows + 1) + i;
                    int i1 = j * (nRows + 1) + (i + 1);
                    int i2 = j * (nRows + 1) + (i + 2);

                    Constraints.Add(new BendingConstraint3d(this, i0, i1, i2, BendStiffness));
                }
            }

        }

    }

}