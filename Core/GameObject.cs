
namespace Soltys.ProgrammerBot.Core
{
    public abstract class GameObject
    {
        protected Game GameHandler;
        
        protected GameObject(Game handler)
        {
            GameHandler = handler;
        }

    }
}
