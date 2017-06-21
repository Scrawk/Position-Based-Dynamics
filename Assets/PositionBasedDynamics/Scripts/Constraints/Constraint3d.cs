using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Constraints
{

    public abstract class Constraint3d
    {

        protected Body3d Body { get; private set; }

        internal Constraint3d(Body3d body)
        {
            Body = body;
        }

        internal virtual void ConstrainPositions(double di)
        {

        }

        internal virtual void ConstrainVelocities()
        {

        }

    }
    
}