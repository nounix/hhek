using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.view;
using Util = battle_ship.dependencies.framework.api.Util;

namespace battle_ship.client.controller
{
    public class Server : Controller
    {
        public void CreateServer()
        {
            var eventHandler = Util.LoadEvents("server.api");
            new TcpServer(2).Start(eventHandler);
        }

        public void CreateClient(string server)
        {
            Client.Init(server.Split(":")[0], int.Parse(server.Split(":")[1]));
        }
    }
}