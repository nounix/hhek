using System;
using System.Collections.Generic;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.view;

namespace battle_ship.client.controller
{
    public class Game : Controller
    {
        public IEnumerable<string> GetAll()
        {
            var response = Client.Send("Game.GetAll");
            return response.DeserializePayload<List<string>>("games");
        }

        public void Add(string fieldSize)
        {
            var data = new Payload {["field_size"] = int.Parse(fieldSize)};
            Client.Send("Game.Add", data);
        }

        public void Join(string id)
        {
            var data = new Payload {["game_id"] = Guid.Parse(id)};
            Client.Send("Game.Join", data, Session.Id);

            Session.Data["game_id"] = Guid.Parse(id);
        }
    }
}