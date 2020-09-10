using System.Collections.Generic;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.view;
using battle_ship.dependencies.model;

namespace battle_ship.client.controller
{
    public class Play : Controller
    {
        public int GetFieldSize()
        {
            var data = new Payload {["game_id"] = Session.Data["game_id"]};
            return Client.Send("Game.GetFieldSize", data, Session.Id).DeserializePayload<int>("field_size");
        }

        public IEnumerable<dependencies.model.Ship> GetAllShips()
        {
            var data = new Payload {["game_id"] = Session.Data["game_id"]};
            var response = Client.Send("Ship.GetAll", data, Session.Id);
            return response.DeserializePayload<List<dependencies.model.Ship>>("ships");
        }
        
        public IEnumerable<dependencies.model.Hit> GetAllHits()
        {
            var data = new Payload {["game_id"] = Session.Data["game_id"]};
            var response = Client.Send("Hit.GetAll", data, Session.Id);
            return response.DeserializePayload<List<dependencies.model.Hit>>("hits");
        }
        public Operation.Status HitReady()
        {
            var data = new Payload { ["game_id"] = Session.Data["game_id"] };
            return Client.Send("Enemy.HitReady", data, Session.Id).Response;
        }

        public Operation GameRunning()
        {
            var data = new Payload { ["game_id"] = Session.Data["game_id"] };
            return Client.Send("Game.Running", data, Session.Id);
        }
        public Operation HitTry(string input)
        {
            int x = input[0] - 48;
            int y = input[1];

            // TODO: add constructor
            var data = new Payload { 
                ["point"] = new Point(x, y, ' '),
                ["game_id"] = Session.Data["game_id"]
            };
            
            return Client.Send("Hit.Try", data, Session.Id);
        }
    }
}