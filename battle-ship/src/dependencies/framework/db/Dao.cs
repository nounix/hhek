using System;
using System.Collections.Generic;
using System.Linq;

namespace battle_ship.dependencies.framework.db
{
    public abstract class Dao<T>
    {
        public static T Get(Guid id)
        {
            return DataBase.GetInstance().Get<T>(id);
        }

        public static List<T> GetAll()
        {
            return DataBase.GetInstance().GetAll<T>();
        }

        public static void Insert(T t)
        {
            DataBase.GetInstance().Insert((IEntity) t);
        }

        public static void InsertAll(IEnumerable<T> list)
        {
            DataBase.GetInstance().InsertAll(list.Select(t => (IEntity) t));
        }

        public static void Update(T t)
        {
            throw new NotImplementedException();
        }

        public static void Delete(T t)
        {
            throw new NotImplementedException();
        }
    }
}