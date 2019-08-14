using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using GensouSakuya.Aria2.Desktop.Model;
using GensouSakuya.Aria2.Desktop.Resource;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using GensouSakuya.Aria2.Desktop.Shell.ViewModels;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class DownloadTaskItemViewModel: ViewModelBase, IDataMergable,ISupportsActivation
    {
        public ViewModelActivator Activator { get; }

        public DownloadTaskItemViewModel()
        {
            Activator = new ViewModelActivator();

            progress = this.WhenAnyValue(p => p.CompleteSize, p => p.TotalSize).DistinctUntilChanged()
                .Select(p => TotalSize == 0 ? 0 : Math.Round((decimal) CompleteSize / TotalSize * 100, 2)).ToProperty(this, p => p.Progress);
            leftSize = this.WhenAnyValue(p => p.TotalSize, p => p.CompleteSize).DistinctUntilChanged()
                .Select(p => TotalSize - CompleteSize)
                .ToProperty(this, p => p.LeftSize);
            leftSeconds = this.WhenAnyValue(p => p.Status, p => p.DownloadSpeed, p=>p.LeftSize).DistinctUntilChanged()
                .Select(p =>
                {
                    if (Status != DownloadStatus.Active)
                        return -2;
                    if (DownloadSpeed <= 0)
                    {
                        return -1;
                    }
                    return LeftSize / DownloadSpeed;
                })
                .ToProperty(this, p => p.LeftSeconds);

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                /* handle activation */
                Disposable
                    .Create(() => { /* handle deactivation */ })
                    .DisposeWith(disposables);
            });
        }

        #region Property

        public string GID { get; set; }
        private string _taskeName;
        public string TaskName 
        {
            get => _taskeName;
            set => this.RaiseAndSetIfChanged(ref _taskeName, value);
        }
        private DownloadStatus _status;
        public DownloadStatus Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
        private decimal _completeSize;
        public decimal CompleteSize
        {
            get => _completeSize;
            set => this.RaiseAndSetIfChanged(ref _completeSize, value);
        }

        private decimal _totalSize;
        public decimal TotalSize
        {
            get => _totalSize;
            set => this.RaiseAndSetIfChanged(ref _totalSize, value);
        }

        private decimal _downloadSpeed = 0m;
        public decimal DownloadSpeed
        {
            get => _downloadSpeed;
            set => this.RaiseAndSetIfChanged(ref _downloadSpeed, value);
        }

        #endregion

        #region View
        readonly ObservableAsPropertyHelper<decimal> progress;
        public decimal Progress => progress.Value;

        
        readonly ObservableAsPropertyHelper<decimal> leftSeconds;
        public decimal LeftSeconds => leftSeconds.Value;
        
        
        readonly ObservableAsPropertyHelper<decimal> leftSize;
        public decimal LeftSize => leftSize.Value;
        #endregion

        public IBitmap Img { get; set; } = BitmapHelper.GetImg(Icons.File);


        //public decimal LeftSeconds => Status != DownloadStatus.Active ? -2 : DownloadSpeed <= 0 ? -1 : LeftSize / DownloadSpeed;
        
        public class LeftTimeConverter : FromDecimalConverter
        {
            public override bool TryConvert(object from, Type toType, object conversionHint, out object result)
            {
                try
                {
                    var leftTime = (decimal)from;
                    if (leftTime == -2)
                    {
                        result = "";
                    }
                    else if (leftTime == -1)
                    {
                        result = "--:--:--";
                    }
                    else
                    {
                        var timesplan = new TimeSpan(0, 0, (int) leftTime);
                        
                        result = $"{Math.Floor(timesplan.TotalHours).ToString("00")}:{timesplan.Minutes.ToString("00")}:{timesplan.Seconds.ToString("00")}";
                    }
                }
                catch
                {
                    result = null;
                    return false;
                }

                return true;
            }
        }

        public class TotalSizeConverter : FromDecimalConverter
        {
            public override bool TryConvert(object from, Type toType, object conversionHint, out object result)
            {
                try
                {
                    result = $"{Tools.ToStringWithUnit((decimal) from)}";
                }
                catch
                {
                    result = null;
                    return false;
                }

                return true;
            }
        }

        public class ProgressConverter : FromDecimalConverter
        {
            public override bool TryConvert(object from, Type toType, object conversionHint, out object result)
            {
                try
                {
                    result = $"{Math.Round((decimal) from, 2).ToString("0.00")}%";
                }
                catch
                {
                    result = null;
                    return false;
                }

                return true;
            }
        }
        public class DownloadSpeedConverter : FromDecimalConverter
        {
            public override bool TryConvert(object from, Type toType, object conversionHint, out object result)
            {
                try
                {
                    var speed = (decimal)from;
                    if (speed < 0)
                        result = "";
                    else
                    {
                        result = Tools.ToStringWithUnit(speed) + "/s";
                    }
                }
                catch
                {
                    result = null;
                    return false;
                }

                return true;
            }
        }

        public ObservableCollection<ToolButtonViewModel> Buttons
        {
            get
            {
                var buttons = new ObservableCollection<ToolButtonViewModel>();

                if (Status == DownloadStatus.Active)
                {
                    buttons.Add(new ToolButtonViewModel
                    {
                        Img = BitmapHelper.GetImg(Icons.Pause),
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
                        Img = BitmapHelper.GetImg(Icons.Start),
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
                        Img = BitmapHelper.GetImg(Icons.Refresh),
                        Click = async () =>
                        {
                            await Aria2Helper.Aria2.Restart(GID);
                        }
                    });
                }

                buttons.Add(new ToolButtonViewModel
                {
                    Img = BitmapHelper.GetImg(Icons.Delete),
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
                DownloadSpeed = task.DownloadSpeed,
                Status = task.Status,
                TotalSize = task.TotalLength,
                CompleteSize = task.CompletedLength
            };
        }
    }
}
