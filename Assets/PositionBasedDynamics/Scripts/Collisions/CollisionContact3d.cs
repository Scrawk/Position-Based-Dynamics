using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Collisions
{

    internal abstract class CollisionContact3d
    {

        internal abstract void ResolveContact(double di);

    }

}