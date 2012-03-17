using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Soltys.ProgrammerBot.OpenGLEngine;
namespace Soltys.ProgrammerBot
{
    public enum MenuVisibilityStatus { Visible, Hidden }
    class Menu
    {
        enum MenuPage { Main, Help };
        int width;
        int height;
        int cursorPosition = 0;
        MenuPage currentPage;

        public delegate void MenuVisibilityChangeHandler(object sender, MenuVisibilityStatus e);
        public event MenuVisibilityChangeHandler ChangeVisibility;

        public delegate void ExitHandler();
        public event ExitHandler Exit;

        public Menu(int w, int h)
        {
            width = w;
            height = h;
            currentPage = MenuPage.Main;
        }
        private void putText(float fontSize, string text, int x, int y, int w, int h)
        {
            using (var dialog = new LabelBox(width, height, new RectangleF(x, y, w, h)))
            {
                dialog.Background = Color.DarkSlateGray;
                dialog.TextColor = Brushes.White;
                dialog.ChangeFontSize(fontSize);
                dialog.Render(text);
            }
        }
        public void MoveCursorUp()
        {
            if (currentPage == MenuPage.Main)
            {
                if (cursorPosition > 0)
                {
                    cursorPosition--;
                }
            }
        }
        public void MoveCursorDown()
        {
            if (currentPage == MenuPage.Main)
            {
                if (cursorPosition < 2)
                {
                    cursorPosition++;
                }
            }
        }


        public void Render()
        {
            setUp();
            GL.ClearColor(Color.DarkSlateGray);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Color4(1.0f, 1.0f, 1.0f, 1f);
            switch (currentPage)
            {
                case MenuPage.Main:
                    putText(45, "Soltys Programmer Bot", width / 2 - 250, 50, 1000, 300);
                    putText(25, "Nowa gra / powrót do gry", 3 * width / 5 - 250, 200, 500, 300);
                    putText(25, "Pomoc", 3 * width / 5 - 250, 250, 500, 300);
                    putText(25, "Wyjście", 3 * width / 5 - 250, 300, 500, 300);
                    putText(18, "Paweł 'Soltys' Sołtysiak", width - 350, 400, 500, 300);
                    renderCursor();
                    break;
                case MenuPage.Help:
                    putText(18, "Spacja - Wykonaj polecenia", 3 * width / 5 - 250, 200, 500, 300);
                    putText(18, "F11 - Wyłącz dźwięk", 3 * width / 5 - 250, 250, 500, 300);
                    putText(25, "Powrót", 3 * width / 5 - 250, 350, 500, 300);
                    renderCursor();
                    break;
            }

            tearDown();
        }

        private void renderCursor()
        {
            float boxSize = 20f;
            GL.PushMatrix();
            {
                GL.Color3(Color.Black);

                GL.Begin(BeginMode.Quads);
                {
                    GL.Vertex2(width / 2 - 150, 205 + 50 * cursorPosition);
                    GL.Vertex2(width / 2 - 150 + boxSize, 205 + 50 * cursorPosition);
                    GL.Vertex2(width / 2 - 150 + boxSize, 205 + boxSize + 50 * cursorPosition);
                    GL.Vertex2(width / 2 - 150, 205 + boxSize + 50 * cursorPosition);
                }
                GL.End();
            }
            GL.PopMatrix();

        }
        private void setUp()
        {
            GL.UseProgram(0);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0, width, height, 0, -1, 1);

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

        public void SetHudSize(int w, int h)
        {
            width = w;
            height = h;
        }


        public void EnterPressed()
        {
            switch (currentPage)
            {
                case MenuPage.Main:
                    MainPageEnter();
                    break;
                case MenuPage.Help:
                    currentPage = MenuPage.Main;
                    cursorPosition = 0;
                    break;
            }
        }

        private void MainPageEnter()
        {
            switch (cursorPosition)
            {
                case 0:
                    if (ChangeVisibility != null)
                    {
                        ChangeVisibility(this, MenuVisibilityStatus.Hidden);
                    }
                    break;
                case 1:
                    currentPage = MenuPage.Help;
                    cursorPosition = 3;
                    break;
                case 2:
                    if (Exit != null)
                    {
                        Exit();
                    }
                    break;

            }
        }


    }
}
