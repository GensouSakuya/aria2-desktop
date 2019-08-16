using System;
using System.Threading.Tasks;
using Avalonia.Diagnostics.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Helper;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class IconButtonViewModel: ViewModelBase
    {
        public Func<Task> Click { get; set; }
        public object Img { get; set; } = BitmapHelper.GetImg(Resource.Icons.Stop);
    }
}
