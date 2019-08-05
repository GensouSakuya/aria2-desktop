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

        //public void SaveChanges()
        //{
        //    using (var context = new DbContext())
        //    {
        //        context.SaveChanges();
        //    }
        //}

        public void InsertTask(DownloadTask task)
        {
            using (var context = new DbContext())
            {
                context.DownloadTasks.Add(task);
                context.SaveChanges();
            }
        }

        public void UpdateTask(DownloadTask task)
        {
            using (var context = new DbContext())
            {
                context.Entry<DownloadTask>(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
                context.SaveChanges();   
            }
        }
    }
}
