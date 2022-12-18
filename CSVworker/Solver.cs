using Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakcages
{
    public static class GaussSolver
    {
        public static List<Vector> Convert(List<Vector> vectors, Vector resolveVector)
        {
            Parallel.ForEach(vectors, (item, state, index) =>
            {
                if (item.TreatmentsNumber != 0)
                {
                    int start = resolveVector.Id + 1;
                    Parallel.For(start, item.Values.Length, (j) =>
                    {
                        var aij = item.Values[j];
                        var aik = resolveVector.Values[j];
                        var akj = item.Values[resolveVector.Id];
                        var akk = resolveVector.Values[resolveVector.Id];
                        var result = aij - (aik * akj / akk);
                        item.Values[j] = result;
                    });
                    item.TreatmentsNumber--;
                }
            });
            return vectors;
        }

        public static float[] ReverseStroke(float[][] matrix)
        {
            int n = matrix.GetLength(0);
            float[] result = new float[n];
            for(int i = n - 1; i >= 0; i--)
            {
                float sum = 0;
                for (int j = 0; j < n; j++) 
                {
                    if (j == i)
                        continue;
                    sum += matrix[i][j] * result[j];
                }
                result[i] = (matrix[i][n] - sum) / matrix[i][i];
            }
            return result;
        }
    }
}
