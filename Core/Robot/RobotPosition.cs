
using Soltys.ProgrammerBot.Core.Level;

namespace Soltys.ProgrammerBot.Core.Robot
{
    public enum Turn { North = 0, East = 270, South = 180, West = 90 };

    public class RobotPosition : GameObject
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Turn Direction { get; set; }
        public TileType TileType { get; set; }
        public TileHeight Height { get; set; }

        public RobotPosition(Game currentGame)
            : base(currentGame)
        {
            Row = 0;
            Column = 0;
            Direction = Turn.North;
            Height = TileHeight.Ground;
        }

        public void ResetRobotPosition()
        {
            Row = GameHandler.Level.StartRow;
            Column = GameHandler.Level.StartColumn;
            Direction = GameHandler.Level.StartDirection;
        }
    }



}
