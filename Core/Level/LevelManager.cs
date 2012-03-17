using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Soltys.ProgrammerBot.Core.Robot;

namespace Soltys.ProgrammerBot.Core.Level
{
    public class LevelManager
    {
        Game gameHandler;

        public Tile[,] CurrentLevel { get { return Load(CurrentLevelName); } }
        string currentLevelName;
        public string CurrentLevelName { get { return currentLevelName; } }

        Queue<string> levelNames = new Queue<string>(); 

        public LevelManager(Game currentGame)
        {
            gameHandler = currentGame;
            levelNames.Enqueue("one");
            levelNames.Enqueue("jump_one");
            levelNames.Enqueue("rekurencja");
            levelNames.Enqueue("two");
            levelNames.Enqueue("high");

            currentLevelName = levelNames.Dequeue();
        }
        const string levelFileExtension = ".lspb";


        public void Save(string outputPath, Tile[,] floor, int floorSize)
        {
            outputPath = outputPath + levelFileExtension;
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("{0}x{1}", floorSize, floorSize);
                writer.WriteLine((int)gameHandler.Position.Direction);
                for (int row = 0; row < floorSize; row++)
                {
                    for (int column = 0; column < floorSize; column++)
                    {
                        writer.WriteLine("{0};{1};{2};{3}", row, column, (int)floor[row, column].Type, (int)floor[row, column].Height);
                    }
                }
            }
        }

        public Tile[,] Load(string inputPath)
        {

            inputPath = inputPath + levelFileExtension;
            Tile[,] floor = null;
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string levelHeader = reader.ReadLine();

                int floorSize = parseLevelHeader(levelHeader);
                gameHandler.Level.StartDirection = (Turn)int.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                floor = new Tile[floorSize, floorSize];

                for (int row = 0; row < floorSize; row++)
                {
                    for (int column = 0; column < floorSize; column++)
                    {
                        Tile parsedTile = parseTileLine(reader, row, column);
                        floor[row, column] = parsedTile;
                    }
                }
            }

            return floor;
        }

        private int parseLevelHeader(string levelHeader)
        {
            string[] tokens = levelHeader.Split('x');
            if (tokens.Length != 2)
            {
                throw new LevelFileException("Wrong amount of tokens for header. Expected 2, was: " + tokens.Length.ToString(CultureInfo.InvariantCulture));
            }
            int rowSize;
            int columnSize;
            try
            {
                rowSize = int.Parse(tokens[0], CultureInfo.InvariantCulture);
                columnSize = int.Parse(tokens[1], CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                throw new LevelFileException("Error while parsing rowSize and coulumnSize", e);
            }

            if (rowSize != columnSize)
            {
                throw new LevelFileException("rowSize are not equal to columnSize");
            }
            return rowSize;
        }
        private Tile parseTileLine(StreamReader reader, int row, int column)
        {
            string floorTile = reader.ReadLine();

            string[] tokens = splitTileInfoLine(row, column, floorTile);

            int tileRow;
            int tileColumn;
            TileType tileType;
            TileHeight tileHeight;
            parseTileLine(tokens, out tileRow, out tileColumn, out tileType, out tileHeight);

            Tile newTile = new Tile();
            newTile.Type = tileType;
            newTile.Height = tileHeight;
            return newTile;
        }
        private string[] splitTileInfoLine(int row, int column, string floorTile)
        {
            string[] tokens = floorTile.Split(';');
            if (tokens.Length != 4)
            {
                throw new LevelFileException(
                    String.Format(CultureInfo.InvariantCulture, "Tokens not equal 4 for tile Row:{0} Column:{1}", row, column, CultureInfo.InvariantCulture));
            }
            return tokens;
        }
        private void parseTileLine(string[] tokens, out int tileRow, out int tileColumn, out TileType tileType, out TileHeight tileHeight)
        {
            try
            {
                tileRow = int.Parse(tokens[0], CultureInfo.InvariantCulture);
                tileColumn = int.Parse(tokens[1], CultureInfo.InvariantCulture);
                tileType = (TileType)int.Parse(tokens[2], CultureInfo.InvariantCulture);
                tileHeight = (TileHeight)int.Parse(tokens[3], CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new LevelFileException("Error while parsing tile settings");
            }
        }

        public void GetNextLevelName()
        {
            if (levelNames.Count > 0)
            {
                currentLevelName = levelNames.Dequeue();
            }
        }
    }
}
