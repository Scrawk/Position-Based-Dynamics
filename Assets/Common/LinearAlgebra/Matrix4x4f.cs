using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Common.Mathematics.LinearAlgebra
{
    /// <summary>
    /// A single precision 3 dimension matrix
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4x4f : IEquatable<Matrix4x4f>
	{

        /// <summary>
        /// The matrix
        /// </summary>
        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        /// <summary>
        /// The Matrix Idenity.
        /// </summary>
        static readonly public Matrix4x4f Identity = new Matrix4x4f(1, 0, 0, 0,
                                                                    0, 1, 0, 0,
                                                                    0, 0, 1, 0,
                                                                    0, 0, 0, 1);

        /// <summary>
        /// A matrix from the following varibles.
        /// </summary>
        public Matrix4x4f(float m00, float m01, float m02, float m03,
                          float m10, float m11, float m12, float m13,
                          float m20, float m21, float m22, float m23,
                          float m30, float m31, float m32, float m33)
        {
			this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
			this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
			this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
			this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;

        }

        /// <summary>
        /// A matrix from the following varibles.
        /// </summary>
        public Matrix4x4f(float v)
        {
			m00 = v; m01 = v; m02 = v; m03 = v;
			m10 = v; m11 = v; m12 = v; m13 = v;
			m20 = v; m21 = v; m22 = v; m23 = v;
			m30 = v; m31 = v; m32 = v; m33 = v;
        }

        /// <summary>
        /// A matrix copied from a array of varibles.
        /// </summary>
        public Matrix4x4f(float[,] m)
        {
            m00 = m[0,0]; m01 = m[0,1]; m02 = m[0,2]; m03 = m[0,3];
			m10 = m[1,0]; m11 = m[1,1]; m12 = m[1,2]; m13 = m[1,3];
			m20 = m[2,0]; m21 = m[2,1]; m22 = m[2,2]; m23 = m[2,3];
			m30 = m[3,0]; m31 = m[3,1]; m32 = m[3,2]; m33 = m[3,3];
        }

		/// <summary>
		/// A matrix copied from a array of varibles.
		/// </summary>
		public Matrix4x4f(float[] m)
		{
			m00 = m[0+0* 4]; m01 = m[0+1* 4]; m02 = m[0+2* 4]; m03 = m[0+3* 4];
			m10 = m[1+0* 4]; m11 = m[1+1* 4]; m12 = m[1+2* 4]; m13 = m[1+3* 4];
			m20 = m[2+0* 4]; m21 = m[2+1* 4]; m22 = m[2+2* 4]; m23 = m[2+3* 4];
			m30 = m[3+0* 4]; m31 = m[3+1* 4]; m32 = m[3+2* 4]; m33 = m[3+3* 4];
		}

        /// <summary>
        /// A matrix copied from the matrix m.
        /// </summary>
        public Matrix4x4f(Matrix4x4f m)
        {
            m00 = m.m00; m01 = m.m01; m02 = m.m02; m03 = m.m03;
			m10 = m.m10; m11 = m.m11; m12 = m.m12; m13 = m.m13;
			m20 = m.m20; m21 = m.m21; m22 = m.m22; m23 = m.m23;
			m30 = m.m30; m31 = m.m31; m32 = m.m32; m33 = m.m33;
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
                    case 3: return m30;

                    case 4: return m01;
                    case 5: return m11;
                    case 6: return m21;
                    case 7: return m31;

                    case 8: return m02;
                    case 9: return m12;
                    case 10: return m22;
                    case 11: return m32;

                    case 12: return m03;
                    case 13: return m13;
                    case 14: return m23;
                    case 15: return m33;
                    default: throw new IndexOutOfRangeException("Matrix4x4f index out of range: " + i);
                }
            }
            set
            {
                switch (i)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m20 = value; break;
                    case 3: m30 = value; break;

                    case 4: m01 = value; break;
                    case 5: m11 = value; break;
                    case 6: m21 = value; break;
                    case 7: m31 = value; break;

                    case 8: m02 = value; break;
                    case 9: m12 = value; break;
                    case 10: m22 = value; break;
                    case 11: m32 = value; break;

                    case 12: m03 = value; break;
                    case 13: m13 = value; break;
                    case 14: m23 = value; break;
                    case 15: m33 = value; break;
                    default: throw new IndexOutOfRangeException("Matrix4x4f index out of range: " + i);
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
                int k = i + j * 4;
                switch (k)
                {
                    case 0: return m00;
                    case 1: return m10;
                    case 2: return m20;
                    case 3: return m30;

                    case 4: return m01;
                    case 5: return m11;
                    case 6: return m21;
                    case 7: return m31;

                    case 8: return m02;
                    case 9: return m12;
                    case 10: return m22;
                    case 11: return m32;

                    case 12: return m03;
                    case 13: return m13;
                    case 14: return m23;
                    case 15: return m33;
                    default: throw new IndexOutOfRangeException("Matrix4x4f index out of range: " + k);
                }
            }
            set
            {
                int k = i + j * 4;
                switch (k)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m20 = value; break;
                    case 3: m30 = value; break;

                    case 4: m01 = value; break;
                    case 5: m11 = value; break;
                    case 6: m21 = value; break;
                    case 7: m31 = value; break;

                    case 8: m02 = value; break;
                    case 9: m12 = value; break;
                    case 10: m22 = value; break;
                    case 11: m32 = value; break;

                    case 12: m03 = value; break;
                    case 13: m13 = value; break;
                    case 14: m23 = value; break;
                    case 15: m33 = value; break;
                    default: throw new IndexOutOfRangeException("Matrix4x4f index out of range: " + k);
                }
            }
        }

        /// <summary>
        /// The transpose of the matrix. The rows and columns are flipped.
        /// </summary>
        public Matrix4x4f Transpose
        {
            get
            {
                Matrix4x4f transpose = new Matrix4x4f();
                transpose.m00 = m00;
                transpose.m01 = m10;
                transpose.m02 = m20;
                transpose.m03 = m30;

                transpose.m10 = m01;
                transpose.m11 = m11;
                transpose.m12 = m21;
                transpose.m13 = m31;

                transpose.m20 = m02;
                transpose.m21 = m12;
                transpose.m22 = m22;
                transpose.m23 = m32;

                transpose.m30 = m03;
                transpose.m31 = m13;
                transpose.m32 = m23;
                transpose.m33 = m33;

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
                return (m00 * Minor(1, 2, 3, 1, 2, 3) -
                        m01 * Minor(1, 2, 3, 0, 2, 3) +
                        m02 * Minor(1, 2, 3, 0, 1, 3) -
                        m03 * Minor(1, 2, 3, 0, 1, 2));
            }
        }

        /// <summary>
        /// The adjoint of a matrix. 
        /// </summary>
        public Matrix4x4f Adjoint
        {
            get
            {
                return new Matrix4x4f(
                        Minor(1, 2, 3, 1, 2, 3),
                        -Minor(0, 2, 3, 1, 2, 3),
                        Minor(0, 1, 3, 1, 2, 3),
                        -Minor(0, 1, 2, 1, 2, 3),

                        -Minor(1, 2, 3, 0, 2, 3),
                        Minor(0, 2, 3, 0, 2, 3),
                        -Minor(0, 1, 3, 0, 2, 3),
                        Minor(0, 1, 2, 0, 2, 3),

                        Minor(1, 2, 3, 0, 1, 3),
                        -Minor(0, 2, 3, 0, 1, 3),
                        Minor(0, 1, 3, 0, 1, 3),
                        -Minor(0, 1, 2, 0, 1, 3),

                        -Minor(1, 2, 3, 0, 1, 2),
                        Minor(0, 2, 3, 0, 1, 2),
                        -Minor(0, 1, 3, 0, 1, 2),
                        Minor(0, 1, 2, 0, 1, 2));
            }
        }

        /// <summary>
        /// The inverse of the matrix.
        /// A matrix multipled by its inverse is the idenity.
        /// </summary>
        public Matrix4x4f Inverse
        {
            get
            {
                return Adjoint * (1.0f / Determinant);
            }
        }

        public float Trace
        {
            get
            {
                return m00 + m11 + m22 + m33;
            }
        }

        /// <summary>
        /// Add two matrices.
        /// </summary>
        public static Matrix4x4f operator +(Matrix4x4f m1, Matrix4x4f m2)
        {
            Matrix4x4f kSum = new Matrix4x4f();
            kSum.m00 = m1.m00 + m2.m00;
            kSum.m01 = m1.m01 + m2.m01;
            kSum.m02 = m1.m02 + m2.m02;
            kSum.m03 = m1.m03 + m2.m03;

            kSum.m10 = m1.m10 + m2.m10;
            kSum.m11 = m1.m11 + m2.m11;
            kSum.m12 = m1.m12 + m2.m12;
            kSum.m13 = m1.m13 + m2.m13;

            kSum.m20 = m1.m20 + m2.m20;
            kSum.m21 = m1.m21 + m2.m21;
            kSum.m22 = m1.m22 + m2.m22;
            kSum.m23 = m1.m23 + m2.m23;

            kSum.m30 = m1.m30 + m2.m30;
            kSum.m31 = m1.m31 + m2.m31;
            kSum.m32 = m1.m32 + m2.m32;
            kSum.m33 = m1.m33 + m2.m33;

            return kSum;
        }

        /// <summary>
        /// Subtract two matrices.
        /// </summary>
        public static Matrix4x4f operator -(Matrix4x4f m1, Matrix4x4f m2)
        {
            Matrix4x4f kSum = new Matrix4x4f();
            kSum.m00 = m1.m00 - m2.m00;
            kSum.m01 = m1.m01 - m2.m01;
            kSum.m02 = m1.m02 - m2.m02;
            kSum.m03 = m1.m03 - m2.m03;

            kSum.m10 = m1.m10 - m2.m10;
            kSum.m11 = m1.m11 - m2.m11;
            kSum.m12 = m1.m12 - m2.m12;
            kSum.m13 = m1.m13 - m2.m13;

            kSum.m20 = m1.m20 - m2.m20;
            kSum.m21 = m1.m21 - m2.m21;
            kSum.m22 = m1.m22 - m2.m22;
            kSum.m23 = m1.m23 - m2.m23;

            kSum.m30 = m1.m30 - m2.m30;
            kSum.m31 = m1.m31 - m2.m31;
            kSum.m32 = m1.m32 - m2.m32;
            kSum.m33 = m1.m33 - m2.m33;
            return kSum;
        }

        /// <summary>
        /// Multiply two matrices.
        /// </summary>
        public static Matrix4x4f operator *(Matrix4x4f m1, Matrix4x4f m2)
        {
            Matrix4x4f kProd = new Matrix4x4f();
   
            kProd.m00 = m1.m00 * m2.m00 + m1.m01 * m2.m10 + m1.m02 * m2.m20 + m1.m03 * m2.m30;
            kProd.m01 = m1.m00 * m2.m01 + m1.m01 * m2.m11 + m1.m02 * m2.m21 + m1.m03 * m2.m31;
            kProd.m02 = m1.m00 * m2.m02 + m1.m01 * m2.m12 + m1.m02 * m2.m22 + m1.m03 * m2.m32;
            kProd.m03 = m1.m00 * m2.m03 + m1.m01 * m2.m13 + m1.m02 * m2.m23 + m1.m03 * m2.m33;

            kProd.m10 = m1.m10 * m2.m00 + m1.m11 * m2.m10 + m1.m12 * m2.m20 + m1.m13 * m2.m30;
            kProd.m11 = m1.m10 * m2.m01 + m1.m11 * m2.m11 + m1.m12 * m2.m21 + m1.m13 * m2.m31;
            kProd.m12 = m1.m10 * m2.m02 + m1.m11 * m2.m12 + m1.m12 * m2.m22 + m1.m13 * m2.m32;
            kProd.m13 = m1.m10 * m2.m03 + m1.m11 * m2.m13 + m1.m12 * m2.m23 + m1.m13 * m2.m33;

            kProd.m20 = m1.m20 * m2.m00 + m1.m21 * m2.m10 + m1.m22 * m2.m20 + m1.m23 * m2.m30;
            kProd.m21 = m1.m20 * m2.m01 + m1.m21 * m2.m11 + m1.m22 * m2.m21 + m1.m23 * m2.m31;
            kProd.m22 = m1.m20 * m2.m02 + m1.m21 * m2.m12 + m1.m22 * m2.m22 + m1.m23 * m2.m32;
            kProd.m23 = m1.m20 * m2.m03 + m1.m21 * m2.m13 + m1.m22 * m2.m23 + m1.m23 * m2.m33;

            kProd.m30 = m1.m30 * m2.m00 + m1.m31 * m2.m10 + m1.m32 * m2.m20 + m1.m33 * m2.m30;
            kProd.m31 = m1.m30 * m2.m01 + m1.m31 * m2.m11 + m1.m32 * m2.m21 + m1.m33 * m2.m31;
            kProd.m32 = m1.m30 * m2.m02 + m1.m31 * m2.m12 + m1.m32 * m2.m22 + m1.m33 * m2.m32;
            kProd.m33 = m1.m30 * m2.m03 + m1.m31 * m2.m13 + m1.m32 * m2.m23 + m1.m33 * m2.m33;
            return kProd;
        }

        /// <summary>
        /// Multiply  a vector by a matrix.
        /// </summary>
        public static Vector3f operator *(Matrix4x4f m, Vector3f v)
        {
            Vector3f kProd = new Vector3f();

			float fInvW = 1.0f / (m.m30 * v.x + m.m31 * v.y + m.m32 * v.z + m.m33);

			kProd.x = (m.m00 * v.x + m.m01 * v.y + m.m02 * v.z + m.m03) * fInvW;
			kProd.y = (m.m10 * v.x + m.m11 * v.y + m.m12 * v.z + m.m13) * fInvW;
			kProd.z = (m.m20 * v.x + m.m21 * v.y + m.m22 * v.z + m.m23) * fInvW;

            return kProd;
        }

        /// <summary>
        /// Multiply  a vector by a matrix.
        /// </summary>
        public static Vector4f operator *(Matrix4x4f m, Vector4f v)
        {
            Vector4f kProd = new Vector4f();

			kProd.x = m.m00 * v.x + m.m01 * v.y + m.m02 * v.z + m.m03 * v.w;
			kProd.y = m.m10 * v.x + m.m11 * v.y + m.m12 * v.z + m.m13 * v.w;
			kProd.z = m.m20 * v.x + m.m21 * v.y + m.m22 * v.z + m.m23 * v.w;
			kProd.w = m.m30 * v.x + m.m31 * v.y + m.m32 * v.z + m.m33 * v.w;

            return kProd;
        }

        /// <summary>
        /// Multiply a matrix by a scalar.
        /// </summary>
        public static Matrix4x4f operator *(Matrix4x4f m1, float s)
        {
            Matrix4x4f kProd = new Matrix4x4f();
            kProd.m00 = m1.m00 * s;
            kProd.m01 = m1.m01 * s;
            kProd.m02 = m1.m02 * s;
            kProd.m03 = m1.m03 * s;

            kProd.m10 = m1.m10 * s;
            kProd.m11 = m1.m11 * s;
            kProd.m12 = m1.m12 * s;
            kProd.m13 = m1.m13 * s;

            kProd.m20 = m1.m20 * s;
            kProd.m21 = m1.m21 * s;
            kProd.m22 = m1.m22 * s;
            kProd.m23 = m1.m23 * s;

            kProd.m30 = m1.m30 * s;
            kProd.m31 = m1.m31 * s;
            kProd.m32 = m1.m32 * s;
            kProd.m33 = m1.m33 * s;
            return kProd;
        }

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public static bool operator ==(Matrix4x4f m1, Matrix4x4f m2)
        {

          if (m1.m00 != m2.m00) return false;
          if (m1.m01 != m2.m01) return false;
          if (m1.m02 != m2.m02) return false;
          if (m1.m03 != m2.m03) return false;

          if (m1.m10 != m2.m10) return false;
          if (m1.m11 != m2.m11) return false;
          if (m1.m12 != m2.m12) return false;
          if (m1.m13 != m2.m13) return false;

          if (m1.m20 != m2.m20) return false;
          if (m1.m21 != m2.m21) return false;
          if (m1.m22 != m2.m22) return false;
          if (m1.m23 != m2.m23) return false;

          if (m1.m30 != m2.m30) return false;
          if (m1.m31 != m2.m31) return false;
          if (m1.m32 != m2.m32) return false;
          if (m1.m33 != m2.m33) return false;

          return true;
        }

        /// <summary>
        /// Are these matrices not equal.
        /// </summary>
        public static bool operator !=(Matrix4x4f m1, Matrix4x4f m2)
        {
          if (m1.m00 != m2.m00) return true;
          if (m1.m01 != m2.m01) return true;
          if (m1.m02 != m2.m02) return true;
          if (m1.m03 != m2.m03) return true;

          if (m1.m10 != m2.m10) return true;
          if (m1.m11 != m2.m11) return true;
          if (m1.m12 != m2.m12) return true;
          if (m1.m13 != m2.m13) return true;

          if (m1.m20 != m2.m20) return true;
          if (m1.m21 != m2.m21) return true;
          if (m1.m22 != m2.m22) return true;
          if (m1.m23 != m2.m23) return true;

          if (m1.m30 != m2.m30) return true;
          if (m1.m31 != m2.m31) return true;
          if (m1.m32 != m2.m32) return true;

            return false;
        }

		/// <summary>
		/// Are these matrices equal.
		/// </summary>
		public override bool Equals (object obj)
		{
			if(!(obj is Matrix4x4f)) return false;

			Matrix4x4f mat = (Matrix4x4f)obj;

			return this == mat;
		}

        /// <summary>
		/// Are these matrices equal.
		/// </summary>
        public bool Equals (Matrix4x4f mat)
		{
			return this == mat;
		}

        /// <summary>
        /// Are these matrices equal.
        /// </summary>
        public bool EqualsWithError(Matrix4x4f m, float eps)
        {
            if (Math.Abs(m00 - m.m00) > eps) return false;
            if (Math.Abs(m10 - m.m10) > eps) return false;
            if (Math.Abs(m20 - m.m20) > eps) return false;
            if (Math.Abs(m30 - m.m30) > eps) return false;

            if (Math.Abs(m01 - m.m01) > eps) return false;
            if (Math.Abs(m11 - m.m11) > eps) return false;
            if (Math.Abs(m21 - m.m21) > eps) return false;
            if (Math.Abs(m31 - m.m31) > eps) return false;

            if (Math.Abs(m02 - m.m02) > eps) return false;
            if (Math.Abs(m12 - m.m12) > eps) return false;
            if (Math.Abs(m22 - m.m22) > eps) return false;
            if (Math.Abs(m32 - m.m32) > eps) return false;

            if (Math.Abs(m03 - m.m03) > eps) return false;
            if (Math.Abs(m13 - m.m13) > eps) return false;
            if (Math.Abs(m23 - m.m23) > eps) return false;
            if (Math.Abs(m33 - m.m33) > eps) return false;

            return true;
        }

		/// <summary>
		/// Matrices hash code. 
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 0;

			for(int i = 0; i < 16; i++)
				hash ^= this[i].GetHashCode();

			return hash;
		}

        /// <summary>
        /// A matrix as a string.
        /// </summary>
        public override string ToString()
        {
            return  this[0, 0] + "," + this[0, 1] + "," + this[0, 2] + "," + this[0, 3] + "\n" +
                    this[1, 0] + "," + this[1, 1] + "," + this[1, 2] + "," + this[1, 3] + "\n" +
                    this[2, 0] + "," + this[2, 1] + "," + this[2, 2] + "," + this[2, 3] + "\n" +
					this[3, 0] + "," + this[3, 1] + "," + this[3, 2] + "," + this[3, 3];
        }

        /// <summary>
        /// The minor of a matrix. 
        /// </summary>
        private float Minor(int r0, int r1, int r2, int c0, int c1, int c2)
        {
			return 	this[r0, c0] * (this[r1, c1] * this[r2, c2] - this[r2, c1] * this[r1, c2]) -
					this[r0, c1] * (this[r1, c0] * this[r2, c2] - this[r2, c0] * this[r1, c2]) +
					this[r0, c2] * (this[r1, c0] * this[r2, c1] - this[r2, c0] * this[r1, c1]);
        }

        /// <summary>
        /// The inverse of the matrix.
        /// A matrix multipled by its inverse is the idenity.
        /// </summary>
        public bool TryInverse(out Matrix4x4f mInv)
        {

            float det = Determinant;

            if (Math.Abs(det) <= 1e-06f)
            {
                mInv = Identity;
                return false;
            }

            mInv = Adjoint * (1.0f / det);
            return true;
        }

        /// <summary>
        /// Get the ith column as a vector.
        /// </summary>
        public Vector4f GetColumn(int iCol)
        {
			return new Vector4f(this[0, iCol], this[1, iCol], this[2, iCol], this[3, iCol]);
        }

        /// <summary>
        /// Set the ith column from a vector.
        /// </summary>
        public void SetColumn(int iCol, Vector4f v)
        {
			this[0, iCol] = v.x;
			this[1, iCol] = v.y;
			this[2, iCol] = v.z;
			this[3, iCol] = v.w;
        }

		/// <summary>
		/// Flip the ith column.
		/// </summary>
		public void FlipColumn(int iCol)
		{
			this[0, iCol] *= -1.0f;
			this[1, iCol] *= -1.0f;
			this[2, iCol] *= -1.0f;
			this[3, iCol] *= -1.0f;
		}

        /// <summary>
        /// Get the ith row as a vector.
        /// </summary>
        public Vector4f GetRow(int iRow)
        {
			return new Vector4f(this[iRow, 0], this[iRow, 1], this[iRow, 2], this[iRow, 3]);
        }

        /// <summary>
        /// Set the ith row from a vector.
        /// </summary>
        public void SetRow(int iRow, Vector4f v)
        {
			this[iRow, 0] = v.x;
			this[iRow, 1] = v.y;
			this[iRow, 2] = v.z;
			this[iRow, 3] = v.w;
        }

		/// <summary>
		/// Flip the ith row.
		/// </summary>
		public void FlipRow(int iRow)
		{
			this[iRow, 0] *= -1.0f;
			this[iRow, 1] *= -1.0f;
			this[iRow, 2] *= -1.0f;
			this[iRow, 3] *= -1.0f;
		}

        /// <summary>
        /// Convert to a 3 dimension matrix.
        /// </summary>
        public Matrix3x3f ToMatrix3x3f()
        {
            Matrix3x3f mat = new Matrix3x3f();

			mat.m00 = m00; mat.m01 = m01; mat.m02 = m02;
			mat.m10 = m10; mat.m11 = m11; mat.m12 = m12;
			mat.m20 = m20; mat.m21 = m21; mat.m22 = m22;

            return mat;
        }

        /// <summary>
        /// Create a translation, rotation and scale.
        /// </summary>
        static public Matrix4x4f TranslateRotateScale(Vector3f t, Quaternion3f r, Vector3f s)
        {
            Matrix4x4f T = Translate(t);
            Matrix4x4f R = r.ToMatrix4x4f();
            Matrix4x4f S = Scale(s);

            return T * R * S;
        }

        /// <summary>
        /// Create a translation and rotation.
        /// </summary>
        static public Matrix4x4f TranslateRotate(Vector3f t, Quaternion3f r)
        {
            Matrix4x4f T = Translate(t);
            Matrix4x4f R = r.ToMatrix4x4f();

            return T * R;
        }

        /// <summary>
        /// Create a translation and scale.
        /// </summary>
        static public Matrix4x4f TranslateScale(Vector3f t, Vector3f s)
        {
            Matrix4x4f T = Translate(t);
            Matrix4x4f S = Scale(s);

            return T * S;
        }

        /// <summary>
        /// Create a rotation and scale.
        /// </summary>
        static public Matrix4x4f RotateScale(Quaternion3f r, Vector3f s)
        {
            Matrix4x4f R = r.ToMatrix4x4f();
            Matrix4x4f S = Scale(s);

            return R * S;
        }

        /// <summary>
        /// Create a translation out of a vector.
        /// </summary>
        static public Matrix4x4f Translate(Vector3f v)
        {
            return new Matrix4x4f(	1, 0, 0, v.x,
                                    0, 1, 0, v.y,
                                    0, 0, 1, v.z,
                                    0, 0, 0, 1);
        }

        /// <summary>
        /// Create a scale out of a vector.
        /// </summary>
        static public Matrix4x4f Scale(Vector3f v)
        {
            return new Matrix4x4f(	v.x, 0, 0, 0,
                                    0, v.y, 0, 0,
                                    0, 0, v.z, 0,
                                    0, 0, 0, 1);
        }

        /// <summary>
        /// Create a scale out of a vector.
        /// </summary>
        static public Matrix4x4f Scale(float s)
        {
            return new Matrix4x4f(s, 0, 0, 0,
                                  0, s, 0, 0,
                                  0, 0, s, 0,
                                  0, 0, 0, 1);
        }

        /// <summary>
        /// Create a rotation out of a angle.
        /// </summary>
        static public Matrix4x4f RotateX(float angle)
        {
			float ca = (float)Math.Cos(angle * Math.PI / 180.0);
			float sa = (float)Math.Sin(angle * Math.PI / 180.0);

            return new Matrix4x4f(	1, 0, 0, 0,
                                    0, ca, -sa, 0,
                                    0, sa, ca, 0,
                                    0, 0, 0, 1);
        }

        /// <summary>
        /// Create a rotation out of a angle.
        /// </summary>
        static public Matrix4x4f RotateY(float angle)
        {
			float ca = (float)Math.Cos(angle * Math.PI / 180.0);
			float sa = (float)Math.Sin(angle * Math.PI / 180.0);

            return new Matrix4x4f(	ca, 0, sa, 0,
                                    0, 1, 0, 0,
                                    -sa, 0, ca, 0,
                                    0, 0, 0, 1);
        }

        /// <summary>
        /// Create a rotation out of a angle.
        /// </summary>
        static public Matrix4x4f RotateZ(float angle)
        {
			float ca = (float)Math.Cos(angle * Math.PI / 180.0);
			float sa = (float)Math.Sin(angle * Math.PI / 180.0);

            return new Matrix4x4f(	ca, -sa, 0, 0,
                                    sa, ca, 0, 0,
                                    0, 0, 1, 0,
                                    0, 0, 0, 1);
        }

        /// <summary>
        /// Create a rotation out of a vector.
        /// </summary>
        static public Matrix4x4f Rotate(Vector3f euler)
        {
            return Quaternion3f.FromEuler(euler).ToMatrix4x4f();
        }

        /// <summary>
        /// Create a perspective matrix.
        /// </summary>
        static public Matrix4x4f Perspective(float fovy, float aspect, float zNear, float zFar)
        {
			float f = 1.0f / (float)Math.Tan((fovy * Math.PI / 180.0) / 2.0);
            return new Matrix4x4f(	f / aspect, 0, 0, 0,
                                    0, f, 0, 0,
                                    0, 0, (zFar + zNear) / (zNear - zFar), (2.0f * zFar * zNear) / (zNear - zFar),
                                    0, 0, -1, 0);
        }

        /// <summary>
        /// Create a ortho matrix.
        /// </summary>
        static public Matrix4x4f Ortho(float xRight, float xLeft, float yTop, float yBottom, float zNear, float zFar)
        {
            float tx, ty, tz;
            tx = -(xRight + xLeft) / (xRight - xLeft);
            ty = -(yTop + yBottom) / (yTop - yBottom);
            tz = -(zFar + zNear) / (zFar - zNear);
            return new Matrix4x4f(	2.0f / (xRight - xLeft), 0, 0, tx,
                                    0, 2.0f / (yTop - yBottom), 0, ty,
                                    0, 0, -2.0f / (zFar - zNear), tz,
                                    0, 0, 0, 1);
        }

		/// <summary>
		/// Creates the matrix need to look at target from position.
		/// </summary>
		static public Matrix4x4f LookAt(Vector3f position, Vector3f target, Vector3f Up)
		{
			
			Vector3f zaxis = (position - target).Normalized;
			Vector3f xaxis = Up.Cross(zaxis).Normalized;
			Vector3f yaxis = zaxis.Cross(xaxis);
			
			return new Matrix4x4f(	xaxis.x, xaxis.y, xaxis.z, -Vector3f.Dot(xaxis, position),
			                      	yaxis.x, yaxis.y, yaxis.z, -Vector3f.Dot(yaxis, position),
			                      	zaxis.x, zaxis.y, zaxis.z, -Vector3f.Dot(zaxis, position),
			                      	0, 0, 0, 1);
		}


    }

}

























