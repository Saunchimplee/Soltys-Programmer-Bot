using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Ninject;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Soltys.ProgrammerBot.Core;
using Soltys.ProgrammerBot.Core.HudElements;
using Soltys.ProgrammerBot.Core.Interfaces;
using Soltys.ProgrammerBot.Core.Level;

namespace Soltys.ProgrammerBot
{
    public class ProgrammerBotWindow : GameWindow
    {
        HashSet<Key> pressedKeys = new HashSet<Key>();
        FractalBackground background = null;
        Matrix4d cameraPosition;

        StandardLighting standardLighting = new StandardLighting();
        ShaderLighting shaderLighting = new ShaderLighting();
        readonly Game theGame;
        readonly Menu mainMenu;
        BackgroundMusic music = new BackgroundMusic();
        bool mainMenuMode = true;

        IKernel kernel;
        public ProgrammerBotWindow(int windowWidth, int windowHeight)
            : base(windowWidth, windowHeight, new OpenTK.Graphics.GraphicsMode(32, 0, 0, 4))
        {
            setupKernel();
            theGame = kernel.Get<Game>();
            mainMenu = new Menu(Width, Height);
            theGame.HUD.HudWidth = windowWidth;
            theGame.HUD.HudHeight = windowHeight;

            setEvents();

            if (Program.UseShaders)
            {
                setShaderEvents();
            }
        }
        private void setupKernel()
        {
            kernel = new StandardKernel();
            kernel.Bind<IRendererFactory>().To<OpenGLEngine.OpenGLRendererFactory>();
            kernel.Bind<ILevelRenderer>().To<OpenGLEngine.LevelRenderer>();
        }

        private void fullScreenMode()
        {
            // Go fullscreen
            WindowState = WindowState.Fullscreen;
        }

        #region Event Setters
        private void setEvents()
        {
            setWindowEvents();
            setKeyboardEvents();
            setMouseEvents();
            setGameEvents();
            setMenuEvents();
        }

        private void setMenuEvents()
        {
            mainMenu.ChangeVisibility += mainMenu_ChangeVisibility;
            mainMenu.Exit += mainMenu_Exit;
        }



        private void setGameEvents()
        {
            Load += theGame.OnLoad;
        }

        private void setWindowEvents()
        {
            Load += OnLoad;
            Unload += OnUnload;
            RenderFrame += OnRender;
            Resize += OnResize;
            UpdateFrame += OnUpdateFrame;
        }

        private void setKeyboardEvents()
        {
            KeyPress += OnKeyPress;
            Keyboard.KeyDown += OnKeyDown;
            Keyboard.KeyUp += OnKeyUp;
        }


        private void setShaderEvents()
        {
            background = new JuliaFractalBackground();
            //background = new MandelbrotFractalBackground();
            Load += background.OnLoad;
            Resize += background.OnResize;
            Unload += background.OnUnload;

            shaderLighting = new ShaderLighting();
            Load += shaderLighting.OnLoad;
            Unload += shaderLighting.OnUnload;
        }

        private void setMouseEvents()
        {
            Mouse.Move += OnMouseMove;
            Mouse.ButtonUp += OnMouseUp;
            Mouse.ButtonDown += OnMouseDown;
        }
        #endregion

        #region Menu Events

        void mainMenu_ChangeVisibility(object sender, MenuVisibilityStatus e)
        {
            switch (e)
            {
                case MenuVisibilityStatus.Visible:
                    mainMenuMode = true;
                    break;
                case MenuVisibilityStatus.Hidden:
                    mainMenuMode = false;
                    break;
            }
        }

        void mainMenu_Exit()
        {
            Exit();
        }

        #endregion

        #region Keyboard Events
        void OnKeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            pressedKeys.Remove(e.Key);
        }
        void OnKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            pressedKeys.Add(e.Key);
            reactOnKeyboardKeys();
        }
        private void reactOnKeyboardKeys()
        {
            foreach (var key in pressedKeys)
            {
                GlobalKeyboardMapping(key);
                if (!mainMenuMode)
                {
                    switch (theGame.GameMode)
                    {
                        case Core.GameModes.Create:
                            CreateKeyboardMapping(key);
                            break;
                        case Core.GameModes.Save:
                            SaveKeyboardMapping(key);
                            break;
                        case Core.GameModes.Load:
                            LoadKeyboardMapping(key);
                            break;
                        case Core.GameModes.Play:
                            PlayKeyboardMapping(key);
                            break;
                    }
                }
                if (key == Key.Escape)
                {
                    mainMenuMode = true;
                }
                else
                {
                    MainMenuKeyboardMapping(key);
                }
            }
        }


        private void keyboardAction(FrameEventArgs e)
        {
            if (pressedKeys.Any())
            {
                timeKeyPressed += TimeSpan.FromSeconds(e.Time);
            }
            else
            {
                timeKeyPressed = TimeSpan.Zero;
            }
            if (timeKeyPressed >= timeReactOnKey)
            {
                reactOnKeyboardKeys();
                timeKeyPressed = TimeSpan.Zero;
            }
        }
        void OnKeyPress(object sender, EventArgs e)
        {
        }
        #endregion

        #region Keyboard Mappings
        void GlobalKeyboardMapping(Key pressedKey)
        {
            switch (pressedKey)
            {

                case Key.F1:
                    theGame.HUD.Message.ClearMessages();
                    theGame.HUD.Message.FirstLine = "Tryb tworzenia planszy";

                    theGame.GameMode = GameModes.Create;
                    theGame.Level.LoadEmptyLevel();
                    break;
                case Key.F3:
                    theGame.HUD.Message.ClearMessages();
                    theGame.GameMode = GameModes.Play;
                    theGame.Level.LoadLevel();
                    break;

                case Key.Period:
                    theGame.Level.DestAngle = LevelRotation.Clockwise;
                    break;
                case Key.Comma:
                    theGame.Level.DestAngle = LevelRotation.CounterClockwise;
                    break;
                case Key.M:
                    theGame.Level.DestAngle = LevelRotation.Normal;
                    break;
                case Key.F11:
                    music.ToggleMusic();
                    break;
            }
        }
        void CreateKeyboardMapping(Key pressedKey)
        {
            switch (pressedKey)
            {
                case Key.F4:
                    theGame.HUD.Message.FirstLine = "Zapisz poziom do:";
                    theGame.HUD.Message.SecondLine = levelFileName;
                    theGame.GameMode = GameModes.Save;
                    break;
                case Key.F5:
                    theGame.HUD.Message.FirstLine = "Odczytaj poziom z pliku:";
                    theGame.HUD.Message.SecondLine = levelFileName;
                    theGame.GameMode = GameModes.Load;
                    break;
                case Key.Up:
                    theGame.Robot.MoveForward();
                    if (!theGame.Level.IsRobotInBounds())
                    {
                        theGame.Robot.MoveBackward();
                    }
                    break;
                case Key.Down:
                    theGame.Robot.MoveBackward();
                    if (!theGame.Level.IsRobotInBounds())
                    {
                        theGame.Robot.MoveForward();
                    }
                    break;
                case Key.Left:
                    theGame.Robot.TurnLeft();
                    break;
                case Key.Right:
                    theGame.Robot.TurnRight();
                    break;
                case Key.Q:
                    theGame.Level.SetTileType(TileType.NoLight);
                    break;
                case Key.W:
                    theGame.Level.SetTileType(TileType.LightOff);
                    break;
                case Key.E:
                    theGame.Level.SetTileType(TileType.LightOn);
                    break;
                case Key.R:
                    theGame.Level.SetTileType(TileType.StartPosition);
                    break;
                case Key.A:
                    theGame.Level.SetTitleHeight(TileHeight.Ground);
                    break;
                case Key.S:
                    theGame.Level.SetTitleHeight(TileHeight.BoxOne);
                    break;
                case Key.D:
                    theGame.Level.SetTitleHeight(TileHeight.BoxTwo);
                    break;

            }
        }
        void PlayKeyboardMapping(Key pressedKey)
        {
            switch (pressedKey)
            {
                case Key.Space:
                    theGame.RunProgram();
                    break;
            }
        }

        private void MainMenuKeyboardMapping(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    mainMenu.MoveCursorUp();
                    break;

                case Key.Down:
                    mainMenu.MoveCursorDown();
                    break;
                case Key.Enter:
                    mainMenu.EnterPressed();
                    break;


            }
        }

        string levelFileName = "";
        void LoadKeyboardMapping(Key key)
        {
            modifyLevelFileName(key);

            if (key == Key.Enter)
            {
                theGame.Level.LoadLevel(levelFileName);
                levelFileName = "";
                theGame.HUD.Message.ClearMessages();
                theGame.HUD.Message.FirstLine = "Tryb tworzenia planszy";
                theGame.GameMode = GameModes.Create;
            }
        }
        void SaveKeyboardMapping(Key key)
        {
            modifyLevelFileName(key);

            if (key == Key.Enter)
            {
                theGame.Level.SaveLevel(levelFileName);
                levelFileName = "";
                theGame.HUD.Message.ClearMessages();
                theGame.HUD.Message.FirstLine = "Tryb tworzenia planszy";
                theGame.GameMode = GameModes.Create;
            }

        }

        private void modifyLevelFileName(Key key)
        {
            string keyInput = key.ToString();
            if (keyInput.StartsWith("Number", StringComparison.OrdinalIgnoreCase))
            {
                keyInput = keyInput.Substring("Number".Length);
            }
            if (keyInput.Length == 1)
            {
                if (char.IsLetterOrDigit(keyInput, 0))
                {
                    levelFileName += keyInput;
                }
            }
            else if (key == Key.BackSpace && levelFileName.Length > 0)
            {
                levelFileName = levelFileName.Substring(0, levelFileName.Length - 1);
            }
            else if (key == Key.Space)
            {
                levelFileName += "_";
            }
            theGame.HUD.Message.SecondLine = levelFileName;
        }
        #endregion

        #region Mouse events
        Point mousePoint = new Point();
        public void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            mousePoint.X = e.X;
            mousePoint.Y = e.Y;
            theGame.HUD.MoveGrabbedIcon(e.X, e.Y);
        }
        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                theGame.HUD.OnIconGrab(new IconGrabEventArgs(e.Position));
            }
        }

        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (theGame.HUD.GrabbedIcon != null)
                {
                    theGame.HUD.OnIconRelease(new IconReleaseEventArgs(e.Position, theGame.HUD.GrabbedIcon));
                }
            }
        }

        #endregion

        readonly TimeSpan timeReactOnKey = TimeSpan.FromMilliseconds(500);
        TimeSpan timeKeyPressed = TimeSpan.Zero;
        void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            theGame.UpdateLogic(new GameTime(TimeSpan.FromSeconds(e.Time)));
            music.Update();
        }
        void OnResize(object sender, EventArgs e)
        {
            theGame.HUD.HudWidth = Width;
            theGame.HUD.HudHeight = Height;
            mainMenu.SetHudSize(Width, Height);
            GL.Viewport(0, 0, Width, Height);

            GL.LoadIdentity();

            GL.MatrixMode(MatrixMode.Projection);						// Select The Projection Matrix
            GL.LoadIdentity();									// Reset The Projection Matrix
#pragma warning disable 612,618
            OpenTK.Graphics.Glu.Perspective(70.0f, aspect: (float)Width / (float)Height, zNear: 0.1f, zFar: 300.0f);
#pragma warning restore 612,618
        }

        public void OnLoad(object sender, EventArgs e)
        {

            // Check for necessary capabilities:
            var version = new Version(GL.GetString(StringName.Version).Substring(0, 3));
            var target = new Version(2, 0);
            if (version < target)
            {
                throw new NotSupportedException(
                    String.Format(CultureInfo.InvariantCulture,
                    "OpenGL {0} is required (you only have {1}).", target, version));
            }

            cameraPosition = Matrix4d.LookAt(eyeX: 8, eyeY: 8, eyeZ: -11, targetX: 2, targetY: -1, targetZ: -7, upX: 0.0, upY: 1.0f, upZ: 0);

            VSync = VSyncMode.On;
            fullScreenMode();
            music.StartMusic();
        }
        public void OnUnload(object sender, EventArgs e)
        {

        }

        readonly TimeSpan fpsUpdate = TimeSpan.FromMilliseconds(500);
        TimeSpan accumulateTime = TimeSpan.Zero;
        int fps;
        int timesCount;
        public void OnRender(object sender, FrameEventArgs e)
        {
            if (accumulateTime >= fpsUpdate)
            {
                theGame.HUD.Message.SecondLine = (fps / timesCount).ToString(CultureInfo.InvariantCulture) + " FPS";
                accumulateTime = TimeSpan.Zero;
                fps = 0;
                timesCount = 0;
            }
            else
            {
                fps += (int)(1 / e.Time);
                timesCount++;
                accumulateTime = accumulateTime.Add(TimeSpan.FromSeconds(e.Time));
            }

            GL.MatrixMode(MatrixMode.Modelview);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.DepthBufferBit |
                             ClearBufferMask.ColorBufferBit |
                             ClearBufferMask.AccumBufferBit |
                             ClearBufferMask.StencilBufferBit);
            GL.LoadIdentity();

            GL.LoadMatrix(ref cameraPosition);
            if (mainMenuMode)
            {
                mainMenu.Render();
                GL.Flush();
            }
            else
            {
                enable();

                renderFractal(e.Time);
                lightingOn();
                theGame.Render();
                lightingOff();

                GL.Flush();
            }
            SwapBuffers();
        }

        private void lightingOff()
        {
            if (Program.UseShaderLighting)
            {
                shaderLighting.TurnOffLights();
            }
            standardLighting.TurnOffLights();
        }

        private void lightingOn()
        {
            standardLighting.TurnOnLights();
            if (Program.UseShaderLighting)
            {
                 shaderLighting.TurnOnLights();
            }
        }

        private void renderFractal(double deltaTime)
        {
            if (Program.UseShaders)
            {
                background.Render(deltaTime);
                GL.UseProgram(0); //Turning off shader
            }
        }


        private void enable()
        {
            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.LineSmooth);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.ShadeModel(ShadingModel.Smooth);
        }
    }
}
