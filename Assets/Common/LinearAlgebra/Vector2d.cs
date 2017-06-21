using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{
    /// <summary>
    /// A 2d double precision vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2d
	{
		public double x, y;

        /// <summary>
        /// The unit x vector.
        /// </summary>
	    public readonly static Vector2d UnitX = new Vector2d(1, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector2d UnitY = new Vector2d(0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector2d Zero = new Vector2d(0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
	    public readonly static Vector2d One = new Vector2d(1, 1); 
		
        /// <summary>
        /// A vector all with the value v.
        /// </summary>
		public Vector2d(double v) 
		{
			this.x = v; 
			this.y = v; 
		}

        /// <summary>
        /// A vector from the varibles.
        /// </summary>
		public Vector2d(double x, double y) 
		{
			this.x = x; 
			this.y = y; 
		}

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException("Vector2d index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException("Vector2d index out of range: " + i);
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
        /// The vector normalized.
        /// </summary>
        public Vector2d Normalized
        {
            get
            {
                double invLength = 1.0 / Math.Sqrt(x * x + y * y);
                return new Vector2d(x * invLength, y * invLength);
            }
        }

        public Vector2d PerpendicularCCW
        {
            get
            {
                return new Vector2d(-y, x);
            }
        }

        public Vector2d PerpendicularCW
        {
            get
            {
                return new Vector2d(y, -x);
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.x + v2.x, v1.y + v2.y);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector2d operator +(Vector2d v1, double s)
        {
            return new Vector2d(v1.x + s, v1.y + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector2d operator -(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.x - v2.x, v1.y - v2.y);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector2d operator -(Vector2d v1, double s)
        {
            return new Vector2d(v1.x - s, v1.y - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector2d operator *(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.x * v2.x, v1.y * v2.y);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector2d operator *(Vector2d v, double s)
        {
            return new Vector2d(v.x * s, v.y * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector2d operator *(double s, Vector2d v)
        {
            return new Vector2d(v.x * s, v.y * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector2d operator /(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.x / v2.x, v1.y / v2.y);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector2d operator /(Vector2d v, double s)
        {
            return new Vector2d(v.x / s, v.y / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector2d v1, Vector2d v2)
		{
			return (v1.x == v2.x && v1.y == v2.y);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector2d v1, Vector2d v2)
		{
			return (v1.x != v2.x || v1.y != v2.y);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector2d)) return false;
			
			Vector2d v = (Vector2d)obj;
			
			return this == v;
		}

		/// <summary>
		/// Are these vectors equal given the error.
		/// </summary>
		public bool EqualsWithError(Vector2d v, double eps)
		{
			if(Math.Abs(x-v.x)> eps) return false;
			if(Math.Abs(y-v.y)> eps) return false;
			return true;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector2d v)
        {
            return this == v;
        }

        /// <summary>
        /// Vectors hash code. 
        /// </summary>
        public override int GetHashCode()
        {
            double hashcode = 23;
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
        static public Vector2d FromString(string s)
        {
            Vector2d v = new Vector2d();

            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = double.Parse(result[0]);
                v.y = double.Parse(result[1]);
            }
            catch { }

            return v;
        }

		/// <summary>
		/// The dot product of two vectors.
		/// </summary>
		public static double Dot(Vector2d v0, Vector2d v1)
		{
			return (v0.x*v1.x + v0.y*v1.y);
		}

        /// <summary>
        /// Normalize the vector.
        /// </summary>
		public void Normalize()
		{
	    	double invLength = 1.0 / Math.Sqrt(x*x + y*y);
	    	x *= invLength;
			y *= invLength;
		}

        /// <summary>
        /// Cross two vectors.
        /// </summary>
        public static double Cross(Vector2d v0, Vector2d v1)
        {
            return v0.x * v1.y - v0.y * v1.x;
        }

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static double Distance(Vector2d v0, Vector2d v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static double SqrDistance(Vector2d v0, Vector2d v1)
        {
            double x = v0.x - v1.x;
            double y = v0.y - v1.y;
            return x * x + y * y;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(double s)
		{
			x = Math.Min(x, s);
			y = Math.Min(y, s);
		}

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector2d v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
        }
		
		/// <summary>
		/// The maximum value between s and each component in vector.
		/// </summary>
        public void Max(double s)
		{
			x = Math.Max(x, s);
			y = Math.Max(y, s);
		}

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector2d v)
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
        public void Clamp(double min, double max)
		{
			x = Math.Max(Math.Min(x, max), min);
			y = Math.Max(Math.Min(y, max), min);
		}

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector2d min, Vector2d max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
        }

        /// <summary>
        /// Lerp between two vectors.
        /// </summary>
        public static Vector2d Lerp(Vector2d v1, Vector2d v2, double a)
        {
            double a1 = 1.0 - a;
			Vector2d v = new Vector2d();
            v.x = v1.x * a1 + v2.x * a;
			v.y = v1.y * a1 + v2.y * a;
			return v;
        }
		
	}

}


































