
using Soltys.ProgrammerBot.Core.HudElements;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class FunctionTwoRenderer : IRenderer<CommandFunction>
    {
        public void Render(CommandFunction obj)
        {
            int i = 0;
            int j = 0;
            foreach (var icon in obj.Icons)
            {
                icon.SetPostion(obj.HudWidth - 500 + i * 60, 500 + j * 60);
                icon.Render(icon);
                i++;
                if (i % 7 == 0)
                {
                    i = 0;
                    j++;
                }
            }
        }
    }
}
