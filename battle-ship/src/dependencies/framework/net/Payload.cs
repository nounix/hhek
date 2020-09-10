using System.Collections.Generic;

namespace battle_ship.dependencies.framework.net
{
    public class Payload
    {
        public Payload()
        {
            Data = new List<Data>();
        }

        public List<Data> Data { get; set; }

        public object this[string key]
        {
            get => Data.Find(data => data.Key.Equals(key));
            set => Data.Add(new Data(key, value));
        }
    }
}