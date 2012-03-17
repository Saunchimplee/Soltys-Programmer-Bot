using System;
using System.Drawing;
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.Core
{

    abstract public class HudIcon : ICloneable
    {
        public Point DrawPoint
        {
            get { return drawPoint; }
            set { drawPoint = value; }
        }
        protected Point drawPoint;
        protected double boxSize = 50.0;
        public bool CopyAble { get; set; }
        public bool IsLocked { get; set; }

        public HudIcon()
        {
            CopyAble = true;
            IsLocked = false;
        }
        public void SetPostion(int x, int y)
        {
            drawPoint.X = x;
            drawPoint.Y = y;
        }

        public bool Contains(Point p)
        {
            var rect = new Rectangle(drawPoint, new Size((int)boxSize, (int)boxSize));
            return rect.Contains(p);
        }
        public abstract void Render(HudIcon icon);
        public abstract Command Command { get; }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public abstract HudIcon Clone();


    }
}
