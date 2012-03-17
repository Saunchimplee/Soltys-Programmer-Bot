using System.Collections.Generic;
using Soltys.ProgrammerBot.Core.Commands;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.Core.HudElements
{
    public class Hud : GameObject
    {
        public CommandBar CommandBar { get; set; }
        public CommandMainProgram MainProgram { get; set; }
        public CommandFunction FunctionOne { get; set; }
        public CommandFunction FunctionTwo { get; set; }
        IHudRenderer renderer;
        IHudIconFactory iconFactory;
        public int HudWidth { get { return renderer.HudWidth; } set { renderer.HudWidth = value; } }
        public int HudHeight { get { return renderer.HudHeight; } set { renderer.HudHeight = value; } }
        public HudMessage Message { get; set; }
        public HudIcon GrabbedIcon { get; set; }

        public delegate void IconReleasedHandler(object sender, IconReleaseEventArgs e);
        private event IconReleasedHandler IconRelease;

        public delegate void IconGrabHandler(object sender, IconGrabEventArgs e);
        public event IconGrabHandler IconGrab;

        public Hud(Game currentGame, IHudRenderer renderer,
            IRenderer<CommandBar> commandBarRenderer, IRenderer<CommandMainProgram> mainProgramRenderer, IRenderer<CommandFunction> functionOneRenderer, IRenderer<CommandFunction> functionTwoRenderer,
            IHudIconFactory iconFactory)
            : base(currentGame)
        {
            this.renderer = renderer;
            this.iconFactory = iconFactory;
           // HudHeight = 786;
           // HudWidth = 1024;
            CommandBar = new CommandBar(this, commandBarRenderer, iconFactory);
            MainProgram = new CommandMainProgram(this, mainProgramRenderer, iconFactory);
            FunctionOne = new CommandFunction(this, functionOneRenderer, iconFactory);
            FunctionOne.DefaultEmptyIcon = iconFactory.EmptyFunctionOneIcon;
            FunctionOne.AddEmptyIcons(iconFactory);
            FunctionTwo = new CommandFunction(this, functionTwoRenderer, iconFactory);
            FunctionTwo.DefaultEmptyIcon = iconFactory.EmptyFunctionTwoIcon;
            FunctionTwo.AddEmptyIcons(iconFactory);
            Message = new HudMessage();

            IconRelease += MainProgram.SetGrabbedIcon;
            IconRelease += FunctionOne.SetGrabbedIcon;
            IconRelease += FunctionTwo.SetGrabbedIcon;

            IconGrab += CommandBar.GrabIcon;
            IconGrab += MainProgram.GrabIcon;
            IconGrab += FunctionOne.GrabIcon;
            IconGrab += FunctionTwo.GrabIcon;

            Message.FirstLine = "";
            Message.SecondLine = "";
        }

        const double boxSize = 50.0;
        public void Render()
        {
            renderer.Render(this);
        }
        public void ClearCommands()
        {
            MainProgram.SetAllAsEmpty();
            FunctionOne.SetAllAsEmpty();
            FunctionTwo.SetAllAsEmpty();
        }

        public void OnIconGrab(IconGrabEventArgs e)
        {
            IconGrab(this, e);
        }

        public void OnIconRelease(IconReleaseEventArgs e)
        {
            IconRelease(this, e);
            GrabbedIcon = null;
        }

        public void MoveGrabbedIcon(int newX, int newY)
        {
            if (GrabbedIcon != null)
            {
                GrabbedIcon.SetPostion(newX, newY);
            }
        }

        public LinkedList<Command> Commands
        {
            get
            {
                return MainProgram.Commands;
            }
        }
        public LinkedList<Command> FunctionOneCommands
        {
            get
            {
                return FunctionOne.Commands;
            }
        }

        public LinkedList<Command> FunctionTwoCommands
        {
            get
            {
                return FunctionTwo.Commands;
            }
        }
    }
}
