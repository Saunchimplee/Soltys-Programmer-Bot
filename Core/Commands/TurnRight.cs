namespace Soltys.ProgrammerBot.Core.Commands
{
    public class TurnRight : Command
    {
        public TurnRight(Game currentGame)
            : base(currentGame)
        {

        }
        public override void Excecute()
        {
            base.GameHandler.Robot.TurnRight();
        }

        public override bool Check()
        {
            return true;
        }

        public override bool IsNOP()
        {
            return false;
        }
    }
}
