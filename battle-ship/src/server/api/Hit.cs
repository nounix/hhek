using System;
using System.Collections.Generic;
using System.Linq;
using battle_ship.dependencies.framework.api;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.model;
using TcpClient = System.Net.Sockets.TcpClient;

namespace battle_ship.server.api
{
    public class Hit : Event
    {
        public Operation GetAll(TcpServer server, TcpClient client, Operation op)
        {
            // TODO: check for existence
            var game = dao.Game.Get(op.DeserializePayload<Guid>("game_id")).Id;
            var user = dao.User.GetBySession(op.Session).Id;

            try
            {
                op.Data["hits"] = dao.Hit.OfUserInGame(user, game);
                op.Response = Operation.Status.Ok;
            }
            catch
            {
                op.Data["hits"] = new List<dependencies.model.Hit>();
                op.Response = Operation.Status.Error;
            }

            return op;
        }

        public Operation Try(TcpServer server, TcpClient client, Operation op)
        {
            var game = dao.Game.Get(op.DeserializePayload<Guid>("game_id")).Id;
            var point = op.DeserializePayload<Point>("point");
            var user = dao.User.GetBySession(op.Session).Id;
            var enemy = dao.User.GetEnemyBySessionInGame(op.Session, game).Id;
            var ships = dao.Ship.OfUserInGame(enemy, game);

            foreach (var p in ships.SelectMany(ship => ship.Points))
            {
                if (p.Equals(point))
                {
                    p.S = 'X';
                    dao.Hit.Insert(new dependencies.model.Hit(user, game, p));
                }
                else
                {
                    point.S = 'O';
                    dao.Hit.Insert(new dependencies.model.Hit(user, game, point));
                }
            }

            return op;
        }
    }
}