using System;
using System.Collections.Generic;
using battle_ship.dependencies.framework.db;

namespace battle_ship.dependencies.model
{
    public class Ship : IEntity
    {
        public Ship()
        {
        }

        public Ship(Guid user, Guid game, List<Point> points)
        {
            Id = Guid.NewGuid();
            User = user;
            Game = game;
            Points = points;
        }

        public Guid User { get; set; }
        public Guid Game { get; set; }
        public List<Point> Points { get; set; }

        public Guid Id { get; set; }
    }
}