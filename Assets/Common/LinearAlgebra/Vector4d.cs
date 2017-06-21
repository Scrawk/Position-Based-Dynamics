using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{

    /// <summary>
    /// A 4d double precision vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4d
	{
	
		public double x, y, z, w;

        /// <summary>
        /// The unit x vector.
        /// </summary>
        public readonly static Vector4d UnitX = new Vector4d(1, 0, 0, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector4d UnitY = new Vector4d(0, 1, 0, 0);

        /// <summary>
        /// The unit z vector.
        /// </summary>
	    public readonly static Vector4d UnitZ = new Vector4d(0, 0, 1, 0);

        /// <summary>
        /// The unit w vector.
        /// </summary>
	    public readonly static Vector4d UnitW = new Vector4d(0, 0, 0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector4d Zero = new Vector4d(0, 0, 0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
	    public readonly static Vector4d One = new Vector4d(1, 1, 1, 1);

        /// <summary>
        /// Convert to a 2 dimension vector.
        /// </summary>
	    public Vector2d xy
	    {
	        get { return new Vector2d(x, y); }
	    }

        /// <summary>
        /// Convert to a 3 dimension vector.
        /// </summary>
	    public Vector3d xyz
	    {
	        get { return new Vector3d(x, y, z); }
	    }

        /// <summary>
        /// A copy of the vector with w as 0.
        /// </summary>
        public Vector4d xyz0
        {
            get { return new Vector4d(x, y, z, 0); }
        }

        /// <summary>
        /// A vector all with the value v.
        /// </summary>
		public Vector4d(double v) 
		{
			this.x = v; 
			this.y = v; 
			this.z = v;
			this.w = v;
		}

        /// <summary>
        /// A vector from the varibles.
        /// </summary>
		public Vector4d(double x, double y, double z, double w) 
		{
			this.x = x; 
			this.y = y;
			this.z = z;
			this.w = w;
		}

        /// <summary>
        /// A vector from a 2d vector and the z and w varibles.
        /// </summary>
		public Vector4d(Vector2d v, double z, double w) 
		{ 
			x = v.x; 
			y = v.y; 
			this.z = z;
			this.w = w;
		}

        /// <summary>
        /// A vector from a 3d vector and the w varible.
        /// </summary>
		public Vector4d(Vector3d v, double w) 
		{ 
			x = v.x; 
			y = v.y; 
			z = v.z;
			this.w = w;
		}

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new IndexOutOfRangeException("Vector4d index out of range: " + i);
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
                    default: throw new IndexOutOfRangeException("Vector4d index out of range: " + i);
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
                return (x * x + y * y + z * z + w * w);
            }
        }

        /// <summary>
        /// The vector normalized.
        /// </summary>
        public Vector4d Normalized
        {
            get
            {
                double invLength = 1.0 / Math.Sqrt(x * x + y * y + z * z + w * w);
                return new Vector4d(x * invLength, y * invLength, z * invLength, w * invLength);
            }
        }

        /// <summary>
        /// The vectors absolute values.
        /// </summary>
        public Vector4d Absolute
        {
            get
            {
                return new Vector4d(Math.Abs(x), Math.Abs(y), Math.Abs(z), Math.Abs(w));
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector4d operator +(Vector4d v1, Vector4d v2)
        {
            return new Vector4d(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector4d operator +(Vector4d v1, double s)
        {
            return new Vector4d(v1.x + s, v1.y + s, v1.z + s, v1.w + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector4d operator -(Vector4d v1, Vector4d v2)
        {
            return new Vector4d(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector4d operator -(Vector4d v1, double s)
        {
            return new Vector4d(v1.x - s, v1.y - s, v1.z - s, v1.w - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector4d operator *(Vector4d v1, Vector4d v2)
        {
            return new Vector4d(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector4d operator *(Vector4d v, double s)
        {
            return new Vector4d(v.x * s, v.y * s, v.z * s, v.w * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector4d operator *(double s, Vector4d v)
        {
            return new Vector4d(v.x * s, v.y * s, v.z * s, v.w * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector4d operator /(Vector4d v1, Vector4d v2)
        {
            return new Vector4d(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector4d operator /(Vector4d v, double s)
        {
            return new Vector4d(v.x / s, v.y / s, v.z / s, v.w / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector4d v1, Vector4d v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector4d v1, Vector4d v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector4d)) return false;
			
			Vector4d v = (Vector4d)obj;
			
			return this == v;
		}

		/// <summary>
		/// Are these vectors equal given the error.
		/// </summary>
		public bool EqualsWithError(Vector4d v, double eps)
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
        public bool Equals(Vector4d v)
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
            hashcode = (hashcode * 37) + z;
            hashcode = (hashcode * 37) + w;

			return (int)hashcode;
        }

        /// <summary>
        /// Vector as a string.
        /// </summary>
		public override string ToString() {
			return x + "," + y + "," + z + "," + w;
	   	}

		/// <summary>
		/// Vector from a string.
		/// </summary>
		static public Vector4d FromString(string s)
		{
            Vector4d v = new Vector4d();
            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = double.Parse(result[0]);
                v.y = double.Parse(result[1]);
                v.z = double.Parse(result[2]);
                v.w = double.Parse(result[3]);
            }
            catch { }
			
			return v;
		}

		/// <summary>
		/// The dot product of two vectors.
		/// </summary>
		public static double Dot(Vector4d v0, Vector4d v1) {
			return (v0.x*v1.x + v0.y*v1.y + v0.z*v1.z + v0.w*v1.w);
		}

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static double Distance(Vector4d v0, Vector4d v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static double SqrDistance(Vector4d v0, Vector4d v1)
        {
            double x = v0.x - v1.x;
            double y = v0.y - v1.y;
            double z = v0.z - v1.z;
            double w = v0.w - v1.w;
            return x * x + y * y + z * z + w * w;
        }

        /// <summary>
        /// Normalize the vector.
        /// </summary>
		public void Normalize()
		{
	    	double invLength = 1.0 / Math.Sqrt(x*x + y*y + z*z + w*w);
	    	x *= invLength;
			y *= invLength;
			z *= invLength;
			w *= invLength;
		}

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(double s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
            z = Math.Min(z, s);
            w = Math.Min(w, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector4d v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
            z = Math.Min(z, v.z);
            w = Math.Min(w, v.w);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(double s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
            z = Math.Max(z, s);
            w = Math.Max(w, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector4d v)
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
        public void Clamp(double min, double max)
        {
            x = Math.Max(Math.Min(x, max), min);
            y = Math.Max(Math.Min(y, max), min);
            z = Math.Max(Math.Min(z, max), min);
            w = Math.Max(Math.Min(w, max), min);
        }

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector4d min, Vector4d max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
            z = Math.Max(Math.Min(z, max.z), min.z);
            w = Math.Max(Math.Min(w, max.w), min.w);
        }

        /// <summary>
        /// Lerp between two vectors.
        /// </summary>
        public static Vector4d Lerp(Vector4d v1, Vector4d v2, double a)
        {
            double a1 = 1.0 - a;
			Vector4d v = new Vector4d();
			v.x = v1.x * a1 + v2.x * a;
			v.y = v1.y * a1 + v2.y * a;
			v.z = v1.z * a1 + v2.z * a;
			v.w = v1.w * a1 + v2.w * a;
			return v;
        }
		
	}

}


































