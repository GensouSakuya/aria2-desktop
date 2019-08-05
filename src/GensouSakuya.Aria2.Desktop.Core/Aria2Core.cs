using GensouSakuya.Aria2.SDK.Model;
using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public class Aria2Core: IDisposable
    {
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
            }
        }

        private static readonly List<DownloadStatus> ListeningStatus = new List<DownloadStatus>
        {
            DownloadStatus.Active, DownloadStatus.Paused, DownloadStatus.Waiting
        };

        public List<DownloadStatusInfo> AllDownloadTask { get; set; } = new List<DownloadStatusInfo>();

        public void RefreshProcessingTasks()
        {
            AllDownloadTask.Where(p => ListeningStatus.Contains(p.Status)).ToList().ForEach(async p =>
            {
                p = await GetTaskDownloadStatus(p.GID);
            });
        }

        public async Task<DownloadStatusInfo> GetTaskDownloadStatus(string gid)
        {
            return (await Aria2.TellStatus(gid)).Convert();
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

        private readonly Aria2Config Aria2Config;

        internal GensouSakuya.Aria2.SDK.Aria2Client Aria2 { get; private set; } = null;
    }
}
