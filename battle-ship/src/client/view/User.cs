using System;
using System.Collections.Generic;
using battle_ship.dependencies.framework.view;

namespace battle_ship.client.view
{
    public class User : View
    {
        private controller.User _controller;

        public override void SetController(Controller controller)
        {
            _controller = (controller.User) controller;
        }

        public override void PreRender()
        {
            Terminal.Clear();
        }

        public override void Render()
        {
            var options = new List<Tuple<string, Action>>
            {
                new Tuple<string, Action>("Add user", Add),
                new Tuple<string, Action>("Login user", Login)
            };

            Terminal.SelectOption(options, () => _controller.Handler.Load("Server"));
        }

        private void Add()
        {
            Terminal.Clear();
            Terminal.PrintCenter("Enter name:");
            _controller.Add(Terminal.ReadCenter());

            _controller.Handler.Load("User");
        }

        private void Login()
        {
            Terminal.Clear();
            Terminal.PrintCenter("Enter name:");

            // TODO: rm, just for speed up testing
//            var name = "asd";
//            _user.Add(name);
//            _user.Login(name);
            _controller.Login(Terminal.ReadCenter());

            _controller.Handler.Load("Game");
        }
    }
}