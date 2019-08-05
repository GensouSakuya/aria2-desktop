using Avalonia.Diagnostics.ViewModels;
using Avalonia.Media.Imaging;
using GensouSakuya.Aria2.Desktop.Model;
using GensouSakuya.Aria2.Desktop.Shell.Helper;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class DownloadTaskItemViewModel: ViewModelBase
    {
        public Bitmap Img { get; set; } = ImgResourceHelper.FileIcon;
        public string TaskName { get; set; } = "新任务";
        public decimal Progress { get; set; } = 0m;

        public decimal DownloadSpeed { get; set; } = 0m;

        public string DownloadSpeedStr
        {
            get
            {
                if (DownloadSpeed <= 0)
                {
                    return "";
                }

                return Tools.ToStringWithUnit(DownloadSpeed) + "/s";
            }
        }
    }

    internal static class DownloadTaskListViewModelExtension
    {
        public static DownloadTaskItemViewModel ConvertToViewModel(this DownloadTask task)
        {
            return new DownloadTaskItemViewModel
            {
                TaskName = task.TaskName, Progress = task.CompletePercent, DownloadSpeed = task.DownloadSpeed
            };
        }
    }
}
