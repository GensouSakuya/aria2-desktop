using Avalonia;
using Avalonia.Markup.Xaml;

namespace GensouSakuya.Aria2.Desktop.Shell
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
