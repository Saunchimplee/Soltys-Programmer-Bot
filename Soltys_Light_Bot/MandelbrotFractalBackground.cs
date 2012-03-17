using System;
using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot
{
    class MandelbrotFractalBackground : FractalBackground
    {
        public override void OnLoad(object sender, EventArgs e)
        {
            GL.Disable(EnableCap.Dither);

            loadShaders();
            // Link the Shaders to a usable Program
            LinkShadersIntoProgram();

            // Flag ShaderObjects for delete when app exits
            GL.DeleteShader(VertexShaderObject);
            GL.DeleteShader(FragmentShaderObject);

            LoadFractalTexture("pal.png");
        }

        void loadShaders()
        {
            try
            {
                VertexShaderObject = ShaderLoader.Load("Shaders/mbrot_VS.glsl", ShaderType.VertexShader);

                FragmentShaderObject = ShaderLoader.Load("Shaders/mbrot_FS.glsl", ShaderType.FragmentShader);
            }
            catch (ShaderCompilationException compilationException)
            {
                throw new Exception(compilationException.Message);
            }
        }

        public override void OnResize(object sender, EventArgs e)
        {

        }

        private void passUniformsToFragmentShader()
        {
            //GL.Uniform1(GL.GetUniformLocation(ProgramObject, "tex"), TextureObject);
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "maxIterations"), 50);
            GL.Uniform2(GL.GetUniformLocation(ProgramObject, "center"), 0, 0);
            GL.Uniform3(GL.GetUniformLocation(ProgramObject, "outerColor2"), 1.0f, 1.0f, 1.0f);
            GL.Uniform3(GL.GetUniformLocation(ProgramObject, "outerColor1"), 0.0f, 0.0f, 0.0f);
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "zoom"), (float)(1.0));
        }



        public override void Render(double time)
        {
            GL.PushMatrix();
            {
                // First, render the next frame of the Julia fractal.
                GL.UseProgram(ProgramObject);


                passUniformsToFragmentShader();
                drawPolygonForFractal();
            }
            GL.PopMatrix();
        }
    }
}
