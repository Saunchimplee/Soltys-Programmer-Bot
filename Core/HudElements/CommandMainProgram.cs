using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.Core.HudElements
{
    public class CommandMainProgram : CommandHolder
    {
        const int PROGRAM_SIZE = 21;

        IRenderer<CommandMainProgram> renderer;

        public CommandMainProgram(Hud currentHud, IRenderer<CommandMainProgram> renderer, IHudIconFactory factory)
            : base(currentHud)
        {
            this.renderer = renderer;
            IconFactory = factory;
            DefaultEmptyIcon = factory.EmptyIcon;
            for (int i = 0; i < PROGRAM_SIZE; i++)
            {
                IconList.Add(factory.EmptyIcon);
            }

        }

        public void Render()
        {
            renderer.Render(this);

        }


    }
}
