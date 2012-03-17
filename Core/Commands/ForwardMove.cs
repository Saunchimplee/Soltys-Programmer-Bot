namespace Soltys.ProgrammerBot.Core.Commands
{
    class ForwardMove : Command
    {
        public ForwardMove(Game currentGame)
            : base(currentGame)
        {

        }
        public override void Excecute()
        {
            GameHandler.Robot.MoveForward();
        }
        public override bool Check()
        {
            return GameHandler.Level.IsForwardMovePossible();
        }

        public override bool IsNOP()
        {
            return false;
        }
    }
}
