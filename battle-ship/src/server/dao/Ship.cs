using System;
using System.Collections.Generic;
using battle_ship.dependencies.framework.db;

namespace battle_ship.server.dao
{
    public class Ship : Dao<dependencies.model.Ship>
    {
        public static List<dependencies.model.Ship> OfUserInGame(Guid user, Guid game)
        {
            return GetAll().FindAll(ship => ship.User.Equals(user) && ship.Game.Equals(game));
        }
    }
}