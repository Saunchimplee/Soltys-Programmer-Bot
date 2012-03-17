using System;

namespace Soltys.ProgrammerBot.Core
{
    public class GameTime
    {
        public TimeSpan ElapsedGameTime { get; set; }

        public GameTime(TimeSpan elapsedTime)
        {
            ElapsedGameTime = elapsedTime;
        }

    }
}
