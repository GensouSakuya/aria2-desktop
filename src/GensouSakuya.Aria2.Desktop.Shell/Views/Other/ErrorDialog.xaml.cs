using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GensouSakuya.Aria2.Desktop.Shell.Enums;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using GensouSakuya.Aria2.Desktop.Shell.ViewModels.Other;

namespace GensouSakuya.Aria2.Desktop.Shell.Views.Other
{
    public class ErrorDialog : Window
    {
        public ErrorDialog()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static void ShowDialogPage(string message, ErrorLevel errorLevel = ErrorLevel.Info)
        {
            DialogHelper.ShowDialog(new ErrorDialog()
            {
                DataContext = new ErrorDialogViewModel
                {
                    Message = message, ErrorLevel = errorLevel
                }
            });
        }
    }
}
