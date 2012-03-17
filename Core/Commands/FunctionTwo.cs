
namespace Soltys.ProgrammerBot.Core.Commands
{
    class FunctionTwo : Function
    {
        public FunctionTwo(Game currentGame)
            : base(currentGame)
        {

        }
        public override void Excecute()
        {
            ReplaceFunctionCommandWithCommands(GameHandler.FunctionTwoCommands.Commands);
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
