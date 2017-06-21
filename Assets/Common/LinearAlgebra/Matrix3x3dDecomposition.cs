
using System;

namespace Common.Mathematics.LinearAlgebra
{

    public static class Matrix3x3dDecomposition
    {
        /// <summary>
        /// Rotates A through phi in pq-plane to set A(p,q) = 0
	    /// Rotation stored in R whose columns are eigenvectors of A
        /// </summary>
        public static void JacobiRotate(ref Matrix3x3d A, ref Matrix3x3d R, int p, int q)
        {

            if (A[p, q] == 0.0f)
                return;

            double d = (A[p, p] - A[q, q]) / (2.0f * A[p, q]);
            double t = 1.0 / (Math.Abs(d) + Math.Sqrt(d * d + 1.0));
            if (d < 0.0f) t = -t;
            double c = 1.0 / Math.Sqrt(t * t + 1);
            double s = t * c;

            A[p, p] += t * A[p, q];
            A[q, q] -= t * A[p, q];
            A[p, q] = A[q, p] = 0.0f;

            // transform A
            int k;
            for (k = 0; k < 3; k++)
            {
                if (k != p && k != q)
                {
                    double Akp = c * A[k, p] + s * A[k, q];
                    double Akq = -s * A[k, p] + c * A[k, q];
                    A[k, p] = A[p, k] = Akp;
                    A[k, q] = A[q, k] = Akq;
                }
            }

            // store rotation in R
            for (k = 0; k < 3; k++)
            {
                double Rkp = c * R[k, p] + s * R[k, q];
                double Rkq = -s * R[k, p] + c * R[k, q];
                R[k, p] = Rkp;
                R[k, q] = Rkq;
            }
        }

        public static void EigenDecomposition(Matrix3x3d A, out Matrix3x3d eigenVecs, out Vector3d eigenVals)
        {

            const int numJacobiIterations = 10;
            const double epsilon = 1e-15;

            Matrix3x3d D = A;

            // only for symmetric matrices!
            eigenVecs = Matrix3x3d.Identity;    // unit matrix
            int iter = 0;

            while (iter < numJacobiIterations)
            {
                // 3 off diagonal elements
                // find off diagonal element with maximum modulus
                int p, q;
                double a, max;
                max = Math.Abs(D.m01);
                p = 0; q = 1;
                a = Math.Abs(D.m02);
                if (a > max) { p = 0; q = 2; max = a; }
                a = Math.Abs(D.m12);
                if (a > max) { p = 1; q = 2; max = a; }
                // all small enough -> done
                if (max < epsilon) break;
                // rotate matrix with respect to that element
                JacobiRotate(ref D, ref eigenVecs, p, q);
                iter++;
            }

            eigenVals = new Vector3d(D.m00, D.m11, D.m22);
        }

        /// <summary>
        /// Perform polar decomposition A = (U D U^T) R
        /// </summary>
        public static void PolarDecomposition(Matrix3x3d A, out Matrix3x3d R, out Matrix3x3d U, out Matrix3x3d D)
        {
            // A = SR, where S is symmetric and R is orthonormal
            // -> S = (A A^T)^(1/2)

            // A = U D U^T R

            Matrix3x3d AAT = new Matrix3x3d();
            AAT.m00 = A.m00 * A.m00 + A.m01 * A.m01 + A.m02 * A.m02;
            AAT.m11 = A.m10 * A.m10 + A.m11 * A.m11 + A.m12 * A.m12;
            AAT.m22 = A.m20 * A.m20 + A.m21 * A.m21 + A.m22 * A.m22;

            AAT.m01 = A.m00 * A.m10 + A.m01 * A.m11 + A.m02 * A.m12;
            AAT.m02 = A.m00 * A.m20 + A.m01 * A.m21 + A.m02 * A.m22;
            AAT.m12 = A.m10 * A.m20 + A.m11 * A.m21 + A.m12 * A.m22;

            AAT.m10 = AAT.m01;
            AAT.m20 = AAT.m02;
            AAT.m21 = AAT.m12;

            R = Matrix3x3d.Identity;
            Vector3d eigenVals;
            EigenDecomposition(AAT, out U, out eigenVals);

            double d0 = Math.Sqrt(eigenVals.x);
            double d1 = Math.Sqrt(eigenVals.y);
            double d2 = Math.Sqrt(eigenVals.z);

            D = new Matrix3x3d();
            D.m00 = d0;
            D.m11 = d1;
            D.m22 = d2;

            const double eps = 1e-15;

            double l0 = eigenVals.x; if (l0 <= eps) l0 = 0.0; else l0 = 1.0 / d0;
            double l1 = eigenVals.y; if (l1 <= eps) l1 = 0.0; else l1 = 1.0 / d1;
            double l2 = eigenVals.z; if (l2 <= eps) l2 = 0.0; else l2 = 1.0 / d2;

            Matrix3x3d S1 = new Matrix3x3d();
            S1.m00 = l0 * U.m00 * U.m00 + l1 * U.m01 * U.m01 + l2 * U.m02 * U.m02;
            S1.m11 = l0 * U.m10 * U.m10 + l1 * U.m11 * U.m11 + l2 * U.m12 * U.m12;
            S1.m22 = l0 * U.m20 * U.m20 + l1 * U.m21 * U.m21 + l2 * U.m22 * U.m22;

            S1.m01 = l0 * U.m00 * U.m10 + l1 * U.m01 * U.m11 + l2 * U.m02 * U.m12;
            S1.m02 = l0 * U.m00 * U.m20 + l1 * U.m01 * U.m21 + l2 * U.m02 * U.m22;
            S1.m12 = l0 * U.m10 * U.m20 + l1 * U.m11 * U.m21 + l2 * U.m12 * U.m22;

            S1.m10 = S1.m01;
            S1.m20 = S1.m02;
            S1.m21 = S1.m12;

            R = S1 * A;

            // stabilize
            Vector3d c0, c1, c2;
            c0 = R.GetColumn(0);
            c1 = R.GetColumn(1);
            c2 = R.GetColumn(2);

            if (c0.SqrMagnitude < eps)
                c0 = c1.Cross(c2);
            else if (c1.SqrMagnitude < eps)
                c1 = c2.Cross(c0);
            else
                c2 = c0.Cross(c1);

            R.SetColumn(0, c0);
            R.SetColumn(1, c1);
            R.SetColumn(2, c2);
        }

        /// <summary>
        /// Return the one norm of the matrix.
        /// </summary>
        public static double OneNorm(Matrix3x3d A)
        {
            double sum1 = Math.Abs(A.m00) + Math.Abs(A.m10) + Math.Abs(A.m20);
            double sum2 = Math.Abs(A.m01) + Math.Abs(A.m11) + Math.Abs(A.m21);
            double sum3 = Math.Abs(A.m02) + Math.Abs(A.m12) + Math.Abs(A.m22);

            double maxSum = sum1;

            if (sum2 > maxSum)
                maxSum = sum2;
            if (sum3 > maxSum)
                maxSum = sum3;

            return maxSum;
        }

        /// <summary>
        /// Return the inf norm of the matrix.
        /// </summary>
        public static double InfNorm(Matrix3x3d A)
        {
            double sum1 = Math.Abs(A.m00) + Math.Abs(A.m01) + Math.Abs(A.m02);
            double sum2 = Math.Abs(A.m10) + Math.Abs(A.m11) + Math.Abs(A.m12);
            double sum3 = Math.Abs(A.m20) + Math.Abs(A.m21) + Math.Abs(A.m22);

            double maxSum = sum1;

            if (sum2 > maxSum)
                maxSum = sum2;
            if (sum3 > maxSum)
                maxSum = sum3;

            return maxSum;
        }

        /// <summary>
        /// Perform a polar decomposition of matrix M and return the rotation matrix R. This method handles the degenerated cases.
        /// </summary>am>
        public static void PolarDecompositionStable(Matrix3x3d M, double tolerance, out Matrix3x3d R)
        {
            Matrix3x3d Mt = M.Transpose;
            double Mone = OneNorm(M);
            double Minf = InfNorm(M);
            double Eone;
            Matrix3x3d MadjTt = new Matrix3x3d();
            Matrix3x3d Et = new Matrix3x3d();

            const double eps = 1.0e-15;

            do
            {
                MadjTt.SetRow(0, Mt.GetRow(1).Cross(Mt.GetRow(2)));
                MadjTt.SetRow(1, Mt.GetRow(2).Cross(Mt.GetRow(0)));
                MadjTt.SetRow(2, Mt.GetRow(0).Cross(Mt.GetRow(1)));

                double det = Mt.m00 * MadjTt.m00 + Mt.m01 * MadjTt.m01 + Mt.m02 * MadjTt.m02;

                if (Math.Abs(det) < eps)
                {
                    int index = int.MaxValue;
                    for (int i = 0; i < 3; i++)
                    {
                        double len = MadjTt.GetRow(i).SqrMagnitude;
                        if (len > eps)
                        {
                            // index of valid cross product
                            // => is also the index of the vector in Mt that must be exchanged
                            index = i;
                            break;
                        }
                    }

                    if (index == int.MaxValue)
                    {
                        R = Matrix3x3d.Identity;
                        return;
                    }
                    else
                    {

                        Mt.SetRow(index, Mt.GetRow((index + 1) % 3).Cross(Mt.GetRow((index + 2) % 3)));
                        MadjTt.SetRow((index + 1) % 3, Mt.GetRow((index + 2) % 3).Cross(Mt.GetRow(index)));
                        MadjTt.SetRow((index + 2) % 3, Mt.GetRow(index).Cross(Mt.GetRow((index + 1) % 3)));
                        Matrix3x3d M2 = Mt.Transpose;

                        Mone = OneNorm(M2);
                        Minf = InfNorm(M2);

                        det = Mt.m00 * MadjTt.m00 + Mt.m01 * MadjTt.m01 + Mt.m02 * MadjTt.m02;
                    }
                }

                double MadjTone = OneNorm(MadjTt);
                double MadjTinf = InfNorm(MadjTt);

                double gamma = Math.Sqrt(Math.Sqrt((MadjTone * MadjTinf) / (Mone * Minf)) / Math.Abs(det));

                double g1 = gamma * 0.5;
                double g2 = 0.5 / (gamma * det);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Et[i, j] = Mt[i, j];
                        Mt[i, j] = g1 * Mt[i, j] + g2 * MadjTt[i, j];
                        Et[i, j] -= Mt[i, j];
                    }
                }

                Eone = OneNorm(Et);

                Mone = OneNorm(Mt);
                Minf = InfNorm(Mt);
            }
            while (Eone > Mone * tolerance);

            // Q = Mt^T 
            R = Mt.Transpose;

            //end of function
        }


        /// <summary>
        /// Perform a singular value decomposition of matrix A: A = U * sigma * V^T.
        /// This function returns two proper rotation matrices U and V^T which do not 
        /// contain a reflection. Reflections are corrected by the inversion handling
        /// proposed by Irving et al. 2004.
        /// </summary>
        public static void SVDWithInversionHandling(Matrix3x3d A, out Vector3d sigma, out Matrix3x3d U, out Matrix3x3d VT)
        {

            Vector3d S;
            Matrix3x3d AT_A, V;

            AT_A = A.Transpose * A;

            // Eigen decomposition of A^T * A
            EigenDecomposition(AT_A, out V, out S);
            int pos;
            // Detect if V is a reflection .
            // Make a rotation out of it by multiplying one column with -1.
            double detV = V.Determinant;
            if (detV < 0.0f)
            {
                double minLambda = double.PositiveInfinity;
                pos = 0;
                for (int l = 0; l < 3; l++)
                {
                    if (S[l] < minLambda)
                    {
                        pos = l;
                        minLambda = S[l];
                    }
                }
                V[0, pos] = -V[0, pos];
                V[1, pos] = -V[1, pos];
                V[2, pos] = -V[2, pos];
            }

            if (S.x < 0.0f) S.x = 0.0f;     // safety for sqrt
            if (S.y < 0.0f) S.y = 0.0f;
            if (S.z < 0.0f) S.z = 0.0f;

            sigma.x = Math.Sqrt(S.x);
            sigma.y = Math.Sqrt(S.y);
            sigma.z = Math.Sqrt(S.z);

            VT = V.Transpose;

            // Check for values of hatF near zero
            int chk = 0;
            pos = 0;
            for (int l = 0; l < 3; l++)
            {
                if (Math.Abs(sigma[l]) < 1.0e-4)
                {
                    pos = l;
                    chk++;
                }
            }

            if (chk > 0)
            {

                if (chk > 1)
                {
                    U = Matrix3x3d.Identity;
                }
                else
                {
                    U = A * V;
                    for (int l = 0; l < 3; l++)
                    {
                        if (l != pos)
                        {
                            for (int m = 0; m < 3; m++)
                            {
                                U[m, l] *= 1.0f / sigma[l];

                            }
                        }
                    }

                    Vector3d[] v = new Vector3d[2];
                    int index = 0;
                    for (int l = 0; l < 3; l++)
                    {
                        if (l != pos)
                        {
                            v[index++] = new Vector3d(U[0, l], U[1, l], U[2, l]);
                        }
                    }

                    Vector3d vec = v[0].Cross(v[1]);
                    vec.Normalize();
                    U[0, pos] = vec[0];
                    U[1, pos] = vec[1];
                    U[2, pos] = vec[2];
                }

            }
            else
            {
                Vector3d sigmaInv = new Vector3d(1.0 / sigma.x, 1.0 / sigma.y, 1.0 / sigma.z);

                U = A * V;
                for (int l = 0; l < 3; l++)
                {
                    for (int m = 0; m < 3; m++)
                    {
                        U[m, l] *= sigmaInv[l];
                    }
                }
            }

            double detU = U.Determinant;

            // U is a reflection => inversion
            if (detU < 0.0)
            {
                //std::cout << "Inversion!\n";
                double minLambda = double.PositiveInfinity;
                pos = 0;
                for (int l = 0; l < 3; l++)
                {
                    if (sigma[l] < minLambda)
                    {
                        pos = l;
                        minLambda = sigma[l];
                    }
                }

                // invert values of smallest singular value
                sigma[pos] = -sigma[pos];
                U[0, pos] = -U[0, pos];
                U[1, pos] = -U[1, pos];
                U[2, pos] = -U[2, pos];
            }

            //end of function
        }

    }

}