using System;
using System.Linq;
using System.Reflection;

namespace battle_ship.dependencies.framework.api
{
    public class Util
    {
        public static EventHandler LoadEvents(string eventNameSpace)
        {
            return LoadEvents(new EventHandler(), eventNameSpace);
        }

        public static EventHandler LoadEvents(EventHandler eventHandler, string eventNameSpace)
        {
            var nameSpace = typeof(Program).Namespace + "." + eventNameSpace;

            var events = Assembly.GetExecutingAssembly().GetTypes().Where(e => e.IsClass && e.Namespace == nameSpace);

            events.ToList().ForEach(e =>
            {
                Console.WriteLine("Loading event: " + e.Name);

                try
                {
                    if (!(Activator.CreateInstance(e) is Event _event)) return;

                    eventHandler.Events.Add(e.Name, _event.Handle);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });

            return eventHandler;
        }
    }
}