using System;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.view;
using battle_ship.dependencies.model;

namespace battle_ship.client.controller
{
    public class User : Controller
    {
        public void Add(string name)
        {
            var data = new Payload {["credentials"] = new Credentials(name, name)};
            Client.Send("User.Add", data);
        }

        public void Login(string name)
        {
            var data = new Payload {["credentials"] = new Credentials(name, name)};
            var response = Client.Send("User.Login", data);

            Session.Id = response.DeserializePayload<Guid>("session_id");
        }
    }
}