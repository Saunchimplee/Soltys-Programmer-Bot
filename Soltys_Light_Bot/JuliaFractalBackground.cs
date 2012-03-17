using System;
using System.Security.Permissions;
using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot
{
    public class JuliaFractalBackground : FractalBackground
    {
        #region Private Fields

        // Julia Variables for animation
        float AnimOffsetX = 0.913f; // using non-zero as starting p to make it more interesting
        float AnimOffsetY = 0.63f;

        const double AnimSpeedX = 0.49; // anim speed scaling is solely used to make the anim more interesting
        const double AnimSpeedY = 0.7;
        const double AnimCosinusPercent = 0.95f; // scales the cosinus down to 85% to avoid the (boring) borders

        float UniformScaleFactorX; // fractal horizontal scaling is only affected by window resize
        float UniformScaleFactorY; // fractal vertical scaling is only affected by window resize
        float UniformOffsetX = 1.8f; // fractal horizontal offset
        float UniformOffsetY = 1.8f; // fractal vertical offset

        #endregion private Fields
        public JuliaFractalBackground()
        {

        }

        [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
        public override void OnLoad(object sender, EventArgs e)
        {
            GL.Disable(EnableCap.Dither);

            loadShaders();
            // Link the Shaders to a usable Program
            LinkShadersIntoProgram();

            // Flag ShaderObjects for delete when app exits
            GL.DeleteShader(VertexShaderObject);
            GL.DeleteShader(FragmentShaderObject);

            LoadFractalTexture("JuliaColorTable_16_1b.bmp");
        }

        private void loadShaders()
        {
            VertexShaderObject = ShaderLoader.Load("Shaders/JuliaSet_VS.glsl", ShaderType.VertexShader);

            try
            {
                FragmentShaderObject = ShaderLoader.Load("Shaders/JuliaSet_SM3_FS_16_1.glsl", ShaderType.FragmentShader);
            }
            catch (ShaderCompilationException compilationException)
            {
                tryLoadBackupShader(compilationException);
            }
        }


        private void tryLoadBackupShader(ShaderCompilationException compilationException)
        {
            Console.WriteLine("Compilation error shader: " + "Shaders/JuliaSet_SM3_FS_16_1.glsl");
            Console.WriteLine("Error message: " + compilationException.Message);

            Console.WriteLine("---");
            Console.WriteLine("Trying backup shader...");
            try
            {
                FragmentShaderObject = ShaderLoader.Load("Shaders/JuliaSet_SM2_FS_16_1.glsl", ShaderType.VertexShader);
            }
            catch (ShaderCompilationException ex)
            {
                Console.WriteLine("Compilation error shader: " + "Shaders/JuliaSet_SM2_FS_16_1.glsl");
                Console.WriteLine("Error message: " + ex.Message);

                Console.WriteLine("Shaders are turned off");
            }
        }


        public override void OnResize(object sender, EventArgs e)
        {
            var gameWindow = sender as ProgrammerBotWindow;
            UniformScaleFactorX = gameWindow.Width / (UniformOffsetX * 2f);
            UniformScaleFactorY = gameWindow.Height / (UniformOffsetY * 2f);
        }

        /// <summary>
        /// Draw next fractal frame 
        /// </summary>
        /// <param name="time">ElapsedGameTime between frames</param>
        public override void Render(double time)
        {

            GL.PushMatrix();
            {
                // First, render the next frame of the Julia fractal.
                GL.UseProgram(ProgramObject);

                // advance the animation by elapsed time, scaling is solely used to make the anim more interesting
                AnimOffsetX += (float)(time * AnimSpeedX);
                AnimOffsetY += (float)(time * AnimSpeedY);

                passUniformsToFragmentShader();
                drawPolygonForFractal();
            }
            GL.PopMatrix();

        }


        private void passUniformsToFragmentShader()
        {
            // pass uniforms into the fragment shader
            // first the texture
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "COLORTABLE"), TextureObject);
            // the rest are floats
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "CETX"), (float)(Math.Cos(AnimOffsetX) * AnimCosinusPercent));
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "CETY"), (float)(Math.Cos(AnimOffsetY) * AnimCosinusPercent));
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "SCALINGX"), UniformScaleFactorX);
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "SCALINGY"), UniformScaleFactorY);
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "OFFSETX"), UniformOffsetX);
            GL.Uniform1(GL.GetUniformLocation(ProgramObject, "OFFSETY"), UniformOffsetY);
        }
    }
}
