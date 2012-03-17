
using Soltys.ProgrammerBot.Core.HudElements;

namespace Soltys.ProgrammerBot.Core.Interfaces
{
    public interface IHudRenderer : IRenderer<Hud>
    {
        int HudWidth { get; set; }
        int HudHeight { get; set; }
    }
}
