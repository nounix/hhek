using System;
using System.Collections.Generic;
using System.Linq;
using battle_ship.dependencies.framework.api;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.model;
using static battle_ship.dependencies.framework.net.Operation;
using TcpClient = System.Net.Sockets.TcpClient;

namespace battle_ship.server.api
{
    public class Ship : Event
    {
        public Operation GetAll(TcpServer server, TcpClient client, Operation op)
        {
            var gameId = op.DeserializePayload<Guid>("game_id");
            var user = dao.User.GetBySession(op.Session);
            var ships = dao.Ship.OfUserInGame(user.Id, gameId);

            op.Data["ships"] = ships;

            return op;
        }

        public Operation PlaceRandom(TcpServer server, TcpClient client, Operation op)
        {
            var count = op.DeserializePayload<int>("ship_count");

            var gameId = op.DeserializePayload<Guid>("game_id");
            var userId = dao.User.GetBySession(op.Session).Id;

            var fieldSize = dao.Game.Get(gameId).FieldSize;


            try
            {
                dao.Ship.InsertAll(PlaceAll(userId, gameId, fieldSize, count, 0));
                op.Response = Status.Ok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                op.Response = Status.Error;
            }

            return op;
        }

        private List<dependencies.model.Ship> PlaceAll(Guid userId, Guid gameId, int fieldSize, int shipCount,
            int counter)
        {
            var ships = new List<dependencies.model.Ship>();

            try
            {
                for (var i = 0; i < shipCount; i++)
                    ships.Add(Place(userId, gameId, fieldSize, ships, i + 1));
            }
            catch (Exception)
            {
                if (counter > 100) throw;
                PlaceAll(userId, gameId, fieldSize, shipCount, ++counter);
            }

            return ships;
        }

        private dependencies.model.Ship Place(Guid userId, Guid gameId, int fieldSize,
            IReadOnlyCollection<dependencies.model.Ship> ships, int shipSize)
        {
            // TODO: need to init outside or reseed??
            var rnd = new Random();
            var counter = 0;
            dependencies.model.Ship ship;

            do
            {
                if (counter > 100) throw new Exception("No possible solution");


                var x = new Point(rnd.Next(1, fieldSize - shipSize), rnd.Next(1, fieldSize - shipSize), 'S');

                while (ships.Count != 0 && Exists(ships, x))
                {
                    if (counter > 100) throw new Exception("No possible solution");

                    x = new Point(rnd.Next(1, fieldSize - shipSize), rnd.Next(1, fieldSize - shipSize), 'S');

                    counter++;
                }

                var y = rnd.Next(0, 2) == 0 ? new Point(x.X, x.Y + shipSize, 'S') : new Point(x.X + shipSize, x.Y, 'S');


                ship = new dependencies.model.Ship(userId, gameId, Fill(x, y));
                counter++;
            } while (ships.Count != 0 && Collision(ships, ship, fieldSize));

            return ship;
        }

        private List<Point> Fill(Point x, Point y)
        {
            var points = new List<Point>();

            points.Add(x);

            if (x.X == y.X)
                for (var i = x.Y + 1; i < y.Y; i++)
                    points.Add(new Point(x.X, i, 'S'));
            else
                for (var i = x.X + 1; i < y.X; i++)
                    points.Add(new Point(i, x.Y, 'S'));

            points.Add(y);

            return points;
        }

        private bool Collision(IEnumerable<dependencies.model.Ship> ships, dependencies.model.Ship ship, int fieldSize)
        {
            return ship.Points.Any(p => p.X >= fieldSize || p.Y >= fieldSize) ||
                   ships.Any(s => s.Points.Any(sp => ship.Points.Contains(sp)));
        }

        private bool Exists(IEnumerable<dependencies.model.Ship> ships, Point point)
        {
            return ships.Any(s => s.Points.Any(point.Equals));
        }
    }
}