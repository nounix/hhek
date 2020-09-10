using System;
using System.Collections.Generic;

namespace battle_ship.dependencies.framework.db
{
    public class DataBase
    {
        private static DataBase _dataBaseBaseInstance;
        private readonly List<IEntity> _entities;

        private DataBase()
        {
            _entities = new List<IEntity>();
        }

        public static DataBase GetInstance()
        {
            if (_dataBaseBaseInstance != null) return _dataBaseBaseInstance;

            _dataBaseBaseInstance = new DataBase();
            return _dataBaseBaseInstance;
        }

        public T Get<T>(Guid id)
        {
            return (T) _entities.Find(e => e.Id.Equals(id));
        }

        public List<T> GetAll<T>()
        {
            var list = new List<T>();

            foreach (var entity in _entities)
                if (entity is T e)
                    list.Add(e);

            return list;
        }

        public void Insert(IEntity t)
        {
            _entities.Add(t);
        }

        public void InsertAll(IEnumerable<IEntity> t)
        {
            _entities.AddRange(t);
        }

//
//        void Update<T>(T t, string[] args);
//
//        void Delete<T>(T t);
    }
}