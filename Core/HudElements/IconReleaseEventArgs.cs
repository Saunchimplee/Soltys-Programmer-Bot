using System;
using System.Drawing;
namespace Soltys.ProgrammerBot.Core
{
    public class IconReleaseEventArgs : EventArgs
    {
        public Point ReleasePoint { get; set; }
        public HudIcon ReleaseIcon { get; set; }
        public IconReleaseEventArgs(Point releasePoint, HudIcon releaseIcon)
            : base()
        {
            ReleasePoint = releasePoint;
            ReleaseIcon = releaseIcon;
        }
    }
}
