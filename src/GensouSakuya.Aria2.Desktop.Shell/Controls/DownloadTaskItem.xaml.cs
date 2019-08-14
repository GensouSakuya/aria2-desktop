using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using ReactiveUI;
using static GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels.DownloadTaskItemViewModel;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class DownloadTaskItem :  ReactiveUserControl<DownloadTaskItemViewModel>
    {
        private const double DefaultBackgroundOpacity = 0;
        private const double SelectedBackgroundOpacity = 0.3;
        public Border BackgroundBorder => this.FindControl<Border>("backgroundBorder");
        public Image FileIconImg => this.FindControl<Image>("fileIcon");
        public TextBlock TaskNameBlock => this.FindControl<TextBlock>("taskNameBlock");
        public TextBlock TotalSizeBlock => this.FindControl<TextBlock>("totalSizeBlock");
        public ProgressBar DownloadProgressBar => this.FindControl<ProgressBar>("downloadProgressBar");
        public TextBlock DownloadProgressBlock => this.FindControl<TextBlock>("downloadProgressBlock");
        public TextBlock DownloadSpeedBlock => this.FindControl<TextBlock>("downloadSpeedBlock");
        public TextBlock DownloadLeftTimeBlock => this.FindControl<TextBlock>("downloadLeftTimeBlock");
        public ItemsControl Buttons => this.FindControl<ItemsControl>("buttons");

        public DownloadTaskItem()
        {
            this.InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, p => p.Img, p => p.FileIconImg.Source)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, p => p.TaskName, p => p.TaskNameBlock.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, p => p.TotalSize, p => p.TotalSizeBlock.Text, vmToViewConverterOverride: new TotalSizeConverter())
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, p => p.Progress, p => p.DownloadProgressBar.Value,
                    vmToViewConverterOverride: new DecimalToDoubleConverter()).DisposeWith(disposables);
                this.OneWayBind(ViewModel, p => p.Progress, p => p.DownloadProgressBlock.Text, vmToViewConverterOverride: new ProgressConverter())
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, p => p.DownloadSpeedStr, p => p.DownloadSpeedBlock.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, p => p.LeftSeconds, p => p.DownloadLeftTimeBlock.Text, vmToViewConverterOverride: new LeftTimeConverter())
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, p => p.Buttons, p => p.Buttons.Items);
                /* Handle view activation etc. */
            });

            Initialized += (sender, e) =>
            {
                PointerEnter += NewPointerEnter;
                PointerLeave += NewPointerLeave;
            };

            BackgroundBorder.Opacity = DefaultBackgroundOpacity;
            BackgroundBorder.Background = new SolidColorBrush(Colors.Gray);
            BackgroundBorder.Transitions.Add(new DoubleTransition
            {
                Duration = new System.TimeSpan(0, 0, 0, 0, 200), Property = Border.OpacityProperty
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void NewPointerEnter(object sender, PointerEventArgs e)
        {
            OpacitySelected();
        }

        private void NewPointerLeave(object sender, PointerEventArgs e)
        {
            OpacityResume();
        }

        private void OpacitySelected()
        {
            OpacityChange(SelectedBackgroundOpacity);
        }

        private void OpacityResume()
        {
            OpacityChange(DefaultBackgroundOpacity);
        }
        private void OpacityChange(double value)
        {
            if (BackgroundBorder != null)
            {
                BackgroundBorder.Opacity = value;
            }
        }

    }
}
