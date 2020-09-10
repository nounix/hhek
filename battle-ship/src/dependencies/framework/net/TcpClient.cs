using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace battle_ship.dependencies.framework.net
{
    public class TcpClient
    {
        private System.Net.Sockets.TcpClient _client;
        private NetworkStream _stream;

        public void Init(string targetIp, int targetPort)
        {
            _client = new System.Net.Sockets.TcpClient(targetIp, targetPort);
            _stream = _client.GetStream();
        }

        public void Close()
        {
            _stream.Close();
            _client.Close();
        }

        public Operation Read()
        {
            var data = new byte[Util.OpMsgSize];
            var bytes = _stream.Read(data, 0, data.Length);
            var msg = Encoding.ASCII.GetString(data, 0, bytes);

            return JsonSerializer.Deserialize<Operation>(msg);
        }

        public Operation Send(string action)
        {
            var op = new Operation(action);
            var data = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(op));
            _stream.Write(data, 0, data.Length);

            return Read();
        }

        public Operation Send(string action, Payload obj)
        {
            var op = new Operation(action, obj);
            var data = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(op));
            _stream.Write(data, 0, data.Length);

            return Read();
        }

        public Operation Send(string action, Guid session)
        {
            var op = new Operation(action, session);
            var data = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(op));
            _stream.Write(data, 0, data.Length);

            return Read();
        }

        public Operation Send(string action, Payload obj, Guid session)
        {
            var op = new Operation(action, obj, session);
            var data = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(op));
            _stream.Write(data, 0, data.Length);

            return Read();
        }
    }
}