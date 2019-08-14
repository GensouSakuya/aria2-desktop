using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GensouSakuya.Aria2.Desktop.Shell.ViewModels;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.Views
{
    public class DownloadTaskList : ReactiveUserControl<DownloadTaskListViewModel>
    {
        public ItemsControl Tasks => this.FindControl<ItemsControl>("tasks");
        public DownloadTaskList()
        {
            ViewModel = new DownloadTaskListViewModel();
            this.WhenActivated(disposableRegistration =>
            {
            });
            this.InitializeComponent();
            Tasks.Items = ViewModel.Tasks;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
