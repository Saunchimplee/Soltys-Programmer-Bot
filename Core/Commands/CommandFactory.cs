
namespace Soltys.ProgrammerBot.Core.Commands
{
    public abstract class CommandFactory
    {
        public static Game GameHandler { get; set; }


        protected CommandFactory() { }

        public abstract Command TurnLeftCommand { get; }
        public abstract Command TurnRightCommand { get; }
        public abstract Command NOPCommand { get; }
        public abstract Command ForwardCommand { get; }
        public abstract Command LightUpCommand { get; }
        public abstract Command JumpCommand { get; }
        public abstract Command FunctionOneCommand { get; }
        public abstract Command FunctionTwoCommand { get; }
    }
}
