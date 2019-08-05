using GensouSakuya.Aria2.Desktop.Model;
using Microsoft.EntityFrameworkCore;
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

        protected void DbInit()
        {
            ///Context考虑改为全局唯一
            using (var context = new Model.DbContext())
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }

        protected void InitDownloadTasks()
        {
            DbInit();
            AllDownloadTask = GetList();
        }

        private static readonly List<DownloadStatus> ListeningStatus = new List<DownloadStatus>
        {
            DownloadStatus.Active, DownloadStatus.Paused, DownloadStatus.Waiting
        };

        public List<DownloadTask> AllDownloadTask { get; set; } = new List<DownloadTask>();

        public void RefreshProcessingTasks()
        {
            lock (_taskListLock)
            {
                AllDownloadTask.Where(p => ListeningStatus.Contains(p.Status)).ToList().ForEach(async p =>
                {
                    p = await GetTask(p.GID);
                    UpdateTask(p);
                });
            }
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
                AllDownloadTask.Add(newTask);
            }
            InsertTask(newTask);
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
