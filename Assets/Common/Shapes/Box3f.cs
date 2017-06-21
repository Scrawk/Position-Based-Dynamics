using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Common.Mathematics.LinearAlgebra;

namespace Common.Geometry.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Box3f
    {

        public Vector3f Center { get { return (Min + Max) / 2.0f; } }

        public Vector3f Size { get { return new Vector3f(Width, Height, Depth); } }

        public float Width { get { return Max.x - Min.x; } }

        public float Height { get { return Max.y - Min.y; } }

        public float Depth { get { return Max.z - Min.z; } }

        public float Area
        {
            get
            {
                return (Max.x - Min.x) * (Max.y - Min.y) * (Max.z - Min.z);
            }
        }

        public float SurfaceArea
        {
            get
            {
                Vector3f d = Max - Min;
                return 2.0f * (d.x * d.y + d.x * d.z + d.y * d.z);
            }
        }

        public Vector3f Min { get; set; }

        public Vector3f Max { get; set; }

        public Box3f(float min, float max)
        {
            Min = new Vector3f(min);
            Max = new Vector3f(max);
        }

        public Box3f(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            Min = new Vector3f(minX, minY, minZ);
            Max = new Vector3f(maxX, maxY, maxZ);
        }

        public Box3f(Vector3f min, Vector3f max)
        {
            Min = min;
            Max = max;
        }

        public Box3f(Vector3i min, Vector3i max)
        {
            Min = new Vector3f(min.x, min.y, min.z);
            Max = new Vector3f(max.x, max.y, max.z); ;
        }

        public Box3f(Box3f box)
        {
            Min = box.Min;
            Max = box.Max;
        }

        public void GetCorners(IList<Vector3f> corners)
        {
            corners[0] = new Vector3f(Min.x, Min.y, Min.z);
            corners[1] = new Vector3f(Min.x, Min.y, Max.z);
            corners[2] = new Vector3f(Max.x, Min.y, Max.z);
            corners[3] = new Vector3f(Max.x, Min.y, Min.z);

            corners[4] = new Vector3f(Min.x, Max.y, Min.z);
            corners[5] = new Vector3f(Min.x, Max.y, Max.z);
            corners[6] = new Vector3f(Max.x, Max.y, Max.z);
            corners[7] = new Vector3f(Max.x, Max.y, Min.z);
        }

        public void GetCorners(IList<Vector4f> corners)
        {
            corners[0] = new Vector4f(Min.x, Min.y, Min.z, 1);
            corners[1] = new Vector4f(Min.x, Min.y, Max.z, 1);
            corners[2] = new Vector4f(Max.x, Min.y, Max.z, 1);
            corners[3] = new Vector4f(Max.x, Min.y, Min.z, 1);

            corners[4] = new Vector4f(Min.x, Max.y, Min.z, 1);
            corners[5] = new Vector4f(Min.x, Max.y, Max.z, 1);
            corners[6] = new Vector4f(Max.x, Max.y, Max.z, 1);
            corners[7] = new Vector4f(Max.x, Max.y, Min.z, 1);
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given point.
        /// </summary>
        public Box3d Enlarge(Vector3f p)
        {
            return new Box3d(Math.Min(Min.x, p.x), Math.Max(Max.x, p.x),
                             Math.Min(Min.y, p.y), Math.Max(Max.y, p.y),
                             Math.Min(Min.z, p.z), Math.Max(Max.z, p.z));
        }

        /// <summary>
        /// Returns the bounding box containing this box and the given box.
        /// </summary>
        public Box3d Enlarge(Box3d r)
        {
            return new Box3d(Math.Min(Min.x, r.Min.x), Math.Max(Max.x, r.Max.x),
                             Math.Min(Min.y, r.Min.y), Math.Max(Max.y, r.Max.y),
                             Math.Min(Min.z, r.Min.z), Math.Max(Max.z, r.Max.z));
        }

        /// <summary>
        /// Returns true if this bounding box contains the given bounding box.
        /// </summary>
        public bool Intersects(Box3d a)
        {
            if (Max.x < a.Min.x || Min.x > a.Max.x) return false;
            if (Max.y < a.Min.y || Min.y > a.Max.y) return false;
            if (Max.z < a.Min.z || Min.z > a.Max.z) return false;
            return true;
        }

        /// <summary>
        /// Returns true if this bounding box contains the given point.
        /// </summary>
        public bool Contains(Vector3f p)
        {
            if (p.x > Max.x || p.x < Min.x) return false;
            if (p.y > Max.y || p.y < Min.y) return false;
            if (p.z > Max.z || p.z < Min.z) return false;
            return true;
        }

        /// <summary>
        /// Returns the closest point to a on the box.
        /// </summary>
        public Vector3f Closest(Vector3f p)
        {
            Vector3f c;

            if (p.x < Min.x)
                c.x = Min.x;
            else if (p.x > Max.x)
                c.x = Max.x;
            else if (p.x - Min.x < Width * 0.5f)
                c.x = Min.x;
            else
                c.x = Max.x;

            if (p.y < Min.y)
                c.y = Min.y;
            else if (p.y > Max.y)
                c.y = Max.y;
            else if (p.y - Min.y < Height * 0.5f)
                c.y = Min.y;
            else
                c.y = Max.y;

            if (p.z < Min.z)
                c.z = Min.z;
            else if (p.z > Max.z)
                c.z = Max.z;
            else if (p.z - Min.z < Depth * 0.5f)
                c.z = Min.z;
            else
                c.z = Max.z;

            return c;
        }

        public bool Intersects(Vector3f p1, Vector3f p2)
        {

            Vector3f d = (p2 - p1) * 0.5f;
            Vector3f e = (Max - Min) * 0.5f;
            Vector3f c = p1 + d - (Min + Max) * 0.5f;
            Vector3f ad = d.Absolute;

            if (Math.Abs(c.x) > e.x + ad.x) return false;
            if (Math.Abs(c.y) > e.y + ad.y) return false;
            if (Math.Abs(c.z) > e.z + ad.z) return false;

            float eps = 1e-12f;

            if (Math.Abs(d.y * c.z - d.z * c.y) > e.y * ad.z + e.z * ad.y + eps) return false;
            if (Math.Abs(d.z * c.x - d.x * c.z) > e.z * ad.x + e.x * ad.z + eps) return false;
            if (Math.Abs(d.x * c.y - d.y * c.x) > e.x * ad.y + e.y * ad.x + eps) return false;

            return true;
        }


    }

}




















