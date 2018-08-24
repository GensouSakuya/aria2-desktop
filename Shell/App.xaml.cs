using System.Windows;

namespace Shell
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void StartUp(object sender, StartupEventArgs e)
        {
            MainManager.StartUp();
        }
    }
}
