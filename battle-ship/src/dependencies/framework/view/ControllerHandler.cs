using System.Collections.Generic;

namespace battle_ship.dependencies.framework.view
{
    public class ControllerHandler
    {
        public ControllerHandler()
        {
            Controllers = new Dictionary<string, Controller>();
        }

        public Dictionary<string, Controller> Controllers { get; set; }

        public void Load(string controller)
        {
            Controllers[controller]?.View.PreRender();
            Controllers[controller]?.View.Render();
        }
    }
}