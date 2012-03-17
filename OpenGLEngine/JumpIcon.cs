using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class JumpIcon : HudIconOpenGL
    {
        public JumpIcon()
        {
            texture = TextureManager.Jump;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.JumpCommand; }
        }
        public override Core.HudIcon Clone()
        {
            var newHud = new JumpIcon();
            newHud.texture = texture.Clone();
            return newHud;
        }
    }
}
