using System;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using GensouSakuya.Aria2.Desktop.Model;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using GensouSakuya.Aria2.Desktop.Shell.ViewModels;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class DownloadTaskItemViewModel: ViewModelBase, IDataMergable
    {
        #region Property

        public string GID { get; set; }
        public string TaskName { get; set; } = "新任务";
        public DownloadStatus Status { get; set; }
        public decimal CompleteSize { get; set; }
        public decimal TotalSize { get; set; }

        #endregion

        public Bitmap Img { get; set; } = ImgResourceHelper.FileIcon;


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

        public string TotalSizeStr => $"{Tools.ToStringWithUnit(TotalSize)}";

        public decimal Progress { get; set; } = 0m;
        public string ProgressStr => $"{Math.Round(Progress, 2).ToString("0.00")}%";

        public decimal DownloadSpeed { get; set; } = 0m;
        public string DownloadSpeedStr => Status != DownloadStatus.Active ? "" : Tools.ToStringWithUnit(DownloadSpeed) + "/s";

        public ObservableCollection<ToolButtonViewModel> Buttons
        {
            get
            {
                var buttons = new ObservableCollection<ToolButtonViewModel>();

                if (Status == DownloadStatus.Active)
                {
                    buttons.Add(new ToolButtonViewModel
                    {
                        Img = ImgResourceHelper.PauseDownloadIcon,
                        Click = async () =>
                        {
                            await Aria2Helper.Aria2.Pause(GID);
                        }
                    });
                }
                else if (Status == DownloadStatus.Paused)
                {
                    buttons.Add(new ToolButtonViewModel
                    {
                        Img = ImgResourceHelper.StartDownloadIcon,
                        Click = async () =>
                        {
                            await Aria2Helper.Aria2.Unpause(GID);
                        }
                    });
                }
                else if (Status == DownloadStatus.Error)
                {
                    buttons.Add(new ToolButtonViewModel
                    {
                        Img = ImgResourceHelper.RestartDownloadTaskIcon,
                        Click = async () =>
                        {
                            await Aria2Helper.Aria2.Restart(GID);
                        }
                    });
                }

                buttons.Add(new ToolButtonViewModel
                {
                    Img = ImgResourceHelper.DeleteDownloadTaskIcon,
                    Click = async () =>
                    {
                        await Aria2Helper.Aria2.Delete(GID);
                    }
                });

                return buttons;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is DownloadTaskItemViewModel model &&
                   GID == model.GID &&
                   Status == model.Status &&
                   CompleteSize == model.CompleteSize &&
                   TotalSize == model.TotalSize;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GID, Status, CompleteSize, TotalSize);
        }

        public object GetKey() => GID;

        public void Update(IDataMergable data)
        {
            if (data == null || !(data is DownloadTaskItemViewModel))
                return;
            var newTask = data as DownloadTaskItemViewModel;
            Status = newTask.Status;
            CompleteSize = newTask.CompleteSize;
            TotalSize = newTask.TotalSize;
            TaskName = newTask.TaskName;
        }
    }

    internal static class DownloadTaskListViewModelExtension
    {
        public static DownloadTaskItemViewModel ConvertToViewModel(this DownloadTask task)
        {
            return new DownloadTaskItemViewModel
            {
                GID = task.GID,
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
