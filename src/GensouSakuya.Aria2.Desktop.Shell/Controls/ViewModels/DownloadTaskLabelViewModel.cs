using Avalonia.Diagnostics.ViewModels;
using Avalonia.Media.Imaging;
using GensouSakuya.Aria2.Desktop.Shell.Helper;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class DownloadTaskLabelViewModel: ViewModelBase
    {
        public Bitmap Img { get; set; } = ImgResourceHelper.FileIcon;
    }
}
