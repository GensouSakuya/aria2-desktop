using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class ToolButton : UserControl
    {
        public ToolButton()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
