using System;
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

        public DownloadStatus Status { get; set; }

        public decimal CompleteSize { get; set; }
        public decimal LeftSize => TotalSize - CompleteSize;
        public decimal LeftSeconds => DownloadSpeed > 0 ? LeftSize / DownloadSpeed : -1;

        public string LeftTime
        {
            get
            {
                if (Status != DownloadStatus.Active)
                {
                    return "";
                }

                if (LeftSeconds <= 0)
                {
                    return "--:--:--";
                }
                else
                {
                    var timesplan = new TimeSpan(0, 0, (int) LeftSeconds);
                    return
                        $"{Math.Floor(timesplan.TotalHours).ToString("00")}:{Math.Floor(timesplan.TotalMinutes).ToString("00")}:{Math.Floor(timesplan.TotalSeconds).ToString("00")}";
                }
            }
        }

        public decimal TotalSize { get; set; }
        public string TotalSizeStr => $"{Tools.ToStringWithUnit(TotalSize)}";

        public decimal Progress { get; set; } = 0m;
        public string ProgressStr => $"{Math.Round(Progress, 2).ToString("0.00")}%";

        public decimal DownloadSpeed { get; set; } = 0m;
        public string DownloadSpeedStr => Status != DownloadStatus.Active ? "" : Tools.ToStringWithUnit(DownloadSpeed) + "/s";

    }

    internal static class DownloadTaskListViewModelExtension
    {
        public static DownloadTaskItemViewModel ConvertToViewModel(this DownloadTask task)
        {
            return new DownloadTaskItemViewModel
            {
                TaskName = task.TaskName,
                Progress = task.CompletePercent,
                DownloadSpeed = task.DownloadSpeed,
                Status = task.Status,
                TotalSize = task.TotalLength,
                CompleteSize = task.CompletedLength
            };
        }
    }
}
