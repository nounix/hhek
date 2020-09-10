using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.view;

namespace battle_ship.client.controller
{
    public class Wait : Controller
    {
        public Operation.Status EnemyIsReady()
        {
            var data = new Payload {["game_id"] = Session.Data["game_id"]};
            return Client.Send("Enemy.IsReady", data, Session.Id).Response;
        }
    }
}