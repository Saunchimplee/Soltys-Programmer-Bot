using System;
using Soltys.ProgrammerBot.Core.Interfaces;
using Soltys.ProgrammerBot.Core.Level;

namespace Soltys.ProgrammerBot.Core.Robot
{
    public class Robot : GameObject
    {
        IRobotRenderer renderer;
        public Robot(Game g, IRobotRenderer r)
            : base(g)
        {
            renderer = r;
        }
        public void Render()
        {
            renderer.Render(this);
        }

        public void OnLogicUpdate(GameTime e)
        {
            renderer.Update(GameHandler.Position, new GameTime(e.ElapsedGameTime));
            GameHandler.Position.Height = GameHandler.Level.Floor[GameHandler.Position.Row, GameHandler.Position.Column].Height;
        }

        public void Jump()
        {
            Tile tile = GameHandler.Level.GetTileInFrontOfRobot();
            TileHeight currentRobotHeight = GameHandler.Position.Height;
            if (Math.Abs(tile.Height - GameHandler.Position.Height) == 1)
            {
                currentRobotHeight += 1 * Math.Sign(tile.Height - currentRobotHeight);
                MoveForward();
            }
            GameHandler.Position.Height = currentRobotHeight;
        }

        public void MoveForward()
        {
            switch (GameHandler.Position.Direction)
            {
                case Turn.North:
                    GameHandler.Position.Row++;
                    break;
                case Turn.East:
                    GameHandler.Position.Column--;
                    break;
                case Turn.South:
                    GameHandler.Position.Row--;
                    break;
                case Turn.West:
                    GameHandler.Position.Column++;
                    break;
            }

        }

        public void MoveBackward()
        {
            switch (GameHandler.Position.Direction)
            {
                case Turn.North:
                    GameHandler.Position.Row--;
                    break;
                case Turn.East:
                    GameHandler.Position.Column++;
                    break;
                case Turn.South:
                    GameHandler.Position.Row++;
                    break;
                case Turn.West:
                    GameHandler.Position.Column--;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (GameHandler.Position.Direction)
            {
                case Turn.North:
                    GameHandler.Position.Direction = Turn.East;
                    break;
                case Turn.East:
                    GameHandler.Position.Direction = Turn.South;
                    break;
                case Turn.South:
                    GameHandler.Position.Direction = Turn.West;
                    break;
                case Turn.West:
                    GameHandler.Position.Direction = Turn.North;
                    break;
            }
        }

        public void TurnLeft()
        {
            switch (GameHandler.Position.Direction)
            {
                case Turn.North:
                    GameHandler.Position.Direction = Turn.West;
                    break;
                case Turn.West:
                    GameHandler.Position.Direction = Turn.South;
                    break;
                case Turn.South:
                    GameHandler.Position.Direction = Turn.East;
                    break;
                case Turn.East:
                    GameHandler.Position.Direction = Turn.North;
                    break;
            }
        }

    }

}


