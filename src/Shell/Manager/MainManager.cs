using GensouSakuya.Aria2.Desktop.Core;

namespace Shell
{
    public static class MainManager
    {
        public static Main Main { get; private set; } = null;
        private static volatile object locker = new object();

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

        public static void Shutdown()
        {
            if (Main != null)
            {
                Main.Shutdown();
            }
        }
    }
}
