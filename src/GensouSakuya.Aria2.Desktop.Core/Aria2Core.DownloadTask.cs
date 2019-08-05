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
            lock (_taskListLock)
            {
                DownloadTasks.Where(p => ListeningStatus.Contains(p.Status)).ToList().ForEach(async p =>
                {
                    var entity = await GetTask(p.GID);
                    Update(p, entity);
                });
            }

            SaveChanges();
        }

        public async Task<DownloadTask> GetTask(string gid)
        {
            return (await Aria2.TellStatus(gid)).Convert();
        }

        public async Task<string> StartDownload(string url)
        {
            var newGid = await Aria2.AddUri(url);
            var newTask = await GetTask(newGid);
            lock (_taskListLock)
            {
                DownloadTasks.Add(newTask);
            }

            SaveChanges();
            return newGid;
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
