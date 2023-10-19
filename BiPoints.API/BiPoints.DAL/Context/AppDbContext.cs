using BiPoints.API.Models;
using BiPoints.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.API
{
    public class AppDbContext : DbContext
    {
        public DbSet<AuthenticateEntity> Authenticates { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PointEntity> Points { get; set; }
        public DbSet<ScanHistoryEntity> ScanHistories { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserEntity>()
                .HasOne(x => x.Authenticate)
                .WithOne(x => x.User)
                .HasForeignKey<UserEntity>(x => x.AuthenticateId);
            builder.Entity<PointEntity>()
                .HasOne(x => x.UserEntity)
                .WithOne(x => x.PointEntity)
                .HasForeignKey<PointEntity>(x => x.UserId);
            builder.Entity<ScanHistoryEntity>()
                .HasOne(x => x.UserEntity)
                .WithOne(x => x.ScanHistoryEntity)
                .HasForeignKey<ScanHistoryEntity>(x => x.UserId);
        }
    }
}
