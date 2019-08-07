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
        public DownloadTaskListViewModel()
        {
            Tasks = new ObservableCollection<DownloadTaskItemViewModel>();
            DispatcherTimer.Run(() =>
            {
                if (Aria2Helper.Aria2 == null)
                    return false;
                var tasks = new ObservableCollection<DownloadTaskItemViewModel>();

                Aria2Helper.Aria2.DownloadTaskView.Select(p => p.ConvertToViewModel()).ToList().ForEach(p => tasks.Add(p));
                Tasks = tasks;
                return true;
            }, new TimeSpan(1000000));
        }

        public ObservableCollection<DownloadTaskItemViewModel> Tasks
        {
            get => _tasks;
            set => this.RaiseAndSetIfChanged(ref _tasks, value);
        }

        private ObservableCollection<DownloadTaskItemViewModel> _tasks;
    }
}
