using Core;

namespace Shell
{
    public static class MainManager
    {
        public static Main Main { get; private set; } = null;
        private static volatile object locker = null;

        public static void StartUp()
        {
            if (Main == null)
            {
                lock (locker)
                {
                    if (Main == null)
                    {
                        Main = new Main();
                    }
                }
            }
        }
    }
}
