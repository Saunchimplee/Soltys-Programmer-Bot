using System;
using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot
{
    class ShaderLighting : ILighting
    {
        int vertexShaderObject, fragmentShaderObject, programObject;
        public void OnLoad(object sender, EventArgs e)
        {
            GL.Disable(EnableCap.Dither);

            vertexShaderObject = ShaderLoader.Load("Data/Shaders/lighting_VS.glsl", ShaderType.VertexShader);

            fragmentShaderObject = ShaderLoader.Load("Data/Shaders/lighting_FS.glsl", ShaderType.FragmentShader);

            // Link the Shaders to a usable Program
            programObject = GL.CreateProgram();
            GL.AttachShader(programObject, vertexShaderObject);
            GL.AttachShader(programObject, fragmentShaderObject);
            GL.LinkProgram(programObject);

            // Flag ShaderObjects for delete when app exits
            GL.DeleteShader(vertexShaderObject);
            GL.DeleteShader(fragmentShaderObject);

        }
        public void OnUnload(object sender, EventArgs e)
        {
            if (programObject != 0)
            {
                GL.DeleteProgram(programObject);
            }
        }
        public void TurnOnLights()
        {
            GL.UseProgram(programObject);
        }

        public void TurnOffLights()
        {
            GL.UseProgram(0);
        }
    }

}
