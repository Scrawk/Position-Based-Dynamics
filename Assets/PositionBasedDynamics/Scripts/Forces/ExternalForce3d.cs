using System;
using System.Collections;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Forces
{

    public abstract class ExternalForce3d
    {

        public abstract void ApplyForce(double dt, Body3d body);

    }

}
