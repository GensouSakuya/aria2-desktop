using Avalonia.Threading;
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
            Tasks = new ObservableCollection<DownloadTaskItemViewModel>();
            DispatcherTimer.Run(() =>
            {
                if (Aria2Helper.Aria2 == null)
                    return false;

                lock (refreshLock)
                {
                    var tasks = new ObservableCollection<DownloadTaskItemViewModel>(Aria2Helper.Aria2.DownloadTaskView
                        .Select(p => p.ConvertToViewModel()).ToList());

                    ViewModelListMerge(_tasks, tasks);
                }

                return true;
            }, new TimeSpan(0, 0, 0, 0, 20));
        }

        public ObservableCollection<DownloadTaskItemViewModel> Tasks
        {
            get => _tasks;
            set => this.RaiseAndSetIfChanged(ref _tasks, value);
        }

        private ObservableCollection<DownloadTaskItemViewModel> _tasks;
    }
}
