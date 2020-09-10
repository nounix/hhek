using battle_ship.dependencies.framework.view;
using static battle_ship.dependencies.framework.net.Operation;

namespace battle_ship.client.view
{
    public class Wait : View
    {
        private controller.Wait _controller;

        public override void SetController(Controller controller)
        {
            _controller = (controller.Wait) controller;
        }

        public override void PreRender()
        {
            Terminal.Clear();
        }

        public override void Render()
        {
            while (_controller.EnemyIsReady() != Status.Yes) Terminal.PrintLoading("Waiting for other player:");

            _controller.Handler.Load("Play");
        }
    }
}