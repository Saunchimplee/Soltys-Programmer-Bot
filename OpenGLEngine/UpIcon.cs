using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class UpIcon : HudIconOpenGL
    {
        public UpIcon()
        {
            texture = TextureManager.Up;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.ForwardCommand; }
        }
        public override Core.HudIcon Clone()
        {
            var newHud = new UpIcon();
            newHud.texture = texture.Clone();
            return newHud;
        }
    }
}
