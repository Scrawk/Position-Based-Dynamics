using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

using PositionBasedDynamics.Bodies;

namespace PositionBasedDynamics.Constraints
{

    public class DihedralConstraint3d : Constraint3d
    {

        private double BendStiffness;

        private double RestAngle;

        private readonly int i0, i1, i2, i3;

        internal DihedralConstraint3d(Body3d body, int i0, int i1, int i2, int i3, float stiffness) : base(body)
        {

            this.i0 = i0;
            this.i1 = i1;
            this.i2 = i2;
            this.i3 = i3;
            BendStiffness = stiffness;

	        Vector3d p0 = body.Positions[i0];
            Vector3d p1 = body.Positions[i1];
            Vector3d p2 = body.Positions[i2];
            Vector3d p3 = body.Positions[i3];

	        Vector3d n1 = (p2 - p0).Cross(p3 - p0);
            n1 /= n1.SqrMagnitude;

	        Vector3d n2 = (p3 - p1).Cross(p2 - p1);
            n2 /= n2.SqrMagnitude;

	        n1.Normalize();
	        n2.Normalize();
	        double dot = Vector3d.Dot(n1, n2);

	        if (dot < -1.0) dot = -1.0;
	        if (dot > 1.0) dot = 1.0;

	        RestAngle = Math.Acos(dot);

        }

        internal override void ConstrainPositions(double di)
        {

            // derivatives from Bridson, Simulation of Clothing with Folds and Wrinkles
            // his modes correspond to the derivatives of the bending angle arccos(n1 dot n2) with correct scaling

            Vector3d p0 = Body.Predicted[i0];
            Vector3d p1 = Body.Predicted[i1];
            Vector3d p2 = Body.Predicted[i2];
            Vector3d p3 = Body.Predicted[i3];

            double invMass = 1.0 / Body.ParticleMass;

            Vector3d e = p3 - p2;
            double elen = e.Magnitude;
            if (elen < 1e-9) return;

            double invElen = 1.0 / elen;

            Vector3d n1 = (p2 - p0).Cross(p3 - p0); 
            n1 /= n1.SqrMagnitude;

            Vector3d n2 = (p3 - p1).Cross(p2 - p1);
            n2 /= n2.SqrMagnitude;

            Vector3d d0 = elen * n1;
            Vector3d d1 = elen * n2;
            Vector3d d2 = Vector3d.Dot(p0 - p3, e) * invElen * n1 + Vector3d.Dot(p1 - p3, e) * invElen * n2;
            Vector3d d3 = Vector3d.Dot(p2 - p0, e) * invElen * n1 + Vector3d.Dot(p2 - p1, e) * invElen * n2;

            n1.Normalize();
            n2.Normalize();
            double dot = Vector3d.Dot(n1, n2);

            if (dot < -1.0) dot = -1.0;
            if (dot > 1.0) dot = 1.0;
			double phi = Math.Acos(dot);

            // fast approximation
            //double phi = (-0.6981317 * dot * dot - 0.8726646) * dot + 1.570796;	

            double lambda = (d0.SqrMagnitude + d1.SqrMagnitude + d2.SqrMagnitude + d3.SqrMagnitude) * invMass;

            if (lambda == 0.0) return;

            double stiffness = BendStiffness;

            // stability
            // 1.5 is the largest magic number I found to be stable in all cases :-)
            //if (stiffness > 0.5 && Math.Abs(phi - RestAngle) > 1.5)		
            //	stiffness = 0.5;

            lambda = (phi - RestAngle) / lambda * stiffness;

            if (Vector3d.Dot(Vector3d.Cross(n1, n2), e) > 0.0)
                lambda = -lambda;

            Body.Predicted[i0] += -invMass * lambda * d0 * di;
            Body.Predicted[i1] += -invMass * lambda * d1 * di;
            Body.Predicted[i2] += -invMass * lambda * d2 * di;
            Body.Predicted[i3] += -invMass * lambda * d3 * di;

        }

    }

}
