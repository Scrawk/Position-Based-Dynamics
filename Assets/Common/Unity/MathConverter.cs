using UnityEngine;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;

namespace Common.Unity.Mathematics
{
	public static class MathConverter
	{
		public static Vector2 ToVector2(Vector2f v)
		{
			return new Vector2(v.x, v.y);
		}

		public static Vector2 ToVector2(Vector2d v)
		{
			return new Vector2((float)v.x, (float)v.y);
		}

		public static Vector2f ToVector2f(Vector2 v)
		{
			return new Vector2f(v.x, v.y);
		}

		public static Vector2d ToVector2d(Vector2 v)
		{
			return new Vector2d(v.x, v.y);
		}

		public static Vector3 ToVector3(Vector3f v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

        public static Vector3 ToVector3(Vector4f v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static Vector3 ToVector3(Vector3d v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

        public static Vector3 ToVector3(Vector4d v)
        {
            return new Vector3((float)v.x, (float)v.y, (float)v.z);
        }

        public static Vector3f ToVector3f(Vector3 v)
		{
			return new Vector3f(v.x, v.y, v.z);
		}
		
		public static Vector3d ToVector3d(Vector3 v)
		{
			return new Vector3d(v.x, v.y, v.z);
		}

		public static Vector4 ToVector4(Vector4f v)
		{
			return new Vector4(v.x, v.y, v.z, v.w);
		}

		public static Vector4 ToVector4(Vector4d v)
		{
			return new Vector4((float)v.x, (float)v.y, (float)v.z, (float)v.w);
		}

        public static Vector4 ToVector4(Vector3d v)
        {
            return new Vector4((float)v.x, (float)v.y, (float)v.z, 1.0f);
        }

        public static Vector4 ToVector4(Vector3f v)
        {
            return new Vector4(v.x, v.y, v.z, 1.0f);
        }

        public static Vector4f ToVector4f(Vector4 v)
		{
			return new Vector4f(v.x, v.y, v.z, v.w);
		}
		
		public static Vector4d ToVector4d(Vector4 v)
		{
			return new Vector4d(v.x, v.y, v.z, v.w);
		}

		public static Quaternion ToQuaternion(Quaternion3f q)
		{
			return new Quaternion(q.x, q.y, q.z, q.w);
		}
		
		public static Quaternion ToQuaternion(Quaternion3d q)
		{
			return new Quaternion((float)q.x, (float)q.y, (float)q.z, (float)q.w);
		}
		
		public static Quaternion3f ToQuaternion3f(Quaternion q)
		{
			return new Quaternion3f(q.x, q.y, q.z, q.w);
		}
		
		public static Quaternion3d ToQuaternion3d(Quaternion q)
		{
			return new Quaternion3d(q.x, q.y, q.z, q.w);
		}

		public static Matrix4x4 ToMatrix4x4(Matrix4x4f m)
		{
			Matrix4x4 mat = new Matrix4x4();
			
			mat.m00 = m[0,0]; mat.m01 = m[0,1]; mat.m02 = m[0,2]; mat.m03 = m[0,3];
			mat.m10 = m[1,0]; mat.m11 = m[1,1]; mat.m12 = m[1,2]; mat.m13 = m[1,3];
			mat.m20 = m[2,0]; mat.m21 = m[2,1]; mat.m22 = m[2,2]; mat.m23 = m[2,3];
			mat.m30 = m[3,0]; mat.m31 = m[3,1]; mat.m32 = m[3,2]; mat.m33 = m[3,3];
			
			return mat;
		}

		public static Matrix4x4 ToMatrix4x4(Matrix4x4d m)
		{
			Matrix4x4 mat = new Matrix4x4();
			
			mat.m00 = (float)m[0,0]; mat.m01 = (float)m[0,1]; mat.m02 = (float)m[0,2]; mat.m03 = (float)m[0,3];
			mat.m10 = (float)m[1,0]; mat.m11 = (float)m[1,1]; mat.m12 = (float)m[1,2]; mat.m13 = (float)m[1,3];
			mat.m20 = (float)m[2,0]; mat.m21 = (float)m[2,1]; mat.m22 = (float)m[2,2]; mat.m23 = (float)m[2,3];
			mat.m30 = (float)m[3,0]; mat.m31 = (float)m[3,1]; mat.m32 = (float)m[3,2]; mat.m33 = (float)m[3,3];
			
			return mat;
		}

		public static Matrix4x4f ToMatrix4x4f(Matrix4x4 mat)
		{
			Matrix4x4f m = new Matrix4x4f();
			
			m[0,0] = mat.m00; m[0,1] = mat.m01; m[0,2] = mat.m02; m[0,3] = mat.m03;
			m[1,0] = mat.m10; m[1,1] = mat.m11; m[1,2] = mat.m12; m[1,3] = mat.m13;
			m[2,0] = mat.m20; m[2,1] = mat.m21; m[2,2] = mat.m22; m[2,3] = mat.m23;
			m[3,0] = mat.m30; m[3,1] = mat.m31; m[3,2] = mat.m32; m[3,3] = mat.m33;
			
			return m;
		}

		public static Matrix4x4d ToMatrix4x4d(Matrix4x4 mat)
		{
			Matrix4x4d m = new Matrix4x4d();
			
			m[0,0] = mat.m00; m[0,1] = mat.m01; m[0,2] = mat.m02; m[0,3] = mat.m03;
			m[1,0] = mat.m10; m[1,1] = mat.m11; m[1,2] = mat.m12; m[1,3] = mat.m13;
			m[2,0] = mat.m20; m[2,1] = mat.m21; m[2,2] = mat.m22; m[2,3] = mat.m23;
			m[3,0] = mat.m30; m[3,1] = mat.m31; m[3,2] = mat.m32; m[3,3] = mat.m33;
			
			return m;
		}

        public static IList<Vector3> ToVector3(IList<Vector3f> list)
        {
            Vector3[] vectors = new Vector3[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector3(list[i].x, list[i].y, list[i].z);

            return vectors;
        }

        public static IList<Vector3> ToVector3(IList<Vector3d> list)
        {
            Vector3[] vectors = new Vector3[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector3((float)list[i].x, (float)list[i].y, (float)list[i].z);

            return vectors;
        }

        public static IList<Vector3f> ToVector3f(IList<Vector3> list)
        {
            Vector3f[] vectors = new Vector3f[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector3f(list[i].x, list[i].y, list[i].z);

            return vectors;
        }

        public static IList<Vector3d> ToVector3d(IList<Vector3> list)
        {
            Vector3d[] vectors = new Vector3d[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector3d(list[i].x, list[i].y, list[i].z);

            return vectors;
        }

        public static IList<Vector4> ToVector4(IList<Vector4f> list)
        {
            Vector4[] vectors = new Vector4[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector4(list[i].x, list[i].y, list[i].z, list[i].w);

            return vectors;
        }

        public static IList<Vector4> ToVector4(IList<Vector4d> list)
        {
            Vector4[] vectors = new Vector4[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector4((float)list[i].x, (float)list[i].y, (float)list[i].z, (float)list[i].w);

            return vectors;
        }

        public static IList<Vector4f> ToVector4f(IList<Vector4> list)
        {
            Vector4f[] vectors = new Vector4f[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector4f(list[i].x, list[i].y, list[i].z, list[i].w);

            return vectors;
        }

        public static IList<Vector4d> ToVector4d(IList<Vector4> list)
        {
            Vector4d[] vectors = new Vector4d[list.Count];
            for (int i = 0; i < list.Count; i++)
                vectors[i] = new Vector4d(list[i].x, list[i].y, list[i].z, list[i].w);

            return vectors;
        }

    }
}
























