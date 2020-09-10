using System;
using battle_ship.dependencies.framework.api;
using battle_ship.dependencies.framework.net;
using static battle_ship.dependencies.framework.net.Operation;
using TcpClient = System.Net.Sockets.TcpClient;

namespace battle_ship.server.api
{
    public class Enemy : Event
    {
        public Operation IsReady(TcpServer server, TcpClient client, Operation op)
        {
            try
            {
                var gameId = op.DeserializePayload<Guid>("game_id");
                var enemy = dao.User.GetEnemyBySessionInGame(op.Session, gameId);
                var ships = dao.Ship.OfUserInGame(enemy.Id, gameId);

                if (ships.Count > 0)
                {
                    dao.Game.Get(gameId).ActualTurn = enemy.Id;
                    op.Response = Status.Yes;
                }
                else
                {
                    op.Response = Status.No;
                }
            }
            catch (Exception)
            {
                op.Response = Status.Error;
            }

            return op;
        }

        public Operation HitReady(TcpServer server, TcpClient client, Operation op)
        {
            try
            {
                var gameId = op.DeserializePayload<Guid>("game_id");
                var user = dao.User.GetBySession(op.Session);
                var actualTurn = dao.Game.Get(gameId).ActualTurn;

                op.Response = actualTurn.Equals(user.Id) ? Status.Yes : Status.No;
            }
            catch
            {
                op.Response = Status.Error;
            }

            return op;
        }
    }
}