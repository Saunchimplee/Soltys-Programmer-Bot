namespace Soltys.ProgrammerBot.Core.Commands
{
    class NOP : Command
    {
        public NOP(Game currentGame)
            : base(currentGame)
        {

        }
        public override void Excecute()
        {
        }

        public override bool Check()
        {
            return true;
        }

        public override bool IsNOP()
        {
            return true;
        }
    }
}
