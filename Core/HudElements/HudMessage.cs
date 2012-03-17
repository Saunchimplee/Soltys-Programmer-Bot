
namespace Soltys.ProgrammerBot.Core.HudElements
{
    public class HudMessage
    {
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }

        public void ClearMessages()
        {
            FirstLine = "";
            SecondLine = "";
        }
    }
}
