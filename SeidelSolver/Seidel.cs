using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidelSolver
{
    public static class Seidel
    {

        public static float[] Solve(float[][] matrix, IProgress<int> progress, double eps = 0.001)
        {
            var c = 0;
            int n = matrix.GetLength(0);
            float[] x = new float[n], x_past = new float[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = 1;
                x_past[i] = 1;
            }
            while (true)
            {
                progress.Report(c++);
                for (int i = 0; i < n; i++)
                {                   
                    float sum1 = 0, sum2 = 0;
                    for (int j = i + 1; j < n; j++)
                    {
                        sum1 += matrix[i][j] * x[j];
                    }
                    for (int j = 0; j < i - 1; j++)
                    {
                        sum2 += matrix[i][j] * x_past[j];
                    }
                    x[i] = (matrix[i][n] - sum2 - sum1) / matrix[i][i];
                }
                var dif = Math.Abs(x_past.Sum() - x.Sum());
                if (dif < eps)
                    break;
                for (int i = 0; i < n; i++)
                    x_past[i] = x[i];
            }
            return x;
        }
    }
}
