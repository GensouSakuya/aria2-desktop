using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shell
{
    public static class DownloadTaskManager
    {
        private static DownloadStatusInfoList _allTasks => new DownloadStatusInfoList(_downloadingTasks.List.Union(_otherTasks.List));
        private static DownloadStatusInfoList _downloadingTasks = new DownloadStatusInfoList();
        private static DownloadStatusInfoList _otherTasks = new DownloadStatusInfoList();

        public static async Task RefreshTasks()
        {
            var allTasks = await Task.Factory.StartNew(() => MainManager.Main.GetAllTasks());
            _downloadingTasks = new DownloadStatusInfoList(allTasks.Where(p => p.Status == DownloadStatus.Active));
            _otherTasks = new DownloadStatusInfoList(allTasks.Where(p => p.Status != DownloadStatus.Active));
        }

        public static List<DownloadStatusInfo> GetAllCompletedTask()
        {
            return _allTasks.GetWithCondition(p => p.Status == DownloadStatus.Complete);
        }

        public static List<DownloadStatusInfo> GetDownloadingTasks()
        {
            return _downloadingTasks.List;
        }

        static DownloadTaskManager()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var downloadingTasks = _downloadingTasks.List;
                        downloadingTasks.ForEach(p =>
                        {
                            p = MainManager.Main.GetTaskDownloadStatus(p.GID);
                        });
                        var otherTasks = downloadingTasks.Where(p => p.Status != DownloadStatus.Active).ToList();
                        if (otherTasks.Any())
                        {
                            _downloadingTasks.RemoveAll(p => otherTasks.Select(q => q.GID).Contains(p.GID));
                            _otherTasks.AddRange(otherTasks);
                        }
                    }
                    catch(Exception ex)
                    {
                        
                    }
                    Thread.Sleep(200);
                }
            });
        }
    }
}
