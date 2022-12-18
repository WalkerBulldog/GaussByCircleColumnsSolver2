using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class ClientRunner
    {
        private static string _ipAddress = "127.0.0.1";
        private static int _port = 11000;
        public static void Main(string[] args)
        {
            while (true)
            {
                //GetSettingsFromFile();
                GaussSolverTcpClient client = new GaussSolverTcpClient(_ipAddress, _port);
                client.Run();
                client.Dispose();
            }
        }
        private static void GetSettingsFromFile()
        {
            var lines = File.ReadAllLines("..\\..\\..\\..\\settings.txt");
            if (ValidateIPv4(lines[0]))
                _ipAddress = lines[0];
            int port;
            if (int.TryParse(lines[1], out port) && port > 0 && port < 65535)
                _port = port;
        }
        private static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
