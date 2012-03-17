
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public class GameRenderer : IRenderer<Core.Game>
    {
        public void Render(Core.Game obj)
        {
            obj.Level.Render();
            obj.Robot.Render();
            obj.HUD.Render();
        }
    }
}
