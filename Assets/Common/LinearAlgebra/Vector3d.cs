using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{

    /// <summary>
    /// A 3d double precision vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3d
	{
		public double x, y, z;

        /// <summary>
        /// The unit x vector.
        /// </summary>
        public readonly static Vector3d UnitX = new Vector3d(1, 0, 0);

        /// <summary>
        /// The unit y vector.
        /// </summary>
	    public readonly static Vector3d UnitY = new Vector3d(0, 1, 0);

        /// <summary>
        /// The unit z vector.
        /// </summary>
	    public readonly static Vector3d UnitZ = new Vector3d(0, 0, 1);

        /// <summary>
        /// A vector of zeros.
        /// </summary>
	    public readonly static Vector3d Zero = new Vector3d(0, 0, 0);

        /// <summary>
        /// A vector of ones.
        /// </summary>
        public readonly static Vector3d One = new Vector3d(1, 1, 1);

        /// <summary>
        /// Convert to a 2 dimension vector.
        /// </summary>
	    public Vector2d xy
	    {
	        get { return new Vector2d(x, y); }
	    }

        /// <summary>
        /// Convert to a 4 dimension vector.
        /// </summary>
        public Vector4d xyz0
        {
            get { return new Vector4d(x, y, z, 0); }
        }

        /// <summary>
        /// Convert to a 4 dimension vector.
        /// </summary>
        public Vector4d xyz1
        {
            get { return new Vector4d(x, y, z, 1); }
        }

        /// <summary>
        /// A vector all with the value v.
        /// </summary>
		public Vector3d(double v) 
		{
			this.x = v; 
			this.y = v; 
			this.z = v;
		}

        /// <summary>
        /// A vector from the varibles.
        /// </summary>
		public Vector3d(double x, double y, double z) 
		{
			this.x = x; 
			this.y = y;
			this.z = z;
		}

        /// <summary>
        /// A vector from a 2d vector and the z varible.
        /// </summary>
		public Vector3d(Vector2d v, double z) 
		{ 
			x = v.x; 
			y = v.y; 
			this.z = z;
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
                    default: throw new IndexOutOfRangeException("Vector3d index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new IndexOutOfRangeException("Vector3d index out of range: " + i);
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
                return (x * x + y * y + z * z);
            }
        }

        /// <summary>
        /// The vector normalized.
        /// </summary>
        public Vector3d Normalized
        {
            get
            {
                double invLength = 1.0 / Math.Sqrt(x * x + y * y + z * z);
                return new Vector3d(x * invLength, y * invLength, z * invLength);
            }
        }

        /// <summary>
        /// The vectors absolute values.
        /// </summary>
        public Vector3d Absolute
        {
            get
            {
                return new Vector3d(Math.Abs(x), Math.Abs(y), Math.Abs(z));
            }
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        /// <summary>
        /// Add vector and scalar.
        /// </summary>
        public static Vector3d operator +(Vector3d v1, double s)
        {
            return new Vector3d(v1.x + s, v1.y + s, v1.z + s);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        /// <summary>
        /// Subtract vector and scalar.
        /// </summary>
        public static Vector3d operator -(Vector3d v1, double s)
        {
            return new Vector3d(v1.x - s, v1.y - s, v1.z - s);
        }

        /// <summary>
        /// Multiply two vectors.
        /// </summary>
        public static Vector3d operator *(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector3d operator *(Vector3d v, double s)
        {
            return new Vector3d(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Multiply a vector and a scalar.
        /// </summary>
        public static Vector3d operator *(double s, Vector3d v)
        {
            return new Vector3d(v.x * s, v.y * s, v.z * s);
        }

        /// <summary>
        /// Divide two vectors.
        /// </summary>
        public static Vector3d operator /(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }

        /// <summary>
        /// Divide a vector and a scalar.
        /// </summary>
        public static Vector3d operator /(Vector3d v, double s)
        {
            return new Vector3d(v.x / s, v.y / s, v.z / s);
        }

		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public static bool operator ==(Vector3d v1, Vector3d v2)
		{
			return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
		}
		
		/// <summary>
		/// Are these vectors not equal.
		/// </summary>
		public static bool operator !=(Vector3d v1, Vector3d v2)
		{
			return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z);
		}
		
		/// <summary>
		/// Are these vectors equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Vector3d)) return false;
			
			Vector3d v = (Vector3d)obj;
			
			return this == v;
		}

		/// <summary>
		/// Are these vectors equal given the error.
		/// </summary>
		public bool EqualsWithError(Vector3d v, double eps)
		{
			if(Math.Abs(x-v.x)> eps) return false;
			if(Math.Abs(y-v.y)> eps) return false;
			if(Math.Abs(z-v.z)> eps) return false;
			return true;
		}

        /// <summary>
        /// Are these vectors equal.
        /// </summary>
        public bool Equals(Vector3d v)
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
		static public Vector3d FromString(string s)
		{
            Vector3d v = new Vector3d();

            try
            {
                string[] separators = new string[] { "," };
                string[] result = s.Split(separators, StringSplitOptions.None);

                v.x = double.Parse(result[0]);
                v.y = double.Parse(result[1]);
                v.z = double.Parse(result[2]);
            }
            catch { }
			
			return v;
		}

		/// <summary>
		/// The dot product of two vectors.
		/// </summary>
		public static double Dot(Vector3d v0, Vector3d v1)
		{
			return (v0.x*v1.x + v0.y*v1.y + v0.z*v1.z);
		}

        /// <summary>
        /// Normalize the vector.
        /// </summary>
		public void Normalize()
		{
	    	double invLength = 1.0 / Math.Sqrt(x*x + y*y + z*z);
	    	x *= invLength;
			y *= invLength;
			z *= invLength;
		}

        /// <summary>
        /// Cross two vectors.
        /// </summary>
		public Vector3d Cross(Vector3d v)
		{
			return new Vector3d(y*v.z - z*v.y, z*v.x - x*v.z, x*v.y - y*v.x);
		}

		/// <summary>
		/// Cross two vectors.
		/// </summary>
		public static Vector3d Cross(Vector3d v0, Vector3d v1)
		{
			return new Vector3d(v0.y*v1.z - v0.z*v1.y, v0.z*v1.x - v0.x*v1.z, v0.x*v1.y - v0.y*v1.x);
		}

        /// <summary>
        /// Distance between two vectors.
        /// </summary>
        public static double Distance(Vector3d v0, Vector3d v1)
        {
            return Math.Sqrt(SqrDistance(v0, v1));
        }

        /// <summary>
        /// Square distance between two vectors.
        /// </summary>
        public static double SqrDistance(Vector3d v0, Vector3d v1)
        {
            double x = v0.x - v1.x;
            double y = v0.y - v1.y;
            double z = v0.z - v1.z;
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// The minimum value between s and each component in vector.
        /// </summary>
        public void Min(double s)
        {
            x = Math.Min(x, s);
            y = Math.Min(y, s);
            z = Math.Min(z, s);
        }

        /// <summary>
        /// The minimum value between each component in vectors.
        /// </summary>
        public void Min(Vector3d v)
        {
            x = Math.Min(x, v.x);
            y = Math.Min(y, v.y);
            z = Math.Min(z, v.z);
        }

        /// <summary>
        /// The maximum value between s and each component in vector.
        /// </summary>
        public void Max(double s)
        {
            x = Math.Max(x, s);
            y = Math.Max(y, s);
            z = Math.Max(z, s);
        }

        /// <summary>
        /// The maximum value between each component in vectors.
        /// </summary>
        public void Max(Vector3d v)
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
        public void Clamp(double min, double max)
		{
			x = Math.Max(Math.Min(x, max), min);
			y = Math.Max(Math.Min(y, max), min);
			z = Math.Max(Math.Min(z, max), min);
		}

        /// <summary>
        /// Clamp the each component to specified min and max.
        /// </summary>
        public void Clamp(Vector3d min, Vector3d max)
        {
            x = Math.Max(Math.Min(x, max.x), min.x);
            y = Math.Max(Math.Min(y, max.y), min.y);
            z = Math.Max(Math.Min(z, max.z), min.z);
        }

        /// <summary>
        /// Lerp between two vectors.
        /// </summary>
        public static Vector3d Lerp(Vector3d v1, Vector3d v2, double a)
		{
			double a1 = 1.0 - a;
			Vector3d v = new Vector3d();
			v.x = v1.x * a1 + v2.x * a;
			v.y = v1.y * a1 + v2.y * a;
			v.z = v1.z * a1 + v2.z * a;
			return v;
		}

	}

    public class Vector3dDistanceComparer : IComparer<Vector3d>
    {
        private Vector3d Point { get; set; }

        public Vector3dDistanceComparer(Vector3d point)
        {
            Point = point;
        }

        public int Compare(Vector3d v0, Vector3d v1)
        {
            double d0 = (v0 - Point).SqrMagnitude;
            double d1 = (v1 - Point).SqrMagnitude;

            return d0.CompareTo(d1);
        }
    }

    public class Vector3dAxisComparer : IComparer<Vector3d>
    {
        private int Axis { get; set; }

        public Vector3dAxisComparer(int axis)
        {
            Axis = axis;
        }

        public int Compare(Vector3d v0, Vector3d v1)
        {
            double d0 = v0[Axis];
            double d1 = v1[Axis];

            return d0.CompareTo(d1);
        }
    }

}


































