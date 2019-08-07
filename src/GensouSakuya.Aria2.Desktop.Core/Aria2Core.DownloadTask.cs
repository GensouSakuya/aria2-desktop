using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        private volatile object _taskListLock = new object();

        protected void InitDownloadTasks()
        {
            DbInit();
        }

        private static readonly List<DownloadStatus> ListeningStatus = new List<DownloadStatus>
        {
            DownloadStatus.Active, DownloadStatus.Paused, DownloadStatus.Waiting
        };

        public void RefreshProcessingTasks()
        {
            DownloadTasks.Where(p => ListeningStatus.Contains(p.Status)).ToList().ForEach(async p =>
            {
                var entity = await GetTask(p.GID);
                if (p != entity)
                {
                    Update(p, entity);
                }
            });

            SaveChanges();
        }

        public async Task<DownloadTask> GetTask(string gid)
        {
            return (await Aria2.TellStatus(gid)).Convert();
        }

        public async Task StartDownload(string url)
        {
            var newGid = await Aria2.AddUri(url);
            var newTask = await GetTask(newGid);
            DownloadTasks.Add(newTask);

            SaveChanges();
        }

        public async Task Pause(string gid)
        {
            await Aria2.Pause(gid);
            DownloadTasks.Find(gid).Status = DownloadStatus.Paused;

            SaveChanges();
        }

        public void AutoRefresh()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        RefreshProcessingTasks();
                        DownloadTaskView = DownloadTasks.ToList();
                    }
                    catch
                    {
                        
                    }
                    Thread.Sleep(200);
                }
            });
        } 
    }
}
