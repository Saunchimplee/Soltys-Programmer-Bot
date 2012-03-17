
namespace Soltys.ProgrammerBot.Core.Interfaces
{
    public interface IHudIconFactory
    {
        HudIcon EmptyIcon { get; }
        HudIcon UpIcon { get; }
        HudIcon TurnLeftIcon { get; }
        HudIcon TurnRightIcon { get; }
        HudIcon JumpIcon { get; }
        HudIcon LightIcon { get; }
        HudIcon LockIcon { get; }
        HudIcon FunctionOneIcon { get; }
        HudIcon FunctionTwoIcon { get; }
        HudIcon EmptyFunctionOneIcon { get; }
        HudIcon EmptyFunctionTwoIcon { get; }
    }
}
