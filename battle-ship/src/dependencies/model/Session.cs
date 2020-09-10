using System;
using System.Collections.Generic;
using battle_ship.dependencies.framework.db;

namespace battle_ship.dependencies.model
{
    public class Session : IEntity
    {
        public Session()
        {
            Data = new Dictionary<string, object>();
        }

        public Session(Guid id)
        {
            Id = id;
        }

        public Dictionary<string, object> Data { get; set; }

        public Guid Id { get; set; }

        public T GetData<T>(string key)
        {
            return (T) Data[key];
        }
    }
}