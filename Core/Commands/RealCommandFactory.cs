
namespace Soltys.ProgrammerBot.Core.Commands
{
    public class RealCommandFactory : CommandFactory
    {
        public static readonly CommandFactory Instance = new RealCommandFactory();
        public override Command TurnLeftCommand
        {
            get { return new TurnLeft(GameHandler); }
        }

        public override Command TurnRightCommand
        {
            get { return new TurnRight(GameHandler); }
        }

        public override Command NOPCommand
        {
            get { return new NOP(GameHandler); }
        }

        public override Command ForwardCommand
        {
            get { return new ForwardMove(GameHandler); }
        }

        public override Command LightUpCommand
        {
            get { return new LightUp(GameHandler); }
        }

        public override Command JumpCommand
        {
            get { return new Jump(GameHandler); }
        }

        public override Command FunctionOneCommand
        {
            get { return new FunctionOne(GameHandler); }
        }

        public override Command FunctionTwoCommand
        {
            get { return new FunctionTwo(GameHandler); }
        }
    }
}
