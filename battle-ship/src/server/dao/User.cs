using System;
using System.Linq;
using battle_ship.dependencies.framework.db;
using battle_ship.dependencies.model;

namespace battle_ship.server.dao
{
    public class User : Dao<dependencies.model.User>
    {
        public static dependencies.model.User GetBySession(Guid session)
        {
            return GetAll().Find(u => u.Session.Equals(session));
        }

        public static dependencies.model.User GetByLoginCredentials(Credentials credentials)
        {
            return GetAll().Find(u => u.Credentials.Equals(credentials));
        }

        public static dependencies.model.User GetEnemyBySessionInGame(Guid session, Guid game)
        {
            var users = Game.Get(game).Users;
            var user = GetBySession(session).Id;

            return Get(users.First(u => !u.Equals(user)));
        }
    }
}