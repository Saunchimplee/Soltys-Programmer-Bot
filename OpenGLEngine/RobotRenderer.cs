using System;

using OpenTK.Graphics.OpenGL;
using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.Interfaces;
using Soltys.ProgrammerBot.Core.Robot;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class RobotRenderer : IRobotRenderer
    {
        double xPosition, yPosition, zPosition;
        double rotation;
        readonly ObjMesh robotModel;
        TimeSpan moveTime = TimeSpan.FromMilliseconds(300);

        public RobotRenderer()
        {
            robotModel = ObjMeshLoader.Load("Data/Models/dragon.obj");
            xPosition = 0;
            yPosition = -0.25;
            zPosition = 0;
        }
        public void Render(Robot robot)
        {
            GL.Disable(EnableCap.ColorMaterial);

            const double DRAGON_SIZE = 1.5;
            GL.PushMatrix();
            {
                GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
                GL.Translate(xPosition, yPosition, -zPosition);
                GL.Rotate(-110, 0, 1.0, 0);
                GL.Scale(DRAGON_SIZE, DRAGON_SIZE, DRAGON_SIZE);
                GL.Rotate(rotation, 0, 1.0, 0);
                setMaterials();
                robotModel.Render();
            }
            GL.PopMatrix();
        }
        private void setMaterials()
        {
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, new float[] { 0.2f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, new float[] { 0.4f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, new float[] { 0.5f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, new float[] { 0.1f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, new float[] { 20.0f });
        }
        public void Update(RobotPosition position, GameTime gameTime)
        {
            int row = position.Row;
            int column = position.Column;
            double heightPosition = LevelRenderer.GetTileHeightOpenGLUnits(position.Height);
            double timeFactor = calculateTimeFactor(gameTime);
            xPosition = calculatePosition(timeFactor, xPosition, row * LevelRenderer.TileSize);
            yPosition = calculatePosition(timeFactor, yPosition, heightPosition);
            zPosition = calculatePosition(timeFactor, zPosition, column * LevelRenderer.TileSize);
            rotation = calculatePosition(timeFactor, rotation, (int)position.Direction);
        }

        private double calculateTimeFactor(GameTime gameTime)
        {
            double timeFactor = gameTime.ElapsedGameTime.TotalMilliseconds / moveTime.TotalMilliseconds;
            if (timeFactor >= 1)
            {
                timeFactor = 1;
            }
            return timeFactor;
        }

        private double calculatePosition(double timeFactor, double from, double to)
        {
            double delta = Math.Abs(from - to);
            from += delta * timeFactor * Math.Sign(to - from);
            return from;
        }
    }
}
