
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class TurnRightIcon : HudIconOpenGL
    {
        public TurnRightIcon()
        {
            texture = TextureManager.TurnRight;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.TurnRightCommand; }
        }

        public override Core.HudIcon Clone()
        {
            var newHud = new TurnRightIcon();
            newHud.texture = texture.Clone();
            return newHud;
        }
    }
}
