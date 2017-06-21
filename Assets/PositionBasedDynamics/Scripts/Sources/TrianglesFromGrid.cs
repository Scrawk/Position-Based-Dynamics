using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;
using Common.Geometry.Shapes;

namespace PositionBasedDynamics.Sources
{

    public class TrianglesFromGrid : TriangleSource
    {

        public double Width { get; private set; }

        public double Height { get; private set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public TrianglesFromGrid(double spacing, double width, double height) : base(spacing)
        {

            Width = width;
            Height = height;
            Rows = (int)(width / Diameter);
            Columns = (int)(height / Diameter);

            CreateParticles();
            CreateEdges();
        }

        private void CreateParticles()
        {

            Positions = new Vector3d[(Rows + 1) * (Columns + 1)];
            Indices = new int[Rows * Columns * 2 * 3];

            double dx = Width / Rows;
            double dy = Height / Columns;

            int index = 0;
            for (int j = 0; j <= Columns; j++)
            {
                for (int i = 0; i <= Rows; i++)
                {
                    double x = dx * i;
                    double z = dy * j;

                    Vector3d pos = new Vector3d(x - Width / 2, 0, z - Height / 2);

                    Positions[index] = pos;
                    index++;
                }
            }

            index = 0;
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    int i0 = i * (Rows + 1) + j;
                    int i1 = i0 + 1;
                    int i2 = i0 + (Rows + 1);
                    int i3 = i2 + 1;

                    if ((j + i) % 2 != 0)
                    {
                        Indices[index++] = i0;
                        Indices[index++] = i2;
                        Indices[index++] = i1;
                        Indices[index++] = i1;
                        Indices[index++] = i2;
                        Indices[index++] = i3;
                    }
                    else
                    {
                        Indices[index++] = i0;
                        Indices[index++] = i2;
                        Indices[index++] = i3;
                        Indices[index++] = i0;
                        Indices[index++] = i3;
                        Indices[index++] = i1;
                    }
                }
            }

        }

    }

}