using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
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
            DownloadTasks = GetDownloadTasks;
        }

        private static readonly List<DownloadStatus> ListeningStatus = new List<DownloadStatus>
        {
            DownloadStatus.Active, DownloadStatus.Paused, DownloadStatus.Waiting
        };

        public async Task RefreshProcessingTasksAsync()
        {
            foreach (var task in DownloadTasks)
            {
                if (!ListeningStatus.Contains(task.Status))
                    continue;
                var entity = await GetTask(task.GID);
                if (entity == null)
                {
                    await SetError(task.GID);
                    task.Status = DownloadStatus.Error;
                    continue;
                }
                else if (task != entity)
                {
                    await UpdateAsync(task, entity);
                    task.Update(entity);
                }
            }
        }

        public async Task<DownloadTask> GetTask(string gid)
        {
            return (await Aria2.TellStatus(gid))?.Convert();
        }

        public async Task StartDownload(string url)
        {
            var newGid = await StartHttpDownload(url);
            await CreateTask(newGid, url);
        }

        public async Task NewTorrentDownload(string filePath)
        {
            var newGid = await StartTorrentFileDownload(filePath);
            await CreateTask(newGid, filePath);
        }

        private async Task CreateTask(string gid, string link)
        {
            var newTask = await GetTask(gid);
            newTask.Link = link;
            Add(newTask);
            DownloadTasks.Add(newTask);
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
            DownloadTasks.RemoveAll(p => p.GID == task.GID);
        }

        protected async Task SetError(string gid)
        {
            var task = DownloadTask(gid);
            task.Status = DownloadStatus.Error;
            await Update(task);
        }

        public void AutoRefresh()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        await RefreshProcessingTasksAsync();
                    }
                    catch
                    {
                        
                    }
                    Thread.Sleep(500);
                }
            });
        } 
    }
}
