using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Server
{
    public static class FileWorker
    {
        public static float[][] ReadMatrix(string pathToFile, IProgress<int> progress)
        {
            Console.WriteLine($"Reading file {pathToFile}");
            var timer = new Stopwatch();
            timer.Start();
            int size;
            using(var reader = new StreamReader(pathToFile))
            {
                size = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            }

            using (var reader = new StreamReader(pathToFile))
            {
                var array = new float[size][];
                int i = 0; 
                while (!reader.EndOfStream)
                {
                    progress.Report(i);
                    array[i] = new float[size];
                    Parallel.ForEach(reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), (item, state, index) =>
                    {
                        array[i][index] = float.Parse(item);
                    });
                    i++;
                }
                timer.Stop();
                Console.WriteLine($"Reading time: {timer.ElapsedMilliseconds} ms");
                return array;
            }
        }
        public static float[] ReadVector(string pathToFile, IProgress<int> progress)
        {
            using (var reader = new StreamReader(pathToFile))
            {
                var i = 0;
                List<float> list = new List<float>();
                while (!reader.EndOfStream)
                {
                    progress.Report(i++);
                    var line = reader.ReadLine();
                    float value = float.Parse(line);                  
                    list.Add(value);
                }
                return list.ToArray();
            }
        }

        public static void WriteVector(string pathToFile, float[] vector, IProgress<int> progress)
        {
            using(var writer = new StreamWriter(pathToFile))
            {
                for (int i = 0; i < vector.Length; i++)
                {
                    writer.WriteLine(vector[i]);
                    progress.Report(i);
                }
                    
            }
        }
    }
}