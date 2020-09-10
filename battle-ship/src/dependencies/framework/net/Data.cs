namespace battle_ship.dependencies.framework.net
{
    public class Data
    {
        public Data()
        {
        }

        public Data(string key, object o)
        {
            Key = key;
            Type = o.GetType().FullName;
            Object = o;
        }

        public string Key { get; set; }
        public string Type { get; set; }
        public object Object { get; set; }
    }
}