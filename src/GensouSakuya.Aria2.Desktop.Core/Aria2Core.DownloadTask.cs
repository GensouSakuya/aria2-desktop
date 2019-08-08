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
                if (entity == null)
                {
                    await SetError(p.GID);
                }
                else if (p != entity)
                {
                    await UpdateAsync(p, entity);
                }
            });
        }

        public async Task<DownloadTask> GetTask(string gid)
        {
            return (await Aria2.TellStatus(gid))?.Convert();
        }

        public async Task StartDownload(string url)
        {
            var newGid = await Aria2.AddUri(url);
            var newTask = await GetTask(newGid);
            newTask.Link = url;
            Add(newTask);
        }

        public async Task Pause(string gid)
        {
            await Aria2.Pause(gid);
            var task = DownloadTask(gid);
            if (task.Status == DownloadStatus.Active)
            {
                task.Status = DownloadStatus.Paused;
                await Update(task);
            }
        }

        public async Task Unpause(string gid)
        {
            await Aria2.Unpause(gid);
            var task = DownloadTask(gid);
            if (task.Status == DownloadStatus.Paused)
            {
                task.Status = DownloadStatus.Active;
                await Update(task);
            }
        }
        
        public async Task Restart(string gid)
        {
            var task = DownloadTask(gid);
            if (task == null)
                return;
            if (task.Status == DownloadStatus.Error)
            {
                await Delete(gid);
                await StartDownload(task.Link);
            }
        }

        public async Task Delete(string gid)
        {
            var task = DownloadTask(gid);
            if (task == null)
                return;

            await DeleteAsync(task);
        }

        protected async Task SetError(string gid)
        {
            var task = DownloadTask(gid);
            task.Status = DownloadStatus.Error;
            await Update(task);
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
