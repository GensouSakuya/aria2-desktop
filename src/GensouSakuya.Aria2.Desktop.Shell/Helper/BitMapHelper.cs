using System.IO;
using Avalonia.Media.Imaging;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{
    public static class BitmapHelper
    {
        public static Bitmap GetImg(byte[] iconBytes)
        {
            return new Bitmap(new MemoryStream(iconBytes));
        }
    }
}
