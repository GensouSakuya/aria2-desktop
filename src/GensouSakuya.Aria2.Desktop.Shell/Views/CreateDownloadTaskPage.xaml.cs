using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GensouSakuya.Aria2.Desktop.Shell.ViewModels;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.Views
{
    public class CreateDownloadTaskPage : ReactiveWindow<CreateDownloadTaskPageViewModel>
    {
        public TextBox DownloadUrlBox => this.FindControl<TextBox>("downloadBox");
        public Button AddTorrentButton => this.FindControl<Button>("addTorrentButton");
        public Button AddMagnetPrefixButton => this.FindControl<Button>("addMagnetPrefix");
        public CreateDownloadTaskPage()
        {
            this.WhenActivated(disposables =>
            {
                this.PointerPressed += DragEvent;
                
                this.Bind(ViewModel, p => p.DownloadLink, p => p.DownloadUrlBox.Text).DisposeWith(disposables);
                this.BindCommand(ViewModel, p => p.SubmitTorrent, p => p.AddTorrentButton);
                this.BindCommand(ViewModel, p => p.AddMagnetPrefixCommand, p => p.AddMagnetPrefixButton);
            });

            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DragEvent(object sender, PointerPressedEventArgs e)
        {
            try
            {
                this.BeginMoveDrag();
            }
            catch { }
        }
    }
}
