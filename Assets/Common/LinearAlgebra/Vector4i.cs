using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4i
	{
		public int x, y, z, w;

        /// <summary>
        /// The unit x vector.
        /// </summary>
	    public readonly static Vector4i UnitX = new Vector4i(1, 0, 0, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector4i UnitY = new Vector4i(0, 1, 0, 0);

        /// <summary>
        /// The unit z vector.
        /// </summary>
	    public readonly static Vector4i UnitZ = new Vector4i(0, 0, 1, 0);

        /// <summary>
        /// The unit w vector.
        /// </summary>
	    public readonly static Vector4i UnitW = new Vector4i(0, 0, 0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector4i Zero = new Vector4i(0, 0, 0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
	    public readonly static Vector4i One = new Vector4i(1, 1, 1, 1);

        public Vector4i(int x, int y, int z, int w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
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
                    case 3: return w;
                    default: throw new IndexOutOfRangeException("Vector4i index out of range: " + i);
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
                    default: throw new IndexOutOfRangeException("Vector4i index out of range: " + i);
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
                return (x * x + y * y + z * z + w * w);
            }
        }

        /// <summary>
        /// The vectors absolute values.
        /// </summary>
        public Vector4i Absolute
        {
            get
            {
                return new Vector4i(Math.Abs(x), Math.Abs(y), Math.Abs(z), Math.Abs(w));
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector4i operator +(Vector4i v1, Vector4i v2)
        {
            return new Vector4i(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector4i operator +(Vector4i v1, int s)
        {
            return new Vector4i(v1.x + s, v1.y + s, v1.z + s, v1.w + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector4i operator -(Vector4i v1, Vector4i v2)
        {
            return new Vector4i(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector4i operator -(Vector4i v1, int s)
        {
            return new Vector4i(v1.x - s, v1.y - s, v1.z - s, v1.w - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector4i operator *(Vector4i v1, Vector4i v2)
        {
            return new Vector4i(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector4i operator *(Vector4i v, int s)
        {
            return new Vector4i(v.x * s, v.y * s, v.z * s, v.w * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector4i operator *(int s, Vector4i v)
        {
            return new Vector4i(v.x * s, v.y * s, v.z * s, v.w * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector4i operator /(Vector4i v1, Vector4i v2)
        {
            return new Vector4i(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector4i operator /(Vector4i v, int s)
        {
            return new Vector4i(v.x / s, v.y / s, v.z / s, v.w / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector4i v1, Vector4i v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector4i v1, Vector4i v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector4i)) return false;
			
			Vector4i v = (Vector4i)obj;
			
			return this == v;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector4i v)
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
			hashcode = (hashcode * 37) + w;
			
			return hashcode;
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
		static public Vector4i FromString(string s)
		{
            Vector4i v = new Vector4i();

            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = int.Parse(result[0]);
                v.y = int.Parse(result[1]);
                v.z = int.Parse(result[2]);
                v.w = int.Parse(result[3]);
            }
            catch { }
			
			return v;
		}

        /// <summary>
        /// The dot product of two vectors.
        /// </summary>
        public static int Dot(Vector4i v0, Vector4i v1)
        {
            return (v0.x * v1.x + v0.y * v1.y + v0.z * v1.z + v0.w * v1.w);
        }

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static double Distance(Vector4i v0, Vector4i v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static double SqrDistance(Vector4i v0, Vector4i v1)
        {
            double x = v0.x - v1.x;
            double y = v0.y - v1.y;
            double z = v0.z - v1.z;
            double w = v0.w - v1.w;
            return x * x + y * y + z * z + w * w;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(int s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
            z = Math.Min(z, s);
            w = Math.Min(w, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector4i v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
            z = Math.Min(z, v.z);
            w = Math.Min(w, v.w);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(int s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
            z = Math.Max(z, s);
            w = Math.Max(w, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector4i v)
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
        public void Clamp(int min, int max)
        {
            x = Math.Max(Math.Min(x, max), min);
            y = Math.Max(Math.Min(y, max), min);
            z = Math.Max(Math.Min(z, max), min);
            w = Math.Max(Math.Min(w, max), min);
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector4i min, Vector4i max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
            z = Math.Max(Math.Min(z, max.z), min.z);
            w = Math.Max(Math.Min(w, max.w), min.w);
        }

    }
	
}
















