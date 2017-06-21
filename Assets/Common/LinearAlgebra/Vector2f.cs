using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{
    /// <summary>
    /// A 2d single precision vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2f
    {
        public float x, y;

        /// <summary>
        /// The unit x vector.
        /// </summary>
        public readonly static Vector2f UnitX = new Vector2f(1, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector2f UnitY = new Vector2f(0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector2f Zero = new Vector2f(0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
	    public readonly static Vector2f One = new Vector2f(1, 1);

        /// <summary>
        /// A vector all with the value v.
        /// </summary>
        public Vector2f(float v)
        {
            this.x = v;
            this.y = v;
        }

        /// <summary>
        /// A vector from the varibles.
        /// </summary>
        public Vector2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException("Vector2f index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException("Vector2f index out of range: " + i);
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
                return (x * x + y * y);
            }
        }

        /// <summary>
        /// The vector normalized.
        /// </summary>
        public Vector2f Normalized
        {
            get
            {
                float invLength = (float)(1.0 / Math.Sqrt(x * x + y * y));
                return new Vector2f(x * invLength, y * invLength);
            }
        }

        public Vector2f PerpendicularCCW
        {
            get
            {
                return new Vector2f(-y, x);
            }
        }

        public Vector2f PerpendicularCW
        {
            get
            {
                return new Vector2f(y, -x);
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector2f operator +(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.x + v2.x, v1.y + v2.y);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector2f operator +(Vector2f v1, float s)
        {
            return new Vector2f(v1.x + s, v1.y + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector2f operator -(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.x - v2.x, v1.y - v2.y);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector2f operator -(Vector2f v1, float s)
        {
            return new Vector2f(v1.x - s, v1.y - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector2f operator *(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.x * v2.x, v1.y * v2.y);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector2f operator *(Vector2f v, float s)
        {
            return new Vector2f(v.x * s, v.y * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector2f operator *(float s, Vector2f v)
        {
            return new Vector2f(v.x * s, v.y * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector2f operator /(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.x / v2.x, v1.y / v2.y);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector2f operator /(Vector2f v, float s)
        {
            return new Vector2f(v.x / s, v.y / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector2f v1, Vector2f v2)
		{
			return (v1.x == v2.x && v1.y == v2.y);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector2f v1, Vector2f v2)
		{
			return (v1.x != v2.x || v1.y != v2.y);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector2f)) return false;
			
			Vector2f v = (Vector2f)obj;
			
			return this == v;
		}

		/// <summary>
		/// Are these vectors equal given the error.
		/// </summary>
		public bool EqualsWithError(Vector2f v, float eps)
		{
			if(Math.Abs(x-v.x)> eps) return false;
			if(Math.Abs(y-v.y)> eps) return false;
			return true;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector2f v)
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

			return (int)hashcode;
        }

        /// <summary>
        /// Vector as a string.
        /// </summary>
        public override string ToString()
        {
            return x + "," + y;
        }

		/// <summary>
		/// Vector from a string.
		/// </summary>
		static public Vector2f FromString(string s)
		{
            Vector2f v = new Vector2f();

            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = float.Parse(result[0]);
                v.y = float.Parse(result[1]);
            }
            catch { }
			
			return v;
		}

		/// <summary>
		/// The dot product of two vectors.
		/// </summary>
		public static float Dot(Vector2f v0, Vector2f v1)
		{
			return (v0.x * v1.x + v0.y * v1.y);
		}

        /// <summary>
        /// Normalize the vector.
        /// </summary>
        public void Normalize()
        {
			float invLength = 1.0f / (float)Math.Sqrt(x * x + y * y);
            x *= invLength;
            y *= invLength;
        }

        /// <summary>
        /// Cross two vectors.
        /// </summary>
        public static float Cross(Vector2f v0, Vector2f v1)
        {
            return v0.x * v1.y - v0.y * v1.x;
        }

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static float Distance(Vector2f v0, Vector2f v1)
        {
            return (float)Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static float SqrDistance(Vector2f v0, Vector2f v1)
        {
            float x = v0.x - v1.x;
            float y = v0.y - v1.y;
            return x * x + y * y;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(float s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector2f v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(float s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector2f v)
        {
            x = Math.Max(x, v.x);
            y = Math.Max(y, v.y);
        }

        /// <summary>
        /// The absolute vector.
        /// </summary>
        public void Abs()
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(float min, float max)
		{
			x = Math.Max(Math.Min(x, max), min);
			y = Math.Max(Math.Min(y, max), min);
		}

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector2f min, Vector2f max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
        }

        /// <summary>
        /// Lerp between two vectors.
        /// </summary>
        public static Vector2f Lerp(Vector2f v1, Vector2f v2, float a)
        {
            float a1 = 1.0f - a;
			Vector2f v = new Vector2f();
            v.x = v1.x * a1 + v2.x * a;
            v.y = v1.y * a1 + v2.y * a;
			return v;
        }

    }

}


































