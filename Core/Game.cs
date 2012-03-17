using System;
using Soltys.ProgrammerBot.Core.Commands;
using Soltys.ProgrammerBot.Core.HudElements;
using Soltys.ProgrammerBot.Core.Interfaces;
using Soltys.ProgrammerBot.Core.Robot;

namespace Soltys.ProgrammerBot.Core
{
    public enum GameModes
    {
        Create,
        Play,
        Save,
        Load,
    }
    public class Game
    {
        readonly IRendererFactory renderFactory;
        #region Game Objects
        public Level.Level Level { get; set; }
        public Robot.Robot Robot { get; set; }
        public Hud HUD { get; set; }
        public CommandList MainProgram { get; set; }
        public CommandList FunctionOneCommands { get; set; }
        public CommandList FunctionTwoCommands { get; set; }

        public RobotPosition Position { get; set; }
        public GameModes GameMode { get; set; }

        #endregion
        public Game(IRendererFactory renderFactory)
        {
            GameMode = GameModes.Play;
            this.renderFactory = renderFactory;
            Level = new Level.Level(this, renderFactory.LevelRenderer);
            Robot = new Robot.Robot(this, renderFactory.RobotRenderer);
            HUD = new Hud(this, renderFactory.HudRenderer,
                renderFactory.CommandBarRenderer, renderFactory.CommandMainProgramRenderer, renderFactory.FunctionOneRenderer, renderFactory.FunctionTwoRenderer,
                renderFactory.HudIconFactory);
            MainProgram = new CommandList(this);
            FunctionOneCommands = new CommandList(this);
            FunctionTwoCommands = new CommandList(this);
            CommandFactory.GameHandler = this;
            Position = new RobotPosition(this);
        }


        public delegate void LogicUpdateEventHandler(GameTime gameTime);
        public event LogicUpdateEventHandler LogicUpdate;

        public void OnLogicUpdate(GameTime gameTime)
        {
            if (GameMode == GameModes.Play)
            {
                HUD.Message.FirstLine = "Nazwa poziomu: " + Level.Manager.CurrentLevelName;
                if (Level.IsLevelCompleted)
                {
                    System.Threading.Thread.Sleep(200);
                    MainProgram.Clear();
                    FunctionOneCommands.Clear();
                    HUD.ClearCommands();

                    Level.LoadNextLevel();
                }
            }
        }
        public void UpdateLogic(GameTime gameTime)
        {

            if (LogicUpdate != null)
            {
                LogicUpdate(gameTime);
            }
            OnLogicUpdate(gameTime);
        }

        public void OnLoad(object sender, EventArgs e)
        {
            Level.LoadLevel();
            LogicUpdate += Robot.OnLogicUpdate;
            LogicUpdate += Level.OnLogicUpdate;
        }
        public void RunProgram()
        {
            Level.TurnOffLight();

            Position.ResetRobotPosition();
            MainProgram.Clear();
            MainProgram.AddCommands(HUD.Commands);
            FunctionTwoCommands.AddCommands(HUD.FunctionTwoCommands);
            FunctionOneCommands.AddCommands(HUD.FunctionOneCommands);
            MainProgram.Exectute();
        }


        public void Render()
        {
            renderFactory.GameRenderer.Render(this);
        }
    }
}
