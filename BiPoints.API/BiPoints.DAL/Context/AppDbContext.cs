using BiPoints.API.Models;
using BiPoints.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.API
{
    public class AppDbContext : DbContext
    {
        public DbSet<AuthenticateEntity> Authenticates { get; set; }
        public DbSet<UserEntity> Users { get; set; }
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
        }
    }
}
