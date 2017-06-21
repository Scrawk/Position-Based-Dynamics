using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;
using Common.Geometry.Shapes;

namespace PositionBasedDynamics.Sources
{

    public abstract class TetrahedronSource : ParticleSource
    {

        public int NumIndices { get { return Indices.Count; } }

        public IList<int> Indices { get; protected set; }

        public int NumEdges { get { return Edges.Count; } }

        public IList<int> Edges { get; protected set; }

        public TetrahedronSource(double radius): base(radius)
        {

        }

        protected void CreateEdges()
        {
            int[,] edges = new int[,]
            {
                {0,1}, {0,2}, {0,3},
                {1,2}, {3,2}, {1,3}
            };

            int numTets = Indices.Count / 4;

            Edges = new List<int>();
            HashSet<Vector2i> set = new HashSet<Vector2i>();
            HashSet<Vector2i> rset = new HashSet<Vector2i>();

            for (int n = 0; n < numTets; n++)
            {
                for (int i = 0; i < 6; i++)
                {
                    int i0 = Indices[4 * n + edges[i, 0]];
                    int i1 = Indices[4 * n + edges[i, 1]];

                    Vector2i edge = new Vector2i(i0, i1);
                    Vector2i redge = new Vector2i(i1, i0);

                    if (!set.Contains(edge) && !rset.Contains(redge))
                    {
                        set.Add(edge);
                        rset.Add(redge);
                        Edges.Add(i0);
                        Edges.Add(i1);
                    }
                }
            }
        }
    }

}