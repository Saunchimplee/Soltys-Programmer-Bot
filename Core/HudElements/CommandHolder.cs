using System.Collections.Generic;
using System.Drawing;
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.Core.HudElements
{
    public abstract class CommandHolder : CommandView
    {
        public HudIcon DefaultEmptyIcon { get; set; }

        public CommandHolder(Hud currentHud)
            : base(currentHud)
        {

        }



        public LinkedList<Command> Commands
        {
            get
            {
                var linkedList = new LinkedList<Command>();
                foreach (var icon in IconList)
                {
                    linkedList.AddLast(icon.Command);
                }
                return linkedList;
            }
        }


        public void SetGrabbedIcon(object sender, IconReleaseEventArgs e)
        {
            for (int i = 0; i < IconList.Count; i++)
            {
                if (IconList[i].Contains(e.ReleasePoint) && !IconList[i].IsLocked)
                {
                    Point p = IconList[i].DrawPoint;
                    IconList[i] = e.ReleaseIcon.Clone();
                    IconList[i].DrawPoint = p;
                }

            }
        }

        public HudIcon GrabIcon(Point p)
        {
            for (int i = 0; i < IconList.Count; i++)
            {
                if (IconList[i].Contains(p) && IconList[i].CopyAble)
                {
                    var retVal = IconList[i].Clone();
                    IconList[i] = DefaultEmptyIcon.Clone();
                    return retVal;
                }
            }
            return null;
        }

        public void GrabIcon(object sender, IconGrabEventArgs e)
        {
            for (int i = 0; i < IconList.Count; i++)
            {
                if (IconList[i].Contains(e.GrabPoint) && IconList[i].CopyAble)
                {
                    var retVal = IconList[i].Clone();
                    IconList[i] = DefaultEmptyIcon.Clone();
                    HudHandler.GrabbedIcon = retVal;
                    HudHandler.MoveGrabbedIcon(e.GrabPoint.X, e.GrabPoint.Y);
                }
            }
        }

        public void SetAllAsEmpty()
        {
            for (int i = 0; i < IconList.Count; i++)
            {
                Point p = IconList[i].DrawPoint;
                IconList[i] = DefaultEmptyIcon.Clone(); ;
                IconList[i].DrawPoint = p;
            }
        }
    }
}
