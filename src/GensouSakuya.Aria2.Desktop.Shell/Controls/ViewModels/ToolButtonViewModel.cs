using System;
using Avalonia.Diagnostics.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Helper;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class ToolButtonViewModel: ViewModelBase
    {
        public Action Click { get; set; }
        public object Img { get; set; } = BitmapHelper.GetImg(Resource.Icons.Stop);
        public string Content { get; set; } = "";
    }
}
