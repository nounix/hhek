using System;
using battle_ship.dependencies.framework.api;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.model;
using static battle_ship.dependencies.framework.net.Operation;
using Session = battle_ship.server.dao.Session;
using TcpClient = System.Net.Sockets.TcpClient;

namespace battle_ship.server.api
{
    public class User : Event
    {
        public Operation Add(TcpServer server, TcpClient client, Operation op)
        {
            var credentials = op.DeserializePayload<Credentials>("credentials");

            var user = dao.User.GetByLoginCredentials(credentials) ?? new dependencies.model.User(credentials);

            dao.User.Insert(user);

            op.Response = Status.Ok;

            return op;
        }

        public Operation Login(TcpServer server, TcpClient client, Operation op)
        {
            var credentials = op.DeserializePayload<Credentials>("credentials");

            var user = dao.User.GetByLoginCredentials(credentials);

            if (user == null)
            {
                op.Response = Status.Error;
                op.Data["session_id"] = Guid.Empty;
                return op;
            }

            if (user.Session.Equals(Guid.Empty))
            {
                var id = Guid.NewGuid();
                Session.Insert(new dependencies.model.Session(id));
                user.Session = id;
            }

            op.Response = Status.Ok;
            op.Data["session_id"] = user.Session;

            return op;
        }
    }
}