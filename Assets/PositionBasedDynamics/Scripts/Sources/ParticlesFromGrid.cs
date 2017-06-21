using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;
using Common.Geometry.Shapes;

namespace PositionBasedDynamics.Sources
{

    public class ParticlesFromGrid : ParticleSource
    {

        public double Width { get; private set; }

        public double Height { get; private set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public ParticlesFromGrid(double spacing, double width, double height) : base(spacing)
        {

            Width = width;
            Height = height;
            Rows = (int)(width / Diameter);
            Columns = (int)(height / Diameter);

            CreateParticles();
        }

        private void CreateParticles()
        {

 
            Positions = new Vector3d[(Rows + 1) * (Columns + 1)];

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

        }

    }

}