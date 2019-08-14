using Avalonia.Threading;
using DynamicData;
using DynamicData.Binding;
using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class DownloadTaskListViewModel : ViewModelBase
    {
        private volatile object refreshLock = new object();

        public DownloadTaskListViewModel()
        {
            result = new ObservableCollectionExtended<DownloadTaskItemViewModel>(Aria2Helper.Aria2.DownloadTasks
                .Select(p => p.ConvertToViewModel()).ToList());
            result.ToObservableChangeSet().Bind(out _tasks).Subscribe();
            DispatcherTimer.Run(() =>
            {
                if (Aria2Helper.Aria2 == null)
                    return false;

                lock (refreshLock)
                {
                    var tasks = Aria2Helper.Aria2.DownloadTasks;
                    var allGid = tasks.Select(p => p.GID).Union(result.Select(p => p.GID)).ToList();

                    allGid.ForEach(gid =>
                    {
                        var oriTask = result.FirstOrDefault(p => p.GID == gid);
                        var ar2Task = tasks.Find(p => p.GID == gid);

                        if (oriTask == null)
                        {
                            result.Add(ar2Task.ConvertToViewModel());
                            return;
                        }

                        if (ar2Task == null)
                        {
                            result.Remove(oriTask);
                            return;
                        }

                        oriTask.TaskName = ar2Task.TaskName;
                        oriTask.DownloadSpeed = ar2Task.DownloadSpeed;
                        oriTask.Status = ar2Task.Status;
                        oriTask.TotalSize = ar2Task.TotalLength;
                        oriTask.CompleteSize = ar2Task.CompletedLength;
                    });

                    //ViewModelListMerge(_tasks, tasks);
                }

                return true;
            }, new TimeSpan(0, 0, 0, 0, 500));
        }
        
        private readonly ReadOnlyObservableCollection<DownloadTaskItemViewModel> _tasks;
        public ReadOnlyObservableCollection<DownloadTaskItemViewModel> Tasks => _tasks;

        public ObservableCollectionExtended<DownloadTaskItemViewModel> result { get; set; }
    }
}
