using System;
using battle_ship.dependencies.framework.db;

namespace battle_ship.dependencies.model
{
    public class Hit : IEntity
    {
        public Hit()
        {
        }

        public Hit(Guid user, Guid game, Point point)
        {
            Id = Guid.NewGuid();
            User = user;
            Game = game;
            Point = point;
        }

        public Guid User { get; set; }
        public Guid Game { get; set; }
        public Point Point { get; set; }

        public Guid Id { get; set; }
    }
}