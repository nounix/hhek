using System;
using System.Collections.Generic;
using System.Linq;
using battle_ship.dependencies.framework.view;
using Util = battle_ship.dependencies.util.Util;

namespace battle_ship.client.view
{
    public class Server : View
    {
        private controller.Server _controller;

        public override void SetController(Controller controller)
        {
            _controller = (controller.Server) controller;
        }

        public override void PreRender()
        {
            Terminal.Clear();
        }

        public override void Render()
        {
            var options = new List<Tuple<string, Action<string>>>
            {
                new Tuple<string, Action<string>>("Create Server", CreateServer),
                new Tuple<string, Action<string>>("Join Server", JoinServer)
            };

            Terminal.SelectOption(options, () => Environment.Exit(0));
        }

        private void CreateServer(string sel)
        {
            Terminal.Clear();

            _controller.CreateServer();

            Terminal.PrintCenter("Server created!");

            // TODO: just for fast debugging
            _controller.Handler.Load("Server");
//            Terminal.ReadCenter();
        }

        private void JoinServer(string sel)
        {
            Terminal.Clear();

            Terminal.PrintCenter("Select server to join:");

            // TODO: replace by discovery service
            var options = Util.ReadLines("server.list");

            // TODO: rm, just for speed up testing
            // var server = options.Last();
           var server = Terminal.SelectOption(options, () => _controller.Handler.Load("Server"));

            // TODO: need this line otherwise when back tracing, the code below would be executed, is there better way?
            if (server.Equals("")) return;

            _controller.CreateClient(server);


            _controller.Handler.Load("User");
        }
    }
}