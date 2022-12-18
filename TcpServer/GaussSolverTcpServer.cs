using Newtonsoft.Json;
using Packages;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public class GaussSolverTcpServer : IDisposable
    {
        private TcpListener _server;
        private List<TcpClient> _clients;
        private List<NetworkStream> _streams;
        public List<Vector> Results { get; private set; }
        public float[][] Matrix { get; private set; }

        public int MaxClients { get; }

        public long LastExecutionTime { get; private set; }

        public GaussSolverTcpServer(int port, string ip, float[][] matrix, int clientsCount)
        {
            _server = new TcpListener(System.Net.IPAddress.Parse(ip), port);
            Matrix = matrix;
            Results = new List<Vector>();
            _clients = new List<TcpClient>();
            _streams = new List<NetworkStream>();
            MaxClients = clientsCount;
        }

        public float[][] Run(IProgress<int> bar)
        {
            var watch = new Stopwatch();
            _server.Start();
            Console.WriteLine(_server.LocalEndpoint);
            AwaitClients();
            watch.Start();
            Console.WriteLine("Creating packages...");
            var packages = ServerWorker.CreatePackages(Matrix, _clients.Count);
            var resolving = packages[0][0];
            Console.WriteLine("Sending packages...");
            SendPackages(packages, resolving);
            packages.Clear();
            int global = 0;
            while (true)
            {
                for (int i = 0; i < _clients.Count; i++)
                {
                    if (!_streams[i].DataAvailable)
                        continue;
                    var vector = ReadResolveVector(i);
                    Thread.Sleep(50);
                    SendVectorToAllClients(vector);
                    global++;
                    bar.Report(global);
                    if(global % 10 == 0)
                        Console.WriteLine($"{(int)((float)global / Matrix.GetLength(0) * 100)}%");
                }
                if (global == Matrix.GetLength(0))
                    break;
            }
            Thread.Sleep(50);
            SendEndCommand();
            while (true)
            {
                for (int i = 0; i < _clients.Count; i++)
                {
                    if (!_streams[i].DataAvailable)
                        continue;
                    ReadAllVectorsBySeparator(i);
                }
                if (Results.Count >= Matrix.GetLength(0) + 1)
                    break;
            }
            Console.WriteLine("Complete...");
            watch.Stop();
            LastExecutionTime = watch.ElapsedMilliseconds;
            return ServerWorker.DecompozePakcages(Results);
        }
        private void ReadAllVectorsBySeparator(int n)
        {
            var start = "";
            while (true)
            {
                var strb = new StringBuilder(start);
                var data = new byte[1 * 1024];
                var received = _streams[n].Read(data, 0, data.Length);
                var str = Encoding.UTF8.GetString(data, 0, received);
                strb.Append(str);
                var vectors = strb.ToString().Split("#", StringSplitOptions.RemoveEmptyEntries);
                if (vectors.Length == 1)
                {
                    if (vectors[0].IndexOf("}") != -1)
                    {
                        var dataL = vectors[0].Replace("&", "");
                        var v = JsonConvert.DeserializeObject<Vector>(dataL);
                        Results.Add(v);
                        start = "";
                        if (vectors[0].IndexOf("&") != -1)
                            break;
                    }
                    else
                    {
                        start = vectors[0];
                        continue;
                    }

                }
                else
                {
                    for (int i = 0; i < vectors.Length - 1; i++)
                    {
                        var v = JsonConvert.DeserializeObject<Vector>(vectors[i]);
                        Results.Add(v);

                    }
                    if (str.IndexOf("&") > -1)
                    {
                        var last = vectors[vectors.Length - 1].Remove(vectors[vectors.Length - 1].Length - 1);
                        Results.Add(JsonConvert.DeserializeObject<Vector>(last));
                        start = "";
                        break;
                    }
                    if (vectors[vectors.Length - 1].IndexOf("}") == -1)
                        start = vectors[vectors.Length - 1];
                }
            }
        }
        private Vector ReadResolveVector(int j)
        {
            var strb = new StringBuilder("");
            while (true)
            {
                var data = new byte[8 * 1024];
                var received = _streams[j].Read(data, 0, data.Length);
                var str = Encoding.UTF8.GetString(data, 0, received);
                strb.Append(str);
                if (str.IndexOf("&") > -1)
                    break;
            }
            strb = strb.Replace("&", "");
            return JsonConvert.DeserializeObject<Vector>(strb.ToString());
        }
        private void AwaitClients()
        {
            int clientsCount = 0;
            while (clientsCount < MaxClients)
            {
                Console.WriteLine("Awaiting client {0}/{1}...", clientsCount, MaxClients);
                TcpClient client = _server.AcceptTcpClient();
                _clients.Add(client);
                _streams.Add(client.GetStream());
                Console.WriteLine("Added #{0} client!", clientsCount + 1);
                clientsCount++;
            }
        }
        private void SendEndCommand()
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Vector() { IsEndMessage = true }) + "&");
                _streams[i].Write(data, 0, data.Length);
            }
        }
        private void SendPackages(List<List<Vector>> packages, Vector resolving)
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                int index = i;
                ThreadPool.QueueUserWorkItem(new WaitCallback(Send), new object[] { _streams[i], packages[i], resolving });           
            }
        }
        private void Send(object state)
        {
            object[] array = state as object[];
            NetworkStream stream = array[0] as NetworkStream;
            List<Vector> package = array[1] as List<Vector>;
            Vector resolving = array[2] as Vector;
            for (int j = 0; j < package.Count; j++)
            {
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(package[j]) + "#");
                stream.Write(data, 0, data.Length);
            }
            resolving.IsResolving = true;
            byte[] dataL = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resolving) + "&");
            stream.Write(dataL, 0, dataL.Length);
        }
        private void SendVectorToAllClients(Vector vector)
        {
            vector.IsResolving = true;
            for (int i = 0; i < _clients.Count; i++)
            {
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(vector) + "&");
                _streams[i].Write(data, 0, data.Length);
            }
        }

        public void Dispose()
        {
            _server.Stop();
            _streams.ForEach(s => s.Close());
            _clients.ForEach(s => s.Close());
        }
    }
}