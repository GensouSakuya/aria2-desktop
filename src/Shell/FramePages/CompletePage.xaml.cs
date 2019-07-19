using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Shell
{
    /// <summary>
    /// CompletePage.xaml 的交互逻辑
    /// </summary>
    public partial class CompletePage : Page
    {
        private DispatcherTimer _dispatcherTimer;
        private List<DownloadStatusInfo> _completedTasks = new List<DownloadStatusInfo>();

        public CompletePage()
        {
            InitializeComponent();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _completedTasks = DownloadTaskManager.GetAllCompletedTask();
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _completedTasks = DownloadTaskManager.GetAllCompletedTask();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Start();
        }
        

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Stop();
        }
    }
}
