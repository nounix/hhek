using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.ui;
using battle_ship.dependencies.model;

namespace battle_ship.dependencies.framework.view
{
    public static class Util
    {
        public static ControllerHandler LoadControllers(string controllerNameSpace, List<View> views)
        {
            return LoadControllers(controllerNameSpace, views, new Session(), new TcpClient(), new ControllerHandler());
        }

        public static ControllerHandler LoadControllers(string controllerNameSpace, List<View> views, Session session,
            TcpClient client, ControllerHandler handler)
        {
            var fullNameSpace = typeof(Program).Namespace + "." + controllerNameSpace;
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == fullNameSpace);

            types.ToList().ForEach(c =>
            {
                try
                {
                    if (!(Activator.CreateInstance(c) is Controller controller)) return;

                    var view = views.Find(v => v.GetType().Name.Equals(c.Name));
                    view.SetController(controller);
                    controller.Init(view, session, client, handler);
                    handler.Controllers.Add(c.Name, controller);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            return handler;
        }

        public static List<View> LoadViews(string nameSpace)
        {
            return LoadViews(nameSpace, new Terminal());
        }

        public static List<View> LoadViews(string nameSpace, Terminal terminal)
        {
            var fullNameSpace = typeof(Program).Namespace + "." + nameSpace;
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == fullNameSpace);

            var views = new List<View>();

            types.ToList().ForEach(v =>
            {
                try
                {
                    if (!(Activator.CreateInstance(v) is View view)) return;

                    view.Terminal = terminal;
                    views.Add(view);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            return views;
        }
    }
}