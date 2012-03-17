
using Soltys.ProgrammerBot.Core.Robot;

namespace Soltys.ProgrammerBot.Core.Interfaces
{
    public interface IRobotRenderer : IRenderer<Robot.Robot>
    {
        void Update(RobotPosition position, GameTime gameTime);
    }
}
