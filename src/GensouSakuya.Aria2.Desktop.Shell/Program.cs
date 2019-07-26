using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using GensouSakuya.Aria2.Desktop.Shell.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Views;

namespace GensouSakuya.Aria2.Desktop.Shell
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp().Start(AppMain, args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug()
                .UseReactiveUI();

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain(Application app, string[] args)
        {
            //TODO:获取配置信息并注入Aria2Core
            
            //_configFilePath = configFilePath ?? ConfigConst.Default_Config_File_Path;

            MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };

            app.Run(MainWindow);
        }

        public static MainWindow MainWindow { get; set; }
    }
}
