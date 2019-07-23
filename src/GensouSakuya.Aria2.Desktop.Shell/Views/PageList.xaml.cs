using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GensouSakuya.Aria2.Desktop.Shell.Views
{
    public class PageList : UserControl
    {
        public PageList()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
