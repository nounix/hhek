using System;
using System.Linq;
using System.Text.Json;

namespace battle_ship.dependencies.framework.net
{
    [Serializable]
    public class Operation
    {
        public enum Status
        {
            Ok,
            Error,
            Yes,
            No
        }

        public Operation()
        {
        }

        public Operation(string action)
        {
            Action = action;
            Data = new Payload();
        }

        public Operation(string action, Payload data)
        {
            Action = action;
            Data = data;
        }

        public Operation(string action, Guid session)
        {
            Action = action;
            Data = new Payload();
            Session = session;
        }

        public Operation(string action, Payload data, Guid session)
        {
            Action = action;
            Data = data;
            Session = session;
        }

        public string Action { get; set; }

        public Status Response { get; set; }

        // TODO: split into req and resp
        public Payload Data { get; set; }
        public Guid Session { get; set; }

        public string Event()
        {
            return Action.Split('.').First();
        }

        public string Method()
        {
            return Action.Split('.').Last();
        }

        public dynamic DeserializePayload(string key)
        {
            return JsonSerializer.Deserialize(((JsonElement) ((Data) Data[key]).Object).GetRawText(),
                Type.GetType(((Data) Data[key]).Type));
        }

        public T DeserializePayload<T>(string key)
        {
            return JsonSerializer.Deserialize<T>(((JsonElement) ((Data) Data[key]).Object).GetRawText());
        }
    }
}