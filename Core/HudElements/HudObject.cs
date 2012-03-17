
namespace Soltys.ProgrammerBot.Core.HudElements
{
    public abstract class HudObject
    {
        protected Hud HudHandler;

        protected HudObject(Hud currentHud)
        {
            HudHandler = currentHud;
        }
        public int HudWidth { get { return HudHandler.HudWidth; } }
        public int HudHeight { get { return HudHandler.HudHeight; } }
    }
}
