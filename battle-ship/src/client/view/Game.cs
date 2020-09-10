using System;
using System.Collections.Generic;
using System.Linq;
using battle_ship.dependencies.framework.view;

namespace battle_ship.client.view
{
    public class Game : View
    {
        private controller.Game _controller;

        public override void SetController(Controller controller)
        {
            _controller = (controller.Game) controller;
        }

        public override void PreRender()
        {
            Terminal.Clear();
        }

        public override void Render()
        {
            var options = new List<Tuple<string, Action>>
            {
                new Tuple<string, Action>("Add game", Add),
                new Tuple<string, Action>("Join game", Join)
            };

            Terminal.SelectOption(options, () => _controller.Handler.Load("User"));
        }

        private void Add()
        {
            Terminal.Clear();
            Terminal.PrintCenter("Enter field size:");
            _controller.Add(Terminal.ReadCenter());

            _controller.Handler.Load("Game");
        }

        private void Join()
        {
            Terminal.Clear();
            Terminal.PrintCenter("Select game to join:");

            // TODO: rm, just for speed up testing
//            _game.Add("10");

            var options = _controller.GetAll().ToList();

            // TODO: rm, just for speed up testing
            // var game = options.Last();
           var game = Terminal.SelectOption(options, () => _controller.Handler.Load("Game"));

            // TODO: need this line otherwise when back tracing, the code below would be executed, is there better way?
            if (game.Equals("")) return;

            _controller.Join(game);

            _controller.Handler.Load("Ship");
        }
    }
}