using UnityEngine;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;
using Common.Geometry.Shapes;
using Common.Unity.Cameras;
using Common.Unity.Drawing;
using Common.Unity.Mathematics;

using PositionBasedDynamics.Bodies;
using PositionBasedDynamics.Bodies.Ridgid;
using PositionBasedDynamics.Sources;
using PositionBasedDynamics.Forces;
using PositionBasedDynamics.Solvers;
using PositionBasedDynamics.Collisions;

namespace PositionBasedDynamics
{
    public class RidgidBodyDemo : MonoBehaviour
    {

        private const double timeStep = 1.0 / 60.0;

        private const int GRID_SIZE = 10;

        public Material sphereMaterial;

        private Solver3d Solver { get; set; }

        private List<List<GameObject>> Spheres { get; set; }

        void Start()
        {

            Matrix4x4d T, R;

            double spacing = 0.125;
            double radius = spacing;
            double mass = 1.0;
            System.Random rnd = new System.Random(0);

            Vector3d min = new Vector3d(-1.0);
            Vector3d max = new Vector3d(1.0);
            Box3d bounds = new Box3d(min, max);

            ParticlesFromBounds source = new ParticlesFromBounds(spacing, bounds);

            T = Matrix4x4d.Translate(new Vector3d(0.0, 4.0, 0.0));
            R = Matrix4x4d.Rotate(new Vector3d(0.0, 0.0, 25.0));

            RidgidBody3d body = new RidgidBody3d(source, radius, mass, T * R);
            body.Dampning = 1.0;
            body.RandomizePositions(rnd, radius * 0.01);

            Solver = new Solver3d();
            Solver.AddBody(body);
            Solver.AddForce(new GravitationalForce3d());
            Solver.AddCollision(new PlanarCollision3d(Vector3d.UnitY, 0));
            Solver.SolverIterations = 2;
            Solver.CollisionIterations = 2;
            Solver.SleepThreshold = 1;
    
            CreateSpheres();

            RenderEvent.AddRenderEvent(Camera.main, DrawOutline);
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
            RenderEvent.RemoveRenderEvent(Camera.main, DrawOutline);
        }

        private void DrawOutline(Camera camera)
        {
            Vector3 min = new Vector3(-GRID_SIZE, 0, -GRID_SIZE);
            Vector3 max = new Vector3(GRID_SIZE, 0, GRID_SIZE);

            DrawLines.DrawGrid(camera, Color.white, min, max, 1, transform.localToWorldMatrix);

            for (int j = 0; j < Solver.Bodies.Count; j++)
            {
                Body3d body = Solver.Bodies[j];
                //DrawLines.DrawBounds(camera, Color.green, body.Bounds, Matrix4x4d.Identity);
            }

        }

        private void CreateSpheres()
        {

            if (sphereMaterial == null) return;

            Spheres = new List<List<GameObject>>();

            for (int j = 0; j < Solver.Bodies.Count; j++)
            {
                Body3d body = Solver.Bodies[j];

                int numParticles = body.NumParticles;
                float diam = (float)body.ParticleDiameter;

                List<GameObject> spheres = new List<GameObject>(numParticles);

                for (int i = 0; i < numParticles; i++)
                {
                    Vector3 pos = MathConverter.ToVector3(body.Positions[i]);

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.parent = transform;
                    sphere.transform.position = pos;
                    sphere.transform.localScale = new Vector3(diam, diam, diam);
                    sphere.GetComponent<Collider>().enabled = false;
                    sphere.GetComponent<MeshRenderer>().material = sphereMaterial;
                    spheres.Add(sphere);
                }


                Spheres.Add(spheres);
            }

        }

        public void UpdateSpheres()
        {

            if (Spheres != null)
            {
                for (int j = 0; j < Solver.Bodies.Count; j++)
                {
                    Body3d body = Solver.Bodies[j];
                    List<GameObject> spheres = Spheres[j];

                    for (int i = 0; i < spheres.Count; i++)
                    {
                        Vector3d pos = body.Positions[i];
                        spheres[i].transform.position = new Vector3((float)pos.x, (float)pos.y, (float)pos.z);
                    }
                }
            }

        }

    }

}
