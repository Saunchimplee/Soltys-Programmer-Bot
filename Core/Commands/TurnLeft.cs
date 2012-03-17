namespace Soltys.ProgrammerBot.Core.Commands
{
    public class TurnLeft : Command
    {
        public TurnLeft(Game currentGame)
            : base(currentGame)
        {

        }
        public override void Excecute()
        {
            base.GameHandler.Robot.TurnLeft();
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
