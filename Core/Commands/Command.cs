
namespace Soltys.ProgrammerBot.Core.Commands
{
    abstract public class Command
    {
        protected Game GameHandler;
        protected Command(Game currentGame)
        {
            GameHandler = currentGame;
        }
        public abstract void Excecute();
        public abstract bool Check();
        public abstract bool IsNOP();

    }
}
