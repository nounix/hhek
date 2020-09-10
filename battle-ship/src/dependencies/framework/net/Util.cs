using System;
using System.Net;
using System.Net.Sockets;

namespace battle_ship.dependencies.framework.net
{
    public static class Util
    {
        // INFO: in prod import this class as own package to be used in server and client
        public static readonly int OpMsgSize = 8192;

        public static string GetLocalIp()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                try
                {
                    socket.Connect("8.8.8.8", 65530);
                    var endPoint = socket.LocalEndPoint as IPEndPoint;
                    return endPoint?.Address.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return "127.0.0.1";
        }

        public static int GetFreePort()
        {
            var l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            var port = ((IPEndPoint) l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}