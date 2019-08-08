using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GensouSakuya.Aria2.Desktop.Model;
using Microsoft.EntityFrameworkCore;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        public List<DownloadTask> DownloadTasks
        {
            get
            {
                using (var context = new Model.DbContext())
                {
                    return context.DownloadTasks.ToList();
                }
            }
        }

        public DownloadTask DownloadTask(string gid)
        {
            using (var context = new Model.DbContext())
            {
                return context.DownloadTasks.Find(gid);
            }
        }

        protected void DbInit()
        {
            using (var context = new Model.DbContext())
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void Add<T>(T entity) where T : class
        {
            using (var context = new Model.DbContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Update<T>(T oriItem,T newItem) where T:class
        {
            using (var context = new Model.DbContext())
            {
                context.Entry<T>(oriItem).State = EntityState.Detached;
                context.Entry<T>(newItem).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Update<T>(T entity) where T : class
        {
            using (var context = new Model.DbContext())
            {
                context.Entry<T>(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public async Task Delete<T>(T entity) where T : class
        {
            using (var context = new Model.DbContext())
            {
                context.Entry<T>(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}
