using UnityEngine;
using System;
using System.Collections.Generic;

using Common.Mathematics.LinearAlgebra;
using Common.Geometry.Shapes;

using Common.Unity.Mathematics;

namespace Common.Unity.Drawing
{

    public enum LINE_MODE { LINES, TRIANGLES, TETRAHEDRON  };

    public static class DrawLines
    {

        private static IList<Vector4d> m_corners4d = new Vector4d[8];
        private static IList<Vector4f> m_corners4f = new Vector4f[8];
        private static IList<Vector4> m_vertices;

        private static Material m_lineMaterial;
        private static Material LineMaterial
        {
            get
            {
                if(m_lineMaterial == null)
                    m_lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
                return m_lineMaterial;
            }
        }

        private static IList<int> m_cube = new int[]
        {
            0, 1, 1, 2, 2, 3, 3, 0,
            4, 5, 5, 6, 6, 7, 7, 4,
            0, 4, 1, 5, 2, 6, 3, 7
        };

        public static void DrawBounds(Camera camera, Color color, Box3d bounds, Matrix4x4d localToWorld)
        {
            if (camera == null) return;

            bounds.GetCorners(m_corners4d);

            IList<Vector4> verts = Vertices(8);
            for (int i = 0; i < 8; i++)
                verts[i] = MathConverter.ToVector4(localToWorld * m_corners4d[i]);

            DrawVerticesAsLines(camera, color, verts, m_cube);
        }

        public static void DrawBounds(Camera camera, Color color, Box3f bounds, Matrix4x4f localToWorld)
        {
            if (camera == null) return;

            bounds.GetCorners(m_corners4f);

            IList<Vector4> verts = Vertices(8);
            for (int i = 0; i < 8; i++)
                verts[i] = MathConverter.ToVector4(localToWorld * m_corners4f[i]);

            DrawVerticesAsLines(camera, color, verts, m_cube);
        }

        public static void DrawVertices(LINE_MODE mode, Camera camera, Color color, IList<Vector4d> vertices, IList<int> indices, Matrix4x4d localToWorld)
        {
            if (camera == null || vertices == null || indices == null) return;

            int count = vertices.Count;
            IList<Vector4> verts = Vertices(count);
            for (int i = 0; i < count; i++)
                verts[i] = MathConverter.ToVector4(localToWorld * vertices[i]);

            switch (mode)
            {
                case LINE_MODE.LINES:
                    DrawVerticesAsLines(camera, color, verts, indices);
                    break;

                case LINE_MODE.TRIANGLES:
                    DrawVerticesAsTriangles(camera, color, verts, indices);
                    break;

                case LINE_MODE.TETRAHEDRON:
                    DrawVerticesAsTetrahedron(camera, color, verts, indices);
                    break;
            }
        }

        public static void DrawVertices(LINE_MODE mode, Camera camera, Color color, IList<Vector3d> vertices, IList<int> indices, Matrix4x4d localToWorld)
        {
            if (camera == null || vertices == null || indices == null) return;

            int count = vertices.Count;
            IList<Vector4> verts = Vertices(count);
            for (int i = 0; i < count; i++)
                verts[i] = MathConverter.ToVector4(localToWorld * vertices[i]);

            switch (mode)
            {
                case LINE_MODE.LINES:
                    DrawVerticesAsLines(camera, color, verts, indices);
                    break;

                case LINE_MODE.TRIANGLES:
                    DrawVerticesAsTriangles(camera, color, verts, indices);
                    break;

                case LINE_MODE.TETRAHEDRON:
                    DrawVerticesAsTetrahedron(camera, color, verts, indices);
                    break;
            }
        }

        public static void DrawVertices(LINE_MODE mode, Camera camera, Color color, IList<Vector4f> vertices, IList<int> indices, Matrix4x4f localToWorld)
        {
            if (camera == null || vertices == null || indices == null) return;

            int count = vertices.Count;
            IList<Vector4> verts = Vertices(count);
            for (int i = 0; i < count; i++)
                verts[i] = MathConverter.ToVector4(localToWorld * vertices[i]);

            switch (mode)
            {
                case LINE_MODE.LINES:
                    DrawVerticesAsLines(camera, color, verts, indices);
                    break;

                case LINE_MODE.TRIANGLES:
                    DrawVerticesAsTriangles(camera, color, verts, indices);
                    break;

                case LINE_MODE.TETRAHEDRON:
                    DrawVerticesAsTetrahedron(camera, color, verts, indices);
                    break;
            }
        }

        public static void DrawVertices(LINE_MODE mode, Camera camera, Color color, IList<Vector3f> vertices, IList<int> indices, Matrix4x4f localToWorld)
        {
            if (camera == null || vertices == null || indices == null) return;

            int count = vertices.Count;
            IList<Vector4> verts = Vertices(count);
            for (int i = 0; i < count; i++)
                verts[i] = MathConverter.ToVector4(localToWorld * vertices[i]);

            switch(mode)
            {
                case LINE_MODE.LINES:
                    DrawVerticesAsLines(camera, color, verts, indices);
                    break;

                case LINE_MODE.TRIANGLES:
                    DrawVerticesAsTriangles(camera, color, verts, indices);
                    break;

                case LINE_MODE.TETRAHEDRON:
                    DrawVerticesAsTetrahedron(camera, color, verts, indices);
                    break;
            }
        }

        private static void DrawVerticesAsLines(Camera camera, Color color, IList<Vector4> vertices, IList<int> indices)
        {
            if (camera == null || vertices == null || indices == null) return;

            GL.PushMatrix();

            GL.LoadIdentity();
            GL.MultMatrix(camera.worldToCameraMatrix);
            GL.LoadProjectionMatrix(camera.projectionMatrix);

            LineMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(color);

            int vertexCount = vertices.Count;

            for (int i = 0; i < indices.Count / 2; i++)
            {
                int i0 = indices[i * 2 + 0];
                int i1 = indices[i * 2 + 1];

                if (i0 < 0 || i0 >= vertexCount) continue;
                if (i1 < 0 || i1 >= vertexCount) continue;

                GL.Vertex(vertices[i0]);
                GL.Vertex(vertices[i1]);
            }

            GL.End();

            GL.PopMatrix();
        }

        private static void DrawVerticesAsTriangles(Camera camera, Color color, IList<Vector4> vertices, IList<int> indices)
        {
            if (camera == null || vertices == null || indices == null) return;

            GL.PushMatrix();

            GL.LoadIdentity();
            GL.MultMatrix(camera.worldToCameraMatrix);
            GL.LoadProjectionMatrix(camera.projectionMatrix);

            LineMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(color);

            int vertexCount = vertices.Count;

            for (int i = 0; i < indices.Count / 3; i++)
            {
                int i0 = indices[i * 3 + 0];
                int i1 = indices[i * 3 + 1];
                int i2 = indices[i * 3 + 2];

                if (i0 < 0 || i0 >= vertexCount) continue;
                if (i1 < 0 || i1 >= vertexCount) continue;
                if (i2 < 0 || i2 >= vertexCount) continue;

                GL.Vertex(vertices[i0]);
                GL.Vertex(vertices[i1]);

                GL.Vertex(vertices[i0]);
                GL.Vertex(vertices[i2]);

                GL.Vertex(vertices[i2]);
                GL.Vertex(vertices[i1]);
            }

            GL.End();

            GL.PopMatrix();
        }

        private static void DrawVerticesAsTetrahedron(Camera camera, Color color, IList<Vector4> vertices, IList<int> indices)
        {
            if (camera == null || vertices == null || indices == null) return;

            GL.PushMatrix();

            GL.LoadIdentity();
            GL.MultMatrix(camera.worldToCameraMatrix);
            GL.LoadProjectionMatrix(camera.projectionMatrix);

            LineMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(color);

            int vertexCount = vertices.Count;

            for (int i = 0; i < indices.Count / 4; i++)
            {
                int i0 = indices[i * 4 + 0];
                int i1 = indices[i * 4 + 1];
                int i2 = indices[i * 4 + 2];
                int i3 = indices[i * 4 + 3];

                if (i0 < 0 || i0 >= vertexCount) continue;
                if (i1 < 0 || i1 >= vertexCount) continue;
                if (i2 < 0 || i2 >= vertexCount) continue;
                if (i3 < 0 || i3 >= vertexCount) continue;

                GL.Vertex3(vertices[i0].x, vertices[i0].y, vertices[i0].z);
                GL.Vertex3(vertices[i1].x, vertices[i1].y, vertices[i1].z);

                GL.Vertex3(vertices[i0].x, vertices[i0].y, vertices[i0].z);
                GL.Vertex3(vertices[i2].x, vertices[i2].y, vertices[i2].z);

                GL.Vertex3(vertices[i0].x, vertices[i0].y, vertices[i0].z);
                GL.Vertex3(vertices[i3].x, vertices[i3].y, vertices[i3].z);

                GL.Vertex3(vertices[i1].x, vertices[i1].y, vertices[i1].z);
                GL.Vertex3(vertices[i2].x, vertices[i2].y, vertices[i2].z);

                GL.Vertex3(vertices[i3].x, vertices[i3].y, vertices[i3].z);
                GL.Vertex3(vertices[i2].x, vertices[i2].y, vertices[i2].z);

                GL.Vertex3(vertices[i1].x, vertices[i1].y, vertices[i1].z);
                GL.Vertex3(vertices[i3].x, vertices[i3].y, vertices[i3].z);
            }

            GL.End();

            GL.PopMatrix();
        }

        public static void DrawGrid(Camera camera, Color color, Vector3 min, Vector3 max, float spacing, Matrix4x4 localToWorld)
        {
            if (camera == null) return;

            GL.PushMatrix();

            GL.LoadIdentity();
            GL.MultMatrix(camera.worldToCameraMatrix);
            GL.LoadProjectionMatrix(camera.projectionMatrix);

            LineMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(color);

            float width = max.x - min.x;
            float depth = max.z - min.z;

            if (spacing <= 0)
                throw new ArgumentException("Spacing must be > 0");

            for(float x = 0; x <= width; x += spacing)
            {
                for (float z = 0; z <= depth; z += spacing)
                {
                    Vector4 v0 = localToWorld * new Vector4(min.x + x, 0, min.z, 1);
                    Vector4 v1 = localToWorld * new Vector4(min.x + x, 0, max.z, 1);

                    GL.Vertex(v0);
                    GL.Vertex(v1);

                    Vector4 v2 = localToWorld * new Vector4(min.x, 0, min.z + z, 1);
                    Vector4 v3 = localToWorld * new Vector4(max.x, 0, min.z + z, 1);

                    GL.Vertex(v2);
                    GL.Vertex(v3);
                }
            }

            GL.End();

            GL.PopMatrix();
        }

        private static IList<Vector4> Vertices(int count)
        {
            if (m_vertices == null || m_vertices.Count < count)
                m_vertices = new Vector4[count];

            return m_vertices;
        }


    }

}