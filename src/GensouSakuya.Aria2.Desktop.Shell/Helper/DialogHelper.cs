using Avalonia.Controls;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{
    public class DialogHelper
    {
        public static void ShowDialog(Window window)
        {
            window.ShowDialog(Program.MainWindow);
        }
    }
}
