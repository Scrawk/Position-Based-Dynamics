
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3i
	{
		public int x, y, z;

        /// <summary>
        /// The unit x vector.
        /// </summary>
	    public readonly static Vector3i UnitX = new Vector3i(1, 0, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector3i UnitY = new Vector3i(0, 1, 0);

        /// <summary>
        /// The unit z vector.
        /// </summary>
	    public readonly static Vector3i UnitZ = new Vector3i(0, 0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector3i Zero = new Vector3i(0, 0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
        public readonly static Vector3i One = new Vector3i(1, 1, 1);

        public Vector3i(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new IndexOutOfRangeException("Vector3i index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new IndexOutOfRangeException("Vector3i index out of range: " + i);
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
        public int SqrMagnitude
        {
            get
            {
                return (x * x + y * y + z * z);
            }
        }

        /// <summary>
        /// The vectors absolute values.
        /// </summary>
        public Vector3i Absolute
        {
            get
            {
                return new Vector3i(Math.Abs(x), Math.Abs(y), Math.Abs(z));
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector3i operator +(Vector3i v1, Vector3i v2)
        {
            return new Vector3i(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector3i operator +(Vector3i v1, int s)
        {
            return new Vector3i(v1.x + s, v1.y + s, v1.z + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector3i operator -(Vector3i v1, Vector3i v2)
        {
            return new Vector3i(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector3i operator -(Vector3i v1, int s)
        {
            return new Vector3i(v1.x - s, v1.y - s, v1.z - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector3i operator *(Vector3i v1, Vector3i v2)
        {
            return new Vector3i(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector3i operator *(Vector3i v, int s)
        {
            return new Vector3i(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector3i operator *(int s, Vector3i v)
        {
            return new Vector3i(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector3i operator /(Vector3i v1, Vector3i v2)
        {
            return new Vector3i(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector3i operator /(Vector3i v, int s)
        {
            return new Vector3i(v.x / s, v.y / s, v.z / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector3i v1, Vector3i v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector3i v1, Vector3i v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector3i)) return false;
			
			Vector3i v = (Vector3i)obj;
			
			return this == v;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector3i v)
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
			hashcode = (hashcode * 37) + z;
			
			return hashcode;
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
		static public Vector3i FromString(string s)
		{
            Vector3i v = new Vector3i();
            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = int.Parse(result[0]);
                v.y = int.Parse(result[1]);
                v.z = int.Parse(result[2]);
            }
            catch { }
			
			return v;
		}
        
        /// <summary>
        /// The dot product of two vectors.
        /// </summary>
        public static int Dot(Vector3i v0, Vector3i v1)
        {
            return (v0.x * v1.x + v0.y * v1.y + v0.z * v1.z);
        }

        /// <summary>
        /// Cross two vectors.
        /// </summary>
        public static Vector3i Cross(Vector3i v0, Vector3i v1)
        {
            return new Vector3i(v0.y * v1.z - v0.z * v1.y, v0.z * v1.x - v0.x * v1.z, v0.x * v1.y - v0.y * v1.x);
        }

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static double Distance(Vector3i v0, Vector3i v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static double SqrDistance(Vector3i v0, Vector3i v1)
        {
            double x = v0.x - v1.x;
            double y = v0.y - v1.y;
            double z = v0.z - v1.z;
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(int s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
            z = Math.Min(z, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector3i v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
            z = Math.Min(z, v.z);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(int s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
            z = Math.Max(z, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector3i v)
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
        public void Clamp(int min, int max)
        {
            x = Math.Max(Math.Min(x, max), min);
            y = Math.Max(Math.Min(y, max), min);
            z = Math.Max(Math.Min(z, max), min);
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector3i min, Vector3i max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
            z = Math.Max(Math.Min(z, max.z), min.z);
        }

    }
	
}









