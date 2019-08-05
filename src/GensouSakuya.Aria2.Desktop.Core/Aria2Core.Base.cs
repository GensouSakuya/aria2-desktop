using System;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core : IDisposable
    {
        private readonly Aria2Config Aria2Config;
        protected SDK.Aria2Client Aria2 { get; private set; }

        public Aria2Core(Aria2Config config)
        {
            Aria2Config = config;
        }

        public void Start()
        {
            if (Aria2 == null)
            {
                Aria2 = Aria2Config.IsAria2ServerExist
                    ? Aria2Helper.Connect(Aria2Config.Aria2Host, Aria2Config.ListenPort)
                    : Aria2Helper.StartUp(Aria2Config.Aria2Path, Aria2Config.ToArgs(), Aria2Config.Aria2Host, Aria2Config.ListenPort);
                
                InitDownloadTasks();
                AutoRefresh();
            }
        }

        public void Shutdown()
        {
            if (!Aria2Config.IsAria2ServerExist)
            {
                Aria2Helper.Shutdown(Aria2);
            }
        }

        public void Dispose()
        {
            Shutdown();
        }

    }
}
