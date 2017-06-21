using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{

    /// <summary>
    /// A 3d single precision vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3f
    {
        public float x, y, z;

        /// <summary>
        /// The unit x vector.
        /// </summary>
        public readonly static Vector3f UnitX = new Vector3f(1, 0, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector3f UnitY = new Vector3f(0, 1, 0);

        /// <summary>
        /// The unit z vector.
        /// </summary>
	    public readonly static Vector3f UnitZ = new Vector3f(0, 0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector3f Zero = new Vector3f(0, 0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
        public readonly static Vector3f One = new Vector3f(1, 1, 1);

        /// <summary>
        /// Convert to a 2 dimension vector.
        /// </summary>
        public Vector2f xy
        {
            get { return new Vector2f(x, y); }
        }

        /// <summary>
        /// Convert to a 4 dimension vector.
        /// </summary>
        public Vector4f xyz0
        {
            get { return new Vector4f(x, y, z, 0); }
        }

        /// <summary>
        /// Convert to a 4 dimension vector.
        /// </summary>
        public Vector4f xyz1
        {
            get { return new Vector4f(x, y, z, 1); }
        }

        /// <summary>
        /// A vector all with the value v.
        /// </summary>
        public Vector3f(float v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
        }

        /// <summary>
        /// A vector from the varibles.
        /// </summary>
        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// A vector from a 2d vector and the z varible.
        /// </summary>
        public Vector3f(Vector2f v, float z)
        {
            x = v.x;
            y = v.y;
            this.z = z;
        }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new IndexOutOfRangeException("Vector3f index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new IndexOutOfRangeException("Vector3f index out of range: " + i);
                }
            }
        }

        /// <summary>
        /// The length of the vector.
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(SqrMagnitude);
            }
        }

        /// <summary>
        /// The length of the vector squared.
        /// </summary>
		public float SqrMagnitude
        {
            get
            {
                return (x * x + y * y + z * z);
            }
        }

        /// <summary>
        /// The vector normalized.
        /// </summary>
        public Vector3f Normalized
        {
            get
            {
                float invLength = (float)(1.0f / Math.Sqrt(x * x + y * y + z * z));
                return new Vector3f(x * invLength, y * invLength, z * invLength);
            }
        }

        /// <summary>
        /// The vectors absolute values.
        /// </summary>
        public Vector3f Absolute
        {
            get
            {
                return new Vector3f(Math.Abs(x), Math.Abs(y), Math.Abs(z));
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector3f operator +(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector3f operator +(Vector3f v1, float s)
        {
            return new Vector3f(v1.x + s, v1.y + s, v1.z + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector3f operator -(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector3f operator -(Vector3f v1, float s)
        {
            return new Vector3f(v1.x - s, v1.y - s, v1.z - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector3f operator *(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector3f operator *(Vector3f v, float s)
        {
            return new Vector3f(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector3f operator *(float s, Vector3f v)
        {
            return new Vector3f(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector3f operator /(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector3f operator /(Vector3f v, float s)
        {
            return new Vector3f(v.x / s, v.y / s, v.z / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector3f v1, Vector3f v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector3f v1, Vector3f v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector3f)) return false;
			
			Vector3f v = (Vector3f)obj;
			
			return this == v;
		}

		/// <summary>
		/// Are these vectors equal given the error.
		/// </summary>
		public bool EqualsWithError(Vector3f v, float eps)
		{
			if(Math.Abs(x-v.x)> eps) return false;
			if(Math.Abs(y-v.y)> eps) return false;
			if(Math.Abs(z-v.z)> eps) return false;
			return true;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector3f v)
        {
            return this == v;
        }

        /// <summary>
        /// Vectors hash code. 
        /// </summary>
        public override int GetHashCode()
        {
            float hashcode = 23;
            hashcode = (hashcode * 37) + x;
            hashcode = (hashcode * 37) + y;
            hashcode = (hashcode * 37) + z;

			return (int)hashcode;
        }

        /// <summary>
        /// Vector as a string.
        /// </summary>
        public override string ToString()
        {
            return x + "," + y + "," + z;
        }

		/// <summary>
		/// Vector from a string.
		/// </summary>
		static public Vector3f FromString(string s)
		{
            Vector3f v = new Vector3f();

            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = float.Parse(result[0]);
                v.y = float.Parse(result[1]);
                v.z = float.Parse(result[2]);
            }
            catch { }
			
			return v;
		}

		/// <summary>
		/// The dot product of two vectors.
		/// </summary>
		public static float Dot(Vector3f v0, Vector3f v1)
		{
			return (v0.x * v1.x + v0.y * v1.y + v0.z * v1.z);
		}

        /// <summary>
        /// Normalize the vector.
        /// </summary>
        public void Normalize()
        {
			float invLength = 1.0f / (float)Math.Sqrt(x * x + y * y + z * z);
            x *= invLength;
            y *= invLength;
            z *= invLength;
        }

        /// <summary>
        /// Cross two vectors.
        /// </summary>
        public Vector3f Cross(Vector3f v)
        {
            return new Vector3f(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x);
        }

		/// <summary>
		/// Cross two vectors.
		/// </summary>
		public static Vector3f Cross(Vector3f v0, Vector3f v1)
		{
			return new Vector3f(v0.y * v1.z - v0.z * v1.y, v0.z * v1.x - v0.x * v1.z, v0.x * v1.y - v0.y * v1.x);
		}

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static float Distance(Vector3f v0, Vector3f v1)
        {
            return (float)Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static float SqrDistance(Vector3f v0, Vector3f v1)
        {
            float x = v0.x - v1.x;
            float y = v0.y - v1.y;
            float z = v0.z - v1.z;
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(float s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
            z = Math.Min(z, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector3f v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
            z = Math.Min(z, v.z);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(float s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
            z = Math.Max(z, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector3f v)
        {
            x = Math.Max(x, v.x);
            y = Math.Max(y, v.y);
            z = Math.Max(z, v.z);
        }

        /// <summary>
        /// The absolute vector.
        /// </summary>
        public void Abs()
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            z = Math.Abs(z);
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(float min, float max)
		{
			x = Math.Max(Math.Min(x, max), min);
			y = Math.Max(Math.Min(y, max), min);
			z = Math.Max(Math.Min(z, max), min);
		}

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector3f min, Vector3f max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
            z = Math.Max(Math.Min(z, max.z), min.z);
        }

        /// <summary>
        /// Lerp between two vectors.
        /// </summary>
        public static Vector3f Lerp(Vector3f v1, Vector3f v2, float a)
        {
            float a1 = 1.0f - a;
			Vector3f v = new Vector3f();
            v.x = v1.x * a1 + v2.x * a;
            v.y = v1.y * a1 + v2.y * a;
            v.z = v1.z * a1 + v2.z * a;
            return v;
        }
    }

    public class Vector3fDistanceComparer : IComparer<Vector3f>
    {
        private Vector3f Point { get; set; }

        public Vector3fDistanceComparer(Vector3f point)
        {
            Point = point;
        }

        public int Compare(Vector3f v0, Vector3f v1)
        {
            float d0 = (v0 - Point).SqrMagnitude;
            float d1 = (v1 - Point).SqrMagnitude;

            return d0.CompareTo(d1);
        }
    }

    public class Vector3fAxisComparer : IComparer<Vector3f>
    {
        private int Axis { get; set; }

        public Vector3fAxisComparer(int axis)
        {
            Axis = axis;
        }

        public int Compare(Vector3f v0, Vector3f v1)
        {
            float d0 = v0[Axis];
            float d1 = v1[Axis];

            return d0.CompareTo(d1);
        }
    }

}


































