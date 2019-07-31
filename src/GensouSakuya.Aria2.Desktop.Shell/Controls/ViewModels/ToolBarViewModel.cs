using System.Collections.ObjectModel;
using Avalonia.Diagnostics.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using GensouSakuya.Aria2.Desktop.Shell.Views;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class ToolBarViewModel: ViewModelBase
    {
        public ToolBarViewModel()
        {
            Buttons = new ObservableCollection<ToolButtonViewModel>(new []
            {
                new ToolButtonViewModel
                {
                    Img = BitmapHelper.GetImg("Icons/icon-wm10-download.png"),
                    Click = () =>
                    {
                        DialogHelper.ShowDialog(new DownloadPage());
                    }
                }
            });
        }

        public ObservableCollection<ToolButtonViewModel> Buttons { get; set; }
    }
}
