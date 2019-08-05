using System;
using System.Linq;
using System.Threading.Tasks;
using GensouSakuya.Aria2.Desktop.Model;
using Microsoft.EntityFrameworkCore;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        public DbSet<DownloadTask> DownloadTasks
        {
            get { return DbContext.DownloadTasks; }
        }

        protected void DbInit()
        {
            if (DbContext.Database.GetPendingMigrations().Any())
            {
                DbContext.Database.Migrate();
            }
        }

        public void Update<T>(T oriItem,T newItem) where T:class
        {
            DbContext.Entry<T>(oriItem).State = EntityState.Detached;
            DbContext.Entry<T>(newItem).State = EntityState.Modified; 
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
