using System;
using System.Collections.Generic;
using battle_ship.dependencies.framework.db;

namespace battle_ship.server.dao
{
    public class Hit : Dao<dependencies.model.Hit>
    {
        public static List<dependencies.model.Hit> OfUserInGame(Guid user, Guid game)
        {
            return GetAll().FindAll(hit => hit.User.Equals(user) && hit.Game.Equals(game));
        }
    }
}