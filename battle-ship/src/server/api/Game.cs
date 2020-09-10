using System;
using System.Linq;
using battle_ship.dependencies.framework.api;
using battle_ship.dependencies.framework.net;
using TcpClient = System.Net.Sockets.TcpClient;

namespace battle_ship.server.api
{
    public class Game : Event
    {
        public Operation GetFieldSize(TcpServer server, TcpClient client, Operation op)
        {
            op.Data["field_size"] = dao.Game.Get(op.DeserializePayload<Guid>("game_id")).FieldSize;
            return op;
        }

        public Operation GetAll(TcpServer server, TcpClient client, Operation op)
        {
            op.Data["games"] = dao.Game.GetAll().Select(g => g.Id.ToString()).ToList();
            return op;
        }

        public Operation Add(TcpServer server, TcpClient client, Operation op)
        {
            var fieldSize = op.DeserializePayload<int>("field_size");

            dao.Game.Insert(new dependencies.model.Game(fieldSize));

            return op;
        }

        public Operation Join(TcpServer server, TcpClient client, Operation op)
        {
            // TODO: check for existence
            var game = dao.Game.Get(op.DeserializePayload<Guid>("game_id"));

            if (game.Users.First().Equals(Guid.Empty))
                game.Users[0] = dao.User.GetBySession(op.Session).Id;
            else
                game.Users[1] = dao.User.GetBySession(op.Session).Id;


            op.Response = Operation.Status.Ok;

            return op;
        }

        public Operation Running(TcpServer server, TcpClient client, Operation op)
        {
            var game = op.DeserializePayload<Guid>("game_id");
            var users = dao.User.GetAll();

            var user = users.Find(u => dao.Ship.OfUserInGame(u.Id, game)
                .All(s => s.Points.All(p => p.S.Equals('X'))));

            if (user.Id.Equals(Guid.Empty)) op.Response = Operation.Status.Yes;
            else
            {
                op.Data["user"] = user.Session;
                op.Response = Operation.Status.No;
            }

            return op;
        }
    }
}