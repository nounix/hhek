using System;
using System.Collections.Generic;
using System.Text.Json;
using battle_ship.dependencies.framework.net;
using TcpClient = System.Net.Sockets.TcpClient;

namespace battle_ship.dependencies.framework.api
{
    public class EventHandler
    {
        public readonly Dictionary<string, Func<TcpServer, TcpClient, Operation, Operation>>
            Events = new Dictionary<string, Func<TcpServer, TcpClient, Operation, Operation>>();

        public Operation Handle(TcpServer server, TcpClient client, string msg)
        {
            // TODO: replace dirty fix
            msg = msg.Substring(0, msg.LastIndexOf('}') + 1);
            var op = JsonSerializer.Deserialize<Operation>(msg);
            return Events[op.Event()].Invoke(server, client, op);
        }
    }
}