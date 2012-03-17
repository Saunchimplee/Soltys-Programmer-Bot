using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public class LabelBox : IDisposable
    {
        Font sans;

        RectangleF dialogParams;
        public Brush TextColor { get; set; }
        public Color Background { get; set; }

        int HudWidth;
        int HudHeight;
        public LabelBox(int HudWidth, int HudHeight, RectangleF dialogParams)
        {
            this.HudHeight = HudHeight;
            this.HudWidth = HudWidth;
            sans = new Font(FontFamily.GenericSansSerif, 15);

            this.dialogParams = dialogParams;
            TextColor = Brushes.White;
            Background = Color.Black;

        }
        public void ChangeFontSize(float newSize)
        {
            sans = new Font(FontFamily.GenericSansSerif, newSize);
        }
        public void Render(string message)
        {
            using (var textRenderer = new TextRenderer((int)dialogParams.Width, (int)dialogParams.Height))
            {
                textRenderer.Clear(Background);
                textRenderer.DrawString(message, sans, TextColor, PointF.Empty);



                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, textRenderer.Texture);
                GL.Begin(BeginMode.Quads);
                {
                    GL.TexCoord2(0, 1f);
                    GL.Vertex2(dialogParams.X, dialogParams.Bottom);
                    GL.TexCoord2(1f, 1f);
                    GL.Vertex2(dialogParams.Right, dialogParams.Bottom);
                    GL.TexCoord2(1f, 0f);
                    GL.Vertex2(dialogParams.Right, dialogParams.Y);
                    GL.TexCoord2(0.0f, 0.0f);
                    GL.Vertex2(dialogParams.X, dialogParams.Y);
                }
                GL.End();
                GL.Flush();
                GL.Disable(EnableCap.Texture2D);
            }

        }
        bool disposed = false;
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
                    sans.Dispose();
                }


                disposed = true;
            }
        }

        ~LabelBox()
        {

            Dispose(false);
        }


    }
}
