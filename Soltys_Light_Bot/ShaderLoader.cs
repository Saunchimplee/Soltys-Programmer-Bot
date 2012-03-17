using System.Diagnostics;
using System.IO;
using NLog;
using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot
{
    static class ShaderLoader
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static int Load(string filePath, ShaderType shaderType)
        {
            int loadedShader = 0;

            using (var shaderReader = new StreamReader(filePath))
            {
                loadedShader = GL.CreateShader(shaderType);
                GL.ShaderSource(loadedShader, shaderReader.ReadToEnd());
                GL.CompileShader(loadedShader);
            }
            logger.Trace("Loading shader: " + filePath);
            getShaderInfo(loadedShader);
            return loadedShader;
        }
        private static void getShaderInfo(int shader)
        {
            string logInfo;
            GL.GetShaderInfoLog(shader, out logInfo);
            logger.Trace(logInfo);
            if (logInfo.Length > 0 && !logInfo.Contains("hardware"))
            {
                Trace.WriteLine("Vertex Shader Log:\n" + logInfo);
            
            }
            else
            {
                Trace.WriteLine("Vertex Shader compiled without complaint.");
            }

        }
    }
}
