using Avalonia;
using GensouSakuya.Aria2.Desktop.Core;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{
    public static class Aria2Helper
    {
        public static Aria2Core Aria2 => AvaloniaLocator.Current.GetService<Aria2Core>();
    }
}
