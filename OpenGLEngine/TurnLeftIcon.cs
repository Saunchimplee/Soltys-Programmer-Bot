using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class TurnLeftIcon : HudIconOpenGL
    {
        public TurnLeftIcon()
        {
            texture = TextureManager.TurnLeft;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.TurnLeftCommand; }
        }
        public override Core.HudIcon Clone()
        {
            var newHud = new TurnLeftIcon();
            newHud.texture = texture.Clone();
            return newHud;
        }
    }
}
