
using Soltys.ProgrammerBot.Core.Commands;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class FunctionOneIcon : HudIconOpenGL
    {
        public FunctionOneIcon()
        {
            texture = new Texture2D("Data/Textures/fun1.bmp");
        }
        public override Command Command
        {
            get { return RealCommandFactory.Instance.FunctionOneCommand; }
        }

        public override Core.HudIcon Clone()
        {
            var newHud = new FunctionOneIcon();
            newHud.texture = texture.Clone();
            return newHud;
        }
    }
}
