using System;
using Tao.FreeGlut;
namespace Soltys.ProgrammerBot
{

    class Program
    {
        public static bool UseShaders { get; private set; }
        public static bool UseShaderLighting { get; private set; }
        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }
        [STAThread]
        static void Main(string[] args)
        {
            UseShaders = true;
            UseShaderLighting = true;

            if (args.Length > 0)
            {
                if (args[0] == "--no-shaders")
                {
                    UseShaders = false;
                }
            }

            WindowWidth = 1024;
            WindowHeight = 786;


            using (var N = new ProgrammerBotWindow(WindowWidth, WindowHeight))
            {
                N.Icon = new System.Drawing.Icon("robot.ico");
                Glut.glutInit();
                N.Title = "Soltys Programmer Bot";
                N.WindowBorder = OpenTK.WindowBorder.Hidden;

                N.Run(100, 75);
            }
        }
    }
}