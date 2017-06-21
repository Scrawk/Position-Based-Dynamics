using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2i
	{
		public int x, y;

        /// <summary>
        /// The unit x vector.
        /// </summary>
        public readonly static Vector2i UnitX = new Vector2i(1, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector2i UnitY = new Vector2i(0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector2i Zero = new Vector2i(0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
	    public readonly static Vector2i One = new Vector2i(1, 1);

        public Vector2i(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException("Vector2i index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException("Vector2i index out of range: " + i);
                }
            }
        }

        /// <summary>
        /// The length of the vector.
        /// </summary>
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(SqrMagnitude);
            }
        }

        /// <summary>
        /// The length of the vector squared.
        /// </summary>
        public double SqrMagnitude
        {
            get
            {
                return (x * x + y * y);
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector2i operator +(Vector2i v1, Vector2i v2)
        {
            return new Vector2i(v1.x + v2.x, v1.y + v2.y);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector2i operator +(Vector2i v1, int s)
        {
            return new Vector2i(v1.x + s, v1.y + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector2i operator -(Vector2i v1, Vector2i v2)
        {
            return new Vector2i(v1.x - v2.x, v1.y - v2.y);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector2i operator -(Vector2i v1, int s)
        {
            return new Vector2i(v1.x - s, v1.y - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector2i operator *(Vector2i v1, Vector2i v2)
        {
            return new Vector2i(v1.x * v2.x, v1.y * v2.y);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector2i operator *(Vector2i v, int s)
        {
            return new Vector2i(v.x * s, v.y * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector2i operator *(int s, Vector2i v)
        {
            return new Vector2i(v.x * s, v.y * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector2i operator /(Vector2i v1, Vector2i v2)
        {
            return new Vector2i(v1.x / v2.x, v1.y / v2.y);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector2i operator /(Vector2i v, int s)
        {
            return new Vector2i(v.x / s, v.y / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector2i v1, Vector2i v2)
		{
			return (v1.x == v2.x && v1.y == v2.y);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector2i v1, Vector2i v2)
		{
			return (v1.x != v2.x || v1.y != v2.y);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector2i)) return false;
			
			Vector2i v = (Vector2i)obj;
			
			return this == v;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector2i v)
        {
            return this == v;
        }

		
		/// <summary>
		/// Vectors hash code. 
		/// </summary>
		public override int GetHashCode()
		{
			int hashcode = 23;
			hashcode = (hashcode * 37) + x;
			hashcode = (hashcode * 37) + y;
			
			return hashcode;
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
		static public Vector2i FromString(string s)
		{

            Vector2i v = new Vector2i();

            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = int.Parse(result[0]);
                v.y = int.Parse(result[1]);
            }
            catch { }

			return v;
		}

        /// <summary>
        /// The dot product of two vectors.
        /// </summary>
        public static int Dot(Vector2i v0, Vector2i v1)
        {
            return (v0.x * v1.x + v0.y * v1.y);
        }

        /// <summary>
        /// Cross two vectors.
        /// </summary>
        public static int Cross(Vector2i v0, Vector2i v1)
        {
            return v0.x * v1.y - v0.y * v1.x;
        }

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static double Distance(Vector2i v0, Vector2i v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static double SqrDistance(Vector2i v0, Vector2i v1)
        {
            double x = v0.x - v1.x;
            double y = v0.y - v1.y;
            return x * x + y * y;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(int s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector2i v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(int s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector2i v)
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
        public void Clamp(int min, int max)
        {
            x = Math.Max(Math.Min(x, max), min);
            y = Math.Max(Math.Min(y, max), min);
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector2i min, Vector2i max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
        }

    }
	
}

















