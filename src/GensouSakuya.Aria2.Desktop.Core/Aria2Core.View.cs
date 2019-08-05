using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        public List<DownloadTask> AllDownloadTask
        {
            get { return DbContext.DownloadTasks.ToList(); }
        }
    }
}
