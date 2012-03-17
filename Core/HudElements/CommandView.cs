using System.Collections.Generic;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.Core.HudElements
{
    public abstract class CommandView : HudObject
    {
        protected List<HudIcon> IconList = new List<HudIcon>();
        protected IHudIconFactory IconFactory;
        public IEnumerable<HudIcon> Icons
        {
            get { return IconList; }
        }

        protected CommandView(Hud currentHud) : base(currentHud) { }


        
    }
}
