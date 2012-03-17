
namespace Soltys.ProgrammerBot.Core.Commands
{
    class Jump : Command
    {
        public Jump(Game currentGame)
            : base(currentGame)
        {

        }

        public override void Excecute()
        {
            base.GameHandler.Robot.Jump();
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
