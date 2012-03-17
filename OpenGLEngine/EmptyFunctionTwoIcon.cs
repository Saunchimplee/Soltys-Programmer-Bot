
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class EmptyFunctionTwoIcon : HudIconOpenGL
    {
        public EmptyFunctionTwoIcon()
        {
            texture = TextureManager.EmptyFunctionTwo;
            CopyAble = false;
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.NOPCommand; }
        }

        public override Core.HudIcon Clone()
        {
            return new EmptyFunctionTwoIcon();
        }
    }
}
