using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class DownloadTaskItem : UserControl
    {
        private const double DefaultBackgroundOpacity = 0;
        private const double SelectedBackgroundOpacity = 0.3;
        private Border BackgroundBorder => this.FindControl<Border>("backgroundBorder") as Border;

        public DownloadTaskItem()
        {
            this.InitializeComponent();

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
