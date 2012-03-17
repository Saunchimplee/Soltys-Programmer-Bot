
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.Core.HudElements
{
    public class CommandFunction : CommandHolder
    {
        private const int size = 14;
        IRenderer<CommandFunction> renderer;

        public CommandFunction(Hud currentHud, IRenderer<CommandFunction> renderer, IHudIconFactory factory)
            : base(currentHud)
        {
            this.renderer = renderer;
            this.IconFactory = factory;
        }
        public void AddEmptyIcons(IHudIconFactory factory)
        {
            for (int i = 0; i < size; i++)
            {
                IconList.Add(DefaultEmptyIcon.Clone());
            }
        }
        public void Render()
        {
            renderer.Render(this);
        }
    }
}
