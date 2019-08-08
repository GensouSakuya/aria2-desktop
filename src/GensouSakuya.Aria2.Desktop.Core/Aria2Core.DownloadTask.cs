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
                    await UpdateAsync(p, entity);
                }
            });
        }

        public async Task<DownloadTask> GetTask(string gid)
        {
            return (await Aria2.TellStatus(gid)).Convert();
        }

        public async Task StartDownload(string url)
        {
            var newGid = await Aria2.AddUri(url);
            var newTask = await GetTask(newGid);
            Add(newTask);
        }

        public async Task Pause(string gid)
        {
            await Aria2.Pause(gid);
            var task = DownloadTask(gid);
            task.Status = DownloadStatus.Paused;
            Update(task);
        }

        public async Task Delete(string gid)
        {
            var task = DownloadTask(gid);
            if (task == null)
                return;

            await DeleteAsync(task);
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
