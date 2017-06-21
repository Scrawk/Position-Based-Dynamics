using System;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{
    /// <summary>
    /// A double precision quaternion.
    /// </summary>
	[StructLayout(LayoutKind.Sequential)]
    public struct Quaternion3d
	{
		
		public double x, y, z, w;

        public readonly static Quaternion3d Identity = new Quaternion3d(0, 0, 0, 1);

        public readonly static Quaternion3d Zero = new Quaternion3d(0, 0, 0, 0);

        /// <summary>
        /// A Quaternion from varibles.
        /// </summary>
        public Quaternion3d(double x, double y, double z, double w)
		{
			this.x = x;
		    this.y = y;
			this.z = z;
			this.w = w;
		}

        /// <summary>
        /// A Quaternion copied from a array.
        /// </summary>
		public Quaternion3d(double[] v)
		{
			this.x = v[0];
		    this.y = v[1];
			this.z = v[2];
			this.w = v[3];
		}

        /// <summary>
        /// A Quaternion copied another quaternion.
        /// </summary>
		public Quaternion3d(Quaternion3d q)
		{
			this.x = q.x;
		    this.y = q.y;
			this.z = q.z;
			this.w = q.w;
		}

        /// <summary>
        /// The inverse of the quaternion.
        /// </summary>
        public Quaternion3d Inverse
        {
            get
            {
                return new Quaternion3d(-x, -y, -z, w);
            }
        }

        /// <summary>
        /// The length of the quaternion.
        /// </summary>
        double Length
        {
            get
            {
                double len = x * x + y * y + z * z + w * w;
                return Math.Sqrt(len);
            }
        }

        /// <summary>
        /// The a normalized quaternion.
        /// </summary>
        public Quaternion3d Normalized
        {
            get
            {
                double invLength = 1.0 / Length;
                return new Quaternion3d(x * invLength, y * invLength, z * invLength, w * invLength);
            }
        }

        /// <summary>
        /// Are these Quaternions equal.
        /// </summary>
        public static bool operator ==(Quaternion3d v1, Quaternion3d v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
		}
		
		/// <summary>
		/// Are these Quaternions not equal.
		/// </summary>
		public static bool operator !=(Quaternion3d v1, Quaternion3d v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w);
		}
		
		/// <summary>
		/// Are these Quaternions equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Quaternion3d)) return false;
			
			Quaternion3d v = (Quaternion3d)obj;
			
			return this == v;
		}
		
		/// <summary>
		/// Quaternions hash code. 
		/// </summary>
		public override int GetHashCode()
		{
            double hashcode = 23;
            hashcode = (hashcode * 37) + x;
            hashcode = (hashcode * 37) + y;
            hashcode = (hashcode * 37) + z;
            hashcode = (hashcode * 37) + w;

            return (int)hashcode;
        }
		
		/// <summary>
		/// Quaternion as a string.
		/// </summary>
		public override string ToString()
		{
			return "(" + x + "," + y + "," + z + "," + w + ")";
		}

        /// <summary>
        /// A Quaternion from a vector axis and angle.
        /// The axis is the up direction and the angle is the rotation.
        /// </summary>
		public Quaternion3d(Vector3d axis, double angle)
		{
		    Vector3d axisN = axis.Normalized;
		    double a = angle * 0.5;
		    double sina = Math.Sin(a);
		    double cosa = Math.Cos(a);
		    x = axisN.x * sina;
		    y = axisN.y * sina;
		    z = axisN.z * sina;
		    w = cosa;
		}

        /// <summary>
        /// A quaternion with the rotation required to
        /// rotation from the from direction to the to direction.
        /// </summary>
        public Quaternion3d(Vector3d to, Vector3d from)
		{
		    Vector3d f = from.Normalized;
		    Vector3d t = to.Normalized;
		
		    double dotProdPlus1 = 1.0 + Vector3d.Dot(f, t);
		
		    if (dotProdPlus1 < 1e-7) 
			{
		        w = 0;
		        if (Math.Abs(f.x) < 0.6) 
				{
		            double norm = Math.Sqrt(1 - f.x * f.x);
		            x = 0;
		            y = f.z / norm;
		            z = -f.y / norm;
		        } 
				else if (Math.Abs(f.y) < 0.6) 
				{
		            double norm = Math.Sqrt(1 - f.y * f.y);
		            x = -f.z / norm;
		            y = 0;
		            z = f.x / norm;
		        } 
				else 
				{
		            double norm = Math.Sqrt(1 - f.z * f.z);
		            x = f.y / norm;
		            y = -f.x / norm;
		            z = 0;
		        }
		    } 
			else 
			{
		        double s = Math.Sqrt(0.5 * dotProdPlus1);
		        Vector3d tmp = (f.Cross(t)) / (2.0 * s);
		        x = tmp.x;
		        y = tmp.y;
		        z = tmp.z;
		        w = s;
		    }
		}
		
		/// <summary>
		/// Multiply two quaternions together.
		/// </summary>
		public static Quaternion3d operator*( Quaternion3d q1, Quaternion3d q2 )
		{
			return new Quaternion3d(q2.w * q1.x  + q2.x * q1.w  + q2.y * q1.z  - q2.z * q1.y,
		                			q2.w * q1.y  - q2.x * q1.z  + q2.y * q1.w  + q2.z * q1.x,
		                			q2.w * q1.z  + q2.x * q1.y  - q2.y * q1.x  + q2.z * q1.w,
		                			q2.w * q1.w  - q2.x * q1.x  - q2.y * q1.y  - q2.z * q1.z);
		}
		
        /// <summary>
        /// Multiply a quaternion and a vector together.
        /// </summary>
		public static Vector3d operator*(Quaternion3d q, Vector3d v)
		{
		    return q.ToMatrix3x3d() * v;
		}

        /// <summary>
        /// Convert to a double precision 3 dimension matrix.
        /// </summary>
		public Matrix3x3d ToMatrix3x3d()
		{
		    double 	xx = x * x,
		         	xy = x * y,
		        	xz = x * z,
		         	xw = x * w,
		         	yy = y * y,
		         	yz = y * z,
		         	yw = y * w,
		         	zz = z * z,
		         	zw = z * w;
			
			return new Matrix3x3d
			(
			 	1.0 - 2.0 * (yy + zz), 2.0 * (xy - zw), 2.0 * (xz + yw),
			 	2.0 * (xy + zw), 1.0 - 2.0 * (xx + zz), 2.0 * (yz - xw),
			 	2.0 * (xz - yw), 2.0 * (yz + xw), 1.0 - 2.0 * (xx + yy)
			);
		}

        /// <summary>
        /// Convert to a double precision 4 dimension matrix.
        /// </summary>
		public Matrix4x4d ToMatrix4x4d()
		{
			double 	xx = x * x,
					xy = x * y,
					xz = x * z,
					xw = x * w,
					yy = y * y,
					yz = y * z,
					yw = y * w,
					zz = z * z,
					zw = z * w;
			
			return new Matrix4x4d
			(
				1.0 - 2.0 * (yy + zz), 2.0 * (xy - zw), 2.0 * (xz + yw), 0.0,
				2.0 * (xy + zw), 1.0 - 2.0 * (xx + zz), 2.0 * (yz - xw), 0.0,
				2.0 * (xz - yw), 2.0 * (yz + xw), 1.0 - 2.0 * (xx + yy), 0.0,
				0.0, 0.0, 0.0, 1.0
			);
		}

        /// <summary>
        /// The normalize the quaternion.
        /// </summary>
		public void Normalize()
		{
		    double invLength = 1.0 / Length;
		    x *= invLength; 
			y *= invLength; 
			z *= invLength; 
			w *= invLength;
		}

		private double Safe_Acos(double r)
		{
			return Math.Acos(Math.Min(1.0, Math.Max(-1.0,r)));	
		}

        /// <summary>
        /// Slerp the quaternion by t.
        /// </summary>
		public Quaternion3d Slerp(Quaternion3d from, Quaternion3d to, double t)
		{
		    if (t <= 0) 
			{
                return new Quaternion3d(from);
		    } 
			else if (t >= 1) 
			{
                return new Quaternion3d(to);
		    } 
			else 
			{
		        double cosom = from.x * to.x + from.y * to.y + from.z * to.z + from.w * to.w;
		        double absCosom = Math.Abs(cosom);
		
		        double scale0;
		        double scale1;
		
		        if ((1 - absCosom) > 1e-6) 
				{
		            double omega = Safe_Acos(absCosom);
		            double sinom = 1.0 / Math.Sin( omega );
		            scale0 = Math.Sin( ( 1.0 - t ) * omega ) * sinom;
		            scale1 = Math.Sin( t * omega ) * sinom;
		        } 
				else 
				{
		            scale0 = 1 - t;
		            scale1 = t;
		        }

                Quaternion3d res = new Quaternion3d(scale0 * from.x + scale1 * to.x,
	                                                scale0 * from.y + scale1 * to.y,
	                                                scale0 * from.z + scale1 * to.z,
	                                                scale0 * from.w + scale1 * to.w);
				
		        return res.Normalized;
		    }
		}

        /// <summary>
        /// Create a rotation out of a vector.
        /// </summary>
        public static Quaternion3d FromEuler(Vector3d euler)
        {
            double degToRad = Math.PI / 180.0;

            double yaw = euler.x * degToRad;
            double pitch = euler.y * degToRad;
            double roll = euler.z * degToRad;
            double rollOver2 = roll * 0.5;
            double sinRollOver2 = Math.Sin(rollOver2);
            double cosRollOver2 = Math.Cos(rollOver2);
            double pitchOver2 = pitch * 0.5;
            double sinPitchOver2 = Math.Sin(pitchOver2);
            double cosPitchOver2 = Math.Cos(pitchOver2);
            double yawOver2 = yaw * 0.5;
            double sinYawOver2 = Math.Sin(yawOver2);
            double cosYawOver2 = Math.Cos(yawOver2);

            Quaternion3d result;
            result.x = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.w = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }

    }

}
























