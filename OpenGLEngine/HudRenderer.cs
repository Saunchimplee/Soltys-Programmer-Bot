using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.HudElements;
using Soltys.ProgrammerBot.Core.Interfaces;

namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public class HudRenderer : IHudRenderer
    {
        public int HudWidth { get; set; }
        public int HudHeight { get; set; }

        private void setUp(int HudWidth, int HudHeight)
        {
            GL.UseProgram(0);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0, HudWidth, HudHeight, 0, -1, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Lighting); //disable the lighting
            GL.Disable(EnableCap.ColorMaterial);

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);


        }
        private void tearDown()
        {
            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.Lighting);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void Render(Hud obj)
        {
            setUp(obj.HudWidth, obj.HudHeight);


            obj.CommandBar.Render();
            obj.MainProgram.Render();
            obj.FunctionOne.Render();
            obj.FunctionTwo.Render();

            if (obj.GrabbedIcon != null)
            {
                obj.GrabbedIcon.Render(obj.GrabbedIcon);
            }


            LabelBox dialog = new LabelBox(obj.HudWidth, obj.HudHeight, new RectangleF(obj.HudWidth - 500, obj.HudHeight - 100, 500, 100));

            dialog.Render(obj.Message.FirstLine + "\n" + obj.Message.SecondLine);
            tearDown();


        }

        public void SetHudWidth(int width, int height)
        {
            HudWidth = width;
            HudHeight = height;
        }
    }
}
