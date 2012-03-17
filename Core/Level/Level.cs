using Soltys.ProgrammerBot.Core.Interfaces;
using Soltys.ProgrammerBot.Core.Robot;

namespace Soltys.ProgrammerBot.Core.Level
{
    public enum LevelRotation
    {
        Normal = 0,
        Clockwise = -60,
        CounterClockwise = 60,
    }

    public class Level : GameObject
    {

        public LevelRotation DestAngle { get; set; }

        ILevelRenderer renderer;
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public Turn StartDirection { get; set; }
        public int FloorSize { get; set; }
        public Tile[,] Floor { get; set; }

        public LevelManager Manager { get; set; }
        public Level(Game currentGame, ILevelRenderer renderer)
            : base(currentGame)
        {
            GameHandler = currentGame;
            this.renderer = renderer;
            Manager = new LevelManager(currentGame);
            FloorSize = 8;
            Floor = new Tile[FloorSize, FloorSize];
            for (int i = 0; i < FloorSize; i++)
            {
                for (int j = 0; j < FloorSize; j++)
                {
                    Floor[i, j] = new Tile();
                }
            }

        }

        public void Render()
        {
            renderer.Render(this);
        }

        public void OnLogicUpdate(GameTime gameTime)
        {
            renderer.UpdateFloorRotation((int)DestAngle, gameTime);
        }

        public bool IsRobotInBounds()
        {
            if (GameHandler.Position.Row < 0 || GameHandler.Position.Column < 0)
            {
                return false;
            }
            else if (GameHandler.Position.Row >= FloorSize || GameHandler.Position.Column >= FloorSize)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsLevelCompleted
        {
            get
            {
                foreach (var tile in Floor)
                {
                    if (tile.Type == TileType.LightOff)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public void SetTileType(TileType type)
        {
            Floor[GameHandler.Position.Row, GameHandler.Position.Column].Type = type;
        }
        public void ToggleLight()
        {
            int row = GameHandler.Position.Row;
            int column = GameHandler.Position.Column;
            if (Floor[row, column].Type == TileType.LightOff)
            {
                Floor[row, column].Type = TileType.LightOn;
            }
            else if (Floor[row, column].Type == TileType.LightOn)
            {
                Floor[row, column].Type = TileType.LightOff;
            }
        }
        public void SetTitleHeight(TileHeight height)
        {
            Floor[GameHandler.Position.Row, GameHandler.Position.Column].Height = height;
        }
        public Tile GetTileInFrontOfRobot()
        {
            int row = GameHandler.Position.Row;
            int column = GameHandler.Position.Column;

            switch (GameHandler.Position.Direction)
            {
                case Turn.North:
                    row++;
                    break;
                case Turn.East:
                    column--;
                    break;
                case Turn.South:
                    row--;
                    break;
                case Turn.West:
                    column++;
                    break;
            }


            if (row < 0 || column < 0)
            {
                return null;
            }
            else if (row >= FloorSize || column >= FloorSize)
            {
                return null;
            }

            return Floor[row, column];
        }
        public bool IsForwardMovePossible()
        {
            Tile tile = GetTileInFrontOfRobot();
            if (tile == null)
            {
                return false;
            }
            if (GameHandler.Position.Height != tile.Height)
            {
                return false;
            }

            return true;
        }
        public void TurnOffLight()
        {
            for (int column = 0; column < FloorSize; column++)
            {
                for (int row = 0; row < FloorSize; row++)
                {
                    if (Floor[row, column].Type == TileType.LightOn)
                    {
                        Floor[row, column].Type = TileType.LightOff;
                    }
                }
            }
        }
        public void SaveLevel(string outputPath)
        {
            Manager.Save(outputPath, Floor, FloorSize);
        }
        public void LoadNextLevel()
        {
            Manager.GetNextLevelName();
            LoadLevel();
        }
        public void LoadLevel()
        {
            LoadLevel(Manager.CurrentLevelName);
        }
        public void LoadEmptyLevel()
        {
            Floor = Manager.Load("empty");

            StartRow = 0;
            StartColumn = 0;

            GameHandler.Position.Row = StartRow;
            GameHandler.Position.Column = StartColumn;
            GameHandler.Position.Direction = Turn.North;
        }
        public void LoadLevel(string inputPath)
        {
            Floor = Manager.Load(inputPath);

            int startRow = 0, startColumn = 0;
            int startPositionCount = 0;
            for (int column = 0; column < FloorSize; column++)
            {
                for (int row = 0; row < FloorSize; row++)
                {
                    if (Floor[row, column].Type == TileType.StartPosition)
                    {
                        startPositionCount++;
                        startRow = row;
                        startColumn = column;
                    }
                }
            }

            if (startPositionCount != 1)
            {
                throw new LevelFileException();
            }

            StartRow = startRow;
            StartColumn = startColumn;

            GameHandler.Position.Row = StartRow;
            GameHandler.Position.Column = StartColumn;
            GameHandler.Position.Direction = StartDirection;
        }

    }

}
