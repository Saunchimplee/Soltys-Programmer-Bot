
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class EmptyFunctionOneIcon : HudIconOpenGL
    {
        public EmptyFunctionOneIcon()
        {
            texture = TextureManager.EmptyFunctionOne;
            CopyAble = false;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.NOPCommand; }
        }

        public override Core.HudIcon Clone()
        {
            return new EmptyFunctionOneIcon();
        }
    }
}
