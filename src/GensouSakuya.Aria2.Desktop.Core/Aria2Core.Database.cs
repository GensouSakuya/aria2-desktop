using System;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.Desktop.Model;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        public List<DownloadTask> GetList()
        {
            using (var context = new DbContext())
            {
                return context.DownloadTasks.ToList();
            }
        }
    }
}
