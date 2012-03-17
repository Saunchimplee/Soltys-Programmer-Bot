using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public class Texture2D : IDisposable, ICloneable
    {
        string texturePath;
        int textureHandler;
        Bitmap bitmap;
        private bool disposed;
        public int Texture
        {
            get
            {
                GL.BindTexture(TextureTarget.Texture2D, textureHandler);
                return textureHandler;
            }
        }

        public Texture2D(string texturePath)
        {
            this.texturePath = texturePath;
            bitmap = new Bitmap(texturePath);
            load();
        }
        private void load()
        {
            GL.GenTextures(1, out textureHandler);
            GL.BindTexture(TextureTarget.Texture2D, textureHandler);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

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
                    bitmap.Dispose();
                }

                GL.DeleteTextures(1, ref textureHandler);

                disposed = true;
            }
        }

        ~Texture2D()
        {

            Dispose(false);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public Texture2D Clone()
        {
            var newTex = new Texture2D(texturePath);
            return newTex;
        }
    }
}
