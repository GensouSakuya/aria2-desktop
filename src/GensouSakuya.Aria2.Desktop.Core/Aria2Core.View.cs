using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        public List<DownloadTask> DownloadTasks { get; private set; }
    }
}
