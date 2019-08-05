using Microsoft.EntityFrameworkCore;

namespace GensouSakuya.Aria2.Desktop.Model
{
    public class DbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseSqlite("Data Source = data.db;");

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<DownloadTask> DownloadTasks { get; set; }
    }
}
