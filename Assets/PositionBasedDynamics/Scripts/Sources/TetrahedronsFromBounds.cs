using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;
using Common.Geometry.Shapes;

namespace PositionBasedDynamics.Sources
{

    public class TetrahedronsFromBounds : TetrahedronSource
    {

        public Box3d Bounds { get; private set; }

        public TetrahedronsFromBounds(double radius, Box3d bounds)
            : base(radius)
        {
            Bounds = bounds;
            CreateParticles();
            CreateEdges();
        }

        private void CreateParticles()
        {

            int numX = (int)(Bounds.Width / Diameter);
            int numY = (int)(Bounds.Height / Diameter);
            int numZ = (int)(Bounds.Depth / Diameter);

            Positions = new Vector3d[numX * numY * numZ];

            for (int z = 0; z < numZ; z++)
            {
                for (int y = 0; y < numY; y++)
                {
                    for (int x = 0; x < numX; x++)
                    {
                        Vector3d pos = new Vector3d();
                        pos.x = Diameter * x + Bounds.Min.x + Spacing;
                        pos.y = Diameter * y + Bounds.Min.y + Spacing;
                        pos.z = Diameter * z + Bounds.Min.z + Spacing;

                        Positions[x + y * numX + z * numX * numY] = pos;
                    }
                }
            }

            Indices = new List<int>();
            for (int z = 0; z < numZ - 1; z++)
            {
                for (int y = 0; y < numY - 1; y++)
                {
                    for (int x = 0; x < numX - 1; x++)
                    {
                        int p0 = x + y * numX + z * numY * numX;
                        int p1 = (x + 1) + y * numX + z * numY * numX;

                        int p3 = x + y * numX + (z + 1) * numY * numX;
                        int p2 = (x + 1) + y * numX + (z + 1) * numY * numX;

                        int p7 = x + (y + 1) * numX + (z + 1) * numY * numX;
                        int p6 = (x + 1) + (y + 1) * numX + (z + 1) * numY * numX;

                        int p4 = x + (y + 1) * numX + z * numY * numX;
                        int p5 = (x + 1) + (y + 1) * numX + z * numY * numX;

                        // Ensure that neighboring tetras are sharing faces
                        if ((x + y + z) % 2 == 1)
                        {
                            Indices.Add(p2); Indices.Add(p1); Indices.Add(p6); Indices.Add(p3);
                            Indices.Add(p6); Indices.Add(p3); Indices.Add(p4); Indices.Add(p7);
                            Indices.Add(p4); Indices.Add(p1); Indices.Add(p6); Indices.Add(p5);
                            Indices.Add(p3); Indices.Add(p1); Indices.Add(p4); Indices.Add(p0);
                            Indices.Add(p6); Indices.Add(p1); Indices.Add(p4); Indices.Add(p3);
                        }
                        else
                        {
                            Indices.Add(p0); Indices.Add(p2); Indices.Add(p5); Indices.Add(p1);
                            Indices.Add(p7); Indices.Add(p2); Indices.Add(p0); Indices.Add(p3);
                            Indices.Add(p5); Indices.Add(p2); Indices.Add(p7); Indices.Add(p6);
                            Indices.Add(p7); Indices.Add(p0); Indices.Add(p5); Indices.Add(p4);
                            Indices.Add(p0); Indices.Add(p2); Indices.Add(p7); Indices.Add(p5);
                        }
                    }
                }
            }

        }

    }

}