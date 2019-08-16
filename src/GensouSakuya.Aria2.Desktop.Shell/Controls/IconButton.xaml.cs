using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GensouSakuya.Aria2.Desktop.Resource;
using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class IconButton : ReactiveUserControl<IconButtonViewModel>
    {
        private const double DefaultOpacity = 0.7;
        private static readonly TimeSpan DefaultAnimationDuration = new TimeSpan(0, 0, 0, 0, 200);

        public Image ImageButton => this.FindControl<Image>("iconButton");

        public bool CanPress { get; set; } = true;
        public bool IsProcessing { get; set; } = false;
        public IconButton()
        {
            this.WhenActivated(disposables =>
            {
                this.ImageButton.Width = this.Width;
                this.ImageButton.Height = this.Height;
                this.ImageButton.Source = BitmapHelper.GetImg(Icons.Stop);

                this.OneWayBind(ViewModel, p => p.Img, p => p.ImageButton.Source).DisposeWith(disposables);
                
                
                Initialized += (sender, e) =>
                {
                    Tapped += NewTappedEventAsync;
                    PointerEnter += NewPointerEnter;
                    PointerLeave += NewPointerLeave;
                };
                Opacity = DefaultOpacity;
                Transitions.Add(new DoubleTransition()
                {
                    Property = OpacityProperty, Duration = DefaultAnimationDuration
                });
            });

            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void NewTappedEventAsync(object sender, RoutedEventArgs e)
        {
            if (!CanPress)
                return;
            BeforeClick();
            if (ViewModel?.Click != null)
            {
                await ViewModel.Click().ContinueWith((task) =>
                {
                    AfterClick();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void BeforeClick()
        {
            IsProcessing = true;
            CanPress = false;
            OpacityChange(0.2);
        }

        private void AfterClick()
        {
            IsProcessing = false;
            CanPress = true;
            OpacityResume();
        }

        private void OpacityChange(double value)
        {
            Opacity = value;
        }

        private void OpacityResume()
        {
            OpacityChange(DefaultOpacity);
        }

        private void NewPointerEnter(object sender, PointerEventArgs e)
        {
            if (!IsProcessing)
            {
                OpacityChange(1);
            }
        }

        private void NewPointerLeave(object sender, PointerEventArgs e)
        {
            if (!IsProcessing)
            {
                OpacityResume();
            }
        }
    }
}
