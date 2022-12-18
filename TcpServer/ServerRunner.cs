using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerRunner
    {
        public static void Main(string[] args)
        {
            //float[][] matrix = new float[][] { new float[] { 2, 1, -1, 8 }, new float[] { 3, -1, 2, -11 }, new float[] { -2, 1, 2, -3 } };
            //var a = FileWorker.ReadMatrix(@"..\..\..\..\test\test2.A");
            //var b = FileWorker.ReadVector(@"..\..\..\..\test\test2.B");
            //float[][] matrix = ServerWorker.ConcatenateAB(a, b);
            //GaussSolverTcpServer server = new GaussSolverTcpServer(11000, "127.0.0.1", matrix, 2); // "192.168.0.101"
            //var results = server.Run();
            //var ans = ServerWorker.BackwardMode(results);  
            //Console.WriteLine("\nAnswers: ");
            //for (int i = 0; i < ans.Length; i++)
            //    Console.WriteLine(ans[i]);
            //Console.WriteLine($"Execution time {server.LastExecutionTime} ms.");
            //var loss = ServerWorker.GetLoss(a, b, ans);
            //Console.WriteLine($"Average accuracy: {loss}%");
            //server.Dispose();
            Console.ReadKey();
        }
    }
}
