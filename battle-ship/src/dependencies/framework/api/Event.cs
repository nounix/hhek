using System;
using battle_ship.dependencies.framework.net;
using TcpClient = System.Net.Sockets.TcpClient;

namespace battle_ship.dependencies.framework.api
{
    public abstract class Event
    {
        public Operation Handle(TcpServer server, TcpClient client, Operation op)
        {
            var method = GetType().GetMethod(op.Method());

            try
            {
                return (Operation) method?.Invoke(this, new object[] {server, client, op});
            }
            catch (Exception e)
            {
                Console.WriteLine("SERVER ERROR START:\n++++++++++++++++++++++++++\n\n" + e +
                                  "\n\n++++++++++++++++++++++++++\nSERVER ERROR END\n");
                op.Response = Operation.Status.Error;
                return op;
            }
        }
    }
}