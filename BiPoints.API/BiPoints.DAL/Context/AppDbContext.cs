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
                .HasOne(u => u.Authenticate)
                .WithOne(a => a.User)
                .HasForeignKey<UserEntity>(u => u.AuthenticateId);
            builder.Entity<PointEntity>()
                .HasOne(p => p.UserEntity)
                .WithOne(b => b.PointEntity)
                .HasForeignKey<PointEntity>(p => p.UserId);
            builder.Entity<UserEntity>()
                .HasMany(u => u.ScanHistories)
                .WithOne(sh => sh.UserEntity)
                .HasForeignKey(u => u.UserId);
            builder.Entity<ScanHistoryEntity>()
                .HasOne(sh => sh.Item)
                .WithMany(i => i.ScanHistories)
                .HasForeignKey(sh => sh.CodeQr)
                .HasPrincipalKey(i => i.CodeQr);
        }
    }
}
