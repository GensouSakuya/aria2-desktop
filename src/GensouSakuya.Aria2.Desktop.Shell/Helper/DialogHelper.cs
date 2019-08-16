using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using GensouSakuya.Aria2.Desktop.Shell.Views;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{
    public class DialogHelper
    {
        private static Window MainWindow => AvaloniaLocator.Current.GetService<MainWindow>();

        public static void ShowDialog(Window window)
        {
            window.ShowDialog(MainWindow);
        }

        public static async Task<string[]> OpenFileDialog(List<FileDialogFilter> filters = null)
        {
            var dialog = new OpenFileDialog();
            dialog.AllowMultiple = false;
            if (filters != null)
            {
                dialog.Filters.AddRange(filters);
            }
            return await dialog.ShowAsync(MainWindow);
        }
    }
}
