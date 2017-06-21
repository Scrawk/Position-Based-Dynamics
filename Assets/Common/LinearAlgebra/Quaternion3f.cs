using System;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{
    /// <summary>
    /// A single precision quaternion.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion3f
    {

        public float x, y, z, w;

        public readonly static Quaternion3f Identity = new Quaternion3f(0, 0, 0, 1);

        public readonly static Quaternion3f Zero = new Quaternion3f(0, 0, 0, 0);

        /// <summary>
        /// A Quaternion from varibles.
        /// </summary>
        public Quaternion3f(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// A Quaternion copied from a array.
        /// </summary>
        public Quaternion3f(float[] v)
        {
            this.x = v[0];
            this.y = v[1];
            this.z = v[2];
            this.w = v[3];
        }

        /// <summary>
        /// A Quaternion copied another quaternion.
        /// </summary>
        public Quaternion3f(Quaternion3f q)
        {
            this.x = q.x;
            this.y = q.y;
            this.z = q.z;
            this.w = q.w;
        }

        /// <summary>
        /// The inverse of the quaternion.
        /// </summary>
        public Quaternion3f Inverse
        {
            get
            {
                return new Quaternion3f(-x, -y, -z, w);
            }
        }

        /// <summary>
        /// The length of the quaternion.
        /// </summary>
        float Length
        {
            get
            {
                float len = x * x + y * y + z * z + w * w;
                return (float)Math.Sqrt(len);
            }
        }

        /// <summary>
        /// The a normalized quaternion.
        /// </summary>
        public Quaternion3f Normalized
        {
            get
            {
                float invLength = 1.0f / Length;
                return new Quaternion3f(x * invLength, y * invLength, z * invLength, w * invLength);
            }
        }

        /// <summary>
        /// Are these Quaternions equal.
        /// </summary>
        public static bool operator ==(Quaternion3f v1, Quaternion3f v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
		}
		
		/// <summary>
		/// Are these Quaternions not equal.
		/// </summary>
		public static bool operator !=(Quaternion3f v1, Quaternion3f v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w);
		}
		
		/// <summary>
		/// Are these Quaternions equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Quaternion3f)) return false;
			
			Quaternion3f v = (Quaternion3f)obj;
			
			return this == v;
		}
		
		/// <summary>
		/// Quaternions hash code. 
		/// </summary>
		public override int GetHashCode()
		{
            float hashcode = 23;
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
        public Quaternion3f(Vector3f axis, float angle)
        {
            Vector3f axisN = axis.Normalized;
            float a = angle * 0.5f;
			float sina = (float)Math.Sin(a);
			float cosa = (float)Math.Cos(a);
            x = axisN.x * sina;
            y = axisN.y * sina;
            z = axisN.z * sina;
            w = cosa;
        }

        /// <summary>
        /// A quaternion with the rotation required to
        /// rotation from the from direction to the to direction.
        /// </summary>
        public Quaternion3f(Vector3f to, Vector3f from)
        {
            Vector3f f = from.Normalized;
            Vector3f t = to.Normalized;

            float dotProdPlus1 = 1.0f + Vector3f.Dot(f, t);

            if (dotProdPlus1 < 1e-7f)
            {
                w = 0;
                if (Math.Abs(f.x) < 0.6f)
                {
					float norm = (float)Math.Sqrt(1 - f.x * f.x);
                    x = 0;
                    y = f.z / norm;
                    z = -f.y / norm;
                }
                else if (Math.Abs(f.y) < 0.6f)
                {
					float norm = (float)Math.Sqrt(1 - f.y * f.y);
                    x = -f.z / norm;
                    y = 0;
                    z = f.x / norm;
                }
                else
                {
					float norm = (float)Math.Sqrt(1 - f.z * f.z);
                    x = f.y / norm;
                    y = -f.x / norm;
                    z = 0;
                }
            }
            else
            {
				float s = (float)Math.Sqrt(0.5f * dotProdPlus1);
                Vector3f tmp = (f.Cross(t)) / (2.0f * s);
                x = tmp.x;
                y = tmp.y;
                z = tmp.z;
                w = s;
            }
        }

        /// <summary>
        /// Multiply two quternions together.
        /// </summary>
		public static Quaternion3f operator *(Quaternion3f q1, Quaternion3f q2)
        {
			return new Quaternion3f(q2.w * q1.x + q2.x * q1.w + q2.y * q1.z - q2.z * q1.y,
                            		q2.w * q1.y - q2.x * q1.z + q2.y * q1.w + q2.z * q1.x,
                            		q2.w * q1.z + q2.x * q1.y - q2.y * q1.x + q2.z * q1.w,
                            		q2.w * q1.w - q2.x * q1.x - q2.y * q1.y - q2.z * q1.z);
        }

        /// <summary>
        /// Multiply a quaternion and a vector together.
        /// </summary>
		public static Vector3f operator *(Quaternion3f q, Vector3f v)
        {
            return q.ToMatrix3x3f() * v;
        }

        /// <summary>
        /// Convert to a single precision 3 dimension matrix.
        /// </summary>
        public Matrix3x3f ToMatrix3x3f()
        {
            float   xx = x * x,
                    xy = x * y,
                    xz = x * z,
                    xw = x * w,
                    yy = y * y,
                    yz = y * z,
                    yw = y * w,
                    zz = z * z,
                    zw = z * w;

            return new Matrix3x3f
            (
                1.0f - 2.0f * (yy + zz), 2.0f * (xy - zw), 2.0f * (xz + yw),
                2.0f * (xy + zw), 1.0f - 2.0f * (xx + zz), 2.0f * (yz - xw),
                2.0f * (xz - yw), 2.0f * (yz + xw), 1.0f - 2.0f * (xx + yy)
            );
        }

        /// <summary>
        /// Convert to a single precision 4 dimension matrix.
        /// </summary>
        public Matrix4x4f ToMatrix4x4f()
        {
            float   xx = x * x,
                    xy = x * y,
                    xz = x * z,
                    xw = x * w,
                    yy = y * y,
                    yz = y * z,
                    yw = y * w,
                    zz = z * z,
                    zw = z * w;

            return new Matrix4x4f
            (
                1.0f - 2.0f * (yy + zz), 2.0f * (xy - zw), 2.0f * (xz + yw), 0.0f,
                2.0f * (xy + zw), 1.0f - 2.0f * (xx + zz), 2.0f * (yz - xw), 0.0f,
                2.0f * (xz - yw), 2.0f * (yz + xw), 1.0f - 2.0f * (xx + yy), 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        }

        /// <summary>
        /// The normalize the quaternion.
        /// </summary>
        public void Normalize()
        {
            float invLength = 1.0f / Length;
            x *= invLength;
            y *= invLength;
            z *= invLength;
            w *= invLength;
        }

		private float Safe_Acos(float r)
		{
			return (float)Math.Acos(Math.Min(1.0f, Math.Max(-1.0f,r)));	
		}

        /// <summary>
        /// Slerp the quaternion from the from rotation to the to rotation by t.
        /// </summary>
		public Quaternion3f Slerp(Quaternion3f from, Quaternion3f to, float t)
        {
            if (t <= 0)
            {
                return new Quaternion3f(from);
            }
            else if (t >= 1)
            {
                return new Quaternion3f(to);
            }
            else
            {
                float cosom = from.x * to.x + from.y * to.y + from.z * to.z + from.w * to.w;
                float absCosom = Math.Abs(cosom);

                float scale0;
                float scale1;

                if ((1 - absCosom) > 1e-6f)
                {
                    float omega = Safe_Acos(absCosom);
					float sinom = 1.0f / (float)Math.Sin(omega);
					scale0 = (float)Math.Sin((1.0f - t) * omega) * sinom;
					scale1 = (float)Math.Sin(t * omega) * sinom;
                }
                else
                {
                    scale0 = 1 - t;
                    scale1 = t;
                }
                Quaternion3f res = new Quaternion3f(scale0 * from.x + scale1 * to.x,
                                                    scale0 * from.y + scale1 * to.y,
                                                    scale0 * from.z + scale1 * to.z,
                                                    scale0 * from.w + scale1 * to.w);

                return res.Normalized;
            }
        }

        /// <summary>
        /// Create a rotation out of a vector.
        /// </summary>
        public static Quaternion3f FromEuler(Vector3f euler)
        {
 
            float degToRad = (float)(Math.PI / 180.0);

            var yaw = euler.x * degToRad;
            var pitch = euler.y * degToRad;
            var roll = euler.z * degToRad;
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)Math.Sin(rollOver2);
            float cosRollOver2 = (float)Math.Cos(rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)Math.Sin(pitchOver2);
            float cosPitchOver2 = (float)Math.Cos(pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)Math.Sin(yawOver2);
            float cosYawOver2 = (float)Math.Cos(yawOver2);

            Quaternion3f result;
            result.x = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.w = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;

        }


    }

}
























