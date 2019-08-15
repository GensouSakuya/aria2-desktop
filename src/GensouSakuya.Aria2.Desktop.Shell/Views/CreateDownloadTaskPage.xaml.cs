using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace GensouSakuya.Aria2.Desktop.Shell.Views
{
    public class CreateDownloadTaskPage : Window
    {
        public CreateDownloadTaskPage()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.PointerPressed += DragEvent;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DragEvent(object sender, PointerPressedEventArgs e)
        {
            try
            {
                this.BeginMoveDrag();
            }
            catch { }
        }
    }
}
