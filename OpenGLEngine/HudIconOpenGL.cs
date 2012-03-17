using System;
using OpenTK.Graphics.OpenGL;
using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    abstract class HudIconOpenGL : HudIcon, ICloneable, IDisposable, IRenderer<HudIcon>
    {
        protected Texture2D texture;

        bool disposed = false;


        public HudIconOpenGL()
        {

        }
        public override void Render(HudIcon obj)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture.Texture);

            GL.Begin(BeginMode.Quads);
            {
                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(0.0f, 0.0f);
                GL.Vertex2(drawPoint.X, drawPoint.Y);

                GL.TexCoord2(1.0f, 0.0f);
                GL.Vertex2(drawPoint.X + boxSize, drawPoint.Y);

                GL.TexCoord2(1.0f, 1.0f);
                GL.Vertex2(drawPoint.X + boxSize, drawPoint.Y + boxSize);

                GL.TexCoord2(0.0f, 1.0f);
                GL.Vertex2(drawPoint.X, drawPoint.Y + boxSize);
            }
            GL.End();
        }
        public void Draw()
        {

            GL.Disable(EnableCap.Texture2D);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    texture.Dispose();
                }

                disposed = true;
            }
        }

        ~HudIconOpenGL()
        {
            Dispose(false);
        }



    }
}
