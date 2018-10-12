using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shell
{
    public static class DownloadTaskManager
    {
        private static List<DownloadStatusInfo> _allTasks => _downloadingTasks.Union(_otherTasks).ToList();
        private static List<DownloadStatusInfo> _downloadingTasks = new List<DownloadStatusInfo>();
        private static List<DownloadStatusInfo> _otherTasks = new List<DownloadStatusInfo>();

        public static async Task RefreshTasks()
        {
            var allTasks = await Task.Factory.StartNew(() => MainManager.Main.GetAllTasks());

        }

        public static List<DownloadStatusInfo> GetAllCompletedTask()
        {
            return _allTasks.Where(p => p.Status == DownloadStatus.Complete).ToList();
        }

        public static List<DownloadStatusInfo> GetDownloadingTasks()
        {
            return _downloadingTasks;
        }

        static DownloadTaskManager()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        _downloadingTasks.ForEach(p =>
                        {
                            p = MainManager.Main.GetTaskDownloadStatus(p.GID);
                        });
                        var otherTasks = _downloadingTasks.Where(p => p.Status != DownloadStatus.Active).ToList();
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
