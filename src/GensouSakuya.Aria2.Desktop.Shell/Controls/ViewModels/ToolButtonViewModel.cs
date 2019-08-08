using System;
using Avalonia.Diagnostics.ViewModels;
using Avalonia.Media.Imaging;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class ToolButtonViewModel: ViewModelBase
    {
        public Action Click { get; set; }
        public object Img { get; set; }
        public string Content { get; set; } = "";
    }
}
