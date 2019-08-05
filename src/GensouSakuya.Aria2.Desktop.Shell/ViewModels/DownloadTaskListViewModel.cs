using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;
using System.Collections.ObjectModel;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class DownloadTaskListViewModel: ViewModelBase
    {
        public DownloadTaskListViewModel()
        {
            Tasks = new ObservableCollection<DownloadTaskItemViewModel>();
            Tasks.Add(new DownloadTaskItemViewModel
            {
                Progress = 20, TaskName = "新任务1"
            });Tasks.Add(new DownloadTaskItemViewModel
            {
                Progress = 20.2m, TaskName = "新任务2"
            });Tasks.Add(new DownloadTaskItemViewModel
            {
                Progress = 70, TaskName = "新任务3"
            });
        }
        
        public ObservableCollection<DownloadTaskItemViewModel> Tasks { get; set; }
    }
}
