using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class HudIconFactory : IHudIconFactory
    {
        public HudIcon UpIcon
        {
            get { return new UpIcon(); }
        }

        public HudIcon TurnLeftIcon
        {
            get { return new TurnLeftIcon(); }
        }

        public HudIcon TurnRightIcon
        {
            get { return new TurnRightIcon(); }
        }

        public HudIcon JumpIcon
        {
            get { return new JumpIcon(); }
        }

        public HudIcon LightIcon
        {
            get { return new LightIcon(); }
        }

        public HudIcon EmptyIcon
        {
            get { return new EmptyIcon(); }
        }
        public HudIcon LockIcon
        {
            get { return new LockIcon(); }
        }

        public HudIcon FunctionOneIcon
        {
            get { return new FunctionOneIcon(); }
        }

        public HudIcon FunctionTwoIcon
        {
            get { return new FunctionTwoIcon(); }
        }


        public HudIcon EmptyFunctionOneIcon
        {
            get { return new EmptyFunctionOneIcon(); }
        }

        public HudIcon EmptyFunctionTwoIcon
        {
            get { return new EmptyFunctionTwoIcon(); }
        }
    }
}
