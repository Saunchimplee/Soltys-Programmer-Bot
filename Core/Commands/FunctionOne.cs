
namespace Soltys.ProgrammerBot.Core.Commands
{
    class FunctionOne : Function
    {
        public FunctionOne(Game currentGame)
            : base(currentGame)
        {

        }
        public override void Excecute()
        {
            ReplaceFunctionCommandWithCommands(GameHandler.FunctionOneCommands.Commands);
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
