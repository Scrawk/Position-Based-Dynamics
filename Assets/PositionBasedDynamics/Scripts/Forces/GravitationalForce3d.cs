using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Forces
{

    public class GravitationalForce3d : ExternalForce3d
    {

        public Vector3d Gravity { get; set; }

        public GravitationalForce3d()
        {
            Gravity = new Vector3d(0.0, -9.81, 0.0);
        }

		public GravitationalForce3d(Vector3d gravity)
		{
			Gravity = gravity;
		}

        public override void ApplyForce(double dt, Body3d body)
        {
            int len = body.NumParticles;
            for (int i = 0; i < len; i++)
            {
                body.Velocities[i] += dt * Gravity;
            }

        }
    }

}
