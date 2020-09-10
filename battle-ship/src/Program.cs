using static battle_ship.dependencies.framework.view.Util;

namespace battle_ship
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LoadControllers("client.controller", LoadViews("client.view")).Load("Server");
        }
    }
}