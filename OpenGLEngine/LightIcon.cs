
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class LightIcon : HudIconOpenGL
    {
        public LightIcon()
        {
            texture = TextureManager.Light;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.LightUpCommand; }
        }

        public override Core.HudIcon Clone()
        {
            var newHud = new LightIcon();
            newHud.texture = texture.Clone();
            return newHud;
        }
    }
}
