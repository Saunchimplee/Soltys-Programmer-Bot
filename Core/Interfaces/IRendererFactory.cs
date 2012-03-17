using Soltys.ProgrammerBot.Core.HudElements;
namespace Soltys.ProgrammerBot.Core.Interfaces
{
    public interface IRendererFactory
    {
        IRenderer<Game> GameRenderer { get; }
        IRobotRenderer RobotRenderer { get; }
        ILevelRenderer LevelRenderer { get; }
        IHudRenderer HudRenderer { get; }
        IHudIconFactory HudIconFactory { get; }
        IRenderer<CommandBar> CommandBarRenderer { get; }
        IRenderer<CommandMainProgram> CommandMainProgramRenderer { get; }
        IRenderer<CommandFunction> FunctionOneRenderer { get; }
        IRenderer<CommandFunction> FunctionTwoRenderer { get; }
    }
}
