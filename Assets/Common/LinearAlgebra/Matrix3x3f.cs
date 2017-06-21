using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{
    /// <summary>
    /// A single precision 3 dimension matrix
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix3x3f
    {
        /// <summary>
        /// The matrix
        /// </summary>
        public float m00, m01, m02;
        public float m10, m11, m12;
        public float m20, m21, m22;

        /// <summary>
        /// The Matrix Idenity.
        /// </summary>
        static readonly public Matrix3x3f Identity = new Matrix3x3f(1, 0, 0, 0, 1, 0, 0, 0, 1);

        /// <summary>
        /// A matrix from the following varibles.
        /// </summary>
        public Matrix3x3f(float m00, float m01, float m02,
                          float m10, float m11, float m12,
                          float m20, float m21, float m22)
        {
			this.m00 = m00; this.m01 = m01; this.m02 = m02;
			this.m10 = m10; this.m11 = m11; this.m12 = m12;
			this.m20 = m20; this.m21 = m21; this.m22 = m22;

        }

        /// <summary>
        /// A matrix from the following varibles.
        /// </summary>
        public Matrix3x3f(float v)
        {
            m00 = v; m01 = v; m02 = v;
            m10 = v; m11 = v; m12 = v;
            m20 = v; m21 = v; m22 = v;
        }

        /// <summary>
        /// A matrix copied from a array of varibles.
        /// </summary>
        public Matrix3x3f(float[,] m)
        {
            m00 = m[0, 0]; m01 = m[0, 1]; m02 = m[0, 2];
            m10 = m[1, 0]; m11 = m[1, 1]; m12 = m[1, 2];
            m20 = m[2, 0]; m21 = m[2, 1]; m22 = m[2, 2];
        }

		/// <summary>
		/// A matrix copied from a array of varibles.
		/// </summary>
		public Matrix3x3f(float[] m)
		{
            m00 = m[0 + 0 * 3]; m01 = m[0 + 1 * 3]; m02 = m[0 + 2 * 3];
            m10 = m[1 + 0 * 3]; m11 = m[1 + 1 * 3]; m12 = m[1 + 2 * 3];
            m20 = m[2 + 0 * 3]; m21 = m[2 + 1 * 3]; m22 = m[2 + 2 * 3];
		}

        /// <summary>
        /// A matrix copied from the matrix m.
        /// </summary>
        public Matrix3x3f(Matrix3x3f m)
        {
            m00 = m.m00; m01 = m.m01; m02 = m.m02;
            m10 = m.m10; m11 = m.m11; m12 = m.m12;
            m20 = m.m20; m21 = m.m21; m22 = m.m22;
        }

        public float Trace
        {
            get
            {
                return m00 + m11 + m22;
            }
        }

        /// <summary>
        /// Access the varible at index i
        /// </summary>
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return m00;
                    case 1: return m10;
                    case 2: return m20;
                    case 3: return m01;
                    case 4: return m11;
                    case 5: return m21;
                    case 6: return m02;
                    case 7: return m12;
                    case 8: return m22;
                    default: throw new IndexOutOfRangeException("Matrix3x3f index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m20 = value; break;
                    case 3: m01 = value; break;
                    case 4: m11 = value; break;
                    case 5: m21 = value; break;
                    case 6: m02 = value; break;
                    case 7: m12 = value; break;
                    case 8: m22 = value; break;
                    default: throw new IndexOutOfRangeException("Matrix3x3f index out of range: " + i);
                }
            }
        }

        /// <summary>
        /// Access the varible at index ij
        /// </summary>
        public float this[int i, int j]
        {
            get
            {
                int k = i + j * 3;
                switch (k)
                {
                    case 0: return m00;
                    case 1: return m10;
                    case 2: return m20;
                    case 3: return m01;
                    case 4: return m11;
                    case 5: return m21;
                    case 6: return m02;
                    case 7: return m12;
                    case 8: return m22;
                    default: throw new IndexOutOfRangeException("Matrix3x3f index out of range: " + k);
                }
            }
            set
            {
                int k = i + j * 3;
                switch (k)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m20 = value; break;
                    case 3: m01 = value; break;
                    case 4: m11 = value; break;
                    case 5: m21 = value; break;
                    case 6: m02 = value; break;
                    case 7: m12 = value; break;
                    case 8: m22 = value; break;
                    default: throw new IndexOutOfRangeException("Matrix3x3f index out of range: " + k);
                }
            }
        }

        /// <summary>
        /// The transpose of the matrix. The rows and columns are flipped.
        /// </summary>
        public Matrix3x3f Transpose
        {
            get
            {
                Matrix3x3f transpose = new Matrix3x3f();

                transpose.m00 = m00;
                transpose.m01 = m10;
                transpose.m02 = m20;

                transpose.m10 = m01;
                transpose.m11 = m11;
                transpose.m12 = m21;

                transpose.m20 = m02;
                transpose.m21 = m12;
                transpose.m22 = m22;

                return transpose;
            }
        }

        /// <summary>
        /// The determinate of a matrix. 
        /// </summary>
        public float Determinant
        {
            get
            {
                float cofactor00 = m11 * m22 - m12 * m21;
                float cofactor10 = m12 * m20 - m10 * m22;
                float cofactor20 = m10 * m21 - m11 * m20;

                float det = m00 * cofactor00 + m01 * cofactor10 + m02 * cofactor20;

                return det;
            }
        }

        /// <summary>
        /// The inverse of the matrix.
        /// A matrix multipled by its inverse is the idenity.
        /// </summary>
        public Matrix3x3f Inverse
        {
            get
            {
                Matrix3x3f inverse;
                TryInverse(out inverse);
                return inverse;
            }
        }

        /// <summary>
        /// Add two matrices.
        /// </summary>
        public static Matrix3x3f operator +(Matrix3x3f m1, Matrix3x3f m2)
        {
            Matrix3x3f kSum = new Matrix3x3f();
            kSum.m00 = m1.m00 + m2.m00;
            kSum.m01 = m1.m01 + m2.m01;
            kSum.m02 = m1.m02 + m2.m02;

            kSum.m10 = m1.m10 + m2.m10;
            kSum.m11 = m1.m11 + m2.m11;
            kSum.m12 = m1.m12 + m2.m12;

            kSum.m20 = m1.m20 + m2.m20;
            kSum.m21 = m1.m21 + m2.m21;
            kSum.m22 = m1.m22 + m2.m22;

            return kSum;
        }

        /// <summary>
        /// Subtract two matrices.
        /// </summary>
        public static Matrix3x3f operator -(Matrix3x3f m1, Matrix3x3f m2)
        {
            Matrix3x3f kSum = new Matrix3x3f();
            kSum.m00 = m1.m00 - m2.m00;
            kSum.m01 = m1.m01 - m2.m01;
            kSum.m02 = m1.m02 - m2.m02;

            kSum.m10 = m1.m10 - m2.m10;
            kSum.m11 = m1.m11 - m2.m11;
            kSum.m12 = m1.m12 - m2.m12;

            kSum.m20 = m1.m20 - m2.m20;
            kSum.m21 = m1.m21 - m2.m21;
            kSum.m22 = m1.m22 - m2.m22;
            return kSum;
        }

        /// <summary>
        /// Multiply two matrices.
        /// </summary>
        public static Matrix3x3f operator *(Matrix3x3f m1, Matrix3x3f m2)
        {
            Matrix3x3f kProd = new Matrix3x3f();

            kProd.m00 = m1.m00 * m2.m00 + m1.m01 * m2.m10 + m1.m02 * m2.m20;
            kProd.m01 = m1.m00 * m2.m01 + m1.m01 * m2.m11 + m1.m02 * m2.m21;
            kProd.m02 = m1.m00 * m2.m02 + m1.m01 * m2.m12 + m1.m02 * m2.m22;

            kProd.m10 = m1.m10 * m2.m00 + m1.m11 * m2.m10 + m1.m12 * m2.m20;
            kProd.m11 = m1.m10 * m2.m01 + m1.m11 * m2.m11 + m1.m12 * m2.m21;
            kProd.m12 = m1.m10 * m2.m02 + m1.m11 * m2.m12 + m1.m12 * m2.m22;

            kProd.m20 = m1.m20 * m2.m00 + m1.m21 * m2.m10 + m1.m22 * m2.m20;
            kProd.m21 = m1.m20 * m2.m01 + m1.m21 * m2.m11 + m1.m22 * m2.m21;
            kProd.m22 = m1.m20 * m2.m02 + m1.m21 * m2.m12 + m1.m22 * m2.m22;

            return kProd;
        }

        /// <summary>
        /// Multiply  a vector by a matrix.
        /// </summary>
        public static Vector3f operator *(Matrix3x3f m, Vector3f v)
        {
            Vector3f kProd = new Vector3f();

			kProd.x = m.m00 * v.x + m.m01 * v.y + m.m02 * v.z;
			kProd.y = m.m10 * v.x + m.m11 * v.y + m.m12 * v.z;
			kProd.z = m.m20 * v.x + m.m21 * v.y + m.m22 * v.z;

            return kProd;
        }

        /// <summary>
        /// Multiply a matrix by a scalar.
        /// </summary>
        public static Matrix3x3f operator *(Matrix3x3f m1, float s)
        {
            Matrix3x3f kProd = new Matrix3x3f();
            kProd.m00 = m1.m00 * s;
            kProd.m01 = m1.m01 * s;
            kProd.m02 = m1.m02 * s;

            kProd.m10 = m1.m10 * s;
            kProd.m11 = m1.m11 * s;
            kProd.m12 = m1.m12 * s;

            kProd.m20 = m1.m20 * s;
            kProd.m21 = m1.m21 * s;
            kProd.m22 = m1.m22 * s;

            return kProd;
        }

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public static bool operator ==(Matrix3x3f m1, Matrix3x3f m2)
        {

            if (m1.m00 != m2.m00) return false;
            if (m1.m01 != m2.m01) return false;
            if (m1.m02 != m2.m02) return false;

            if (m1.m10 != m2.m10) return false;
            if (m1.m11 != m2.m11) return false;
            if (m1.m12 != m2.m12) return false;

            if (m1.m20 != m2.m20) return false;
            if (m1.m21 != m2.m21) return false;
            if (m1.m22 != m2.m22) return false;

            return true;
        }

        /// <summary>
        /// Are these matrices not equal.
        /// </summary>
        public static bool operator !=(Matrix3x3f m1, Matrix3x3f m2)
        {
            if (m1.m00 != m2.m00) return true;
            if (m1.m01 != m2.m01) return true;
            if (m1.m02 != m2.m02) return true;

            if (m1.m10 != m2.m10) return true;
            if (m1.m11 != m2.m11) return true;
            if (m1.m12 != m2.m12) return true;

            if (m1.m20 != m2.m20) return true;
            if (m1.m21 != m2.m21) return true;
            if (m1.m22 != m2.m22) return true;

            return false;
        }

		/// <summary>
		/// Are these matrices equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Matrix3x3f)) return false;
			
			Matrix3x3f mat = (Matrix3x3f)obj;
			
			return this == mat;
		}

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public bool Equals(Matrix3x3f mat)
        {
            return this == mat;
        }

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public bool EqualsWithError(Matrix3x3f m, float eps)
        {
            if (Math.Abs(m00 - m.m00) > eps) return false;
            if (Math.Abs(m10 - m.m10) > eps) return false;
            if (Math.Abs(m20 - m.m20) > eps) return false;

            if (Math.Abs(m01 - m.m01) > eps) return false;
            if (Math.Abs(m11 - m.m11) > eps) return false;
            if (Math.Abs(m21 - m.m21) > eps) return false;

            if (Math.Abs(m02 - m.m02) > eps) return false;
            if (Math.Abs(m12 - m.m12) > eps) return false;
            if (Math.Abs(m22 - m.m22) > eps) return false;

            return true;
        }
		
		/// <summary>
		/// Matrices hash code. 
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 0;
			
			for(int i = 0; i < 9; i++)
				hash ^= this[i].GetHashCode();
			
			return hash;
		}

        /// <summary>
        /// A matrix as a string.
        /// </summary>
        public override string ToString()
        {
			return 	this[0, 0] + "," + this[0, 1] + "," + this[0, 2] + "\n" +
					this[1, 0] + "," + this[1, 1] + "," + this[1, 2] + "\n" +
					this[2, 0] + "," + this[2, 1] + "," + this[2, 2];
        }

        /// <summary>
        /// The Inverse of the matrix copied into mInv.
        /// Returns false if the matrix has no inverse.
        /// A matrix multipled by its inverse is the idenity.
        /// </summary>
        public bool TryInverse(out Matrix3x3f mInv)
        {
            // Invert a 3x3 using cofactors.  This is about 8 times faster than
            // the Numerical Recipes code which uses Gaussian elimination.
			mInv.m00 = m11 * m22 - m12 * m21;
			mInv.m01 = m02 * m21 - m01 * m22;
			mInv.m02 = m01 * m12 - m02 * m11;
			mInv.m10 = m12 * m20 - m10 * m22;
			mInv.m11 = m00 * m22 - m02 * m20;
			mInv.m12 = m02 * m10 - m00 * m12;
			mInv.m20 = m10 * m21 - m11 * m20;
			mInv.m21 = m01 * m20 - m00 * m21;
			mInv.m22 = m00 * m11 - m01 * m10;

			float fDet = m00 * mInv.m00 + m01 * mInv.m10 + m02 * mInv.m20;

            if (Math.Abs(fDet) <= 1e-06f)
            {
                mInv = Identity;
                return false;
            }

            float fInvDet = 1.0f / fDet;

            mInv.m00 *= fInvDet; mInv.m01 *= fInvDet; mInv.m02 *= fInvDet;
            mInv.m10 *= fInvDet; mInv.m11 *= fInvDet; mInv.m12 *= fInvDet;
            mInv.m20 *= fInvDet; mInv.m21 *= fInvDet; mInv.m22 *= fInvDet;

            return true;
        }

        /// <summary>
        /// Get the ith column as a vector.
        /// </summary>
        public Vector3f GetColumn(int iCol)
        {
			return new Vector3f(this[0, iCol], this[1, iCol], this[2, iCol]);
        }

        /// <summary>
        /// Set the ith column from avector.
        /// </summary>
        public void SetColumn(int iCol, Vector3f v)
        {
			this[0, iCol] = v.x;
			this[1, iCol] = v.y;
			this[2, iCol] = v.z;
        }

        /// <summary>
        /// Get the ith row as a vector.
        /// </summary>
        public Vector3f GetRow(int iRow)
        {
			return new Vector3f(this[iRow, 0], this[iRow, 1], this[iRow, 2]);
        }

        /// <summary>
        /// Set the ith row from avector.
        /// </summary>
        public void SetRow(int iRow, Vector3f v)
        {
			this[iRow, 0] = v.x;
			this[iRow, 1] = v.y;
			this[iRow, 2] = v.z;
        }

        /// <summary>
        /// Convert to a single precision 4 dimension matrix.
        /// </summary>
        public Matrix4x4f ToMatrix4x4f()
        {
			return new Matrix4x4f(m00, m01, m02, 0.0f,
			                      m10, m11, m12, 0.0f,
			                      m20, m21, m22, 0.0f,
                                  0.0f, 0.0f, 0.0f, 0.0f);
        }

        /// <summary>
        /// Create a translation out of a vector.
        /// </summary>
        static public Matrix3x3f Translate(Vector2f v)
        {
            return new Matrix3x3f(1, 0, v.x,
                                  0, 1, v.y,
                                  0, 0, 1);
        }

        /// <summary>
        /// Create a scale out of a vector.
        /// </summary>
        static public Matrix3x3f Scale(Vector2f v)
        {
            return new Matrix3x3f(v.x, 0, 0,
                                  0, v.y, 0, 
                                  0, 0, 1);                      
        }

        /// <summary>
        /// Create a scale out of a vector.
        /// </summary>
        static public Matrix3x3f Scale(float s)
        {
            return new Matrix3x3f(s, 0, 0,
                                  0, s, 0,
                                  0, 0, 1);
        }

        /// <summary>
        /// Create a rotation out of a angle.
        /// </summary>
        static public Matrix3x3f RotateX(double angle)
        {
            float ca = (float)Math.Cos(angle * Math.PI / 180.0);
            float sa = (float)Math.Sin(angle * Math.PI / 180.0);

            return new Matrix3x3f(1, 0, 0,
                                  0, ca, -sa,
                                  0, sa, ca);
        }

        /// <summary>
        /// Create a rotation out of a angle.
        /// </summary>
        static public Matrix3x3f RotateY(double angle)
        {
            float ca = (float)Math.Cos(angle * Math.PI / 180.0);
            float sa = (float)Math.Sin(angle * Math.PI / 180.0);

            return new Matrix3x3f(ca, 0, sa,
                                  0, 1, 0,
                                  -sa, 0, ca);
        }

        /// <summary>
        /// Create a rotation out of a angle.
        /// </summary>
        static public Matrix3x3f RotateZ(double angle)
        {
            float ca = (float)Math.Cos(angle * Math.PI / 180.0);
            float sa = (float)Math.Sin(angle * Math.PI / 180.0);

            return new Matrix3x3f(ca, -sa, 0,
                                  sa, ca, 0,
                                  0, 0, 1);
        }

        /// <summary>
        /// Create a rotation out of a vector.
        /// </summary>
        static public Matrix3x3f Rotate(Vector3f euler)
        {
            return Quaternion3f.FromEuler(euler).ToMatrix3x3f();
        }

    }

}

























