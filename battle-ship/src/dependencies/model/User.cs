using System;
using battle_ship.dependencies.framework.db;

namespace battle_ship.dependencies.model
{
    public class User : IEntity
    {
        public User()
        {
        }

        public User(Credentials credentials)
        {
            Id = Guid.NewGuid();
            Credentials = credentials;
        }

        public Guid Session { get; set; }
        public Credentials Credentials { get; set; }

        public Guid Id { get; set; }
    }
}