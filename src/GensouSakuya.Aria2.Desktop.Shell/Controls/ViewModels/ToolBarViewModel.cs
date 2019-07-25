using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Diagnostics.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using GensouSakuya.Aria2.Desktop.Shell.Views;
using GensouSakuya.Aria2.Desktop.Shell.Views.Other;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels
{
    public class ToolBarViewModel: ViewModelBase
    {
        public ToolBarViewModel()
        {
            CreateDownload = new ToolButtonViewModel
            {
                Content ="新建下载任务",
                Click = () =>
                {
                    DialogHelper.ShowDialog(new DownloadPage());
                }
            };
        }

        public ToolButtonViewModel CreateDownload { get; set; }

    }
}
