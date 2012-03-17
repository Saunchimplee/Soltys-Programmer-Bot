using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class LockIcon : HudIconOpenGL
    {
        public LockIcon()
        {
            texture = TextureManager.Lock;
            CopyAble = false;
            IsLocked = true;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.NOPCommand; }
        }

        public override HudIcon Clone()
        {
            var newHud = new LockIcon {texture = texture.Clone()};
            return newHud;
        }
    }
}
