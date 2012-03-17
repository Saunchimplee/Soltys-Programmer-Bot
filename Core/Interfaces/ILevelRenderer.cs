
namespace Soltys.ProgrammerBot.Core.Interfaces
{
    public interface ILevelRenderer : IRenderer<Level.Level>
    {
        void UpdateFloorRotation(int angle, GameTime gameTime);   
    }
}
