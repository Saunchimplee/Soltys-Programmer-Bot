using Soltys.ProgrammerBot.Core.HudElements;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public class CommandBarRenderer : IRenderer<CommandBar>
    {
        public void Render(CommandBar obj)
        {
            int i = 0;
            foreach (var icon in obj.Icons)
            {
                icon.SetPostion(obj.HudWidth - 500 + i * 60, 50);
                icon.Render(icon);
                i++;
            }
        }
    }
}
