
namespace Soltys.ProgrammerBot.Core.Level
{
    public enum TileHeight { Ground, BoxOne, BoxTwo, BoxThree };
    public enum TileType { NoLight, LightOff, LightOn, StartPosition };
    public class Tile
    {
        public TileType Type { get; set; }
        public TileHeight Height { get; set; }
        public const float TILE_SIZE = 1.0f;
    }
}
