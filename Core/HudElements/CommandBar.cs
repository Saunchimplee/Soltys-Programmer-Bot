using System.Drawing;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.Core.HudElements
{
    public class CommandBar : CommandView
    {

        IRenderer<CommandBar> renderer;
        public CommandBar(Hud currentHud, IRenderer<CommandBar> render, IHudIconFactory factory)
            : base(currentHud)
        {
            this.renderer = render;
            IconList.Add(factory.UpIcon);
            IconList.Add(factory.TurnLeftIcon);
            IconList.Add(factory.TurnRightIcon);
            IconList.Add(factory.JumpIcon);
            IconList.Add(factory.LightIcon);
            IconList.Add(factory.FunctionOneIcon);
            IconList.Add(factory.FunctionTwoIcon);
        }
        public HudIcon GrabIcon(Point p)
        {
            foreach (var icon in IconList)
            {
                if (icon.Contains(p))
                {
                    return icon.Clone();
                }
            }
            return null;
        }
        public void GrabIcon(object sender, IconGrabEventArgs e)
        {
            foreach (var icon in IconList)
            {
                if (icon.Contains(e.GrabPoint))
                {
                    HudHandler.GrabbedIcon = icon.Clone();
                    HudHandler.MoveGrabbedIcon(e.GrabPoint.X, e.GrabPoint.Y);
                }
            }

        }

        public void Render()
        {
            renderer.Render(this);
        }
    }
}
