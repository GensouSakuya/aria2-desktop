using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class PageListViewModel : ViewModelBase
    {
        public PageListViewModel()
        {
            IconModel = new IconButtonViewModel();
        }
        private IconButtonViewModel iconModel;

        public IconButtonViewModel IconModel 
        {
            get { return iconModel; }
            set { this.RaiseAndSetIfChanged(ref iconModel, value); }
        }
    }
}
