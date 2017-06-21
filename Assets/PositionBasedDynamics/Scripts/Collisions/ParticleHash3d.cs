using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

namespace PositionBasedDynamics.Collisions
{

    public class ParticleHash3d
    {

        private class HashEntry
        {
            public int TimeStamp;

            readonly public List<int> Indices;

            public HashEntry(int capacity, int timeStamp)
            {
                Indices = new List<int>(capacity);
                TimeStamp = timeStamp;
            }
        }

        public int[,] Neighbors { get; private set; }

        public int[] NumNeighbors { get; private set; }

        public int NumParticles { get; private set; }

        private int MaxNeighbors { get; set; }

        private int MaxParticlesPerCell { get; set; }

        private int[,] Indexes { get; set; }

        private double CellSize { get; set; }

        private double InvCellSize { get; set; }

        private int CurrentTimeStamp { get; set; }

        private Dictionary<int, HashEntry> GridMap { get; set; }

        public ParticleHash3d(int numParticles, double cellSize, int maxNeighbors = 60, int maxParticlesPerCell = 50)
        {

            NumParticles = numParticles;
            MaxParticlesPerCell = maxParticlesPerCell;
            MaxNeighbors = maxNeighbors;
            CurrentTimeStamp = 0;

            CellSize = cellSize;
            InvCellSize = 1.0 / CellSize;

            GridMap = new Dictionary<int, HashEntry>();

            NumNeighbors = new int[NumParticles];
            Neighbors = new int[NumParticles, MaxNeighbors];

            Indexes = new int[27, 3];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        int i = x + y * 3 + z * 9;

                        Indexes[i, 0] = x;
                        Indexes[i, 1] = y;
                        Indexes[i, 2] = z;
                    }
                }
            }
        }

        public void IncrementTimeStamp()
        {
            CurrentTimeStamp++;
        }

        int Floor(double v)
        {
            return (int)(v * InvCellSize + 32768.1) - 32768;
        }

        void Floor(Vector3d v, out int pos1, out int pos2, out int pos3)
        {
            pos1 = (int)(v.x * InvCellSize + 32768.1) - 32768;
            pos2 = (int)(v.y * InvCellSize + 32768.1) - 32768;
            pos3 = (int)(v.z * InvCellSize + 32768.1) - 32768;
        }

        int Hash(int x, int y, int z)
        {
            int p1 = 73856093 * x;
            int p2 = 19349663 * y;
            int p3 = 83492791 * z;
            return p1 + p2 + p3;
        }

        int Hash(Vector3d particle)
        {
            int x = (int)(particle.x * InvCellSize + 32768.1) - 32768 + 1;
            int y = (int)(particle.y * InvCellSize + 32768.1) - 32768 + 1;
            int z = (int)(particle.z * InvCellSize + 32768.1) - 32768 + 1;

            int p1 = 73856093 * x;
            int p2 = 19349663 * y;
            int p3 = 83492791 * z;
            return p1 + p2 + p3;
        }

        void AddToGrid(int i, Vector3d particle)
        {

            int cellPos = Hash(particle);
            HashEntry entry = null;

            if (GridMap.TryGetValue(cellPos, out entry))
            {
                if (entry.TimeStamp != CurrentTimeStamp)
                {
                    entry.TimeStamp = CurrentTimeStamp;
                    entry.Indices.Clear();
                }
            }
            else
            {
                entry = new HashEntry(MaxParticlesPerCell, CurrentTimeStamp);
                GridMap.Add(cellPos, entry);
            }

            entry.Indices.Add(i);
        }

        public void NeighborhoodSearch(Vector3d[] particles)
        {

            if (particles.Length > NumParticles)
                throw new ArgumentException("Particle array length larger than expected");

            double r2 = CellSize * CellSize;

            for (int i = 0; i < NumParticles; i++)
            {
                AddToGrid(i, particles[i]);
            }

            for (int i = 0; i < NumParticles; i++)
            {
                NumNeighbors[i] = 0;

                Vector3d p0 = particles[i];

                int cellPos1, cellPos2, cellPos3;
                Floor(p0, out cellPos1, out cellPos2, out cellPos3);

                for (int j = 0; j < 27; j++)
                {
                    int cellPos = Hash(cellPos1 + Indexes[j, 0], cellPos2 + Indexes[j, 1], cellPos3 + Indexes[j, 2]);

                    HashEntry entry = null;
                    GridMap.TryGetValue(cellPos, out entry);

                    if (entry != null && entry.TimeStamp == CurrentTimeStamp)
                    {
                        int count = entry.Indices.Count;
                        for (int m = 0; m < count; m++)
                        {
                            int pi = entry.Indices[m];
                            if (pi == i) continue;

                            Vector3d p;
                            p.x = p0.x - particles[pi].x;
                            p.y = p0.y - particles[pi].y;
                            p.z = p0.z - particles[pi].z;

                            double dist2 = p.x * p.x + p.y * p.y + p.z * p.z;

                            if (dist2 < r2)
                            {
                                if (NumNeighbors[i] < MaxNeighbors)
                                    Neighbors[i, NumNeighbors[i]++] = pi;
                                else
                                    throw new InvalidOperationException("too many neighbors detected");
                            }
                        }
                    }
                }
            }

            //end of function
        }

        public void NeighborhoodSearch(Vector3d[] particles, Vector3d[] boundary)
        {

            if (particles.Length > NumParticles)
                throw new ArgumentException("Particle array length larger than expected");

            //double invCellSize = 1.0 / CellSize;
            double r2 = CellSize * CellSize;

            for (int i = 0; i < NumParticles; i++)
            {
                AddToGrid(i, particles[i]);
            }

            for (int i = 0; i < boundary.Length; i++)
            {
                AddToGrid(NumParticles + i, boundary[i]);
            }

            for (int i = 0; i < NumParticles; i++)
            {
                Vector3d p0 = particles[i];
                NumNeighbors[i] = 0;

                int cellPos1, cellPos2, cellPos3;
                Floor(particles[i], out cellPos1, out cellPos2, out cellPos3);

                for (int j = 0; j < 27; j++)
                {
                    int cellPos = Hash(cellPos1 + Indexes[j, 0], cellPos2 + Indexes[j, 1], cellPos3 + Indexes[j, 2]);

                    HashEntry entry = null;
                    GridMap.TryGetValue(cellPos, out entry);

                    if (entry != null && entry.TimeStamp == CurrentTimeStamp)
                    {
                        int count = entry.Indices.Count;
                        for (int m = 0; m < count; m++)
                        {
                            int pi = entry.Indices[m];
                            if (pi == i) continue;

                            double dist2 = 0.0;

                            if (pi < NumParticles)
                            {
                                Vector3d p;
                                p.x = p0.x - particles[pi].x;
                                p.y = p0.y - particles[pi].y;
                                p.z = p0.z - particles[pi].z;

                                dist2 = p.x * p.x + p.y * p.y + p.z * p.z;
                            }
                            else
                            {
                                int bi = pi - NumParticles;

                                Vector3d p;
                                p.x = p0.x - boundary[bi].x;
                                p.y = p0.y - boundary[bi].y;
                                p.z = p0.z - boundary[bi].z;

                                dist2 = p.x * p.x + p.y * p.y + p.z * p.z;
                            }

                            if (dist2 < r2)
                            {
                                if (NumNeighbors[i] < MaxNeighbors)
                                    Neighbors[i, NumNeighbors[i]++] = pi;
                                else
                                    throw new InvalidOperationException("too many neighbors detected");
                            }
                        }
                    }
                }
            }

            //End of function
        }
    }


}