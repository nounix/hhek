using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using static System.Text.Encoding;
using static battle_ship.dependencies.util.Util;
using EventHandler = battle_ship.dependencies.framework.api.EventHandler;

namespace battle_ship.dependencies.framework.net
{
    public class TcpServer
    {
        public TcpServer()
        {
        }

        public TcpServer(int clientLimit)
        {
            IpAddress = IPAddress.Parse(Util.GetLocalIp());
            Port = Util.GetFreePort();
            Listener = new TcpListener(IpAddress, Port);
            ClientLimit = clientLimit;
        }

        public IPAddress IpAddress { get; }
        public int Port { get; }
        private TcpListener Listener { get; }
        public int ClientLimit { get; }

        public void Close()
        {
            Listener.Stop();
        }

        public void Start(EventHandler eventHandler)
        {
            Listener.Start();

            for (var i = 0; i < ClientLimit; i++) AcceptClient(eventHandler);

            // TODO: replace by discovery service
            WriteLine("server.list", IpAddress + ":" + Port);
        }

        private void AcceptClient(EventHandler eventHandler)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                var client = Listener.AcceptTcpClient();
                var stream = client.GetStream();
                var bytes = new byte[Util.OpMsgSize];

                while (stream.Read(bytes) != 0)
                {
                    stream.Write(ASCII.GetBytes(JsonSerializer.Serialize(
                        eventHandler.Handle(this, client, ASCII.GetString(bytes)))));

                    Array.Clear(bytes, 0, bytes.Length);
                }

                stream.Close();
                client.Close();
            }).Start();
        }
    }
}