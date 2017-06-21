using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Constraints
{

    public class StaticConstraint3d : Constraint3d
    {

        private readonly int i0;

        private Vector3d Position { get; set; }

        internal StaticConstraint3d(Body3d body, int i) : base(body)
        {
            i0 = i;
            Position = body.Positions[i];
        }

        internal override void ConstrainPositions(double di)
        {
            Body.Positions[i0] = Position;
            Body.Predicted[i0] = Position;
        }

    }

}
