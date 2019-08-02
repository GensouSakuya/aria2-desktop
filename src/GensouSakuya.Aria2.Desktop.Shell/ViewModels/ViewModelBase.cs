using Avalonia.Controls;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public Window Self { get; set; }
    }
}
