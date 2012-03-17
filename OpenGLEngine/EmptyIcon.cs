using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class EmptyIcon : HudIconOpenGL
    {
        public EmptyIcon()
        {
            texture = TextureManager.Empty;
            CopyAble = false;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.NOPCommand; }
        }
        public override Core.HudIcon Clone()
        {
            var newHud = new EmptyIcon();
            newHud.texture = texture.Clone();
            return newHud;
        }
    }
}
