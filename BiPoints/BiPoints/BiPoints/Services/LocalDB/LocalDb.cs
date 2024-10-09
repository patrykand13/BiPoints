using BiPoints.Helpers.LocalDB;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BiPoints.Services.LocalDB
{
    class LocalDb : DbContext
    {
        public DbSet<LocalProfileClass> MainLocalProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SQLitePCL.Batteries_V2.Init();
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "BiPointsDataBase.sqlite")}");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
