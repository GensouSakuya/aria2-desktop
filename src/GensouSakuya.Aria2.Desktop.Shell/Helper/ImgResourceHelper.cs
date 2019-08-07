using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{
    public static class ImgResourceHelper
    {
        public static Bitmap DownloadIcon => GetImg("Icons/icon-wm10-download.png");

        public static Bitmap FileIcon => GetImg("Icons/icon-wm10-page.png");

        public static Bitmap StartDownloadIcon => GetImg("Icons/icon-wm10-play.png");

        public static Bitmap PauseDownloadIcon => GetImg("Icons/icon-wm10-play.png");

        public static Bitmap DeleteDownloadTaskIcon => GetImg("Icons/icon-wm10-play.png");

        private static Bitmap GetImg(string assertPath)
        {
            var uri = new Uri($"avares://{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}/{assertPath}");

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            return new Bitmap(assets.Open(uri));
        }
    }
}
