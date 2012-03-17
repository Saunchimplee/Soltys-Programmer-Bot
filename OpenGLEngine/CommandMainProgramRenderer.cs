using Soltys.ProgrammerBot.Core.HudElements;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class CommandMainProgramRenderer : IRenderer<CommandMainProgram>
    {
        public void Render(CommandMainProgram obj)
        {
            int i = 0;
            int j = 0;
            foreach (var icon in obj.Icons)
            {
                icon.SetPostion(obj.HudWidth - 500 + i * 60, 150 + j * 60);
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
