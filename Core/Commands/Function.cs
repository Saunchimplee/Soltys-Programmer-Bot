using System.Collections.Generic;

namespace Soltys.ProgrammerBot.Core.Commands
{
    abstract class Function : Command
    {
        public Function(Game currentGame)
            : base(currentGame)
        { }
        protected void ReplaceFunctionCommandWithCommands(LinkedList<Command> commands)
        {
            var replacedNode = GameHandler.MainProgram.Commands.Find(this);
            foreach (var cmd in commands)
            {
                GameHandler.MainProgram.Commands.AddBefore(replacedNode, cmd);
            }
        }
    }
}
