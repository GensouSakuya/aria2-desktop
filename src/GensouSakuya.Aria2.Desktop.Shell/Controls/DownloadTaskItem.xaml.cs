using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class DownloadTaskItem : UserControl
    {
        public DownloadTaskItem()
        {
            this.InitializeComponent();

            Initialized += (sender, e) =>
            {
                PointerEnter += NewPointerEnter;
                PointerLeave += NewPointerLeave;
            };

            Background = new SolidColorBrush(Colors.Gray, 0);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void NewPointerEnter(object sender, PointerEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gray,0.24);
        }

        private void NewPointerLeave(object sender, PointerEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gray, 0);
        }
    }
}
