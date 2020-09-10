using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.view;

namespace battle_ship.client.controller
{
    public class Ship : Controller
    {
        public void PlaceRandom(string count)
        {
            var data = new Payload
            {
                ["game_id"] = Session.Data["game_id"],
                ["ship_count"] = int.Parse(count)
            };
            Client.Send("Ship.PlaceRandom", data, Session.Id);
        }
    }
}