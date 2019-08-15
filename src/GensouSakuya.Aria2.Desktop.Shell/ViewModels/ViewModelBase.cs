using Avalonia.Controls;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class ViewModelBase : ReactiveObject, ISupportsActivation
    {
        public Window Self { get; set; }
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
