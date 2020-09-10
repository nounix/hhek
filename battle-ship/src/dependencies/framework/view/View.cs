using battle_ship.dependencies.framework.ui;

namespace battle_ship.dependencies.framework.view
{
    public abstract class View
    {
        public Terminal Terminal { get; set; }

        public abstract void SetController(Controller controller);
        public abstract void PreRender();
        public abstract void Render();
    }
}