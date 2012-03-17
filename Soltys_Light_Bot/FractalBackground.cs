using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot
{
    public abstract class FractalBackground
    {
        // GLSL Objects
        protected int VertexShaderObject;
        protected int FragmentShaderObject;
        protected int ProgramObject;
        protected int TextureObject;
        protected void LinkShadersIntoProgram()
        {
            ProgramObject = GL.CreateProgram();
            GL.AttachShader(ProgramObject, VertexShaderObject);
            GL.AttachShader(ProgramObject, FragmentShaderObject);
            GL.LinkProgram(ProgramObject);
        }

        protected void LoadFractalTexture(string filePath)
        {
            // Load&Bind the 1D texture for color lookups
            GL.ActiveTexture(TextureUnit.Texture0); // select TMU0
            GL.GenTextures(1, out TextureObject);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureWrapS, (int)(TextureWrapMode)All.ClampToEdge);

            using (Bitmap bitmap = new Bitmap(filePath))
            {
                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                GL.TexImage1D(TextureTarget.Texture1D, 0, PixelInternalFormat.Rgb8, data.Width, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr,
                              PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);
            }
        }

        public abstract void OnLoad(object sender, EventArgs e);
        public abstract void OnResize(object sender, EventArgs e);
        public void OnUnload(object sender, EventArgs e)
        {
            if (TextureObject != 0)
            {
                GL.DeleteTextures(1, ref TextureObject);
            }
            if (ProgramObject != 0)
            {
                GL.DeleteProgram(ProgramObject);
            }
        }
        public abstract void Render(double time);


      

        protected void drawPolygonForFractal()
        {     

            GL.Rotate(75, 1, -0.4, 0.5);
            GL.Begin(BeginMode.Polygon);
            {

                GL.TexCoord2(0, 0);
                GL.Vertex3(-200, -200, 5.0);
                GL.TexCoord2(1, 0);
                GL.Vertex3(200, -200, 5.0);
                GL.TexCoord2(1, 1);
                GL.Vertex3(200, 200, 5.0);
                GL.TexCoord2(0, 1);
                GL.Vertex3(-200, 200, 5.0);
            }
            GL.End();

          
        }
    }
}
