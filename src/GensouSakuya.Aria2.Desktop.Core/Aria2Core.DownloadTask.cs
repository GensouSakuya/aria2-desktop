using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        private static readonly List<DownloadStatus> ListeningStatus = new List<DownloadStatus>
        {
            DownloadStatus.Active, DownloadStatus.Paused, DownloadStatus.Waiting
        };

        public List<DownloadTask> AllDownloadTask { get; set; } = new List<DownloadTask>();

        public void RefreshProcessingTasks()
        {
            AllDownloadTask.Where(p => ListeningStatus.Contains(p.Status)).ToList().ForEach(async p =>
            {
                p = await GetTask(p.GID);
            });
        }

        public async Task<DownloadTask> GetTask(string gid)
        {
            return (await Aria2.TellStatus(gid)).Convert();
        }

        public async Task<string> StartDownload(string url)
        {
            return await Aria2.AddUri(url);
        }
    }
}
