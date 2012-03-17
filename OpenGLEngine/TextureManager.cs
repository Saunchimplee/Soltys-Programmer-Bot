
namespace Soltys.ProgrammerBot.OpenGLEngine
{
    class TextureManager
    {
        static Texture2D emptyIcon = new Texture2D("Data/Textures/empty.bmp");
        static Texture2D lockIcon = new Texture2D("Data/Textures/lock.bmp");
        static Texture2D upIcon = new Texture2D("Data/Textures/up_arrow.bmp");

        static Texture2D lightIcon = new Texture2D("Data/Textures/lightbulb.bmp");
        static Texture2D turnRightIcon = new Texture2D("Data/Textures/turn_right.bmp");
        static Texture2D turnLeftIcon = new Texture2D("Data/Textures/turn_left.bmp");

        static Texture2D jumpIcon = new Texture2D("Data/Textures/jump.bmp");

        static Texture2D emptyFunctionOneIcon = new Texture2D("Data/Textures/empty_fun1.bmp");
        static Texture2D emptyFunctionTwoIcon = new Texture2D("Data/Textures/empty_fun2.bmp");

        public static Texture2D Empty
        {
            get { return emptyIcon; }
        }
        public static Texture2D Lock
        {
            get { return lockIcon; }
        }
        public static Texture2D Up
        {
            get { return upIcon; }
        }

        public static Texture2D Light
        {
            get { return lightIcon; }
        }
        public static Texture2D TurnRight
        {
            get { return turnRightIcon; }
        }
        public static Texture2D TurnLeft
        {
            get { return turnLeftIcon; }
        }

        public static Texture2D Jump
        {
            get { return jumpIcon; }
        }

        public static Texture2D EmptyFunctionOne
        {
            get { return emptyFunctionOneIcon; }
        }

        public static Texture2D EmptyFunctionTwo
        {
            get { return emptyFunctionTwoIcon; }
        }
    }
}
