using System.Drawing;

namespace Soltys.ProgrammerBot.Core.HudElements
{
    public class IconGrabEventArgs
    {
        public Point GrabPoint { get; set; }
        public IconGrabEventArgs(Point grabPoint)
        {
            GrabPoint = grabPoint;
        }
    }
}
