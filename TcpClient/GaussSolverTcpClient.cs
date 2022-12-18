using Newtonsoft.Json;
using Packages;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class GaussSolverTcpClient : IDisposable
    {

        private TcpClient _client;

        private Vector _currentResolveColumn;

        private List<Vector> _vectors;
        private NetworkStream _stream;

        public GaussSolverTcpClient(string ipAddress, int port)
        {
            _client = new TcpClient();
            while(!_client.Connected) 
            {
                try
                {
                    _client.Connect(ipAddress, port);
                }
                catch (Exception ex) 
                {
                }
            }
            _stream = _client.GetStream();
            _vectors = new List<Vector>();
        }
        public void Run()
        {
            ReadAllVectorsBySeparator();
            _vectors = GaussSolver.Convert(_vectors, _currentResolveColumn);

            foreach (var vector in _vectors)
            {
                if (vector.TreatmentsNumber == 1)
                {
                    vector.IsResolving = true;
                    SendVector(vector, "&");
                    break;
                }
            }
            while (true)
            {
                if (!_stream.DataAvailable)
                    continue;
                var readedVector = ReadResolveVector();
                if (readedVector.IsEndMessage)
                    break;
                else
                {                 
                    if (readedVector.IsResolving)
                        _currentResolveColumn = readedVector;
                    else
                        continue;

                }
                _vectors = GaussSolver.Convert(_vectors, _currentResolveColumn);

                foreach (var vector in _vectors)
                {
                    if (vector.TreatmentsNumber == 1)
                    {
                        vector.IsResolving = true;
                        SendVector(vector, "&");
                    }
                }

            }
            SendAllVectors();
            Console.Clear();
            Console.WriteLine("Завершено.");
        }

        private Vector ReadResolveVector()
        {
            var strb = new StringBuilder("");
            while (true)
            {
                var data = new byte[8 * 1024];
                var received = _stream.Read(data, 0, data.Length);
                var str = Encoding.UTF8.GetString(data, 0, received);
                strb.Append(str);
                if (str.IndexOf("&") > -1)
                    break;
            }
            strb = strb.Replace("#", "").Replace("&", "");
            return JsonConvert.DeserializeObject<Vector>(strb.ToString());
        }
        private void SendAllVectors()
        {
            for(int i = 0; i < _vectors.Count; i++)
            {
                if (i == _vectors.Count - 1)
                    SendVector(_vectors[i], "&");
                else
                    SendVector(_vectors[i], "#");
            }
        }
        private void SendVector(Vector vector, string separator)
        {
            byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(vector) + separator);
            _stream.Write(data, 0, data.Length);
        }


        private void ReadAllVectorsBySeparator()
        {
            var start = "";
            while (true)
            {
                var strb = new StringBuilder(start);
                var data = new byte[1 * 1024];
                var received = _stream.Read(data, 0, data.Length);
                var str = Encoding.UTF8.GetString(data, 0, received);
                strb.Append(str);
                var vectors = strb.ToString().Split("#", StringSplitOptions.RemoveEmptyEntries);
                if (vectors.Length == 1)
                {
                    if (vectors[0].IndexOf("}") != -1)
                    {
                        var dataL = vectors[0].Replace("&", "");
                        var v = JsonConvert.DeserializeObject<Vector>(dataL);
                        if (v.IsResolving)
                            _currentResolveColumn = v;
                        else
                            _vectors.Add(v);
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
                         _vectors.Add(v);
                        
                    }
                    if (str.IndexOf("&") > -1)
                    {
                        var last = vectors[vectors.Length - 1].Remove(vectors[vectors.Length - 1].Length - 1);
                        _currentResolveColumn = JsonConvert.DeserializeObject<Vector>(last);
                        start = "";
                        break;
                    }
                    if (vectors[vectors.Length - 1].IndexOf("}") == -1)
                        start = vectors[vectors.Length - 1];
                }
            }
        }
        public void Dispose()
        {
            _stream.Close();
            _client.Close();
        }
    }
}