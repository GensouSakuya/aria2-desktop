using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{
    public static class BitmapHelper
    {
        public static Bitmap GetImg(string assertPath)
        {
            var uri = new Uri($"avares://{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}/{assertPath}");

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            return new Bitmap(assets.Open(uri));
        }
    }
}
