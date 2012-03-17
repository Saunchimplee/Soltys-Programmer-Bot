
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class FunctionTwoIcon : HudIconOpenGL
    {
        public FunctionTwoIcon()
        {
            texture = new Texture2D("Data/Textures/fun2.bmp");
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.FunctionTwoCommand; }
        }

        public override Core.HudIcon Clone()
        {
            var clone = new FunctionTwoIcon();
            clone.texture = texture.Clone();
            return clone;
        }
    }
}
