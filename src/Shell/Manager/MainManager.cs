using GensouSakuya.Aria2.Desktop.Core;

namespace Shell
{
    public static class MainManager
    {
        public static Aria2Core Aria2Core { get; private set; } = null;
        private static volatile object locker = new object();

        public static void StartUp()
        {
            //if (Aria2Core == null)
            //{
            //    lock (locker)
            //    {
            //        if (Aria2Core == null)
            //        {
            //            Aria2Core = new Aria2Core();
            //        }
            //    }
            //}
        }

        public static void Shutdown()
        {
            //if (Aria2Core != null)
            //{
            //    Aria2Core.Shutdown();
            //}
        }
    }
}
