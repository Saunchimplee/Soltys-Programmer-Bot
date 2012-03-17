using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.HudElements;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public class OpenGLRendererFactory : IRendererFactory
    {
        public IRobotRenderer RobotRenderer
        {
            get
            {

                return new RobotRenderer();
            }
        }


        public ILevelRenderer LevelRenderer
        {
            get
            {
                return new LevelRenderer();
            }
        }

        public IHudRenderer HudRenderer
        {
            get
            {
                return new HudRenderer();
            }
        }

        public IHudIconFactory HudIconFactory
        {
            get
            {
                return new HudIconFactory();
            }
        }

        public IRenderer<CommandBar> CommandBarRenderer
        {
            get
            {
                return new CommandBarRenderer();
            }
        }


        public IRenderer<CommandMainProgram> CommandMainProgramRenderer
        {
            get
            {
                return new CommandMainProgramRenderer();
            }
        }



        IRenderer<CommandFunction> IRendererFactory.FunctionOneRenderer
        {
            get { return new FunctionOneRenderer(); }
        }

        IRenderer<CommandFunction> IRendererFactory.FunctionTwoRenderer
        {
            get { return new FunctionTwoRenderer(); }
        }

        public IRenderer<Game> GameRenderer
        {
            get { return new GameRenderer(); }
        }
    }
}
