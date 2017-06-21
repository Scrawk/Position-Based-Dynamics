using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Collisions
{

    public abstract class Collision3d
    {


        internal virtual void FindContacts(IList<Body3d> bodies, List<CollisionContact3d> contacts)
        {

        }


    }

}