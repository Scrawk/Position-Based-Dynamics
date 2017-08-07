using UnityEngine;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;
using Common.Geometry.Shapes;
using Common.Unity.Drawing;
using Common.Unity.Mathematics;

using PositionBasedDynamics.Bodies;
using PositionBasedDynamics.Bodies.Cloth;
using PositionBasedDynamics.Sources;
using PositionBasedDynamics.Forces;
using PositionBasedDynamics.Solvers;
using PositionBasedDynamics.Collisions;

namespace PositionBasedDynamics
{
    public class ClothBodyDemo : MonoBehaviour
    {

        private const double timeStep = 1.0 / 60.0;

        private const int GRID_SIZE = 10;

        public bool drawLines = true;

        public Material sphereMaterial;

        private List<GameObject> Spheres { get; set; }

        private ClothBody3d Body { get; set; }

        private Solver3d Solver { get; set; }

        private Box3d StaticBounds { get; set; }

        void Start()
        {

            double stretchStiffness = 0.25;
            double bendStiffness = 0.5;
            double mass = 1.0;
            double radius = 0.125;

            double width = 5.0;
            double height = 4.0;
            double depth = 5.0;

            TrianglesFromGrid source = new TrianglesFromGrid(radius, width, depth);

            Matrix4x4d T = Matrix4x4d.Translate(new Vector3d(0.0, height, 0.0));
            Matrix4x4d R = Matrix4x4d.Rotate(new Vector3d(0.0, 0.0, 0.0));
            Matrix4x4d RT = T * R;

            Body = new ClothBody3d(source, radius, mass, stretchStiffness, bendStiffness, RT);
            Body.Dampning = 1.0;

            Vector3d min = new Vector3d(-width / 2 - 0.1, height - 0.1, -depth / 2 - 0.1);
            Vector3d max = new Vector3d(width / 2 + 0.1, height + 0.1, -depth / 2 + 0.1);
            StaticBounds = new Box3d(min, max);

            Body.MarkAsStatic(StaticBounds);

            Solver = new Solver3d();
            Solver.AddBody(Body);
            Solver.AddForce(new GravitationalForce3d());
            Solver.AddCollision(new PlanarCollision3d(Vector3d.UnitY, 0));
            Solver.SolverIterations = 2;
            Solver.CollisionIterations = 2;
            Solver.SleepThreshold = 1;

            CreateSpheres();
        }

        void Update()
        {
            int iterations = 4;
            double dt = timeStep / iterations;

            for (int i = 0; i < iterations; i++)
                Solver.StepPhysics(dt);

            UpdateSpheres();
        }

        void OnDestroy()
        {

            if (Spheres != null)
            {
                for (int i = 0; i < Spheres.Count; i++)
                    DestroyImmediate(Spheres[i]);
            }
        }

        private void OnRenderObject()
        {
            if (drawLines)
            {
                Camera camera = Camera.current;

                Vector3 min = new Vector3(-GRID_SIZE, 0, -GRID_SIZE);
                Vector3 max = new Vector3(GRID_SIZE, 0, GRID_SIZE);

                DrawLines.DrawGrid(camera, Color.white, min, max, 1, transform.localToWorldMatrix);

                Matrix4x4d m = MathConverter.ToMatrix4x4d(transform.localToWorldMatrix);
                DrawLines.DrawVertices(LINE_MODE.TRIANGLES, camera, Color.red, Body.Positions, Body.Indices, m);

                DrawLines.DrawBounds(camera, Color.green, StaticBounds, Matrix4x4d.Identity);

            }
        }

        private void CreateSpheres()
        {
            if (sphereMaterial == null) return;

            Spheres = new List<GameObject>();

            int numParticles = Body.NumParticles;
            float diam = (float)Body.ParticleRadius * 2.0f;

            for (int i = 0; i < numParticles; i++)
            {
                Vector3 pos = MathConverter.ToVector3(Body.Positions[i]);

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.parent = transform;
                sphere.transform.position = pos;
                sphere.transform.localScale = new Vector3(diam, diam, diam);
                sphere.GetComponent<Collider>().enabled = false;
                sphere.GetComponent<MeshRenderer>().material = sphereMaterial;
                Spheres.Add(sphere);
            }

        }

        public void UpdateSpheres()
        {

            if (Spheres != null)
            {
                for (int i = 0; i < Spheres.Count; i++)
                {
                    Vector3d pos = Body.Positions[i];
                    Spheres[i].transform.position = new Vector3((float)pos.x, (float)pos.y, (float)pos.z);
                }
            }

        }

    }

}
