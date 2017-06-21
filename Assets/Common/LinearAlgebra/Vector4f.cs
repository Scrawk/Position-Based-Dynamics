using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{

    /// <summary>
    /// A 4d float precision vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4f
    {

        public float x, y, z, w;

        /// <summary>
        /// The unit x vector.
        /// </summary>
        public readonly static Vector4f UnitX = new Vector4f(1, 0, 0, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector4f UnitY = new Vector4f(0, 1, 0, 0);

        /// <summary>
        /// The unit z vector.
        /// </summary>
	    public readonly static Vector4f UnitZ = new Vector4f(0, 0, 1, 0);

        /// <summary>
        /// The unit w vector.
        /// </summary>
	    public readonly static Vector4f UnitW = new Vector4f(0, 0, 0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector4f Zero = new Vector4f(0, 0, 0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
	    public readonly static Vector4f One = new Vector4f(1, 1, 1, 1);

        /// <summary>
        /// Convert to a 2 dimension vector.
        /// </summary>
        public Vector2f xy
        {
            get { return new Vector2f(x, y); }
        }

        /// <summary>
        /// Convert to a 3 dimension vector.
        /// </summary>
        public Vector3f xyz
        {
            get { return new Vector3f(x, y, z); }
        }

        /// <summary>
        /// A copy of the vector with w as 0.
        /// </summary>
        public Vector4f xyz0
        {
            get { return new Vector4f(x, y, z, 0); }
        }

        /// <summary>
        /// A vector all with the value v.
        /// </summary>
        public Vector4f(float v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
            this.w = v;
        }

        /// <summary>
        /// A vector from the varibles.
        /// </summary>
        public Vector4f(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// A vector from a 2d vector and the z and w varibles.
        /// </summary>
        public Vector4f(Vector2f v, float z, float w)
        {
            x = v.x;
            y = v.y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// A vector from a 3d vector and the w varible.
        /// </summary>
        public Vector4f(Vector3f v, float w)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            this.w = w;
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
                    case 3: return w;
                    default: throw new IndexOutOfRangeException("Vector4f index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default: throw new IndexOutOfRangeException("Vector4f index out of range: " + i);
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
                return (x * x + y * y + z * z + w * w);
            }
        }

        /// <summary>
        /// The vector normalized.
        /// </summary>
        public Vector4f Normalized
        {
            get
            {
                float invLength = (float)(1.0 / Math.Sqrt(x * x + y * y + z * z + w * w));
                return new Vector4f(x * invLength, y * invLength, z * invLength, w * invLength);
            }
        }

        /// <summary>
        /// The vectors absolute values.
        /// </summary>
        public Vector4f Absolute
        {
            get
            {
                return new Vector4f(Math.Abs(x), Math.Abs(y), Math.Abs(z), Math.Abs(w));
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector4f operator +(Vector4f v1, Vector4f v2)
        {
            return new Vector4f(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector4f operator +(Vector4f v1, float s)
        {
            return new Vector4f(v1.x + s, v1.y + s, v1.z + s, v1.w + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector4f operator -(Vector4f v1, Vector4f v2)
        {
            return new Vector4f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector4f operator -(Vector4f v1, float s)
        {
            return new Vector4f(v1.x - s, v1.y - s, v1.z - s, v1.w - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector4f operator *(Vector4f v1, Vector4f v2)
        {
            return new Vector4f(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector4f operator *(Vector4f v, float s)
        {
            return new Vector4f(v.x * s, v.y * s, v.z * s, v.w * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector4f operator *(float s, Vector4f v)
        {
            return new Vector4f(v.x * s, v.y * s, v.z * s, v.w * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector4f operator /(Vector4f v1, Vector4f v2)
        {
            return new Vector4f(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector4f operator /(Vector4f v, float s)
        {
            return new Vector4f(v.x / s, v.y / s, v.z / s, v.w / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector4f v1, Vector4f v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector4f v1, Vector4f v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector4f)) return false;
			
			Vector4f v = (Vector4f)obj;
			
			return this == v;
		}

		/// <summary>
		/// Are these vectors equal given the error.
		/// </summary>
		public bool EqualsWithError(Vector4f v, float eps)
		{
			if(Math.Abs(x-v.x)> eps) return false;
			if(Math.Abs(y-v.y)> eps) return false;
			if(Math.Abs(z-v.z)> eps) return false;
			if(Math.Abs(w-v.w)> eps) return false;
			return true;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector4f v)
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
            hashcode = (hashcode * 37) + w;

			return (int)hashcode;
        }

        /// <summary>
        /// Vector as a string.
        /// </summary>
        public override string ToString()
        {
            return x + "," + y + "," + z + "," + w;
        }

		/// <summary>
		/// Vector from a string.
		/// </summary>
		static public Vector4f FromString(string s)
		{
            Vector4f v = new Vector4f();
            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = float.Parse(result[0]);
                v.y = float.Parse(result[1]);
                v.z = float.Parse(result[2]);
                v.w = float.Parse(result[3]);
            }
            catch { }
			
			return v;
		}

		/// <summary>
		/// The dot product of two vectors.
		/// </summary>
		public static float Dot(Vector4f v0, Vector4f v1)
		{
			return (v0.x * v1.x + v0.y * v1.y + v0.z * v1.z + v0.w * v1.w);
		}

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static float Distance(Vector4f v0, Vector4f v1)
        {
            return (float)Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static float SqrDistance(Vector4f v0, Vector4f v1)
        {
            float x = v0.x - v1.x;
            float y = v0.y - v1.y;
            float z = v0.z - v1.z;
            float w = v0.w - v1.w;
            return x * x + y * y + z * z + w*w;
        }

        /// <summary>
        /// Normalize the vector.
        /// </summary>
        public void Normalize()
        {
			float invLength = 1.0f / (float)Math.Sqrt(x * x + y * y + z * z + w * w);
            x *= invLength;
            y *= invLength;
            z *= invLength;
            w *= invLength;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(float s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
            z = Math.Min(z, s);
            w = Math.Min(w, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector4f v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
            z = Math.Min(z, v.z);
            w = Math.Min(w, v.w);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(float s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
            z = Math.Max(z, s);
            w = Math.Max(w, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector4f v)
        {
            x = Math.Max(x, v.x);
            y = Math.Max(y, v.y);
            z = Math.Max(z, v.z);
            w = Math.Max(w, v.w);
        }

        /// <summary>
        /// The absolute vector.
        /// </summary>
        public void Abs()
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            z = Math.Abs(z);
            w = Math.Abs(w);
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(float min, float max)
		{
			x = Math.Max(Math.Min(x, max), min);
			y = Math.Max(Math.Min(y, max), min);
			z = Math.Max(Math.Min(z, max), min);
			w = Math.Max(Math.Min(w, max), min);
		}

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector4f min, Vector4f max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
            z = Math.Max(Math.Min(z, max.z), min.z);
            w = Math.Max(Math.Min(w, max.w), min.w);
        }

        /// <summary>
        /// Lerp between two vectors.
        /// </summary>
        public static Vector4f Lerp(Vector4f v1, Vector4f v2, float a)
        {
            float a1 = 1.0f - a;
			Vector4f v = new Vector4f();
            v.x = v1.x * a1 + v2.x * a;
			v.y = v1.y * a1 + v2.y * a;
			v.z = v1.z * a1 + v2.z * a;
			v.w = v1.w * a1 + v2.w * a;
            return v;
        }

    }

}


































