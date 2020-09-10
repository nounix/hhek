using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.model;

namespace battle_ship.dependencies.framework.view
{
    public abstract class Controller
    {
        public View View { get; set; }
        public Session Session { get; set; }
        public TcpClient Client { get; set; }

        public ControllerHandler Handler { get; set; }

        public void Init(View view, Session session, TcpClient client, ControllerHandler handler)
        {
            View = view;
            Session = session;
            Client = client;

            Handler = handler;
        }
    }
}