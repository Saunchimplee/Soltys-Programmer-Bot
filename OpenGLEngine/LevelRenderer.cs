using System;
using OpenTK.Graphics.OpenGL;
using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.Interfaces;
using Soltys.ProgrammerBot.Core.Level;
using Tao.FreeGlut;
namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public class LevelRenderer : ILevelRenderer
    {

        double currentAngle;
        TimeSpan rotateTime = TimeSpan.FromMilliseconds(500);

        public static double GetTileHeightOpenGLUnits(TileHeight height)
        {
            switch (height)
            {
                case TileHeight.Ground:
                    return -0.25;
                case TileHeight.BoxOne:
                    return 0.5;
                case TileHeight.BoxTwo:
                    return 1.2;
                case TileHeight.BoxThree:
                    break;
                default:
                    return 0.0;
            }
            return 0.0;
        }
        public static double TileSize { get { return 1.0; } }
        int floorSize;
        public LevelRenderer()
        {

        }

        public void UpdateFloorRotation(int angle, GameTime gameTime)
        {
            double timeFactor = gameTime.ElapsedGameTime.TotalMilliseconds / rotateTime.TotalMilliseconds;
            if (timeFactor >= 1)
            {
                timeFactor = 1;
            }
            currentAngle = calculateAngle(timeFactor, currentAngle, (double)angle);
        }

        private double calculateAngle(double timeFactor, double from, double to)
        {
            double delta = Math.Abs(from - to);
            from += delta * timeFactor * Math.Sign(to - from);
            return from;
        }


        public void Render(Level level)
        {
            floorSize = level.FloorSize;
            GL.Translate(Tile.TILE_SIZE * floorSize / 2, 0, -Tile.TILE_SIZE * floorSize / 2);
            GL.Rotate(currentAngle, 0, 1, 0);
            GL.Translate(-Tile.TILE_SIZE * floorSize / 2, 0, Tile.TILE_SIZE * floorSize / 2);
            drawBase();
            drawTiles(level);
        }

        private void drawBase()
        {
            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);

            GL.PushMatrix();
            GL.Translate(3.5f, -1.0f, -3.5f);
            GL.Scale(new OpenTK.Vector3(floorSize * Tile.TILE_SIZE, 1, floorSize * Tile.TILE_SIZE));

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });

            //GL.Color3(0.2f, 0.2f, 0.2f);
            Glut.glutSolidCube(0.99999999);
            Glut.glutWireCube(1);
            GL.PopMatrix();
        }
        private void drawTiles(Level level)
        {
            for (int row = 0; row < floorSize; row++)
            {
                for (int column = 0; column < floorSize; column++)
                {
                    GL.PushMatrix();
                    GL.Translate(TileSize * row, -0.5f, -TileSize * column);
                    renderTile(level.Floor[row, column]);
                    GL.PopMatrix();
                }
            }
        }

        private void renderTile(Tile tile)
        {

            GL.Material(MaterialFace.Front, MaterialParameter.AmbientAndDiffuse, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.PushMatrix();
            {
                //GL.Translate(TileSize * row, -0.5, -TileSize * column);
                GL.Enable(EnableCap.ColorMaterial);
                GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.Ambient);

                drawBox(tile);
                getTileDrawColor(tile);
                GL.Disable(EnableCap.ColorMaterial);
                GL.Scale(TileSize - 0.05f, 0.25f, TileSize - 0.05f);
                Glut.glutSolidCube(TileSize);
            }
            GL.PopMatrix();
        }

        private void drawBox(Tile tile)
        {
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.Front, MaterialParameter.AmbientAndDiffuse, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            switch (tile.Height)
            {
                case TileHeight.Ground:
                    break;
                case TileHeight.BoxOne:
                    GL.Scale(1, 1.5, 1);
                    Glut.glutSolidCube(1.0);
                    GL.Translate(0, 0.5, 0);
                    break;
                case TileHeight.BoxTwo:
                    GL.Translate(0, 0.5, 0);
                    GL.Scale(1, 1.8, 1);
                    Glut.glutSolidCube(1.0);
                    GL.Translate(0, 0.5, 0);
                    break;
                case TileHeight.BoxThree:
                    break;
                default:

                    break;
            }
        }

        private void getTileDrawColor(Tile tile)
        {
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            switch (tile.Type)
            {
                case TileType.NoLight:
                    GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                    GL.Color3(0.5f, 0.5f, 0.5);
                    break;

                case TileType.LightOn:
                    GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 1.0f, 1.0f, 0.0f, 1.0f });
                    GL.Color3(1f, 1f, 0);
                    break;

                case TileType.LightOff:
                    //        GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.5f, 0.5f, 0.5f, 1.0f });
                    GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 0.1f, 0.1f, 0.1f, 1.0f });
                    GL.Material(MaterialFace.Front, MaterialParameter.Shininess, new float[] { 20.0f });
                    GL.Color3(1.0f, 1.0f, 1.0f);
                    break;

                case TileType.StartPosition:
                    GL.Color3(0, 1f, 0);
                    break;
            }

        }
    }
}
