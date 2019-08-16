using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GensouSakuya.Aria2.Desktop.Shell.Controls;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using GensouSakuya.Aria2.Desktop.Shell.ViewModels;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.Views
{
    public class PageList : ReactiveUserControl<PageListViewModel>
    {
        public IconButton Icon2 => this.FindControl<IconButton>("ico"); 
        public PageList()
        {
            this.WhenActivated(disposal =>
            {
                ViewModel = new PageListViewModel();
                ViewModel.IconModel.Click = async () => { await DialogHelper.OpenFileDialog(null); };

                this.OneWayBind(ViewModel, p => p.IconModel, p => p.Icon2.ViewModel)
                    .DisposeWith(disposal);
            });

            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
