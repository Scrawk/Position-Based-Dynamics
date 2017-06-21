using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Collisions
{
    public class PlanarCollision3d : Collision3d
    {

        private Vector3d Normal { get; set; }

        private double Distance { get; set; }

        public PlanarCollision3d(Vector3d normal, float distance)
        {
            Normal = normal.Normalized;

            Distance = distance;
        }

        internal override void FindContacts(IList<Body3d> bodies, List<CollisionContact3d> contacts)
        {

            for (int j = 0; j < bodies.Count; j++)
            {
                Body3d body = bodies[j];

                int numParticles = body.NumParticles;
                double radius = body.ParticleRadius;

                for (int i = 0; i < numParticles; i++)
                {
                    double d = Vector3d.Dot(Normal, body.Predicted[i]) + Distance - radius;

                    if (d < 0.0)
                        contacts.Add(new BodyPlaneContact3d(body, i, Normal, Distance));
                }
            }

        }

    }
}