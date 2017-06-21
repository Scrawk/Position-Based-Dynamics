using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{
    /// <summary>
    /// A single precision 2 dimension matrix
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix2x2f
    {
        /// <summary>
        /// The matrix
        /// </summary>
        public float m00, m10, m01, m11;

        /// <summary>
        /// The Matrix Idenity.
        /// </summary>
        static readonly public Matrix2x2f Identity = new Matrix2x2f(1, 0, 0, 1);

        /// <summary>
        /// A matrix from the following varibles.
        /// </summary>
        public Matrix2x2f(float m00, float m01, float m10, float m11)
        {
			this.m00 = m00; this.m01 = m01;
			this.m10 = m10; this.m11 = m11;
        }

        /// <summary>
        /// A matrix from the following varibles.
        /// </summary>
        public Matrix2x2f(float v)
        {
            m00 = v; m01 = v;
            m10 = v; m11 = v;
        }

        /// <summary>
        /// A matrix copied from a array of varibles.
        /// </summary>
        public Matrix2x2f(float[,] m)
        {
            m00 = m[0,0]; m01 = m[0,1];
            m10 = m[1,0]; m11 = m[1,1];
        }

		/// <summary>
		/// A matrix copied from a array of varibles.
		/// </summary>
		public Matrix2x2f(float[] m)
		{
            m00 = m[0 + 0 * 2]; m01 = m[0 + 1 * 2];
            m10 = m[1 + 0 * 2]; m11 = m[1 + 1 * 2];
		}

        /// <summary>
        /// A matrix copied from the matrix m.
        /// </summary>
        public Matrix2x2f(Matrix2x2f m)
        {
            m00 = m.m00; m01 = m.m01;
            m10 = m.m10; m11 = m.m11;
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
                    case 2: return m01;
                    case 3: return m11;
                    default: throw new IndexOutOfRangeException("Matrix2x2f index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m01 = value; break;
                    case 3: m11 = value; break;
                    default: throw new IndexOutOfRangeException("Matrix2x2f index out of range: " + i);
                }
            }
        }

        /// <summary>
        /// The transpose of the matrix. The rows and columns are flipped.
        /// </summary>
        public Matrix2x2f Transpose
        {
            get
            {
                Matrix2x2f kTranspose = new Matrix2x2f();
                kTranspose.m00 = m00;
                kTranspose.m10 = m01;
                kTranspose.m01 = m10;
                kTranspose.m11 = m11;

                return kTranspose;
            }
        }

        /// <summary>
        /// The determinate of a matrix. 
        /// </summary>
        public float Determinant
        {
            get
            {
                return m00 * m11 - m10 * m01;
            }
        }

        /// <summary>
        /// The inverse of the matrix.
        /// A matrix multipled by its inverse is the idenity.
        /// </summary>
        public Matrix2x2f Inverse
        {
            get
            {
                Matrix2x2f kInverse = new Matrix2x2f();
                TryInverse(ref kInverse);
                return kInverse;
            }
        }

        public float Trace
        {
            get
            {
                return m00 + m11;
            }
        }

        /// <summary>
        /// Access the varible at index i,j.
        /// </summary>
        public float this[int i, int j]
        {
            get
            {
                int k = i + j * 2;
                switch (k)
                {
                    case 0: return m00;
                    case 1: return m10;
                    case 2: return m01;
                    case 3: return m11;
                    default: throw new IndexOutOfRangeException("Matrix2x2f index out of range: " + k);
                }
            }
            set
            {
                int k = i + j * 2;
                switch (k)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m01 = value; break;
                    case 3: m11 = value; break;
                    default: throw new IndexOutOfRangeException("Matrix2x2f index out of range: " + k);
                }
            }
        }

        /// <summary>
        /// Add two matrices.
        /// </summary>
        public static Matrix2x2f operator +(Matrix2x2f m1, Matrix2x2f m2)
        {
            Matrix2x2f kSum = new Matrix2x2f();
            kSum.m00 = m1.m00 + m2.m00;
            kSum.m10 = m1.m10 + m2.m10;
            kSum.m01 = m1.m01 + m2.m01;
            kSum.m11 = m1.m11 + m2.m11;

            return kSum;
        }

        /// <summary>
        /// Subtract two matrices.
        /// </summary>
        public static Matrix2x2f operator -(Matrix2x2f m1, Matrix2x2f m2)
        {
            Matrix2x2f kSum = new Matrix2x2f();
            kSum.m00 = m1.m00 - m2.m00;
            kSum.m10 = m1.m10 - m2.m10;
            kSum.m01 = m1.m01 - m2.m01;
            kSum.m11 = m1.m11 - m2.m11;
            return kSum;
        }

        /// <summary>
        /// Multiply two matrices.
        /// </summary>
        public static Matrix2x2f operator *(Matrix2x2f m1, Matrix2x2f m2)
        {
            Matrix2x2f kProd = new Matrix2x2f();
            kProd.m00 = m1.m00 * m2.m00 + m1.m01 * m2.m10;
            kProd.m10 = m1.m10 * m2.m00 + m1.m11 * m2.m10;
            kProd.m01 = m1.m00 * m2.m01 + m1.m01 * m2.m11;
            kProd.m11 = m1.m10 * m2.m01 + m1.m11 * m2.m11;

            return kProd;
        }

        /// <summary>
        /// Multiply  a vector by a matrix.
        /// </summary>
        public static Vector2f operator *(Matrix2x2f m, Vector2f v)
        {
            Vector2f kProd = new Vector2f();

			kProd.x = m.m00 * v.x + m.m01 * v.y;
			kProd.y = m.m10 * v.x + m.m11 * v.y;

            return kProd;
        }

        /// <summary>
        /// Multiply a matrix by a scalar.
        /// </summary>
        public static Matrix2x2f operator *(Matrix2x2f m, float s)
        {
            Matrix2x2f kProd = new Matrix2x2f();
            kProd.m00 = m.m00 * s;
            kProd.m10 = m.m10 * s;
            kProd.m01 = m.m01 * s;
            kProd.m11 = m.m11 * s;

            return kProd;
        }

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public static bool operator ==(Matrix2x2f m1, Matrix2x2f m2)
        {

            if (m1.m00 != m2.m00) return false;
            if (m1.m10 != m2.m10) return false;
            if (m1.m01 != m2.m01) return false;
            if (m1.m11 != m2.m11) return false;

            return true;
        }

        /// <summary>
        /// Are these matrices not equal.
        /// </summary>
        public static bool operator !=(Matrix2x2f m1, Matrix2x2f m2)
        {
            if (m1.m00 != m2.m00) return true;
            if (m1.m10 != m2.m10) return true;
            if (m1.m01 != m2.m01) return true;
            if (m1.m11 != m2.m11) return true;

            return false;
        }

		/// <summary>
		/// Are these matrices equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Matrix2x2f)) return false;
			
			Matrix2x2f mat = (Matrix2x2f)obj;
			
			return this == mat;
		}

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public bool Equals(Matrix2x2f mat)
        {
            return this == mat;
        }

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public bool EqualsWithError(Matrix2x2f m, float eps)
        {
            if (Math.Abs(m00 - m.m00) > eps) return false;
            if (Math.Abs(m10 - m.m10) > eps) return false;
            if (Math.Abs(m01 - m.m01) > eps) return false;
            if (Math.Abs(m11 - m.m11) > eps) return false;

            return true;
        }
		
		/// <summary>
		/// Matrices hash code. 
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 0;
			
			for(int i = 0; i < 4; i++)
				hash ^= this[i].GetHashCode();
			
			return hash;
		}

        /// <summary>
        /// A matrix as a string.
        /// </summary>
        public override string ToString()
        {
            return this[0, 0] + "," + this[0, 1] + "\n" + this[1, 0] + "," + this[1, 1];
        }

        /// <summary>
        /// The Inverse of the matrix copied into mInv.
        /// Returns false if the matrix has no inverse.
        /// A matrix multipled by its inverse is the idenity.
        /// </summary>
        public bool TryInverse(ref Matrix2x2f mInv)
        {
            float det = Determinant;

            if (Math.Abs(det) <= 1e-06f)
            {
                return false;
            }

            float invDet = 1.0f / det;

			mInv.m00 = m11 * invDet;
			mInv.m01 = -m01 * invDet;
			mInv.m10 = -m10 * invDet;
			mInv.m11 = m00 * invDet;
            return true;
        }

        /// <summary>
        /// Get the ith column as a vector.
        /// </summary>
        public Vector2f GetColumn(int iCol)
        {
			return new Vector2f(this[0, iCol], this[1, iCol]);
        }

        /// <summary>
        /// Set the ith column from avector.
        /// </summary>
        public void SetColumn(int iCol, Vector2f v)
        {
			this[0, iCol] = v.x;
			this[1, iCol] = v.y;
        }

        /// <summary>
        /// Get the ith row as a vector.
        /// </summary>
        public Vector2f GetRow(int iRow)
        {
			return new Vector2f(this[iRow, 0], this[iRow, 1]);
        }

        /// <summary>
        /// Set the ith row from avector.
        /// </summary>
        public void SetRow(int iRow, Vector2f v)
        {
			this[iRow, 0 ] = v.x;
			this[iRow, 1 ] = v.y;
        }

        /// <summary>
        /// Convert to a single precision 3 dimension matrix.
        /// </summary>
        public Matrix3x3f ToMatrix3x3f()
        {
			return new Matrix3x3f(m00, m01, 0.0f,
			                      m10, m11, 0.0f,
                                  0.0f, 0.0f, 0.0f);
        }

    }

}

























