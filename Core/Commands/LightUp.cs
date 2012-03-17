
namespace Soltys.ProgrammerBot.Core.Commands
{
    public class LightUp : Command
    {
        public LightUp(Game currentGame)
            : base(currentGame)
        {

        }
        public override void Excecute()
        {
            base.GameHandler.Level.ToggleLight();
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
