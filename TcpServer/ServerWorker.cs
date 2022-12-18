using Packages;

namespace Server
{
    public static class ServerWorker
    {
        public static List<List<Vector>> CreatePackages(float[][] matrix, int clientsCount)
        {
            var packages = new List<List<Vector>>();
            for(var i = 0; i < clientsCount; i++)
                packages.Add(new List<Vector>());

            var currentColumnNumber = 0;
            var columnsCount = matrix.GetLength(0) + 1;
            while (currentColumnNumber < columnsCount)
            {
                for(var i = 0; i < clientsCount; i++)
                {
                    if (currentColumnNumber == columnsCount)
                        break;
                    packages[i].Add(new Vector()
                    {
                        Id = currentColumnNumber,
                        TreatmentsNumber = currentColumnNumber + 1,
                        Values = GetColumn(matrix, currentColumnNumber)
                    });
                    currentColumnNumber++;
                }
            }
            return packages;
        }

        public static float[][] DecompozePakcages(List<Vector> packages)
        {
            var matrix = new float[packages[0].Values.Length][];
            for (var i = 0; i < matrix.GetLength(0); i++)
                matrix[i] = new float[packages[0].Values.Length + 1];
            foreach (var package in packages)
                    matrix = SetColumn(matrix, package.Values, package.Id);
            return matrix;
        }
        private static float[][] SetColumn(float[][] matrix, float[] column, int num)
        {
            for(var i = 0; i < column.Length; i++)
                matrix[i][num] = column[i];
            return matrix;
        }
        public static float[] GetColumn(float[][] matrix, int num)
        {
            float[] column = new float[matrix.GetLength(0)];
            for(var i = 0; i < column.Length; i++)
                column[i] = matrix[i][num];
            return column;
        }
        public static float[] BackwardMode(float[][] matrix)
        {
            int n = matrix.GetLength(0);
            float[] result = new float[n];
            for (int i = n - 1; i >= 0; i--)
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

        public static float[][] ConcatenateAB(float[][] A, float[] B)
        {
            float[][] matrix = new float[A.Length][];
            for (int i = 0; i < A.Length; i++)
            {
                matrix[i] = new float[A[i].Length + 1];
                for (int j = 0; j < matrix[i].Length - 1; j++)
                    matrix[i][j] = A[i][j];
                matrix[i][matrix[i].Length - 1] = B[i];
            }
            return matrix;
        }

        public static float GetAccuracy(float[][] A, float[] expectedB, float[] actualAns)
        {
            float[] actualB = new float[actualAns.Length];
            for(int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(0); j++)
                    actualB[i] += A[i][j] * actualAns[i];
            float expSum = Math.Abs(expectedB.Sum());
            float actSum = Math.Abs(actualB.Sum());
            if (expSum < actSum)
                return expSum / actSum * 100;
            return actSum / expSum * 100;
        }
    }
}
