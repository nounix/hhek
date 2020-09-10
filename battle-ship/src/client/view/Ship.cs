using battle_ship.dependencies.framework.view;

namespace battle_ship.client.view
{
    public class Ship : View
    {
        private controller.Ship _controller;

        public override void SetController(Controller controller)
        {
            _controller = (controller.Ship) controller;
        }

        public override void PreRender()
        {
            Terminal.Clear();
        }

        public override void Render()
        {
            Terminal.PrintCenter("Enter ship count:");

            _controller.PlaceRandom(Terminal.ReadCenter());

            _controller.Handler.Load("Wait");
        }
    }
}